using System.Linq;
using Aura.Web.Common;
using Aura.Web.Models;

namespace Aura.Web.Data
{
    public class EconomicRepository : Repository, IEntityRepository<EconomicViewModel>
    {
        private EconomicViewModel GetItems(string tableName)
        {
            var query = BuildAggregateQueryPattern(tableName, (int)Tables.Economic, 22);
            var stockData = Execute<EconomicModel>(
                query,                (int)EconomicColumns.ObemVody,
                EconomicColumns.ObemVody,
                (int)EconomicColumns.Izjato,
                EconomicColumns.Izjato,
                (int)EconomicColumns.IspolzovanoVsego,
                EconomicColumns.IspolzovanoVsego,
                (int)EconomicColumns.HozjajstvennoBytovoeVodopotreblenie,
                EconomicColumns.HozjajstvennoBytovoeVodopotreblenie,
                (int)EconomicColumns.PromyshlennoeVodopotreblenie,
                EconomicColumns.PromyshlennoeVodopotreblenie,
                (int)EconomicColumns.SelskohozjajstvennoeVodopotreblenie,
                EconomicColumns.SelskohozjajstvennoeVodopotreblenie,
                (int)EconomicColumns.RybohozjajstvennoeVodopotreblenie,
                EconomicColumns.RybohozjajstvennoeVodopotreblenie,
                (int)EconomicColumns.Ploshhad,
                EconomicColumns.Ploshhad,
                (int)EconomicColumns.Vmestimost,
                EconomicColumns.Vmestimost,
                (int)EconomicColumns.Sapropel,
                EconomicColumns.Sapropel,
                (int)EconomicColumns.Dobyto1,
                EconomicColumns.Dobyto1,
                (int)EconomicColumns.ZagotovkaMakrofitov,
                EconomicColumns.ZagotovkaMakrofitov,
                (int)EconomicColumns.Zagotovleno1,
                EconomicColumns.Zagotovleno1,
                (int)EconomicColumns.Fitoplankton,
                EconomicColumns.Fitoplankton,
                (int)EconomicColumns.Dobyto2,
                EconomicColumns.Dobyto2,
                (int)EconomicColumns.Zooplankton,
                EconomicColumns.Zooplankton,
                (int)EconomicColumns.Dobyto3,
                EconomicColumns.Dobyto3,
                (int)EconomicColumns.ZagotovkaZoobentosa,
                EconomicColumns.ZagotovkaZoobentosa,
                (int)EconomicColumns.Zagotovleno2,
                EconomicColumns.Zagotovleno2,
                (int)EconomicColumns.Rybolovstvo,
                EconomicColumns.Rybolovstvo,
                (int)EconomicColumns.Vylov,
                EconomicColumns.Vylov,
                (int)EconomicColumns.OzerOopt,
                EconomicColumns.OzerOopt);

            return new EconomicViewModel { Items = stockData.OrderBy(stock => stock.RegionName) };
        }

        public EconomicViewModel GetStocks()
        {
            return GetItems("stocks");
        }

        public EconomicViewModel GetUse()
        {
            return GetItems("use");
        }

        public ResultsViewModel GetResult()
        {
            throw new System.NotImplementedException();
        }
    }
}