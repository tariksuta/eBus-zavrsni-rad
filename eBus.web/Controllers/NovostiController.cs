using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eBus.Model.Requests;
using eBus.web.Helper;
using eBus.web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace eBus.web.Controllers
{
    public class NovostiController : Controller
    {
        private APIService _novostiService = new APIService("Novosti");
        private APIService _notifikacijePutnikService = new APIService("PutnikNotifikacije");
        private APIService _putnikService = new APIService("Putnik");
        private APIService _notifikacijeService = new APIService("Notifikacije");
        public async Task<IActionResult> Prikaz()
        {

            NovostiVM model = new NovostiVM();

            model.ListaNovina = new List<Model.Novosti>();

            var resNovost = await _novostiService.Get(null);

            if (resNovost.IsSuccessStatusCode)
            {
                var novosti = JsonConvert.DeserializeObject<List<Model.Novosti>>(resNovost.Content.ReadAsStringAsync().Result);

                foreach (var item in novosti.OrderByDescending(d => d.DatumObjave))
                {
                    item.BrojPregleda = await brojPregleda(item.Id);
                    model.ListaNovina.Add(item);
                }
            }

            return View(model);
        }

        public async Task<int> brojPregleda(int novostId)
        {
            var ukupno = 0;

            var respn = await _notifikacijePutnikService.Get(null);

            if (respn.IsSuccessStatusCode)
            {
                var result = respn.Content.ReadAsStringAsync().Result;
                var notifputn = JsonConvert.DeserializeObject<List<Model.PutnikNotifikacije>>(result);

                foreach (var item in notifputn)
                {
                    if (item.Notifikacija.NovostId == novostId && item.Pregledana == true)
                    {
                        ukupno++;
                    }
                }
            }

            return ukupno;
        }

        public async Task<IActionResult> PrikaziNovost(int novostId)
        {
            var resNovost = await _novostiService.GetById(novostId);

            Model.Novosti novost = null;

            if (resNovost.IsSuccessStatusCode)
            {
                novost = JsonConvert.DeserializeObject<Model.Novosti>(resNovost.Content.ReadAsStringAsync().Result);

                var resNotif = await _notifikacijeService.Get(new NotifikacijeSearchRequest() { NovostId = novostId});

                if (resNotif.IsSuccessStatusCode)
                {
                    var notifikacija = JsonConvert.DeserializeObject<List<Model.Notifikacije>>(resNotif.Content.ReadAsStringAsync().Result);

                    await oznaciProcitano(notifikacija[0].Id);
                }

                
            }

            return View(novost);
        }

        private async Task oznaciProcitano(int notifikacijaId)
        {
            var resPutnik = await _putnikService.Get(new PutnikSearchRequest() { KorisnickoIme = APIService.Username });

            if (resPutnik.IsSuccessStatusCode)
            {

                var putnik = JsonConvert.DeserializeObject<List<Model.Putnik>>(resPutnik.Content.ReadAsStringAsync().Result);

                if (putnik.Count > 0)
                {
                    var search = new PutnikNotifikacijeSearchRequest()
                    {
                        NotifikacijaId = notifikacijaId,
                        PutnikId = putnik[0].Id
                    };
                    var respn = await _notifikacijePutnikService.Get(search);

                    if (respn.IsSuccessStatusCode)
                    {
                        var result = respn.Content.ReadAsStringAsync().Result;
                        var notifputn = JsonConvert.DeserializeObject<List<Model.PutnikNotifikacije>>(result);

                        if(notifputn.Count > 0)
                        {
                            foreach (var item in notifputn)
                            {
                                if (item.Pregledana == false)
                                {
                                    var pnu = new PutnikNotifikacijeUpsertRequest()
                                    {
                                        PutnikId = item.PutnikId,
                                        NotifikacijaId = item.NotifikacijaId,
                                        Pregledana = true
                                    };

                                    await _notifikacijePutnikService.Update(item.Id, pnu);
                                }
                            }
                        }
                        else
                        {
                            var notifputnik = new PutnikNotifikacijeUpsertRequest()
                            {
                                NotifikacijaId = notifikacijaId,
                                Pregledana = true,
                                PutnikId = putnik[0].Id
                            };

                            await _notifikacijePutnikService.Insert(notifputnik);
                        }
                       
                    }
                }

            }

        }
    }
}