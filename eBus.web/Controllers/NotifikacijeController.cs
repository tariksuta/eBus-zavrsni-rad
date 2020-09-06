using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using eBus.Model;
using eBus.Model.Requests;
using eBus.web.Helper;
using eBus.web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace eBus.web.Controllers
{
    public class NotifikacijeController : Controller
    {
        private APIService _notifikacijeService = new APIService("Notifikacije");
        private APIService _notifikacijePutnikService = new APIService("PutnikNotifikacije");
        private APIService _novostiService = new APIService("Novosti");
        private APIService _putnikService = new APIService("Putnik");

        public async Task<IActionResult> PrikaziNotifikacije()
        {
            NotifikacijeVM model = new NotifikacijeVM();

            // model.ListaNOtifikacija = new List<Model.Notifikacije>();

            model.ListaNOtifikacija = new List<NotifikacijeVM.Row>();

            HttpResponseMessage res = await _notifikacijeService.Get(null);

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                var notifikacije = JsonConvert.DeserializeObject<List<Model.Notifikacije>>(result);


                // var lista = new List<Model.Notifikacije>();


                var resPutnik = await _putnikService.Get(new PutnikSearchRequest() { KorisnickoIme = APIService.Username });

                foreach (var notifikacija in notifikacije)
                {

                   

                    if (resPutnik.IsSuccessStatusCode)
                    {
                        var putnik = JsonConvert.DeserializeObject<List<Model.Putnik>>(resPutnik.Content.ReadAsStringAsync().Result);
                    
                        if(putnik.Count > 0) { 
                            var search = new PutnikNotifikacijeSearchRequest()
                            {
                                PutnikId = putnik[0].Id,
                                NotifikacijaId = notifikacija.Id
                            };

                            var resPutnikNotif = await _notifikacijePutnikService.Get(search);

                            if (resPutnikNotif.IsSuccessStatusCode)
                            {
                                var resultpn = resPutnikNotif.Content.ReadAsStringAsync().Result;
                                var notifikacijaputnik = JsonConvert.DeserializeObject<List<Model.PutnikNotifikacije>>(resultpn);

                                //  if (notifikacijaputnik.Count > 0 && notifikacijaputnik[0].Pregledana == false)
                                //  {

                                //lista.Add(notifikacija);

                                if(notifikacijaputnik.Count > 0)
                                {
                                    model.ListaNOtifikacija.Add(new NotifikacijeVM.Row()
                                    {
                                        Id = notifikacija.Id,
                                        DatumSlanja = notifikacija.DatumSlanja,
                                        Naslov = notifikacija.Naslov,
                                        NovostId = notifikacija.NovostId,
                                        Procitano = notifikacijaputnik[0].Pregledana
                                    });
                                }

                               

                               // }
                            }
                        }
                    }
                }

                //model.ListaNOtifikacija = lista;
            }
            return View(model);

        }

      

        private async Task<int> brojPregleda(int novostId)
        {
            var ukupno = 0;

            var respn = await _notifikacijePutnikService.Get(null);

            if (respn.IsSuccessStatusCode)
            {
                var result = respn.Content.ReadAsStringAsync().Result;
                var notifputn = JsonConvert.DeserializeObject<List<PutnikNotifikacije>>(result);

                foreach (var item in notifputn)
                {
                    if(item.Notifikacija.NovostId == novostId && item.Pregledana == true) {
                        ukupno++;
                    }
                }
            }

            return ukupno;
        }

        private async Task oznaciProcitano(int notifikacijaId)
        {
            var resPutnik = await _putnikService.Get(new PutnikSearchRequest() { KorisnickoIme = APIService.Username });

            if (resPutnik.IsSuccessStatusCode)
            {

                var putnik = JsonConvert.DeserializeObject<List<Model.Putnik>>(resPutnik.Content.ReadAsStringAsync().Result);

                if(putnik.Count > 0)
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
                        var notifputn = JsonConvert.DeserializeObject<List<PutnikNotifikacije>>(result);

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
                }
                
            }

           
        }

        public async Task<IActionResult> OznaciNeprocitanim(int notifId)
        {
            var resPutnik = await _putnikService.Get(new PutnikSearchRequest() { KorisnickoIme = APIService.Username });

            if (resPutnik.IsSuccessStatusCode)
            {

                var putnik = JsonConvert.DeserializeObject<List<Model.Putnik>>(resPutnik.Content.ReadAsStringAsync().Result);

                if (putnik.Count > 0)
                {
                    var search = new PutnikNotifikacijeSearchRequest()
                    {
                        NotifikacijaId = notifId,
                        PutnikId = putnik[0].Id
                    };
                    var respn = await _notifikacijePutnikService.Get(search);

                    if (respn.IsSuccessStatusCode)
                    {
                        var result = respn.Content.ReadAsStringAsync().Result;
                        var notifputn = JsonConvert.DeserializeObject<List<PutnikNotifikacije>>(result);

                        foreach (var item in notifputn)
                        {
                            if (item.Pregledana == true)
                            {
                                var pnu = new PutnikNotifikacijeUpsertRequest()
                                {
                                    PutnikId = item.PutnikId,
                                    NotifikacijaId = item.NotifikacijaId,
                                    Pregledana = false
                                };

                                await _notifikacijePutnikService.Update(item.Id, pnu);
                            }
                        }
                    }
                }

              

            }
            return RedirectToAction("PrikaziNotifikacije");
        }

        public async Task<int> BrojNotifikacija() // ovo iskoristiti
        {
            int brojac = 0;

            var resPutnik = await _putnikService.Get(new PutnikSearchRequest() { KorisnickoIme = APIService.Username });

            if (resPutnik.IsSuccessStatusCode)
            {
                var putnik = JsonConvert.DeserializeObject<List<Model.Putnik>>(resPutnik.Content.ReadAsStringAsync().Result);

                if(putnik.Count > 0)
                {
                    var resNotif = await _notifikacijePutnikService.Get(new PutnikNotifikacijeSearchRequest() { PutnikId = putnik[0].Id });

                    if (resNotif.IsSuccessStatusCode)
                    {
                        var notifikacije = JsonConvert.DeserializeObject<List<Model.PutnikNotifikacije>>(resNotif.Content.ReadAsStringAsync().Result);

                        foreach (var item in notifikacije)
                        {
                            if (item.Pregledana == false)
                                brojac += 1;
                        }
                    }
                }
            }

            return brojac;
        }
           
    }

   
}