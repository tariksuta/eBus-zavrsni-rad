using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eBus.web.ViewModels
{
    public class LoginVM
    {

        [Required(ErrorMessage = "Obavezno polje")]
        public string KorisnickoIme { get; set; }
        [Required(ErrorMessage = "Obavezno polje")]
        public string Lozinka { get; set; }
    }
}
