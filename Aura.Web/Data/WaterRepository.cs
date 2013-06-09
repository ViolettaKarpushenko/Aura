using System.Linq;

using Aura.Web.Common;
using Aura.Web.Models;

namespace Aura.Web.Data
{
    public class WaterRepository : Repository, IEntityRepository<WaterViewModel>
    {
        private WaterViewModel GetItems(string tableName)
        {
            var query = BuildAggregateQueryPattern(tableName, (int)Tables.Water, 3);
            var stockData = Execute<WaterModel>(
                query,
                (int)WaterColumns.RechnoiStok,
                WaterColumns.RechnoiStok,
                (int)WaterColumns.PodzemnyeVody,
                WaterColumns.PodzemnyeVody,
                (int)WaterColumns.ObemVody,
                WaterColumns.ObemVody);

            return new WaterViewModel { Items = stockData.OrderBy(stock => stock.RegionName) };
        }

        public WaterViewModel GetStocks()
        {
            return GetItems("stocks");
        }

        public WaterViewModel GetUse()
        {
            return GetItems("use");
        }

        public ResultsViewModel GetResult()
        {
            var stocks = GetStocks().Items.AsParallel();

            var avgObemVody = stocks.Average(stock => stock.ObemVody);
            var avgPodzemnyeVody = stocks.Average(stock => stock.PodzemnyeVody);
            var avgRechnoiStok = stocks.Average(stock => stock.RechnoiStok);

            var results = from stock in stocks
                          let zapasyPhg = stock.RechnoiStok / avgRechnoiStok + stock.PodzemnyeVody / avgPodzemnyeVody + stock.ObemVody / avgObemVody
                          let zapasyOzera = stock.ObemVody / avgObemVody
                          orderby stock.RegionName
                          select new ResultModel
                          {
                              RegionId = stock.RegionId,
                              RegionName = stock.RegionName,
                              ZapasyPhg = zapasyPhg,
                              ZapasyOzera = zapasyOzera,
                              Percent = zapasyOzera / zapasyPhg,
                              KoefBalansa = zapasyOzera / (stock.RechnoiStok / avgRechnoiStok + stock.PodzemnyeVody / avgPodzemnyeVody)
                          };

            return new ResultsViewModel { Items = results };
        }
    }
}