using System.Linq;
using Aura.Web.Common;
using Aura.Web.Common.Columns;
using Aura.Web.Interfaces;
using Aura.Web.Models;

namespace Aura.Web.Data
{
    public class TerritorialRepository : Repository, IEntityRepository<TerritorialViewModel>
    {
        public TerritorialViewModel GetStocks()
        {
            var data = ExecuteAggregateEntityQuery<TerritorialModel>(
                "stocks",
                (int)Tables.Territorial,
                TerritorialColumns.Plochad,
                TerritorialColumns.Zemelnye,
                TerritorialColumns.Sh,
                TerritorialColumns.Ozernye);

            return new TerritorialViewModel { Items = data.OrderBy(stock => stock.RegionName) };
        }

        public TerritorialViewModel GetUse()
        {
            var stocksData = ExecuteAggregateEntityQuery<TerritorialModel>("stocks", (int)Tables.Territorial, TerritorialColumns.Plochad)
                                .AsParallel();
            var useData = ExecuteAggregateEntityQuery<TerritorialModel>("use", (int)Tables.Territorial, TerritorialColumns.VmestimostRekreacionnyhZon, TerritorialColumns.PlochadOopt)
                                .AsParallel();

            var items = from use in useData
                        join stock in stocksData on use.RegionId equals stock.RegionId
                        select new TerritorialModel
                        {
                            RegionId = use.RegionId,
                            RegionName = use.RegionName,
                            VmestimostRekreacionnyhZon = use.VmestimostRekreacionnyhZon,
                            PlochadOopt = use.PlochadOopt,
                            Plochad = stock.Plochad
                        };

            return new TerritorialViewModel { Items = items.OrderBy(stock => stock.RegionName) };
        }

        public ResultsViewModel GetResult()
        {
            var stocks = GetStocks().Items.AsParallel();

            var avgZemelnye = stocks.Average(stock => stock.Zemelnye);
            var avgOzernye = stocks.Average(stock => stock.Ozernye);

            var results = from stock in stocks
                          let zapasyPhg = stock.Zemelnye / avgZemelnye + stock.Ozernye / avgOzernye
                          let zapasyOzera = stock.Ozernye / avgOzernye
                          orderby stock.RegionName
                          select new ResultModel
                          {
                              RegionId = stock.RegionId,
                              RegionName = stock.RegionName,
                              ZapasyPhg = zapasyPhg,
                              ZapasyOzera = zapasyOzera,
                              Percent = zapasyOzera / zapasyPhg,
                              KoefBalansa = zapasyOzera / (stock.Zemelnye / avgZemelnye)
                          };

            return new ResultsViewModel { Items = results };
        }
    }
}