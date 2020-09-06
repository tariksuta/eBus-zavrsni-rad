using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eBus.Model.Requests;
using eBus.web.Helper;
using eBus.web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace eBus.web.Controllers
{
    public class RezervacijeController : Controller
    {
        private APIService _rezervacijeService = new APIService("Rezervacija");
        private APIService _putnikService = new APIService("Putnik");
       public async Task<IActionResult> Prikaz()
        {
            RezervacijeVM model = new RezervacijeVM();
            model.ListaRezervacija = new List<RezervacijeVM.Row>();

            var resPutnik = await _putnikService.Get(new PutnikSearchRequest() { KorisnickoIme = APIService.Username });
            if (resPutnik.IsSuccessStatusCode)
            {
                var putnik = JsonConvert.DeserializeObject<List<Model.Putnik>>(resPutnik.Content.ReadAsStringAsync().Result);

                if(putnik.Count > 0)
                {
                    var resReze = await _rezervacijeService.Get(new RezervacijaSearchRequest() { PoAngazmanu = false, PutnikId = putnik[0].Id });

                    if (resReze.IsSuccessStatusCode)
                    {
                        var rezervacije = JsonConvert.DeserializeObject<List<Model.Rezervacija>>(resReze.Content.ReadAsStringAsync().Result);

                        foreach (var item in rezervacije)
                        {
                            bool vazeca = true;
                            if (item.Karta.DatumIzdavanja.Date < DateTime.Now.Date)
                                vazeca = false;

                            model.ListaRezervacija.Add(new RezervacijeVM.Row { 
                                Id = item.Id,
                                DatumIsteka = item.DatumIsteka,
                                DatumKreiranja = item.DatumKreiranja,
                                KartaId = item.KartaId,
                                Karta = item.Karta,
                                Otkazana = item.Otkazana,
                                PutnikId = item.PutnikId,
                                Qrcode = item.Qrcode,
                                Vazeca = vazeca
                                
                            });
                        }
                    }
                }
            }


            return View(model);
        }

        public async Task<IActionResult> Detalji(int id)
        {
            var resReze = await _rezervacijeService.GetById(id);

            if (resReze.IsSuccessStatusCode)
            {
                var rezervacija = JsonConvert.DeserializeObject<Model.Rezervacija>(resReze.Content.ReadAsStringAsync().Result);


                return PartialView(rezervacija);
            }

            return RedirectToAction("Prikaz");
        }

        public async Task<IActionResult> Otkazi(int id)
        {

            var resReze = await _rezervacijeService.GetById(id);

            if (resReze.IsSuccessStatusCode)
            {
                var rezervacija = JsonConvert.DeserializeObject<Model.Rezervacija>(resReze.Content.ReadAsStringAsync().Result);

                if(rezervacija != null)
                {
                    var update = new RezervacijaUpsertRequest()
                    {
                        DatumIsteka = rezervacija.DatumIsteka,
                        DatumKreiranja = rezervacija.DatumKreiranja,
                        KartaId = rezervacija.KartaId,
                        Otkazana = true,
                        PutnikId = rezervacija.PutnikId,
                        Qrcode = rezervacija.Qrcode
                    };

                    await _rezervacijeService.Update(rezervacija.Id, update);

                    ViewData["poruka"] = "Uspješno ste otkazali rezervaciju";

                    return RedirectToAction("Prikaz");
                }
            }
            ViewData["error"] = "Greška na serveru";

            return RedirectToAction("Prikaz");
        }
    }
}