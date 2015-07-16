
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using OdeToFood.Models;
using PagedList;

namespace OdeToFood.Controllers
{
    public class HomeController : Controller
    {
        OdeToFoodDB db = new OdeToFoodDB();

        public ActionResult Autocomplete(string term)
        {
            var model =
                db.Restaurants
                    .Where(r => r.Name.StartsWith(term))
                    .Take(10)
                    .Select(r => new
                    {
                        label = r.Name
                    });
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [OutputCache(Duration = 360, VaryByHeader = "X-Requested-With", Location = OutputCacheLocation.Server)]
        public ActionResult Index(string searchTerm = null, int page = 1)
        {
            var model =
                db.Restaurants
                    .OrderByDescending(r => r.Reviews.Average(review => review.Rating))
                    .Where(r => searchTerm == null || r.Name.StartsWith(searchTerm))
                    .Select(r => new RestaurantListViewModel
                    {
                        Id = r.Id,
                        Name = r.Name,
                        City = r.City,
                        Country = r.Country,
                        CountOfReviews = r.Reviews.Count()
                    }).ToPagedList(page, 10);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Restaurants", model);
            }

            return View(model);
        }

        public ActionResult About()
        {
            var model = new AboutModel();
            model.Name = "Philip";
            model.Location = "California, USA";

            return View(model);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (db != null)
                db.Dispose();
            base.Dispose(disposing);
        }
    }
}
