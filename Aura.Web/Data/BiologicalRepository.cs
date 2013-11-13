using System.Linq;
using Aura.Web.Common;
using Aura.Web.Common.Columns;
using Aura.Web.Interfaces;
using Aura.Web.Models;

namespace Aura.Web.Data
{
    public class BiologicalRepository : Repository, IEntityRepository<BiologicalViewModel>
    {
        public BiologicalViewModel GetStocks()
        {
            var data = ExecuteAggregateEntityQuery<BiologicalModel>(
                "stocks",
                (int)Tables.Biological,
                BiologicalColumns.Drevesina,
                BiologicalColumns.Lekarstvennye,
                BiologicalColumns.Pishcevye,
                BiologicalColumns.Griby,
                BiologicalColumns.Fitoplankton,
                BiologicalColumns.Makrofity);

            return new BiologicalViewModel { Items = data.OrderBy(stock => stock.RegionName) };
        }

        public BiologicalViewModel GetUse()
        {
            var useData = ExecuteAggregateEntityQuery<BiologicalModel>("use", (int)Tables.Biological, BiologicalColumns.ZagotovlenoMakrofitov, BiologicalColumns.ZagotovlenoFitoplankton)
                                .AsParallel();
            var stocksData = ExecuteAggregateEntityQuery<BiologicalModel>("stocks", (int)Tables.Biological, BiologicalColumns.Makrofity, BiologicalColumns.Fitoplankton)
                                .AsParallel();

            var items = from use in useData
                        join stock in stocksData on use.RegionId equals stock.RegionId
                        select new BiologicalModel
                        {
                            RegionId = use.RegionId,
                            RegionName = use.RegionName,

                            ZagotovlenoMakrofitov = use.ZagotovlenoMakrofitov,
                            ZagotovlenoFitoplankton = use.ZagotovlenoFitoplankton,
                            Makrofity = stock.Makrofity,
                            Fitoplankton = stock.Fitoplankton
                        };

            return new BiologicalViewModel { Items = items.OrderBy(stock => stock.RegionName) };
        }

        public ResultsViewModel GetResult()
        {
            var stocks = GetStocks().Items.AsParallel();

            var avgDrevesina = stocks.Average(stock => stock.Drevesina);
            var avgLekarstvennye = stocks.Average(stock => stock.Lekarstvennye);
            var avgPishcevye = stocks.Average(stock => stock.Pishcevye);
            var avgGriby = stocks.Average(stock => stock.Griby);
            var avgFitoplankton = stocks.Average(stock => stock.Fitoplankton);
            var avgMakrofity = stocks.Average(stock => stock.Makrofity);

            var useData = ExecuteAggregateEntityQuery<BiologicalModel>(
                "use",
                (int)Tables.Biological,
                BiologicalColumns.Fitoplankton,
                BiologicalColumns.Makrofity).AsParallel();

            var avgUseFitoplankton = useData.Average(stock => stock.Fitoplankton);
            var avgUseMakrofity = useData.Average(stock => stock.Makrofity);

            var results = from stock in stocks
                          join use in useData on stock.RegionId equals use.RegionId
                          let zapasyPhg = (stock.Drevesina / avgDrevesina + stock.Lekarstvennye / avgLekarstvennye) +
                                          (stock.Pishcevye / avgPishcevye + stock.Griby / avgGriby +
                                           stock.Fitoplankton / avgFitoplankton) +
                                          (stock.Makrofity / avgMakrofity)
                          let zapasyOzera = stock.Fitoplankton / avgFitoplankton + stock.Makrofity / avgMakrofity
                          let useZapasyOzera = use.Fitoplankton / avgUseFitoplankton + use.Makrofity / avgUseMakrofity
                          let dolaResursovOzerVSumarnomZapasePercent = zapasyPhg / zapasyOzera
                          orderby stock.RegionName
                          select new ResultModel
                              {
                                  RegionId = stock.RegionId,
                                  RegionName = stock.RegionName,
                                  DolaResursovTerritoriiVSumarnomZapasePercent = 1 - dolaResursovOzerVSumarnomZapasePercent,
                                  DolaResursovOzerVSumarnomZapasePercent = dolaResursovOzerVSumarnomZapasePercent,
                                  KoefSootnosheniaResursov =
                                      zapasyOzera /
                                      (stock.Drevesina / avgDrevesina +
                                       stock.Lekarstvennye / avgLekarstvennye +
                                       stock.Pishcevye / avgPishcevye + stock.Griby / avgGriby),
                                  IndexVelichinyIspolzovaniyaOzerVHozDeatelnosti = useZapasyOzera / zapasyOzera
                              };

            return new ResultsViewModel { Items = results };
        }
    }
}