using System.Web.Mvc;

namespace Aura.Web.Controllers
{
    public class EcologicalController : Controller
    {
        [HttpGet]
        public ActionResult HydrochemicalAssessment()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GeochemicalAssessment()
        {
            return View();
        }

        [HttpGet]
        public ActionResult HydrobiologicalAssessment()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ReservoirTransformation()
        {
            return View();
        }
    }
}
