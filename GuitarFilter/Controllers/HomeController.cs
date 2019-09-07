using GuitarFilter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using GuitarFilter.Entities;

namespace GuitarFilter.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HomeController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult Index(string []idvalues)
        {
            var model = GetListFilters(_context);

            int[] valuesId = new int[0];
            if (idvalues != null)
            {
                valuesId = idvalues.Select(v => int.Parse(v)).ToArray();
            }
            //int[] idValuesFilter = { 8 };
            var listProduct = GetFilterProductList(_context, model, valuesId);

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
        private List<ProductViewModel> GetFilterProductList(ApplicationDbContext context,
            List<FNameViewModel> filterList, int[] values)
        {
            var query = context.Products.AsQueryable();
            //проходимо цикл по назвам фільтра
            foreach (var fName in filterList)
            {
                int count = 0; //кількість співпадінь в даній групі фільтрів
                var predicate = PredicateBuilder.False<Product>();
                //проходимо цикл по значенням назв фільтрів
                foreach (var fValue in fName.Children)
                {
                    for (int i = 0; i < values.Length; i++)
                    {
                        var idValue = fValue.Id;
                        //перевірка на співпадіння(додаємо до запита "OR" якщо є співпадіння в однакових групах)
                        if (idValue == values[i])
                        {
                            predicate = predicate
                                .Or(p => p.Filters
                                    .Any(f => f.FilterValueId == idValue));
                            count++;
                        }
                    }
                }
                //додаємо до запита "AND" якщо є співпадіння в різних групах
                if (count > 0)
                {
                    query = query.Where(predicate);
                }
            }
            //формуємо новий список(відфільтрований)
            var result = query.Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price//,
                //Quantity = p.Quantity
            }).ToList();
            return result;
        }
        //[HttpPost]
        //public ActionResult FilterProduct(List<ProductViewModel> model)
        //{
        //    //int l = model;

        //    //if (list != null)
        //    //{
        //    //    return View("index");
        //    //}

        //    //var gl = GetListFilters(_context);
        //    //foreach(var item in gl)
        //    //{
        //    //    //var s = item
        //    //}
        //    ////int l = gl.Count();
        //    ///
        //    Product product = new Product
        //    {

        //    };

        //    return RedirectToAction("index");
        //}

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