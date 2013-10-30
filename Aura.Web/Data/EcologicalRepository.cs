using System.Collections.Generic;
using System.Linq;
using Aura.Web.Common;
using Aura.Web.Interfaces;
using Aura.Web.Models;

namespace Aura.Web.Data
{
    public class EcologicalRepository : Repository, IEcologicalRepository
    {
        public IEnumerable<EcologicalModel> GetEcologicalItems(Tables table, int? regionId = null)
        {
            var items = EcecuteBaseEcologicalQuery((int)table, regionId);
            return items.OrderBy(x => x.Name);
        }

        public IEnumerable<KeyValuePair<int, string>> GetEcologicalRegions(Tables table)
        {
            var query = "SELECT [RegionID] AS [Key], [RegionName] AS [Value]" +
                        "FROM [ecological]" +
                        "WHERE [TableID] = {0}" +
                        "AND [RegionID] IS NOT NULL" +
                        "AND [RegionName] IS NOT NULL" +
                        "GROUP BY [RegionID], [RegionName]";

            var result = Execute<KeyValuePair<int, string>>(query, (int) table);

            return result;
        }
    }
}