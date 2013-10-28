using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
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
            return View();
        }

        [HttpGet]
        public ActionResult GeochemicalAssessment()
        {
            var regions = Enumerable
                            .Range(1, 10)
                            .Select(index =>
                              new SelectListItem
                                  {
                                      Selected = false,
                                      Value = index.ToString(CultureInfo.InvariantCulture),
                                      Text = "Region " + index
                                  })
                                .ToArray();

            regions.First().Selected = true;
            var model = new GeochemicalAssessmentModel { Regions = regions, RegionId = 1 };

            return View(model);
        }

        [HttpGet]
        public ActionResult GeochemicalAssessmentGrid(int regionId)
        {
            var model = new List<GeochemicalAssessmentItemModel>();
            var random = new Random();
            for (var i = 0; i < 10; i++)
            {
                model.Add(new GeochemicalAssessmentItemModel
                    {
                        Name = Guid.NewGuid().ToString("N").Substring(0, 2).ToUpper(),
                        Value = random.Next(0, 100)
                    });
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult HydrobiologicalAssessment()
        {
            var model = _ecologicalRepository.GetHydrobiologicalAssessment();
            return View(model);
        }

        [HttpGet]
        public ActionResult ReservoirTransformation()
        {
            return View();
        }
    }
}
