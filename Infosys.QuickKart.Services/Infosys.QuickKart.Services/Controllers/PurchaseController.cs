using Infosys.QuickKart.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Infosys.QuickKart.Services.Models;


namespace Infosys.QuickKart.Services.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PurchaseController : Controller
    {

        [HttpGet]
        public JsonResult GetPurchaseDetailsByEmailId(string emailId)
        {
            try
            {
                var dal = new QuickKartRepository();
                var purchaseList = dal.DisplayPurchaseDetailsByCustomer(emailId);

                var purchases = new List<PurchaseDetails>();
                if (purchaseList.Any())
                {
                    foreach (var purchase in purchaseList)
                    {
                        var purchaseObj = new PurchaseDetails();
                        purchaseObj.PurchaseId = purchase.PurchaseId;
                        purchaseObj.EmailId = purchase.EmailId;
                        purchaseObj.ProductId = purchase.ProductId;
                        purchaseObj.ProductName = purchase.Product.ProductName;
                        purchaseObj.QuantityPurchased = purchase.QuantityPurchased;
                        purchaseObj.PurchaseDate = purchase.DateOfPurchase.ToShortDateString();
                        purchases.Add(purchaseObj);
                    }
                }
                return Json(purchases);
            }
            catch (Exception ex)
            {
                return Json(null);
            }

        }

    }
}
