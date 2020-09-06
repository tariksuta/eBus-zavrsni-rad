using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eBus.web.ViewModels
{
    public class LicniPodaciUrediVM
    {

        public int? Id { get; set; }
        [Required]
        public string Ime { get; set; }
        [Required]
        public string Prezime { get; set; }
        [Required]
        public string KorisnickoIme { get; set; }

        [Required(ErrorMessage = "Slika je potrebna")]
        public byte[] Slika { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public DateTime? DatumRegistracije { get; set; }
        public DateTime? DatumRodjenja { get; set; }

        [Required]
        public string Lozinka { get; set; }

        [Required]
        public string PotvrdiLozinku { get; set; }

        public IFormFile SlikaZaDodat { get; set; }
       

        public string StaraSlika { get; set; }
    }
}
