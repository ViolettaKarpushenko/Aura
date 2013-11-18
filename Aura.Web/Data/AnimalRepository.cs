using System.Linq;

using Aura.Web.Common;
using Aura.Web.Common.Columns;
using Aura.Web.Interfaces;
using Aura.Web.Models;

namespace Aura.Web.Data
{
    public class AnimalRepository : Repository, IEntityRepository<AnimalViewModel>
    {
        public AnimalViewModel GetStocks()
        {
            var data = ExecuteAggregateEntityQuery<AnimalModel>(
                "stocks",
                (int)Tables.Animal,
                AnimalColumns.Los,
                AnimalColumns.Olen,
                AnimalColumns.Kosula,
                AnimalColumns.Kaban,
                AnimalColumns.ZayacBelak,
                AnimalColumns.ZayacRusak,
                AnimalColumns.Kunica,
                AnimalColumns.Lisica,
                AnimalColumns.Ondatra,
                AnimalColumns.Norka,
                AnimalColumns.Bobr,
                AnimalColumns.Volk,
                AnimalColumns.Barsuk,
                AnimalColumns.Vydra,
                AnimalColumns.EnotovidnayaSobaka,
                AnimalColumns.Rys,
                AnimalColumns.Belka,
                AnimalColumns.Gluhar,
                AnimalColumns.Teterev,
                AnimalColumns.Rabchik,
                AnimalColumns.SerayaKuropatka,
                AnimalColumns.DikayaUtka,
                AnimalColumns.Zooplankton,
                AnimalColumns.Bentos,
                AnimalColumns.Ryba);

            return new AnimalViewModel { Items = data.OrderBy(stock => stock.RegionName) };
        }

        public AnimalViewModel GetUse()
        {
            var useData = ExecuteAggregateEntityQuery<AnimalModel>("use", (int)Tables.Animal, AnimalColumns.ZagotovlenoZooplankton, AnimalColumns.ZagotovlenoBentos, AnimalColumns.ZagotovlenoRyba)
                                .AsParallel();
            var stocksData = ExecuteAggregateEntityQuery<AnimalModel>("stocks", (int)Tables.Animal, AnimalColumns.Zooplankton, AnimalColumns.Bentos, AnimalColumns.Ryba)
                                .AsParallel();

            var items = from use in useData
                        join stock in stocksData on use.RegionId equals stock.RegionId
                        select new AnimalModel
                        {
                            RegionId = use.RegionId,
                            RegionName = use.RegionName,

                            ZagotovlenoZooplankton = use.ZagotovlenoZooplankton,
                            ZagotovlenoBentos = use.ZagotovlenoBentos,
                            ZagotovlenoRyba = use.ZagotovlenoRyba,
                            Zooplankton = stock.Zooplankton,
                            Bentos = stock.Bentos,
                            Ryba = stock.Ryba
                        };

            return new AnimalViewModel { Items = items.OrderBy(stock => stock.RegionName) };
        }

