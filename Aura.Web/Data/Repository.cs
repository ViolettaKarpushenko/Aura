using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Web;
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

        protected static string ConnectionString { get; set; }

        protected IEnumerable<T> Execute<T>(string query)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                var result = connection.Query<T>(query);
                return result;
            }
        }

        protected void Execute(string query)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                connection.Query(query);
            }
        }
    }
}