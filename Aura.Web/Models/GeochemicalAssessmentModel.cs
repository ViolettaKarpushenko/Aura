using System.Collections.Generic;
using System.Web.Mvc;

namespace Aura.Web.Models
{
    public class GeochemicalAssessmentModel
    {
        public IEnumerable<SelectListItem> Regions { get; set; }

        public int RegionId { get; set; }
    }
}