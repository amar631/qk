using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Infosys.QuickKart.DAL;
using Microsoft.AspNetCore.Mvc;
using Infosys.QuickKart.Services.Models;

namespace Infosys.QuickKart.Services.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : Controller
    {
        QuickKartRepository repository;
        public ProductController()
        {
            repository = new QuickKartRepository();
        }
    
    [HttpGet]
        public JsonResult GetProducts()
        {
            try
            {
                var dal = new QuickKartRepository();
                var productList = dal.DisplayProductDetails();
                var products = new List<Product>();
                if (productList.Any())
                {
                    foreach (var prod in productList)
                    {
                        var product = new Product();
                        product.ProductId = prod.ProductId;
                        product.ProductName = prod.ProductName;
                        product.CategoryId = prod.CategoryId;
                        product.Price = prod.Price;
                        product.QuantityAvailable = prod.QuantityAvailable;

                        products.Add(product);
                    }
                }
                return Json(products);
            }
            catch (Exception)
            {
                return Json(null);
            }

        }
    }
}
