using System.Text.RegularExpressions;
using System.Web.Mvc;

using Aura.Web.Common;
using Aura.Web.Data;

namespace Aura.Web.Controllers
{
    public class RegionsController : Controller
    {
        private readonly RegionsRepository _regionsRepository;

        public RegionsController()
        {
            _regionsRepository = new RegionsRepository();
        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = _regionsRepository.GetRegions();

            return View(model);
        }

        [HttpPost]
        public JsonResult Update(int id, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Json(new { success = false, selector = "input.region-name", message = "Название области не может быть пустым." });
            }

            if (name.Length > 50)
            {
                return Json(new { success = false, selector = "input.region-name", message = "Название области не может быть длиннее 50 символов." });
            }

            if (!new Regex("[А-Яа-я ]+").IsMatch(name))
            {
                return Json(new { success = false, selector = "input.region-name", message = "Название области не может содержать символы отличные от русских букв и пробелов." });
            }

            try
            {
                _regionsRepository.Update(id, name);
            }
            catch (PresentationException exception)
            {
                return Json(new { success = false, selector = "input.region-name", message = exception.Message });
            }
            catch
            {
                return Json(new { success = false, selector = "input.region-name", global = true, message = "Сервис временно не доступен." });
            }

            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult Add(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Json(new { success = false, selector = "input.new-region-name", message = "Название области не может быть пустым." });
            }

            if (name.Length > 50)
            {
                return Json(new { success = false, selector = "input.new-region-name", message = "Название области не может быть длиннее 50 символов." });
            }

            if (!new Regex("^[А-Яа-я ]+$").IsMatch(name))
            {
                return Json(new { success = false, selector = "input.new-region-name", message = "Название области не может содержать символы отличные от русских букв и пробелов." });
            }

            try
            {
                _regionsRepository.Add(name);
            }
            catch (PresentationException exception)
            {
                return Json(new { success = false, selector = "input.new-region-name", message = exception.Message });
            }
            catch
            {
                return Json(new { success = false, selector = "input.new-region-name", global = true, message = "Сервис временно не доступен." });
            }

            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                _regionsRepository.Delete(id);
            }
            catch (PresentationException exception)
            {
                return Json(new { success = false, message = exception.Message });
            }
            catch
            {
                return Json(new { success = false, global = true, message = "Сервис временно не доступен." });
            }

            return Json(new { success = true });
        }
    }
}