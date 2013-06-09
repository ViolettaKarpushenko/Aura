using System.Linq;

using Aura.Web.Common;
using Aura.Web.Models;

namespace Aura.Web.Data
{
    public class MineralRepository : Repository, IEntityRepository<MineralViewModel>
    {
        private MineralViewModel GetItems(string tableName)
        {
            var query = BuildAggregateQueryPattern(tableName, (int)Tables.Minerals, 6);
            var stockData = Execute<MineralModel>(
                query,
                (int)MineralsColumns.Dolomity,
                MineralsColumns.Dolomity,
                (int)MineralsColumns.Glinistye,
                MineralsColumns.Glinistye,
                (int)MineralsColumns.Peski,
                MineralsColumns.Peski,
                (int)MineralsColumns.Torf,
                MineralsColumns.Torf,
                (int)MineralsColumns.GravinoPeschanye,
                MineralsColumns.GravinoPeschanye,
                (int)MineralsColumns.Sapropel,
                MineralsColumns.Sapropel);

            return new MineralViewModel { Items = stockData.OrderBy(stock => stock.RegionName) };
        }

        public MineralViewModel GetStocks()
        {
            return GetItems("stocks");
        }

        public MineralViewModel GetUse()
        {
            return GetItems("use");
        }

        public ResultsViewModel GetResult()
        {
            var stocks = GetStocks().Items.AsParallel();

            var avgDolomity = stocks.Average(stock => stock.Dolomity);
            var avgGlinistye = stocks.Average(stock => stock.Glinistye);
            var avgPeski = stocks.Average(stock => stock.Peski);
            var avgTorf = stocks.Average(stock => stock.Torf);
            var avgSapropel = stocks.Average(stock => stock.Sapropel);
            var avgGravinoPeschanye = stocks.Average(stock => stock.GravinoPeschanye);

            var results = from stock in stocks
                          let zapasyPhg = stock.Dolomity / avgDolomity + stock.Peski / avgPeski + stock.Glinistye / avgGlinistye + stock.GravinoPeschanye / avgGravinoPeschanye + stock.Torf / avgTorf + stock.Sapropel / avgSapropel
                          let zapasyOzera = stock.Sapropel / avgSapropel
                          orderby stock.RegionName
                          select new ResultModel
                          {
                              RegionId = stock.RegionId,
                              RegionName = stock.RegionName,
                              ZapasyPhg = zapasyPhg,
                              ZapasyOzera = zapasyOzera,
                              Percent = zapasyOzera / zapasyPhg,
                              KoefBalansa = stock.Glinistye / zapasyOzera
                          };

            return new ResultsViewModel { Items = results };
        }
    }
}