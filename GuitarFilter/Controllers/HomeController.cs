using GuitarFilter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace GuitarFilter.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HomeController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var model = GetListFilters(_context);
            return View(model);
        }
        private static List<FNameViewModel> GetListFilters(ApplicationDbContext context)
        {
            var queryName = from f in context.FilterNames
                            select f;
            var queryGroup = from g in context.FilterNameGroups
                                .Include(v => v.FilterValueOf)
                             select g;
            //Отримуємо запгальну моножину значень
            var query = from u in queryName
                        join g in queryGroup on u.Id equals g.FilterNameId into ua
                        from aEmp in ua.DefaultIfEmpty()
                        select new
                        {
                            FNameId = u.Id,
                            FName = u.Name,
                            FValueId = aEmp != null ? aEmp.FilterValueId : 0,
                            FValue = aEmp != null ? aEmp.FilterValueOf.Name : null,
                        };
            //Групуємо по іменам і сортуємо по спаданю імен
            var groupNames = (from f in query
                              group f by new
                              {
                                  Id = f.FNameId,
                                  Name = f.FName
                              } into g
                              //orderby g.Key.Name
                              select g).OrderByDescending(g => g.Key.Name);
            //По групах отримуємо
            var result = from fName in groupNames
                         select
                         new FNameViewModel
                         {
                             Id = fName.Key.Id,
                             Name = fName.Key.Name,
                             Children = (from v in fName
                                         group v by new FValueViewModel
                                         {
                                             Id = v.FValueId,
                                             Name = v.FValue
                                         } into g
                                         select g.Key)
                                         .OrderBy(l => l.Name).ToList()
                         };
            return result.ToList();

        }
        [HttpPost]
        public ActionResult FilterProduct(List<FNameViewModel> list)
        {
            if (list == null)
            {
                return View("index");
            }



            return RedirectToAction("index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}