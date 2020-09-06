using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eBus.web.ViewModels
{
    public class RegistracijaVM
    {


        [Required(ErrorMessage = "Ovo polje je obavezno")]
        public string Ime { get; set; }
        [Required(ErrorMessage = "Ovo polje je obavezno")]
        public string Prezime { get; set; }
        [Required(ErrorMessage ="Ovo polje je obavezno")]
        
        public string KorisnickoIme { get; set; }

        [Required(ErrorMessage = "Slika je potrebna")]
        public IFormFile Slika { get; set; }
        [EmailAddress]
        [RegularExpression(@"^[a-zA-Z.]+@[a-z]+(?:.[a-z]+).[a-z]+$", ErrorMessage = "example@mail.com")]
        public string Email { get; set; }
     
        public DateTime? DatumRodjenja { get; set; }

        [Required(ErrorMessage = "Ovo polje je obavezno")]
        public string Lozinka { get; set; }

        [Required(ErrorMessage = "Ovo polje je obavezno")]
        public string PotvrdiLozinku { get; set; }
    }
}
