using System.Linq;
using Aura.Web.Common;
using Aura.Web.Interfaces;
using Aura.Web.Models;

namespace Aura.Web.Data
{
    public class EcologicalRepository : Repository, IEcologicalRepository
    {
        public EcologicalViewModel GetHydrobiologicalAssessment()
        {
            var items = EcecuteBaseEcologicalQuery((int)Tables.HydrobiologicalAssessmentS);
            return new EcologicalViewModel { Items = items.OrderBy(x => x.Name) };
        }
    }
}