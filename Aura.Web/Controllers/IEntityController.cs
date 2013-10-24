using System.Web.Mvc;

namespace Aura.Web.Controllers
{
    public interface IEntityController
    {
        ActionResult Animal();

        ActionResult Biological();

        ActionResult Mineral();

        ActionResult Territorial();

        ActionResult Water();
    }
}