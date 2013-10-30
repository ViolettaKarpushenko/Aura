using System.Collections.Generic;
using Aura.Web.Common;
using Aura.Web.Models;

namespace Aura.Web.Interfaces
{
    public interface IEcologicalRepository
    {
        IEnumerable<EcologicalModel> GetEcologicalItems(Tables table, int? regionId = null);
        IEnumerable<KeyValuePair<int, string>> GetEcologicalRegions(Tables table);
    }
}