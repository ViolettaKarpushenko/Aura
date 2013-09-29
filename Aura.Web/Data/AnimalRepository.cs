using System;
using System.Linq;
using System.Runtime.InteropServices;
using Aura.Web.Common;
using Aura.Web.Models;

namespace Aura.Web.Data
{
    public class AnimalRepository : Repository, IEntityRepository<AnimalViewModel>
    {
        private AnimalViewModel GetItems(string tableName)
        {
            var query = BuildAggregateQueryPattern(tableName, (int)Tables.Animal, 6);
            var stockData = Execute<AnimalModel>(
                query,
                (int)AnimalColumns.Los,
                AnimalColumns.Los,
                (int)AnimalColumns.Olen,
                AnimalColumns.Olen,
                (int)AnimalColumns.Kosula,
                AnimalColumns.Kosula,
                (int)AnimalColumns.Kaban,
                AnimalColumns.Kaban,
                (int)AnimalColumns.ZayacBelak,
                AnimalColumns.ZayacBelak,
                (int)AnimalColumns.ZayacRusak,
                AnimalColumns.ZayacRusak,
                (int)AnimalColumns.Kunica,
                AnimalColumns.Kunica,
                (int)AnimalColumns.Lisica,
                AnimalColumns.Lisica,
                (int)AnimalColumns.Ondatra,
                AnimalColumns.Ondatra,
                (int)AnimalColumns.Norka,
                AnimalColumns.Norka,
                (int)AnimalColumns.Bobr,
                AnimalColumns.Bobr,
                (int)AnimalColumns.Volk,
                AnimalColumns.Volk,
                (int)AnimalColumns.Barsuk,
                AnimalColumns.Barsuk,
                (int)AnimalColumns.Vydra,
                AnimalColumns.Vydra,
                (int)AnimalColumns.EnotovidnayaSobaka,
                AnimalColumns.EnotovidnayaSobaka,
                (int)AnimalColumns.Rys,
                AnimalColumns.Rys,
                (int)AnimalColumns.Belka,
                AnimalColumns.Belka,
                (int)AnimalColumns.Gluhar,
                AnimalColumns.Gluhar,
                (int)AnimalColumns.Teterev,
                AnimalColumns.Teterev,
                (int)AnimalColumns.Rabchik,
                AnimalColumns.Rabchik,
                (int)AnimalColumns.SerayKuropatka,
                AnimalColumns.SerayKuropatka,
                (int)AnimalColumns.DikayaUtka,
                AnimalColumns.DikayaUtka,
                (int)AnimalColumns.Zooplankton,
                AnimalColumns.Zooplankton,
                (int)AnimalColumns.Bentos,
                AnimalColumns.Bentos,
                (int)AnimalColumns.Ryba,
                AnimalColumns.Ryba);

            return new AnimalViewModel { Items = stockData.OrderBy(stock => stock.RegionName) };
        }

        public AnimalViewModel GetStocks()
        {
            return GetItems("stocks");
        }

        public AnimalViewModel GetUse()
        {
            return GetItems("use");
        }

        public ResultsViewModel GetResult()
        {
            throw new NotImplementedException();
            /*
            var stocks = GetStocks().Items.AsParallel();

            var avgKopytnye = stocks.Average(stock => stock.Los + stock.Olen + stock.Kosula + stock.Kaban);

            var avgPushnie = stocks.Average(stock =>
                        stock.ZayacBelak + stock.ZayacRusak + stock.Kunica + stock.Lisica + stock.Ondatra + stock.Norka +
                        stock.Bobr + stock.Volk + stock.Barsuk + stock.Vydra + stock.EnotovidnayaSobaka + stock.Rys +
                        stock.Belka);

            var avgPtici = stocks.Average(stock => stock.Gluhar + stock.Teterev + stock.Rabchik + stock.SerayKuropatka + stock.DikayaUtka);

            var avgZooplankton = stocks.Average(stosk => stosk.Zooplankton);

            var avgBentos = stocks.Average(stock => stock.Bentos);

            var avgRuba = stocks.Average(stock => stock.Ryba);

            var results = stocks.Select(stock => new
            {
                stock,
                zapasyPhg = 
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
            */
        }
    }
}