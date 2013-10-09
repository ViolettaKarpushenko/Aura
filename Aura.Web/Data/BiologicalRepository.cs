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