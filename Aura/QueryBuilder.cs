using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace Aura
{
    public class QueryBuilder
    {
        private readonly SQLiteConnection _connection;

        private readonly Dictionary<DataTables, Func<DataTable>> _methods;

        public QueryBuilder(SQLiteConnection connection)
        {
            _connection = connection;
            _methods = new Dictionary<DataTables, Func<DataTable>>
                        {
                            { DataTables.MineralData, () => SelectAll("v_t_mineral_resources") },
                            { DataTables.WaterData, () => SelectAll("v_t_water_resources") },
                            { DataTables.BiologicalData, () => SelectAll("v_t_biological_resources") },
                            { DataTables.TerritorialData, () => SelectAll("v_t_territorial_resources") },
                            { DataTables.AnimalData, SelectAnimals}
                        };
        }

        public DataTable Get(DataTables view)
        {
            Func<DataTable> method;
            if (!_methods.TryGetValue(view, out method))
            {
                var dataTable = new DataTable();
                dataTable.Rows.Add("Empty");
                return dataTable;
            }

            return method();
        }

        public DataTable Get(string view)
        {
            return SelectAll(view);
        }

        private DataTable SelectAll(string viewName)
        {
            _connection.Open();
            var reader = new SQLiteCommand(string.Format("SELECT * FROM [{0}];", viewName), _connection).ExecuteReader();
            var table = new DataTable();
            table.Load(reader);

            _connection.Close();

            return table;
        }

        private DataTable SelectAnimals()
        {
            _connection.Open();

            var reader = new SQLiteCommand("SELECT [ID], [Name] FROM [t_animal_species]", _connection).ExecuteReader();
            var table = new DataTable();
            table.Load(reader);
            var animals = table.Rows.Cast<DataRow>().ToDictionary(row => (long)row["ID"], row => (string)row["Name"]);


            reader = new SQLiteCommand("SELECT [ID], [Name] FROM [t_animal_others]", _connection).ExecuteReader();
            table = new DataTable();
            table.Load(reader);
            var others = table.Rows.Cast<DataRow>().ToDictionary(row => (long)row["ID"], row => (string)row["Name"]);

            _connection.Close();

            var command = "SELECT\r\n\t [rg].[Name] AS 'Регион'\r\n";
            command += animals.Select(pair => string.Format("\t,[a{0}].[Quantity] AS '{1}'\r\n", pair.Key, pair.Value)).Aggregate((s, s1) => s + s1);
            command += others.Select(pair => string.Format("\t,[o{0}].[Quantity] AS '{1}'\r\n", pair.Key, pair.Value)).Aggregate((s, s1) => s + s1);
            command += "FROM [t_animals] AS [an]\r\n\tJOIN [t_regions] AS [rg] ON [rg].[ID] = [an].[ID]\r\n";
            command += animals.Select(pair => string.Format("\tJOIN [t_animal_species_to_animals] AS [a{0}] ON [a{0}].[AID] = [an].[ID]\r\n", pair.Key)).Aggregate((s, s1) => s + s1);
            command += others.Select(pair => string.Format("\tJOIN [t_animal_others_to_animals] AS [o{0}] ON [o{0}].[AID] = [an].[ID]\r\n", pair.Key)).Aggregate((s, s1) => s + s1);
            command += "WHERE 1=1\r\n";
            command += animals.Select(pair => string.Format("\tAND [a{0}].[SID] = {0}\r\n", pair.Key)).Aggregate((s, s1) => s + s1);
            command += others.Select(pair => string.Format("\tAND [o{0}].[OID] = {0}\r\n", pair.Key)).Aggregate((s, s1) => s + s1);

            _connection.Open();

            reader = new SQLiteCommand(command, _connection).ExecuteReader();
            table = new DataTable();
            table.Load(reader);

            _connection.Close();

            return table;
        }
    }
}