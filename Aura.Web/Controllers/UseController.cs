using System.Web.Mvc;
using Aura.Web.Interfaces;
using Aura.Web.Models;

namespace Aura.Web.Controllers
{
    public class UseController : EntityControllerBase, IEntityController
    {
        private readonly IEntityRepository<MineralViewModel> _mineralRepository;
        private readonly IEntityRepository<BiologicalViewModel> _biologicalRepository;
        private readonly IEntityRepository<TerritorialViewModel> _territorialRepository;
        private readonly IEntityRepository<WaterViewModel> _waterRepository;
        private readonly IEntityRepository<AnimalViewModel> _animalRepository;

        public UseController(
            IEntityRepository<MineralViewModel> mineralRepository,
            IEntityRepository<BiologicalViewModel> biologicalRepository,
            IEntityRepository<TerritorialViewModel> territorialRepository,
            IEntityRepository<WaterViewModel> waterRepository,
            IEntityRepository<AnimalViewModel> animalRepository,
            ICommonRepository commonRepository)
            : base("use", commonRepository)
        {
            _mineralRepository = mineralRepository;
            _biologicalRepository = biologicalRepository;
            _territorialRepository = territorialRepository;
            _waterRepository = waterRepository;
            _animalRepository = animalRepository;
        }

        [HttpGet]
        public ActionResult Animal()
        {
            var model = _animalRepository.GetUse();

            return View(model);
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
    }
}
