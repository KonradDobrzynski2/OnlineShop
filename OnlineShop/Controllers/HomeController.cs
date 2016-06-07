using OnlineShop.DataAccessLayer;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class HomeController : Controller
    {
        public ApplicationDbContext DBConnection = new ApplicationDbContext();
        private ApplicationUser CurrentUser = null;

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            ViewBag.EnterToken = 0;

            if (requestContext.HttpContext.User.Identity.IsAuthenticated)
            {
                ApplicationUser User = DBConnection.Users.SingleOrDefault(x => x.UserName == requestContext.HttpContext.User.Identity.Name);

                if (User != null)
                {
                    CurrentUser = User; // Przypisanie usera
                }
            }
            else
            {
                //User niezalogowany
            }

            base.Initialize(requestContext);
        }
      
        [HttpGet]
        public ActionResult Index()
        {
            return View(DBConnection.Product.ToList());
        }

        public ActionResult AddToBasket(int id)
        {
            if (CurrentUser != null)
            {
                Singletons.Singleton_ProductInBasket.Instance.ProductInBasket.Add(DBConnection.Product.Find(id));

                ViewBag.Message = "Produkt został dodany do koszyka";
            }
            else
            {
                ViewBag.Message = "Zaloguj się aby dokonać zakupu. Jeśli nie masz jeszcze konta wejdz do sekcji \"Rejestracja\"";
            }

            return View("Index", DBConnection.Product.ToList());

            //Product product = DBConnection.ProductTable.Find(id);//usuwanie z bazy
            //DBConnection.ProductTable.Remove(product);
            //DBConnection.SaveChanges();

            //return RedirectToAction("Index");
        }   
    }
}