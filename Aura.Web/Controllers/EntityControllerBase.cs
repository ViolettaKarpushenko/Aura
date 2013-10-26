using System;
using System.Web.Mvc;
using Aura.Web.Interfaces;

namespace Aura.Web.Controllers
{
    public abstract class EntityControllerBase : Controller
    {
        private readonly ICommonRepository _commonRepository;
        private readonly string _tableName;

        protected EntityControllerBase(
            string tableName,
            ICommonRepository commonRepository)
        {
            _tableName = tableName;
            _commonRepository = commonRepository;
        }

        [HttpPost]
        public JsonResult Update(int tableId, int columnId, int regionId, double value)
        {
            try
            {
                _commonRepository.UpdateValue(_tableName, tableId, columnId, regionId, value);
                return Json(new { success = true });
            }
            catch (Exception exception)
            {
                return Json(new { success = false, message = exception.Message });
            }
        }
    }
}