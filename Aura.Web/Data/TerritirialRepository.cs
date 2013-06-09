using System.Linq;
using Aura.Web.Common;
using Aura.Web.Models;

namespace Aura.Web.Data
{
    public class TerritirialRepository : Repository, IEntityRepository<TerritorialViewModel>
    {
        private TerritorialViewModel GetItems(string tableName)
        {
            var query = BuildAggregateQueryPattern(tableName, (int)Tables.Territorial, 4);
            var stockData = Execute<TerritorialModel>(
                query,
                (int)TerritorialColumns.Plochad,
                TerritorialColumns.Plochad,
                (int)TerritorialColumns.Zemelnye,
                TerritorialColumns.Zemelnye,
                (int)TerritorialColumns.Sh,
                TerritorialColumns.Sh,
                (int)TerritorialColumns.Ozernye,
                TerritorialColumns.Ozernye);

            return new TerritorialViewModel { Items = stockData.OrderBy(stock => stock.RegionName) };
        }

        public TerritorialViewModel GetStocks()
        {
            return GetItems("stocks");
        }

        public TerritorialViewModel GetUse()
        {
            return GetItems("use");
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