        public ResultsViewModel GetResult()
        {
            var stocks = GetStocks().Items.AsParallel();

            var avgLos = stocks.Average(stock => stock.Los);
            var avgOlen = stocks.Average(stock => stock.Olen);
            var avgKosula = stocks.Average(stock => stock.Kosula);
            var avgKaban = stocks.Average(stock => stock.Kaban);
            var avgZayacBelak = stocks.Average(stock => stock.ZayacBelak);
            var avgZayacRusak = stocks.Average(stock => stock.ZayacRusak);
            var avgKunica = stocks.Average(stock => stock.Kunica);
            var avgLisica = stocks.Average(stock => stock.Lisica);
            var avgOndatra = stocks.Average(stock => stock.Ondatra);
            var avgNorka = stocks.Average(stock => stock.Norka);
            var avgBobr = stocks.Average(stock => stock.Bobr);
            var avgVolk = stocks.Average(stock => stock.Volk);
            var avgBarsuk = stocks.Average(stock => stock.Barsuk);
            var avgVydra = stocks.Average(stock => stock.Vydra);
            var avgEnotovidnayaSobaka = stocks.Average(stock => stock.EnotovidnayaSobaka);
            var avgRys = stocks.Average(stock => stock.Rys);
            var avgBelka = stocks.Average(stock => stock.Belka);
            var avgGluhar = stocks.Average(stock => stock.Gluhar);
            var avgTeterev = stocks.Average(stock => stock.Teterev);
            var avgRabchik = stocks.Average(stock => stock.Rabchik);
            var avgSerayaKuropatka = stocks.Average(stock => stock.SerayaKuropatka);
            var avgDikayaUtka = stocks.Average(stock => stock.DikayaUtka);
            var avgZooplankton = stocks.Average(stock => stock.Zooplankton);
            var avgBentos = stocks.Average(stock => stock.Bentos);
            var avgRyba = stocks.Average(stock => stock.Ryba);

            var useData = ExecuteAggregateEntityQuery<AnimalModel>(
                "use",
                (int)Tables.Animal,
                AnimalColumns.Zooplankton,
                AnimalColumns.Bentos,
                AnimalColumns.Ryba).AsParallel();

            var avgUseZooplankton = useData.Average(stock => stock.Zooplankton);
            var avgUseBentos = useData.Average(stock => stock.Bentos);
            var avgUseRyba = useData.Average(stock => stock.Ryba);

            var results = from stock in stocks
                          join use in useData on stock.RegionId equals use.RegionId
                          let zapasyOzera = stock.Bentos / avgBentos +
                                            stock.Zooplankton / avgZooplankton +
                                            stock.Ryba / avgRyba
                          let useZapasyOzera = use.Bentos / avgUseBentos +
                                                use.Zooplankton / avgUseZooplankton +
                                                use.Ryba / avgUseRyba
                          let zapasyPhg = zapasyOzera +
                                          (stock.DikayaUtka / avgDikayaUtka +
                                           stock.SerayaKuropatka / avgSerayaKuropatka +
                                           stock.Rabchik / avgRabchik +
                                           stock.Teterev / avgTeterev +
                                           stock.Gluhar / avgGluhar) / 4 +

                                          (stock.ZayacBelak / avgZayacBelak +
                                           stock.ZayacRusak / avgZayacRusak +
                                           stock.Kunica / avgKunica +
                                           stock.Lisica / avgLisica +
                                           stock.Ondatra / avgOndatra +
                                           stock.Norka / avgNorka +
                                           stock.Bobr / avgBobr +
                                           stock.Volk / avgVolk +
                                           stock.Barsuk / avgBarsuk +
                                           stock.Vydra / avgVydra +
                                           stock.EnotovidnayaSobaka / avgEnotovidnayaSobaka +
                                           stock.Rys / avgRys +
                                           stock.Belka / avgBelka) / 13 +

                                          (stock.Los / avgLos +
                                           stock.Olen / avgOlen +
                                           stock.Kosula / avgKosula +
                                           stock.Kaban / avgKaban) / 4
                          let dolaResursovOzerVSumarnomZapasePercent = zapasyOzera / zapasyPhg
                          orderby stock.RegionName
                          select new ResultModel
                              {
                                  DolaResursovOzerVSumarnomZapasePercent = dolaResursovOzerVSumarnomZapasePercent,
                                  DolaResursovTerritoriiVSumarnomZapasePercent = 1 - dolaResursovOzerVSumarnomZapasePercent,
                                  KoefSootnosheniaResursov = dolaResursovOzerVSumarnomZapasePercent / (1 - dolaResursovOzerVSumarnomZapasePercent),
                                  RegionId = stock.RegionId,
                                  RegionName = stock.RegionName,
                                  IndexVelichinyIspolzovaniyaOzerVHozDeatelnosti = useZapasyOzera / zapasyOzera,
                                  ZapasyPhg = zapasyPhg
                              };

            return new ResultsViewModel { Items = results };
        }
    }
}
