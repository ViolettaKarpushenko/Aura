using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Linq;
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

        private static string ConnectionString { get; set; }

        protected IEnumerable<T> Execute<T>(string query, params object[] @params)
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

        protected void Execute(string query, params object[] @params)
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
    }
}