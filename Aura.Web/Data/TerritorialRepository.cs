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
            var stocksData = ExecuteAggregateEntityQuery<TerritorialModel>("stocks", (int)Tables.Territorial, TerritorialColumns.Ozernye)
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
                            Plochad = stock.Ozernye
                        };

            return new TerritorialViewModel { Items = items.OrderBy(stock => stock.RegionName) };
        }

        public ResultsViewModel GetResult()
        {
            var stocks = GetStocks().Items.AsParallel();

            var avgZemelnye = stocks.Average(stock => stock.Zemelnye);
            var avgOzernye = stocks.Average(stock => stock.Ozernye);

            var useData = ExecuteAggregateEntityQuery<TerritorialModel>(
                            "use",
                            (int)Tables.Territorial,
                            TerritorialColumns.Ozernye)
                        .AsParallel();

            var avgUseOzernye = useData.Average(use => use.Ozernye);

            var results = from stock in stocks
                          join use in useData on stock.RegionId equals use.RegionId
                          let zapasyPhg = stock.Zemelnye / avgZemelnye + stock.Ozernye / avgOzernye
                          let zapasyOzera = stock.Ozernye / avgOzernye
                          let useZapasyOzera = use.Ozernye / avgUseOzernye
                          let dolaResursovOzerVSumarnomZapasePercent = zapasyOzera / zapasyPhg
                          orderby stock.RegionName
                          select new ResultModel
                              {
                                  RegionId = stock.RegionId,
                                  RegionName = stock.RegionName,
                                  DolaResursovTerritoriiVSumarnomZapasePercent = 1 - dolaResursovOzerVSumarnomZapasePercent,
                                  DolaResursovOzerVSumarnomZapasePercent = dolaResursovOzerVSumarnomZapasePercent,
                                  KoefSootnosheniaResursov = dolaResursovOzerVSumarnomZapasePercent / (1 - dolaResursovOzerVSumarnomZapasePercent),
                                  IndexVelichinyIspolzovaniyaOzerVHozDeatelnosti = useZapasyOzera / zapasyOzera,
                                  ZapasyPhg = zapasyPhg
                              };

            return new ResultsViewModel { Items = results };
        }
    }
}