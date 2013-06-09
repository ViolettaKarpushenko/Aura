using System;
using System.Web.Mvc;
using Aura.Web.Data;
using Aura.Web.Models;

namespace Aura.Web.Controllers
{
    public class ResultController : Controller
    {
        private readonly IEntityRepository<MineralViewModel> _mineralRepository;
        private readonly IEntityRepository<BiologicalViewModel> _biologicalRepository;
        private readonly IEntityRepository<TerritorialViewModel> _territirialRepository;
        private readonly IEntityRepository<WaterViewModel> _waterRepository;

        public ResultController()
        {
            _mineralRepository = new MineralRepository();
            _biologicalRepository = new BiologicalRepository();
            _territirialRepository = new TerritirialRepository();
            _waterRepository = new WaterRepository();
        }

        [HttpGet]
        public ActionResult Animal()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public ActionResult Biological()
        {
            var model = _biologicalRepository.GetResult();

            return View(model);
        }

        [HttpGet]
        public ActionResult Mineral()
        {
            var model = _mineralRepository.GetResult();

            return View(model);
        }

        [HttpGet]
        public ActionResult Territorial()
        {
            var model = _territirialRepository.GetResult();

            return View(model);
        }

        [HttpGet]
        public ActionResult Water()
        {
            var model = _waterRepository.GetResult();

            return View(model);
        }
    }
}
