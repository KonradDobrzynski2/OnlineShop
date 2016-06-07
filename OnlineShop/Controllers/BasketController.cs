using OnlineShop.DataAccessLayer;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class BasketController : Controller
    {
        public ApplicationDbContext DBConnection = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View(Singletons.Singleton_ProductInBasket.Instance.ProductInBasket);
        }

        public ActionResult DeleteInBasket(int id)
        {
            Singletons.Singleton_ProductInBasket.Instance.ProductInBasket.RemoveAll(x => x.Id == id);
            ViewBag.Message = "Produkt został usunięty z koszyka";

            return View("Index",Singletons.Singleton_ProductInBasket.Instance.ProductInBasket);
        }
    }
}