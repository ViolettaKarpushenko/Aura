using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Aura
{
    public partial class MainForm : Form
    {
        private readonly SQLiteConnection _connection;
        private readonly QueryBuilder _queryBuilder;

        public MainForm()
        {
            InitializeComponent();

            var connectionStringBuilder = new SQLiteConnectionStringBuilder { DataSource = "aura.db3" };

            _connection = new SQLiteConnection(connectionStringBuilder.ConnectionString);

            _queryBuilder = new QueryBuilder(_connection);

            comboBox1.SelectedIndex = 5;
            foreach (var column in Controls.OfType<DataGridViewTextBoxColumn>())
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            var version = Assembly.GetExecutingAssembly().GetName().Version;
            Text = string.Format("ГОПРП {0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build, version.Revision);
        }

        private void OnLoad(object sender, EventArgs e)
        {
            _dgvMineralD.DataSource = _queryBuilder.Get(DataTables.MineralData);
            _dgvMineralD.Update();

            _dgvWaterD.DataSource = _queryBuilder.Get(DataTables.WaterData);
            _dgvWaterD.Update();

            _dgvBiologicalD.DataSource = _queryBuilder.Get(DataTables.BiologicalData);
            _dgvBiologicalD.Update();

            _dgvTerritorialD.DataSource = _queryBuilder.Get(DataTables.TerritorialData);
            _dgvTerritorialD.Update();

            _dgvAnimalsD.DataSource = _queryBuilder.Get(DataTables.AnimalData);
            _dgvAnimalsD.Update();

            SelectedIndexChanged(null, null);
        }

        private void SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.Text)
            {
                case "Минеральные":
                    dataGridView1.DataSource = _queryBuilder.Get("v_result_mineral_resources");
                    break;
                case "Водные":
                    dataGridView1.DataSource = _queryBuilder.Get("v_result_water_resources");
                    break;
                case "Территориальные":
                    dataGridView1.DataSource = _queryBuilder.Get("v_result_territorial_resources");
                    break;
                case "Растительные":
                    dataGridView1.DataSource = _queryBuilder.Get("v_result_biological_resources");
                    break;
                case "Животные":
                    dataGridView1.DataSource = _queryBuilder.Get("v_result_animals_resorces");
                    break;
                default:
                    dataGridView1.DataSource = _queryBuilder.Get("v_resut");
                    break;
            }

            dataGridView1.Update();
        }

        private void AddRegion(object sender, EventArgs e)
        {
            // Add region name.
            var queries = new List<string> { "BEGIN TRANSACTION;\r\n" };
            {
                var dialog = new AddRegionForm("Регион", "Название", s => string.IsNullOrEmpty(s) || s.Length > 49 ? "Неверное имя региона" : null);
                var showDialog = dialog.ShowDialog();
                if (showDialog == DialogResult.OK)
                {
                    queries.Add(string.Format("INSERT INTO [t_regions] ([ID], [Name]) VALUES ({{0}}, '{0}');\r\n", dialog.Value.Replace('\'', '`')));
                }
                else
                {
                    return;
                }
            }

            // Fill simple tables
            #region Tables definitions
            var simpleTables = new[]
                                   {
                                       new SimpleTable
                                           {
                                               TableName = "t_mineral_resources",
                                               Caption = "Минеральные ресурсы",
                                               Columns = new Dictionary<string, string>
                                                   {
                                                       {"DL", "Доломиты"},
                                                       {"AR", "Глинистые"},
                                                       {"GS", "Гривийно-пещаные"},
                                                       {"SA", "Пески"},
                                                       {"PE", "Торф"},
                                                       {"SP", "Сапропель"}
                                                   }
                                           },
                                       new SimpleTable
                                           {
                                               TableName = "t_water_resources",
                                               Caption = "Водные ресурсы",
                                               Columns = new Dictionary<string, string>
                                                   {
                                                       {"RW", "Речной сток"},
                                                       {"UW", "Подземные воды"},
                                                       {"LW", "Обем воды в озерах"}
                                                   }
                                           },
                                       new SimpleTable
                                           {
                                               TableName = "t_territorial_resources",
                                               Caption = "Территориальные ресурсы",
                                               Columns = new Dictionary<string, string>
                                                   {
                                                       {"GA", "Земельные"},
                                                       {"AA", "Сельское хозяйство"},
                                                       {"LA", "Озерные"}
                                                   }
                                           },
                                       new SimpleTable
                                           {
                                               TableName = "t_biological_resources",
                                               Caption = "Биологические ресурсы",
                                               Columns = new Dictionary<string, string>
                                                   {
                                                       {"WO", "Древесина"},
                                                       {"MP", "Лекарственные"},
                                                       {"FP", "Пищевые"},
                                                       {"MU", "Грибы"},
                                                       {"PH", "Фитопланктон"},
                                                       {"MC", "Макрофиты"}
                                                   }
                                           }
                                   };
            #endregion

            foreach (var simpleTable in simpleTables)
            {
                var dictionnary = new Dictionary<string, string>();

                foreach (var column in simpleTable.Columns)
                {
                    double tempDouble;
                    var dialog = new AddRegionForm(simpleTable.Caption, column.Value, s => !double.TryParse(s, NumberStyles.Any, CultureInfo.CurrentCulture, out tempDouble) ? "Неверное значение" : null);
                    var showDialog = dialog.ShowDialog();
                    if (showDialog == DialogResult.OK)
                    {
                        dictionnary.Add(column.Key, dialog.Value);
                    }
                    else
                    {
                        return;
                    }
                }

                var columnsNames = simpleTable.Columns.Select(pair => pair.Key).Select(name => ", [" + name.Replace('\'', '`') + "]").Aggregate((s, s1) => s + s1);
                var columnsValues = simpleTable.Columns.Select(pair => dictionnary[pair.Key]).Select(name => ", '" + name.Replace('\'', '`') + "'").Aggregate((s, s1) => s + s1);

                queries.Add(string.Format("INSERT INTO [{2}] ([ID]{0}) VALUES ({{0}}{1});\r\n", columnsNames, columnsValues, simpleTable.TableName));
            }

            // Fill animals
            queries.Add("INSERT INTO [t_animals] ([ID]) VALUES ('{0}');\r\n");
            _connection.Open();
            var table = new DataTable();
            table.Load(new SQLiteCommand("SELECT * FROM [t_animal_species];", _connection).ExecuteReader());
            _connection.Close();
            foreach (var column in table.Rows.Cast<DataRow>().Select(row => new { Id = (long)row["ID"], Name = (string)row["Name"] }))
            {
                double tempDouble;
                var dialog = new AddRegionForm("Животные ресурсы", column.Name, s => !double.TryParse(s, NumberStyles.Any, CultureInfo.CurrentCulture, out tempDouble) ? "Неверное значение" : null);
                var showDialog = dialog.ShowDialog();
                if (showDialog == DialogResult.OK)
                {
                    queries.Add(string.Format("INSERT INTO [t_animal_species_to_animals] ([SID], [AID], [Quantity]) VALUES ('{0}', '{{0}}', '{1}');\r\n", column.Id, dialog.Value));
                }
                else
                {
                    return;
                }
            }
            _connection.Open();
            var table2 = new DataTable();
            table2.Load(new SQLiteCommand("SELECT * FROM [t_animal_others];", _connection).ExecuteReader());
            _connection.Close();
            foreach (var column in table2.Rows.Cast<DataRow>().Select(row => new { Id = (long)row["ID"], Name = (string)row["Name"] }))
            {
                double tempDouble;
                var dialog = new AddRegionForm("Животные ресурсы", column.Name, s => !double.TryParse(s, NumberStyles.Any, CultureInfo.CurrentCulture, out tempDouble) ? "Неверное значение" : null);
                var showDialog = dialog.ShowDialog();
                if (showDialog == DialogResult.OK)
                {
                    queries.Add(string.Format("INSERT INTO [t_animal_others_to_animals] ([OID], [AID], [Quantity]) VALUES ('{0}', '{{0}}', '{1}');\r\n", column.Id, dialog.Value));
                }
                else
                {
                    return;
                }
            }

            _connection.Open();
            long maximumIndex = (long)new SQLiteCommand("SELECT max([ID]) FROM [t_regions]", _connection).ExecuteScalar() + 1L;
            _connection.Close();
            queries.Add("COMMIT;\r\n");
            string aggregate = queries.Aggregate((s, s1) => s + s1);
            string result = string.Format(aggregate, maximumIndex);
            _connection.Open();
            new SQLiteCommand(result, _connection).ExecuteNonQuery();
            _connection.Close();
        }

        private void ShowMap(object sender, EventArgs e)
        {
            var data = MapData(_queryBuilder.Get("v_resut").Rows.Cast<DataRow>());

            new MapForm(data).ShowDialog();
        }

        private IEnumerable<ReportingResult> MapData(IEnumerable<DataRow> rows)
        {
            var result = new List<ReportingResult>();
            foreach (var row in rows)
            {
                try
                {
                    result.Add(new ReportingResult
                    {
                        Id = (long)row["id"],
                        A1 = (double)row[2],
                        A2 = (double)row[3],
                        A3 = (double)row[4],
                        A4 = (double)row[5]
                    });
                }
                catch { }
            }

            return result;
        }

        private void OpenManual(object sender, EventArgs e)
        {
            Process.Start("Методика.doc");
        }
    }
}
