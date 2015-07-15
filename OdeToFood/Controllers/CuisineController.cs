using System;
using System.Web.Mvc;
using OdeToFood.Filters;

namespace MvcContacts.Controllers
{
    public class CuisineController : Controller
    {
        // GET: Cuisine
        //[Authorize]
        [Log]
        public ActionResult Search(string name = "french")
        {
            var message = Server.HtmlEncode(name);
            return Json(new {Message = message, Name = "Philip"}, JsonRequestBehavior.AllowGet);
        }
    }
}