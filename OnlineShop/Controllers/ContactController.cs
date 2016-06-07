using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class ContactController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult SendMessage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendMessage(ContactForm contactForm) // zrobic jeszcze wiadomosc na górze storny ze wiadomosc zostala wyslana
        {
            if (!(new EmailAddressAttribute().IsValid(contactForm.Email)))
            {
                ViewBag.Title = "Podany adres e-mial jest nieporawidłowy";
                return View("Index");
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Title = "Wypełnij wszystkie pola i spróbuj ponownie";
                return View("Index");
            }

            string RecipientAddress = contactForm.Email;
            string Topic = contactForm.Topic;

            string SenderAddress = "CarOnlineShopPortal@gmail.com";  
            string SenderPassword = "KonradDobrzynski";


            string Message = "Od: " + contactForm.Email + "\n\n";
            Message += "Temat: " + contactForm.Topic + "\n\n";
            Message += "Wiadomość: " + contactForm.Message + "\n\n";

            // smtp settings
            var smtp = new System.Net.Mail.SmtpClient();
            {
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(SenderAddress, SenderPassword);
                smtp.Timeout = 20000;
            }

            smtp.Send(RecipientAddress, SenderAddress, Topic, Message);

            return RedirectToAction("Index");
        }
    }
}