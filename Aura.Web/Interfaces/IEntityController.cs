using System.Web.Mvc;

namespace Aura.Web.Interfaces
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