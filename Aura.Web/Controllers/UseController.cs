using System;
using System.Web.Mvc;
using Aura.Web.Data;
using Aura.Web.Models;

namespace Aura.Web.Controllers
{
    public class UseController : Controller
    {
        private readonly IEntityRepository<MineralViewModel> _mineralRepository;
        private readonly IEntityRepository<BiologicalViewModel> _biologicalRepository;
        private readonly IEntityRepository<TerritorialViewModel> _territorialRepository;
        private readonly IEntityRepository<WaterViewModel> _waterRepository;
        private readonly IEntityRepository<EconomicViewModel> _economicRepository;
        private readonly CommonRepository _commonRepository;

        public UseController()
        {
            _mineralRepository = new MineralRepository();
            _biologicalRepository = new BiologicalRepository();
            _territorialRepository = new TerritorialRepository();
            _waterRepository = new WaterRepository();
            _commonRepository = new CommonRepository();
            _economicRepository = new EconomicRepository();
        }

        [HttpGet]
        public ActionResult Animal()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public ActionResult Biological()
        {
            var model = _biologicalRepository.GetUse();

            return View(model);
        }

        [HttpGet]
        public ActionResult Mineral()
        {
            var model = _mineralRepository.GetUse();

            return View(model);
        }

        [HttpGet]
        public ActionResult Territorial()
        {
            var model = _territorialRepository.GetUse();

            return View(model);
        }

        [HttpGet]
        public ActionResult Water()
        {
            var model = _waterRepository.GetUse();

            return View(model);
        }

        [HttpGet]
        public ActionResult Economic()
        {
            var model = _economicRepository.GetUse();

            return View(model);
        }

        [HttpPost]
        public JsonResult Update(int tableId, int columnId, int regionId, double value)
        {
            try
            {
                _commonRepository.UpdateValue("use", tableId, columnId, regionId, value);
                return Json(new { success = true });
            }
            catch (Exception exception)
            {
                return Json(new { success = false, message = exception.Message });
            }
        }
    }
}
