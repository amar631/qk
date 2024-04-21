using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Infosys.QuickKart.DAL;
using Infosys.QuickKart.DAL.Models;
using Infosys.QuickKart.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace Infosys.QuickKart.Services.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : Controller
    {
        [HttpPost]
        public JsonResult ValidateUserCredentials(User userObj)
        {
            string roleName = "";
            try
            {
                var dal = new QuickKartRepository();
                roleName = dal.ValidateLoginUsingLinq(userObj.EmailId, userObj.UserPassword);
            }
            catch (Exception)
            {
                roleName = "Invalid credentials";
            }

            return Json(roleName);
        }

        [HttpPost]
        public JsonResult InsertUserDetails(Models.User user)
        {
            bool status;
            try
            {
                Users userObj = new Users();
                userObj.EmailId = user.EmailId;
                userObj.UserPassword = user.UserPassword;
                userObj.Gender = user.Gender;
                userObj.DateOfBirth = (DateTime)user.DateOfBirth;
                userObj.Address = user.Address;

                var dal = new QuickKartRepository();

                status = dal.RegisterUserUsingLinq(userObj);
            }
            catch (Exception)
            {
                status = false;
            }
            return Json(status);
        }

        [HttpPost]
        public JsonResult AddProductToCart(Models.Cart cartObj)
        {
            int returnvalue = -1;
            try
            {
                var dal = new QuickKartRepository();
                returnvalue = dal.AddProductToCartUsingUSP(cartObj.ProductId, cartObj.EmailId);
            }
            catch (Exception)
            {
                returnvalue = -1;
            }
            return Json(returnvalue);
        }

        [HttpGet]
        public JsonResult GetCartProducts(string emailId)
        {
            try
            {
                var dal = new QuickKartRepository();
            var cartProductList = dal.FetchCartProductsByEmailId(emailId);
            CartProductsDetails product;
            var cartProducts = new List<CartProductsDetails>();
            if (cartProductList.Any())
            {
                foreach (var prod in cartProductList)
                {
                    product = new CartProductsDetails();
                    product.ProductId = prod.ProductId;
                    product.ProductName = prod.ProductName;
                    product.Quantity = prod.Quantity;
                    product.QuantityAvailable = prod.QuantityAvailable;
                    product.Price = prod.Price;

                    cartProducts.Add(product);
                }
            }
            return Json(cartProducts);
        }
            catch (Exception)
            {
                return null;
            }

}

        [HttpPut]
        public JsonResult UpdateCartProducts(Models.Cart cartObj)
        {
            bool status = false;
            try
            {
                var dal = new QuickKartRepository();
                status = dal.UpdateCartProductsLinq(cartObj.ProductId, cartObj.EmailId, cartObj.Quantity);
            }
            catch (Exception)
            {
                status = false;
            }

            return Json(status);
        }

        [HttpDelete]
        public JsonResult DeleteCartProduct(Models.Cart cartObj)
        {
            var status = false;
            try
            {
                var dal = new QuickKartRepository();
                status = dal.DeleteCartProduct(cartObj.ProductId, cartObj.EmailId);
            }
            catch (Exception ex)
            {
                status = false;
            }
            return Json(status);
        }
    }
}
