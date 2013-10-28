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

            var results = stocks.Select(stock => new
                {
                    stock,
                    zapasyPhg = (stock.Drevesina / avgDrevesina + stock.Lekarstvennye / avgLekarstvennye) +
                                (stock.Pishcevye / avgPishcevye + stock.Griby / avgGriby + stock.Fitoplankton / avgFitoplankton) +
                                (stock.Makrofity / avgMakrofity)
                })
                .Select(stock => new
                {
                    stock,
                    zapasyOzera = stock.stock.Fitoplankton / avgFitoplankton + stock.stock.Makrofity / avgMakrofity
                })
                .Select(stock => new ResultModel
                {
                    RegionId = stock.stock.stock.RegionId,
                    RegionName = stock.stock.stock.RegionName,
                    ZapasyPhg = stock.stock.zapasyPhg,
                    ZapasyOzera = stock.zapasyOzera,
                    Percent = stock.zapasyOzera / stock.stock.zapasyPhg,
                    KoefBalansa =
                        stock.zapasyOzera /
                        (stock.stock.stock.Drevesina / avgDrevesina + stock.stock.stock.Lekarstvennye / avgLekarstvennye +
                        stock.stock.stock.Pishcevye / avgPishcevye + stock.stock.stock.Griby / avgGriby)
                })
                .OrderBy(stock => stock.RegionName);

            return new ResultsViewModel { Items = results };
        }
    }
}