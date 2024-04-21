using Infosys.QuickKart.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
namespace Infosys.QuickKart.DAL
{
    public class QuickKartRepository
    {
        private QuickKartDBContext Context { get; set; }

        public QuickKartRepository()
        {
            Context = new QuickKartDBContext();
        }

        //Validate user using LINQ    
        public string ValidateLoginUsingLinq(string emailId, string password)
        {
            string roleName = "";
            try
            {
                var objUser = (from usr in Context.Users
                               where usr.EmailId == emailId && usr.UserPassword == password
                               select usr.Role).FirstOrDefault<Roles>();

                if (objUser != null)
                {
                    roleName = objUser.RoleName;
                }
                else
                {
                    roleName = "Invalid credentials";
                }
            }
            catch (Exception)
            {
                roleName = "Invalid credentials";
            }
            return roleName;
        }

        //Register customer using LINQ
        public bool RegisterUserUsingLinq(Users user)
        {
            bool status;
            try
            {
                var role = (from rol in Context.Roles where rol.RoleName == "Customer" select rol).FirstOrDefault<Roles>();

                if (role != null)
                {
                    user.Role = role;
                }
                else
                {
                    status = false;
                }
                Context.Users.Add(user);
                Context.SaveChanges();
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }

            return status;
        }

        //Get all categories using LINQ
        public List<Categories> GetCategoriesUsingLinq()
        {
            List<Categories> lstCategories = null;
            try
            {
                lstCategories = (from c in Context.Categories
                                 orderby c.CategoryId
                                     ascending
                                 select c).ToList<Categories>();
            }
            catch (Exception)
            {
                lstCategories = null;
            }
            return lstCategories;
        }

        //Display all the products
        public List<Products> DisplayProductDetails()
        {
            List<Products> lstProducts = null;
            try
            {
                lstProducts = (from c in Context.Products
                               orderby c.CategoryId
                                   ascending
                               select c).ToList<Products>();
            }
            catch (Exception ex)
            {
                lstProducts = null;
            }
            return lstProducts;
        }

        //Display purchases of customer
        public List<PurchaseDetails> DisplayPurchaseDetailsByCustomer(string emailId)
        {
            List<PurchaseDetails> lstPurchaseDetails = null;
            try
            {
                lstPurchaseDetails = Context.PurchaseDetails.Include(x => x.Product).Where(x => x.EmailId == emailId).OrderByDescending(x => x.DateOfPurchase).Select(x => x).ToList<PurchaseDetails>();
            }
            catch (Exception ex)
            {
                lstPurchaseDetails = null;
            }
            return lstPurchaseDetails;
        }

        //Display product based upon product name
        public List<Products> DisplayProductDetailsByProductName(string subStr)
        {
            List<Products> lstProducts = null;
            try
            {
                lstProducts = (from c in Context.Products
                               where c.ProductName.ToLower().Contains(subStr.ToLower())
                               orderby c.CategoryId ascending
                               select c).ToList<Products>();
            }
            catch (Exception ex)
            {
                lstProducts = null;
            }
            return lstProducts;
        }

        //Add Products to cart
        public int AddProductToCartUsingUSP(string productId, string emailId)
        {
            System.Nullable<int> returnvalue = -1;
            try
            {
                SqlParameter prmCategoryName = new SqlParameter("@ProductId", productId);
                SqlParameter prmCategoryId = new SqlParameter("@EmailId", emailId);
                var returnval = Context.Database.ExecuteSqlCommand("EXEC dbo.usp_AddProductToCart @ProductId, @EmailId", new[] { prmCategoryName, prmCategoryId });
                returnvalue = Convert.ToInt32(returnval);
            }
            catch (Exception ex)
            {
                returnvalue = -1;
            }
            return Convert.ToInt32(returnvalue);
        }

        public List<CartProducts> FetchCartProductsByEmailId(string emailId)
        {
            List<CartProducts> lstProduct = null;
            try
            {
                SqlParameter prmEmailId = new SqlParameter("@EmailId", emailId);
                lstProduct = Context.CartProducts
                                    .FromSql("SELECT * FROM dbo.ufn_FetchCartProductByEmailId(@EmailId)", prmEmailId)
                                    .ToList();
            }
            catch (Exception ex)
            {
                lstProduct = null;
            }
            return lstProduct;
        }


        public bool UpdateCartProductsLinq(string productId, string emailId, short quantity)
        {
            bool status = false;

            Cart cartproduct = null;
            try
            {

                cartproduct = (from cartProd in Context.Cart where cartProd.ProductId == productId && cartProd.EmailId == emailId select cartProd).FirstOrDefault<Cart>();
                if (cartproduct != null)
                {
                    cartproduct.Quantity = quantity;
                    Context.SaveChanges();
                    status = true;
                }
                else
                {
                    status = false;
                }
            }
            catch (Exception ex)
            {
                status = false;
            }
            return status;
        }

        public bool DeleteCartProduct(string productId, string emailId)
        {
            bool status = false;
            try
            {
                var product = (from cart in Context.Cart
                               where cart.ProductId == productId && cart.EmailId == emailId
                               select cart).FirstOrDefault<Cart>();
                Context.Cart.Remove(product);
                Context.SaveChanges();
                status = true;
            }
            catch (Exception ex)
            {
                status = false;
            }
            return status;
        }

        public List<Rating> DisplayAllReviewDetailsByCustomer(string emailId)
        {
            List<Rating> lstReviewDetails = null;
            try
            {
                lstReviewDetails = Context.Rating.Where(x => x.EmailId == emailId).ToList<Rating>();
            }
            catch (Exception ex)
            {
                lstReviewDetails = null;
            }
            return lstReviewDetails;
        }

        public bool AddRatings(Rating rating)
        {
            bool status = false;
            try
            {
                Rating ratingObj = new Rating();
                ratingObj.EmailId = rating.EmailId;
                ratingObj.ProductId = rating.ProductId;
                ratingObj.ProductName = rating.ProductName;
                ratingObj.ReviewRating = rating.ReviewRating;
                ratingObj.ReviewComments = rating.ReviewComments;

                Context.Rating.Add(ratingObj);
                Context.SaveChanges();
                status = true;
            }
            catch (Exception ex)
            {
                status = false;
            }
            return status;
        }

        public Rating GetProductReviewByCustomer(string emailId, string productId)
        {
            Rating rating;
            try
            {
                rating = Context.Rating.Where(r => r.EmailId == emailId && r.ProductId == productId).Select(r => r).SingleOrDefault();
            }
            catch (Exception ex)
            {
                rating = null;
            }
            return rating;
        }

        public bool UpdateReviewComments(string emailId, string productId, string newComment)
        {
            bool status = false;
            try
            {
                Rating rating = Context.Rating.Where(r => (r.EmailId == emailId && r.ProductId == productId)).Select(r => r).FirstOrDefault<Rating>();
                rating.ReviewComments = newComment;
                Context.SaveChanges();
                status = true;
            }
            catch (Exception ex)
            {
                status = false;
            }
            return status;
        }

        public bool DeleteRating(string emailId, string productId)
        {
            bool status = false;
            try
            {
                Rating rating = Context.Rating.Where(r => (r.EmailId == emailId && r.ProductId == productId)).Select(r => r).FirstOrDefault<Rating>();
                Context.Rating.Remove(rating);
                Context.SaveChanges();
                status = true;
            }
            catch (Exception ex)
            {
                status = false;
            }
            return status;
        }
    }
}
