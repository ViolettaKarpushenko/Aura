using System.Linq;

using Aura.Web.Common;
using Aura.Web.Common.Columns;
using Aura.Web.Interfaces;
using Aura.Web.Models;

namespace Aura.Web.Data
{
    public class MineralRepository : Repository, IEntityRepository<MineralViewModel>
    {
        private static MineralViewModel GetItems(string tableName)
        {
            var data = ExecuteAggregateQuery<MineralModel>(
                tableName,
                (int)Tables.Minerals,
                MineralsColumns.Dolomity,
                MineralsColumns.Glinistye,
                MineralsColumns.Peski,
                MineralsColumns.Torf,
                MineralsColumns.GravinoPeschanye,
                MineralsColumns.Sapropel);

            return new MineralViewModel { Items = data.OrderBy(stock => stock.RegionName) };
        }

        public MineralViewModel GetStocks()
        {
            return GetItems("stocks");
        }

        public MineralViewModel GetUse()
        {
            var useData = ExecuteAggregateQuery<MineralModel>("use", (int)Tables.Minerals, MineralsColumns.DobychaSapropel).AsParallel();
            var stocksData = ExecuteAggregateQuery<MineralModel>("stocks", (int)Tables.Minerals, MineralsColumns.Sapropel).AsParallel();

            var items = from use in useData
                        join stock in stocksData on use.RegionId equals stock.RegionId
                        select new MineralModel
                        {
                            RegionId = use.RegionId,
                            RegionName = use.RegionName,
                            DobychaSapropel = use.DobychaSapropel,
                            Sapropel = stock.Sapropel
                        };

            return new MineralViewModel { Items = items.OrderBy(stock => stock.RegionName) };
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
                          let zapasyPhg = stock.Dolomity / avgDolomity +
                                            stock.Peski / avgPeski +
                                            stock.Glinistye / avgGlinistye +
                                            stock.GravinoPeschanye / avgGravinoPeschanye +
                                            stock.Torf / avgTorf +
                                            stock.Sapropel / avgSapropel
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