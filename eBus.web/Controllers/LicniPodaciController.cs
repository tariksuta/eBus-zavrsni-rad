using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eBus.web.Helper;
using Microsoft.AspNetCore.Mvc;

using eBus.Model.Requests;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using eBus.web.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace eBus.web.Controllers
{
    // [Autorizacija(putnik: true)]
    public class LicniPodaciController : Controller
    {
        private APIService _putnikService = new APIService("Putnik");

      
        public IActionResult Index()
        {
            return View();
        }

      

        public async Task<IActionResult> Prikaz()
        {

            Model.Putnik putnik = null;

            var search = new PutnikSearchRequest()
            {
                KorisnickoIme = APIService.Username
            };

            HttpResponseMessage resKupac = await _putnikService.Get(search);

            if (resKupac.IsSuccessStatusCode)
            {
                var result = resKupac.Content.ReadAsStringAsync().Result;
                var putnici = JsonConvert.DeserializeObject<List<Model.Putnik>>(result);

                if (putnici.Count == 1)
                {
                    putnik = putnici[0];
                }
            }

            return View(putnik);
        }

        public async Task<IActionResult> Uredi()
        {
            LicniPodaciUrediVM putnik = new LicniPodaciUrediVM();
          /*  Model.Requests.PutnikUpsertRequest putnik = new PutnikUpsertRequest()*/;
            var resPutnik = await _putnikService.Get(new PutnikSearchRequest() { KorisnickoIme = APIService.Username });
            if (resPutnik.IsSuccessStatusCode)
            {
                var putnik1 = JsonConvert.DeserializeObject<List<Model.Putnik>>(resPutnik.Content.ReadAsStringAsync().Result);

                putnik.DatumRegistracije = putnik1[0].DatumRegistracije;
                putnik.DatumRodjenja = putnik1[0].DatumRodjenja;
                putnik.Email = putnik1[0].Email;
                putnik.Ime = putnik1[0].Ime;
                putnik.Prezime = putnik1[0].Prezime;
                putnik.Slika = putnik1[0].Slika;
                putnik.KorisnickoIme = putnik1[0].KorisnickoIme;
                putnik.Id = putnik1[0].Id;
                putnik.StaraSlika = Convert.ToBase64String(putnik1[0].Slika);
               

            }
            return View(putnik);
        }

        public async Task<IActionResult> Snimi(LicniPodaciUrediVM podaci)
        {

            try
            {
                if (podaci.Lozinka == null || podaci.PotvrdiLozinku == null)
                {
                    ViewData["error_poruka"] = "Potvrdite izmjene unosom vaše lozinke";
                    podaci.Slika = Convert.FromBase64String(podaci.StaraSlika);
                    return View("Uredi", podaci);

                }

                if (podaci.Lozinka != podaci.PotvrdiLozinku)
                {
                    ViewData["error_poruka"] = "Lozinke se ne podudaraju";
                    podaci.Slika = Convert.FromBase64String(podaci.StaraSlika);

                    return View("Uredi", podaci);
                }



                if (!Regex.IsMatch(podaci.KorisnickoIme, @"^[a-z]+$"))
                {
                    ViewData["error_poruka"] = "Korisničko ime mailm slovima!";
                    podaci.Slika = Convert.FromBase64String(podaci.StaraSlika);

                    return View("Uredi", podaci);
                }

                if (await provjeriKorisnickoIme(podaci.KorisnickoIme, podaci.Id.Value))
                {
                    ViewData["error_poruka"] = "Ovo korisničko ime nije dozvoljeno";
                    podaci.Slika = Convert.FromBase64String(podaci.StaraSlika);

                    return View("Uredi", podaci);
                }
                var update = new PutnikUpsertRequest();


                if (podaci.SlikaZaDodat == null)
                {
                    var data = Convert.FromBase64String(podaci.StaraSlika);

                    update.Slika = data;
                }
                else
                {

                    update.Slika = await FormFileExtensions.GetBytes(podaci.SlikaZaDodat);
                }

                update.Ime = podaci.Ime;
                update.Prezime = podaci.Prezime;
                update.KorisnickoIme = podaci.KorisnickoIme;
                update.DatumRegistracije = podaci.DatumRegistracije;
                update.DatumRodjenja = podaci.DatumRodjenja;
                update.Lozinka = podaci.Lozinka;
                update.PotvrdiLozinku = podaci.PotvrdiLozinku;
                update.Id = podaci.Id.Value;
                update.Email = podaci.Email;


                await _putnikService.Update(update.Id, update);



                return RedirectToAction("Prikaz");
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

          
          
        }

        public async Task<bool> provjeriKorisnickoIme(string kime, int id) // ovo mogu iskoristiti za unit test
        {
            var resPutnik = await _putnikService.Get(null);

            if (resPutnik.IsSuccessStatusCode)
            {
                var putnici = JsonConvert.DeserializeObject<List<Model.Putnik>>(resPutnik.Content.ReadAsStringAsync().Result);

                foreach (var item in putnici)
                {
                    if (item.KorisnickoIme == kime && item.Id != id)
                        return true;
                }
            }

            return false;
        }

        
    }
}