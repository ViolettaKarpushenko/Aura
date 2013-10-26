using System;
using System.Web.Mvc;
using Aura.Web.Interfaces;

namespace Aura.Web.Controllers
{
    public class IntegratedController : EntityControllerBase, IEntityController
    {
        public IntegratedController(ICommonRepository commonRepository)
            : base("integrated", commonRepository)
        {
        }

        [HttpGet]
        public ActionResult Animal()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public ActionResult Biological()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public ActionResult Mineral()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public ActionResult Territorial()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public ActionResult Water()
        {
            throw new NotImplementedException();
        }
    }
}
