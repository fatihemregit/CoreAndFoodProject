using CoreAndFood.Data.Models;
using CoreAndFood.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace CoreAndFood.Controllers
{
    public class FoodController : Controller
    {
        FoodRepository foodRepository = new FoodRepository();
        Context c = new Context();
        public IActionResult Index(int page= 1)
        {

            return View(foodRepository.TList("Category").ToPagedList(page,3));
        }


        [HttpGet]
        public IActionResult AddFood()
        {
           
            List<SelectListItem> values = (from x in c.Categories.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CategoryName,
                                               Value = x.CategoryID.ToString(),
                                           }
                                           
                                           ).ToList();

            ViewBag.vls = values;
            return View();
        }
        [HttpPost]
        public IActionResult AddFood(urunekle p)
        {

            Food f = new Food();
            if (p.ImageURL != null)
            {
                var extension = Path.GetExtension(p.ImageURL.FileName);
                var newimagename = Guid.NewGuid() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/resimler/",newimagename);
                var stream = new FileStream(location,FileMode.Create);
                p.ImageURL.CopyTo(stream);
                f.ImageURL = newimagename;
            }
            f.Name = p.Name;
            f.Price = p.Price;
            f.Stock = p.Stock;
            f.CategoryID = p.CategoryID;
            f.Description = "sss";

            foodRepository.TAdd(f);
            return RedirectToAction("Index");
        
        }

        public IActionResult DeleteFood(int id)
        {
            foodRepository.TDelete(foodRepository.TGet(id));
            return RedirectToAction("Index");
        }

        public IActionResult FoodGet(int id)
        {
            //TODO change var to food object
            var x = foodRepository.TGet(id);
            Food f = new Food()
            {
                FoodID = x.FoodID,
                CategoryID = x.CategoryID,
                Name = x.Name,
                Price = x.Price,
                Stock = x.Stock,
                Description = x.Description,
                ImageURL = x.ImageURL,
                
                
            };
			List<SelectListItem> values = (from u in c.Categories.ToList()
										   select new SelectListItem
										   {
											   Text = u.CategoryName,
											   Value = u.CategoryID.ToString(),
										   }

										   ).ToList();

			ViewBag.vls = values;

			return View(f);
        }


        [HttpPost]
        public IActionResult FoodUpdate(Food p)
        {
            var x = foodRepository.TGet(p.FoodID);
            x.Name = p.Name;
			x.Stock = p.Stock;
			x.Price = p.Price;
			x.ImageURL = p.ImageURL;
			x.Description = p.Description;
            x.CategoryID = p.CategoryID;
            foodRepository.TUpdate(x);
            return RedirectToAction("Index");
        
        }





	}
}
