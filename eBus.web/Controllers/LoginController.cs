using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using eBus.Model.Requests;
using eBus.web.Helper;
using eBus.web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace eBus.web.Controllers
{
    public class LoginController : Controller
    {
        private APIService _service = new APIService("Uloga");
        private APIService _putnikService = new APIService("Putnik");
        public IActionResult PrijaviSe()
        {
            return View(new LoginVM());
        }

        public async Task<IActionResult> Prijava(LoginVM input)
        {
          

            APIService.Username = input.KorisnickoIme;
            APIService.Password = input.Lozinka;

            try
            {
                var resUloge = await _service.Get(null);

                if (!resUloge.IsSuccessStatusCode)
                {
                    APIService.Username = null;
                    APIService.Password = null;
                    ViewData["error_poruka2"] = "Pogrešno korisničko ime ili lozinka! ";
                    return View("PrijaviSe");
                }

                var resPutnik = await _putnikService.Get(new PutnikSearchRequest() { KorisnickoIme = APIService.Username });
                if (resPutnik.IsSuccessStatusCode)
                {
                    var result = resPutnik.Content.ReadAsStringAsync().Result;
                    var putnik = JsonConvert.DeserializeObject<List<Model.Putnik>>(result);

                    if(putnik.Count > 0)
                    {
                        return RedirectToAction("PrikaziNotifikacije", "Notifikacije");
                    }
                }
            }
            catch (Exception)
            {

                APIService.Username = null;
                APIService.Password = null;
                TempData["error_poruka1"] = "Greška na serveru, pokušajte kasnije!";
                return View("PrijaviSe", input);//View,Parametar
            }

            APIService.Username = null;
            APIService.Password = null;
            ViewData["error_poruka2"] = "Pogrešno korisničko ime ili lozinka! ";
            return View("PrijaviSe");//View,Parametar
        }

        public IActionResult OdjaviSe()
        {
            APIService.Username = null;
            APIService.Password = null;

            return RedirectToAction("Index", "Home");
        }

        public  IActionResult RegistrujSe()
        {
            return View();
        }

        public async Task<IActionResult> Registracija(RegistracijaVM podaci)
        {

            if (!ModelState.IsValid)
            {
                return View("RegistrujSe", podaci);
            }

            if(podaci.Slika != null)
            {
                ViewData["error_poruka"] = "Molimo da unesete i sliku!";

                return View("RegistrujSe", podaci);
            }

            if(podaci.Slika != null && podaci.Slika.Length == 0)
            {
                ViewData["error_poruka"] = "Molimo da unesete i sliku!";

                return View("RegistrujSe", podaci);
            }
            
            if(podaci.Lozinka != podaci.PotvrdiLozinku)
            {
                ViewData["error_poruka"] = "Lozinke se ne podudaraju";

                return View("RegistrujSe", podaci);
            }



            try
            {
                var niz = await FormFileExtensions.GetBytes(podaci.Slika);

                //DODATI I KORISNICKO IME NA FORMU

                var newPuntik = new PutnikUpsertRequest()
                {

                    KorisnickoIme = podaci.KorisnickoIme,
                    Ime = podaci.Ime,
                    Prezime = podaci.Prezime,
                    DatumRegistracije = DateTime.Now,
                    DatumRodjenja = podaci.DatumRodjenja,
                    Email = podaci.Email,
                    Lozinka = podaci.Lozinka,
                    PotvrdiLozinku = podaci.PotvrdiLozinku,
                    Slika = niz
                };

                await _putnikService.Insert(newPuntik);

                APIService.Username = newPuntik.KorisnickoIme;
                APIService.Password = newPuntik.Lozinka;

                return RedirectToAction("PrikaziNotifikacije", "Notifikacije");
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
          
        }

       


    }

    
}