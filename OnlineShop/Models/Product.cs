using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.Models
{
    public class Product
    {
        [Key DisplayName("Id produktu")]
        public int Id { get; set; }


        [DisplayName("Nazwa"), Required(ErrorMessage = "Proszę podać nazwe produktu")]
        public string Name { get; set; }


        [DisplayName("Opis"), Required(ErrorMessage = "Proszę podać opis produktu")]
        public string Description { get; set; }


        [DisplayName("Cena"), Required(ErrorMessage = "Proszę podać cenę produktu")]
        public float Price { get; set; }


        [DisplayName("Ścieżka do pliku")]
        public string PathImage { get; set; }

        public ICollection<ApplicationUser> Users { get; set; } //Koszyk

        public virtual ApplicationUser User { get; set; } // definicja relacji
        public string UserId { get; set; } // wiązanie do tabeli ApplicationUser
    }
}