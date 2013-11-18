using System.Linq;

using Aura.Web.Common;
using Aura.Web.Common.Columns;
using Aura.Web.Interfaces;
using Aura.Web.Models;

namespace Aura.Web.Data
{
    public class WaterRepository : Repository, IEntityRepository<WaterViewModel>
    {
        public WaterViewModel GetStocks()
        {
            var data = ExecuteAggregateEntityQuery<WaterModel>(
                "stocks", 
                (int)Tables.Water, 
                WaterColumns.RechnoiStok,
                WaterColumns.PodzemnyeVody,
                WaterColumns.ObemVody);

            return new WaterViewModel { Items = data.OrderBy(stock => stock.RegionName) };
        }

        public WaterViewModel GetUse()
        {
            var useQuery = BuildAggregateEntityQueryPattern("use", (int)Tables.Water, 6);
            var useData = Execute<WaterModel>(
                useQuery,
                (int)WaterColumns.VodyIzato,
                WaterColumns.VodyIzato,
                (int)WaterColumns.VodyIspolzovano,
                WaterColumns.VodyIspolzovano,
                (int)WaterColumns.HbPotreblenie,
                WaterColumns.HbPotreblenie,
                (int)WaterColumns.PPotreblenie,
                WaterColumns.PPotreblenie,
                (int)WaterColumns.ShPotreblenie,
                WaterColumns.ShPotreblenie,
                (int)WaterColumns.RhPotreblenie,
                WaterColumns.RhPotreblenie).AsParallel();

            var stocksQuery = BuildAggregateEntityQueryPattern("stocks", (int)Tables.Water, 1);
            var stocksData = Execute<WaterModel>(
                stocksQuery,
                (int)WaterColumns.ObemVody,
                WaterColumns.ObemVody).AsParallel();

            var items = from use in useData
                        join stock in stocksData on use.RegionId equals stock.RegionId
                        select new WaterModel
                            {
                                RegionId = use.RegionId,
                                RegionName = use.RegionName,
                                VodyIzato = use.VodyIzato,
                                VodyIspolzovano = use.VodyIspolzovano,
                                HbPotreblenie = use.HbPotreblenie,
                                PPotreblenie = use.PPotreblenie,
                                ShPotreblenie = use.ShPotreblenie,
                                RhPotreblenie = use.RhPotreblenie,
                                ObemVody = stock.ObemVody
                            };

            return new WaterViewModel { Items = items.OrderBy(stock => stock.RegionName) };
        }

        public ResultsViewModel GetResult()
        {
            var stocks = GetStocks().Items.AsParallel();

            var avgObemVody = stocks.Average(stock => stock.ObemVody);
            var avgPodzemnyeVody = stocks.Average(stock => stock.PodzemnyeVody);
            var avgRechnoiStok = stocks.Average(stock => stock.RechnoiStok);
            
            var useData = ExecuteAggregateEntityQuery<WaterModel>(
                "stocks",
                (int)Tables.Water,
                WaterColumns.ObemVody).AsParallel();

            var avgUseObemVody = useData.Average(use => use.ObemVody);

            var results = from stock in stocks
                          join use in useData on stock.RegionId equals use.RegionId
                          let zapasyPhg = stock.RechnoiStok / avgRechnoiStok + stock.PodzemnyeVody / avgPodzemnyeVody + stock.ObemVody / avgObemVody
                          let zapasyOzera = stock.ObemVody / avgObemVody
                          let useZapasyOzera = use.ObemVody / avgUseObemVody
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