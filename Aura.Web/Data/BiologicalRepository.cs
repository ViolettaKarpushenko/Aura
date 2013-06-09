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

            var results =
                stocks.Select(
                    stock =>
                    new
                        {
                            stock,
                            zapasyPhg =
                        stock.Drevesina/avgDrevesina + stock.Lekarstvennye/avgLekarstvennye +
                        stock.Pishcevye/avgPishcevye + stock.Griby/avgGriby + stock.Fitoplankton/avgFitoplankton +
                        stock.Makrofity/avgMakrofity
                        })
                      .Select(
                          @t =>
                          new
                              {
                                  @t,
                                  zapasyOzera = @t.stock.Fitoplankton/avgFitoplankton + @t.stock.Makrofity/avgMakrofity
                              })
                      .OrderBy(@t => @t.@t.stock.RegionName)
                      .Select(@t => new ResultModel
                          {
                              RegionId = @t.@t.stock.RegionId,
                              RegionName = @t.@t.stock.RegionName,
                              ZapasyPhg = @t.@t.zapasyPhg,
                              ZapasyOzera = @t.zapasyOzera,
                              Percent = @t.zapasyOzera/@t.@t.zapasyPhg,
                              KoefBalansa =
                                  @t.zapasyOzera/
                                  (@t.@t.stock.Drevesina/avgDrevesina + @t.@t.stock.Lekarstvennye/avgLekarstvennye +
                                   @t.@t.stock.Pishcevye/avgPishcevye + @t.@t.stock.Griby/avgGriby)
                          });

            return new ResultsViewModel { Items = results };
        }
    }
}