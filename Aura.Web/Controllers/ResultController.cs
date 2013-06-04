using System.Web.Mvc;
using Aura.Web.Data;

namespace Aura.Web.Controllers
{
    public class ResultController : Controller
    {
        private readonly ResultRepository _resultRepository;

        public ResultController()
        {
            _resultRepository = new ResultRepository();
        }

        [HttpGet]
        public ActionResult Animal()
        {
            var model = _resultRepository.GetAnimalModel();
            return View(model);
        }

        [HttpGet]
        public ActionResult Biological()
        {
            var model = _resultRepository.GetBiologicalModel();
            return View(model);
        }

        [HttpGet]
        public ActionResult Mineral()
        {
            var model = _resultRepository.GetMineralModel();
            return View(model);
        }

        [HttpGet]
        public ActionResult Territorial()
        {
            var model = _resultRepository.GetTerritorialModel();
            return View(model);
        }

        [HttpGet]
        public ActionResult Water()
        {
            var model = _resultRepository.GetWaterModel();
            return View(model);
        }
    }
}
