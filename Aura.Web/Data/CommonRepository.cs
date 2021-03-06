﻿using System.Globalization;
using Aura.Web.Interfaces;

namespace Aura.Web.Data
{
    public class CommonRepository : Repository, ICommonRepository
    {
        public void UpdateValue(string tableName, int tableId, int columnId, int regionId, double value)
        {
            var queryPattern =
                "INSERT OR IGNORE INTO [{0}] ([ColumnID], [RegionID], [TableID], [Value]) VALUES ({2}, {3}, {1}, {4}); " +
                "UPDATE [{0}] SET [Value] = {4} WHERE [TableID] = {1} AND [ColumnID] = {2} AND [RegionID] = {3}; ";

            Execute(queryPattern, tableName, tableId, columnId, regionId, value.ToString(CultureInfo.CreateSpecificCulture("en-us")));
        }
    }
}