using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Aura.Web.Common;
using Aura.Web.Interfaces;
using Aura.Web.Models;

namespace Aura.Web.Controllers
{
    public class EcologicalController : Controller
    {
        private readonly IEcologicalRepository _ecologicalRepository;

        public EcologicalController(IEcologicalRepository ecologicalRepository)
        {
            _ecologicalRepository = ecologicalRepository;
        }

        [HttpGet]
        public ActionResult HydrochemicalAssessment()
        {
            var items = _ecologicalRepository.GetEcologicalItems(Tables.HydrochemicalAssessmentIzv);
            var model = new EcologicalViewModel
            {
                DataSets = new Dictionary<int, IEnumerable<EcologicalModel>> { { 0, items } }
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult GeochemicalAssessment()
        {
            var regions = _ecologicalRepository.GetEcologicalRegions(Tables.GeochemicalAssessmentZc)
                            .Select(pair =>
                              new SelectListItem
                                  {
                                      Selected = false,
                                      Value = pair.Key.ToString(CultureInfo.InvariantCulture),
                                      Text = pair.Value
                                  })
                                .ToArray();

            regions.First().Selected = true;

            var items = _ecologicalRepository.GetEcologicalItems(Tables.GeochemicalAssessmentIpm);
            var model = new EcologicalViewModel
            {
                Regions = regions,
                DataSets = new Dictionary<int, IEnumerable<EcologicalModel>> { { 0, items } }
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult GeochemicalAssessmentGrid(int regionId)
        {
            var model = _ecologicalRepository.GetEcologicalItems(Tables.GeochemicalAssessmentZc, regionId);
            return View(model);
        }

        [HttpGet]
        public ActionResult HydrobiologicalAssessment()
        {
            var items = _ecologicalRepository.GetEcologicalItems(Tables.HydrobiologicalAssessmentS);
            var model = new EcologicalViewModel
            {
                DataSets = new Dictionary<int, IEnumerable<EcologicalModel>> { { 0, items } }
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult ReservoirTransformation()
        {
            var items = _ecologicalRepository.GetEcologicalItems(Tables.ReservoirTransformationAt);
            var model = new EcologicalViewModel
            {
                DataSets = new Dictionary<int, IEnumerable<EcologicalModel>> { { 0, items } }
            };

            return View(model);
        }
    }
}
