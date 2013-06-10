using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Linq;
using System.Text;
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
        
        protected string BuildAggregateQueryPattern(string tableName, int tableId, int columnQuantity)
        {
            // TODO: Use this query.
            /*
            SELECT
                 [r].[ID] AS [RegionID]
                ,[r].[Name] AS [RegionName]
                ,ifnull([s1].[Value], 0.0) AS [Dolomity]
                ,ifnull([s2].[Value], 0.0) AS [Glinistye]
            FROM [regions] AS [r]
            LEFT JOIN [stocks] AS [s1] ON [s1].[RegionID] = [r].[Id] AND [s1].[TableID] = 1 AND [s1].[ColumnID] = 1
            LEFT JOIN [stocks] AS [s2] ON [s2].[RegionID] = [r].[Id] AND [s2].[TableID] = 1 AND [s2].[ColumnID] = 2
             */

            var query = new StringBuilder();
            query.Append("SELECT ");
            query.Append("[r].[ID] AS [RegionID] ");
            query.Append(",[r].[Name] AS [RegionName] ");

            for (var i = 1; i <= columnQuantity; i++)
            {
                query.AppendFormat(
                    ",ifnull((SELECT [s{0}].[Value] FROM [{1}] AS [s{0}] WHERE [s{0}].[TableID] = {2} AND [s{0}].[ColumnID] = {{{3}}} AND [s{0}].[RegionID] = [r].[ID]), 0.0) AS [{{{4}}}] ",
                    i,
                    tableName,
                    tableId,
                    (i * 2) - 2,
                    (i * 2) - 1);
            }

            query.Append("FROM [regions] AS [r] ");

            return query.ToString();
        }
    }
}