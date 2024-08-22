﻿using CoreAndFood.Data.Models;
using CoreAndFood.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreAndFood.Controllers
{
    public class CategoryController : Controller
    {
		CategoryRepository categoryRepository = new CategoryRepository();

        //[Authorize]
		public IActionResult Index(string p)
        {
            if (!string.IsNullOrEmpty(p))
            { 
                return View(categoryRepository.List(x=>x.CategoryName == p));
            }


            return View(categoryRepository.TList().Where(x => x.Status== true).ToList());
        }

        [HttpGet]
        public IActionResult CategoryAdd()
        {

            return View();
        }

        [HttpPost]
        public IActionResult CategoryAdd(Category p)
        {

            if (!ModelState.IsValid)
            {
                return View("CategoryAdd");
            }
            Console.WriteLine("model valid");
            p.Status = true;
            categoryRepository.TAdd(p);
            return RedirectToAction("Index");
        }
        public IActionResult CategoryGet(int id)
        {
            var x = categoryRepository.TGet(id);
            Category ct = new Category()
            {
                CategoryName = x.CategoryName,
                CategoryDescription = x.CategoryDescription,
                CategoryID = x.CategoryID
            };
            return View(ct);

        }

        public IActionResult CategoryUpdate(Category p)
        {
            var x = categoryRepository.TGet(p.CategoryID);
            x.CategoryName = p.CategoryName;
            x.CategoryDescription = p.CategoryDescription;
            x.Status = true;
            categoryRepository.TUpdate(x);
            return RedirectToAction("Index");
        }

        public IActionResult CategoryDelete(int id)
        {
            var x =categoryRepository.TGet(id);
            x.Status = false;
            categoryRepository.TUpdate(x);
            return RedirectToAction("Index");
        }




    }
}
