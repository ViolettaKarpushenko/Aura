using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Web;
using Aura.Web.Models;
using Dapper;

namespace Aura.Web.Data
{
    public abstract class Repository
    {
        static Repository()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["Common"].ConnectionString;
            var builder = new SQLiteConnectionStringBuilder(connectionString);
            builder.DataSource = HttpContext.Current.Server.MapPath(builder.DataSource);

            ConnectionString = builder.ConnectionString;
        }

        private static string ConnectionString { get; set; }

        protected static IEnumerable<T> Execute<T>(string query, params object[] @params)
        {
            if (@params != null && @params.Any())
            {
                query = string.Format(query, @params);
            }

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                var result = connection.Query<T>(query);
                return result;
            }
        }

        protected static void Execute(string query, params object[] @params)
        {
            if (@params != null && @params.Any())
            {
                query = string.Format(query, @params);
            }

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                connection.Execute(query);
            }
        }

        protected static IEnumerable<T> ExecuteAggregateEntityQuery<T>(string tableName, int tableId, params Enum[] columns)
        {
            var content = CreateAggregateEntityPatternFillContent(columns).ToArray();
            var queryPattern = BuildAggregateEntityQueryPattern(tableName, tableId, columns.Length);
            var query = string.Format(queryPattern, content);

            return Execute<T>(query);
        }

        protected static string BuildAggregateEntityQueryPattern(string tableName, int tableId, int columnQuantity)
        {
            var query = new StringBuilder();
            query.Append("SELECT ");
            query.Append("[r].[ID] AS [RegionID] ");
            query.Append(",[r].[Name] AS [RegionName] ");

            for (var i = 1; i <= columnQuantity; i++)
            {
                query.AppendFormat(
                    ",ifnull([s{0}].[Value], 0.0) AS [{{{1}}}] ",
                    i,
                    (i * 2) - 1);
            }

            query.Append("FROM [regions] AS [r] ");

            for (var i = 1; i <= columnQuantity; i++)
            {
                query.AppendFormat(
                    "LEFT JOIN [{1}] AS [s{0}] ON [s{0}].[RegionID] = [r].[Id] AND [s{0}].[TableID] = {2} AND [s{0}].[ColumnID] = {{{3}}} ",
                    i,
                    tableName,
                    tableId,
                    (i * 2) - 2);
            }

            return query.ToString();
        }

        protected IEnumerable<EcologicalModel> EcecuteBaseEcologicalQuery(int tableId, int? regionId = null)
        {
            var condition = " WHERE [TableID] = {0} ";
            if (regionId != null)
            {
                condition += " AND [RegionID] = {1} ";
            }

            var query = string.Format("SELECT [ID], [AlterName], [Name], [TableID], [Value] FROM [ecological]" + condition + ";", tableId, regionId);
            var result = Execute<EcologicalModel>(query);

            return result;
        }

        private static IEnumerable<object> CreateAggregateEntityPatternFillContent(IEnumerable<Enum> columns)
        {
            foreach (var column in columns)
            {
                var value = (ulong)Convert.ChangeType(column, typeof(ulong));
                var name = Enum.GetName(column.GetType(), column);

                yield return value;
                yield return name;
            }
        }
    }
}