﻿using System;
using System.Web.Mvc;
using Aura.Web.Data;
using Aura.Web.Models;

namespace Aura.Web.Controllers
{
    public class StocksController : Controller, IEntityController
    {
        private readonly IEntityRepository<MineralViewModel> _mineralRepository;
        private readonly IEntityRepository<BiologicalViewModel> _biologicalRepository;
        private readonly IEntityRepository<TerritorialViewModel> _territorialRepository;
        private readonly IEntityRepository<WaterViewModel> _waterRepository;
        private readonly IEntityRepository<AnimalViewModel> _animalRepository;
        private readonly CommonRepository _commonRepository;

        public StocksController()
        {
            _mineralRepository = new MineralRepository();
            _biologicalRepository = new BiologicalRepository();
            _territorialRepository = new TerritorialRepository();
            _waterRepository = new WaterRepository();
            _commonRepository = new CommonRepository();
            _animalRepository = new AnimalRepository();
        }

        [HttpGet]
        public ActionResult Animal()
        {
            var model = _animalRepository.GetStocks();

            return View(model);
        }

        [HttpGet]
        public ActionResult Biological()
        {
            var model = _biologicalRepository.GetStocks();

            return View(model);
        }

        [HttpGet]
        public ActionResult Mineral()
        {
            var model = _mineralRepository.GetStocks();

            return View(model);
        }

        [HttpGet]
        public ActionResult Territorial()
        {
            var model = _territorialRepository.GetStocks();

            return View(model);
        }

        [HttpGet]
        public ActionResult Water()
        {
            var model = _waterRepository.GetStocks();

            return View(model);
        }

        [HttpPost]
        public JsonResult Update(int tableId, int columnId, int regionId, double value)
        {
            try
            {
                _commonRepository.UpdateValue("stocks", tableId, columnId, regionId, value);
                return Json(new { success = true });
            }
            catch (Exception exception)
            {
                return Json(new { success = false, message = exception.Message });
            }
        }
    }
}
