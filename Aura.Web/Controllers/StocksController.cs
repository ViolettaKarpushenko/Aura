using System.Web.Mvc;

namespace Aura.Web.Controllers
{
    public class StocksController : Controller
    {
        [HttpGet]
        public ActionResult Animal()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Biological()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Mineral()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Territorial()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Water()
        {
            return View();
        }
    }
}
