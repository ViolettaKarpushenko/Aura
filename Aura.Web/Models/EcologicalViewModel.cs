using System.Collections.Generic;
using System.Web.Mvc;

namespace Aura.Web.Models
{
    public class EcologicalViewModel
    {
        public IDictionary<int, IEnumerable<EcologicalModel>> DataSets { get; set; }

        public IEnumerable<SelectListItem> Regions { get; set; }
    }
}