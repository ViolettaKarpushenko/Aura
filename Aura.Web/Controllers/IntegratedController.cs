using System;
using System.Web.Mvc;

namespace Aura.Web.Controllers
{
    public class IntegratedController : Controller, IEntityController
    {
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
