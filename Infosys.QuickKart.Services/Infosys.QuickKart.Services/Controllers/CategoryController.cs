using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Infosys.QuickKart.DAL;
using Infosys.QuickKart.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace Infosys.QuickKart.Services.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : Controller
    {

        [HttpGet]
        public JsonResult GetCategories()
        {
            try
            {
                var dal = new QuickKartRepository();
                var categoryList = dal.GetCategoriesUsingLinq();
                var categories = new List<Category>();
                if (categoryList.Any())
                {
                    foreach (var cat in categoryList)
                    {

                        var category = new Category();
                        category.CategoryId = cat.CategoryId;
                        category.CategoryName = cat.CategoryName;
                        categories.Add(category);
                    }
                }
                return Json(categories);
            }
            catch (Exception ex)
            {
                return Json(null);
            }


        }
    }
}
