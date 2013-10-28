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

            var results = stocks.Select(stock => new
                {
                    stock,
                    zaposyOzer = stock.Bentos / avgBentos +
                                    stock.Zooplankton / avgZooplankton +
                                    stock.Ryba / avgRyba
                })
                .Select(stock => new
                {
                    stock.stock,
                    stock.zaposyOzer,
                    zapasyPhg = stock.zaposyOzer +
                           (stock.stock.DikayaUtka / avgDikayaUtka +
                            stock.stock.SerayaKuropatka / avgSerayaKuropatka +
                            stock.stock.Rabchik / avgRabchik +
                            stock.stock.Teterev / avgTeterev +
                            stock.stock.Gluhar / avgGluhar) / 4 +

                           (stock.stock.ZayacBelak / avgZayacBelak +
                            stock.stock.ZayacRusak / avgZayacRusak +
                            stock.stock.Kunica / avgKunica +
                            stock.stock.Lisica / avgLisica +
                            stock.stock.Ondatra / avgOndatra +
                            stock.stock.Norka / avgNorka +
                            stock.stock.Bobr / avgBobr +
                            stock.stock.Volk / avgVolk +
                            stock.stock.Barsuk / avgBarsuk +
                            stock.stock.Vydra / avgVydra +
                            stock.stock.EnotovidnayaSobaka / avgEnotovidnayaSobaka +
                            stock.stock.Rys / avgRys +
                            stock.stock.Belka / avgBelka) / 13 +

                           (stock.stock.Los / avgLos +
                            stock.stock.Olen / avgOlen +
                            stock.stock.Kosula / avgKosula +
                            stock.stock.Kaban / avgKaban) / 4
                })
                .Select(stock => new ResultModel
                {
                    ZapasyPhg = stock.zapasyPhg,
                    ZapasyOzera = stock.zaposyOzer,
                    Percent = stock.zapasyPhg * stock.zaposyOzer,
                    KoefBalansa = stock.zaposyOzer / (stock.zapasyPhg - stock.zaposyOzer),
                    RegionId = stock.stock.RegionId,
                    RegionName = stock.stock.RegionName
                })
                .OrderBy(stock => stock.RegionName);

            return new ResultsViewModel { Items = results };
        }
    }
}
