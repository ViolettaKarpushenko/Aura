using Aura.Web.Common;
using Aura.Web.Models;
using System.Linq;

namespace Aura.Web.Data
{
    public class BiologicalRepository : Repository, IEntityRepository<BiologicalViewModel>
    {
        private BiologicalViewModel GetItems(string tableName)
        {
            var query = BuildAggregateQueryPattern(tableName, (int)Tables.Biological, 6);
            var stockData = Execute<BiologicalModel>(
                query,
                (int)BiologicalColumns.Drevesina,
                BiologicalColumns.Drevesina,
                (int)BiologicalColumns.Lekarstvennye,
                BiologicalColumns.Lekarstvennye,
                (int)BiologicalColumns.Pishcevye,
                BiologicalColumns.Pishcevye,
                (int)BiologicalColumns.Griby,
                BiologicalColumns.Griby,
                (int)BiologicalColumns.Fitoplankton,
                BiologicalColumns.Fitoplankton,
                (int)BiologicalColumns.Makrofity,
                BiologicalColumns.Makrofity);

            return new BiologicalViewModel { Items = stockData.OrderBy(stock => stock.RegionName) };
        }

        public BiologicalViewModel GetStocks()
        {
            return GetItems("stocks");
        }

        public BiologicalViewModel GetUse()
        {
            return GetItems("use");
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
                .OrderBy(@t => @t.stock.stock.RegionName)
                .Select(@t => new ResultModel
                    {
                        RegionId = @t.stock.stock.RegionId,
                        RegionName = @t.stock.stock.RegionName,
                        ZapasyPhg = @t.stock.zapasyPhg,
                        ZapasyOzera = @t.zapasyOzera,
                        Percent = @t.zapasyOzera / @t.stock.zapasyPhg,
                        KoefBalansa =
                            @t.zapasyOzera /
                            (@t.stock.stock.Drevesina / avgDrevesina + @t.stock.stock.Lekarstvennye / avgLekarstvennye +
                            @t.stock.stock.Pishcevye / avgPishcevye + @t.stock.stock.Griby / avgGriby)
                    });

            return new ResultsViewModel { Items = results };
        }
    }
}