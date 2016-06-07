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
    public class AddProductController : Controller
    {
        public ApplicationDbContext DBConnection = new ApplicationDbContext();
        private  ApplicationUser CurrentUser = null;

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

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(ProductViewModel productViewModel)
        {
            string[] validImageTypes = new string[]
            {
                "image/gif",
                "image/jpeg",
                "image/pjpeg",
                "image/png"
            };

            if (productViewModel.ImageUpload == null)
            {
                // tu trzeba zrobić tak że imidż nie jest wymagany. I jeśli użytkownik nie poda swojego obrazka to ustawia sie jakiś domyślny jak na fb czy allegro.
                ModelState.AddModelError("ImageUpload", "Porsze podać ścieżkę do obrazka");
            }
            else if (!validImageTypes.Contains(productViewModel.ImageUpload.ContentType))
            {
                ModelState.AddModelError("ImageUpload", "Dostępny format pliku to: .gif , .jpeg , .pjpeg , .png");
            }

            if (ModelState.IsValid)
            {
                if (productViewModel.ImageUpload != null && productViewModel.ImageUpload.ContentLength > 0)
                {
                    // indiwidualna nazwa dla pliku, zastosowane szyfrowanie
                    var ImageName = string.Join("", MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(CurrentUser.Email + "_" + DateTime.Now)).Select(s => s.ToString("x2")));
                   
                    // nazwa pliku + rozszerzenie
                    string FullImageName = ImageName + Path.GetExtension(productViewModel.ImageUpload.FileName);

                    // sciezka gdzie sa przechowywane zdjecia
                    string CatalogName = "/Content/uploads/products/";

                    string FullImagePath = Path.Combine(Server.MapPath(CatalogName), FullImageName);

                    // zapis pliku do katalogu
                    productViewModel.ImageUpload.SaveAs(FullImagePath);
                    productViewModel.Product.PathImage = CatalogName + FullImageName;
                    productViewModel.Product.UserId = CurrentUser.Id;


                    DBConnection.Product.Add(productViewModel.Product);
                    DBConnection.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }
    }
}