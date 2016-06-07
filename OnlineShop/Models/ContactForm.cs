using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.Models
{
    public class ContactForm
    {
        [Key DisplayName("Id")]
        public int Id { get; set; }

        [DisplayName("E-mail"), Required(ErrorMessage = "Proszę podać swój adress")]
        public string Email { get; set; }

        [DisplayName("Temat"), Required(ErrorMessage = "Proszę podać temat")]
        public string Topic { get; set; }

        [DisplayName("Wiadomość"), Required(ErrorMessage = "Proszę podać treść wiadomości")]
        public string Message { get; set; }
    }
}