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
    public class KompanijeController : Controller
    {
        private APIService _ocjeneService = new APIService("Ocjena");
        private APIService _kompanijeService = new APIService("Kompanija");
        private APIService _putnikService = new APIService("Putnik");

        public async Task<IActionResult> Prikaz()
        {

            KompanijeVM model = new KompanijeVM();
            
           // var resOcjene = await _ocjeneService.Get(null);

            Dictionary<Model.Kompanija, int> kompanijeOcjene = new Dictionary<Model.Kompanija, int>();

            var lista = new List<Model.Kompanija>();
            List<Model.Kompanija> kompanije = new List<Model.Kompanija>();

            var resKomp = await _kompanijeService.Get(null);

            if (resKomp.IsSuccessStatusCode)
            {
                kompanije = JsonConvert.DeserializeObject<List<Model.Kompanija>>(resKomp.Content.ReadAsStringAsync().Result);

                model.SveKompanije = kompanije;
            }

                //if (resOcjene.IsSuccessStatusCode)
                //{
                //    var ocjene = JsonConvert.DeserializeObject<List<Model.Ocjena>>(resOcjene.Content.ReadAsStringAsync().Result);



                //    var resKomp = await _kompanijeService.Get(null);

                //    if (resKomp.IsSuccessStatusCode)
                //    {
                //         kompanije = JsonConvert.DeserializeObject<List<Model.Kompanija>>(resKomp.Content.ReadAsStringAsync().Result);

                //        model.SveKompanije = kompanije;

                //        foreach (var komp in kompanije)
                //        {
                //            var zbir = 0;
                //            foreach (var ocjena in ocjene)
                //            {
                //                if(ocjena.KompanijaId == komp.Id)
                //                {
                //                    zbir += ocjena.OcjenaUsluge;
                //                }
                //            }

                //            if (!kompanijeOcjene.ContainsKey(komp))
                //            {
                //                kompanijeOcjene.Add(komp, zbir);
                //            }
                //        }

                //        var resPutnici = await _putnikService.Get(null);

                //        if (resPutnici.IsSuccessStatusCode)
                //        {
                //            var putnici = JsonConvert.DeserializeObject<List<Model.Putnik>>(resPutnici.Content.ReadAsStringAsync().Result);

                //            var najveca = 1;

                //            foreach (var item2 in kompanije)
                //            {
                //                var rez = 0;
                //                foreach (var item in kompanijeOcjene.ToList())
                //                {

                //                    if(item2.Id == item.Key.Id)
                //                        rez += item.Value;


                //                }

                //                rez = rez / putnici.Count;
                //                if (rez > najveca)
                //                {
                //                    najveca = rez;
                //                    lista.Insert(0, item2);
                //                }
                //                else
                //                {
                //                    lista.Add(item2);
                //                }
                //            }

                //        }

                //    }

                //}

                //if(lista.Count == 0)
                //{
                //    model.ListaKomanija = kompanije;
                //    return View(model);
                //}

                //model.ListaKomanija = lista;
                return View(model);
        }

        public async Task<IActionResult> PrikaziRangListu()
        {
            var resOcjene = await _ocjeneService.Get(null);
            var lista = new List<Model.Kompanija>();

            var model = new KompanijeVM();
            model.ListaKomanija = new List<Model.Kompanija>();

            Dictionary<Model.Kompanija, int> kompanijeOcjene = new Dictionary<Model.Kompanija, int>();

            if (resOcjene.IsSuccessStatusCode)
            {
                var ocjene = JsonConvert.DeserializeObject<List<Model.Ocjena>>(resOcjene.Content.ReadAsStringAsync().Result);
                var resKompanije = await _kompanijeService.Get(null);
                if (resKompanije.IsSuccessStatusCode)
                {
                    var kompanije = JsonConvert.DeserializeObject<List<Model.Kompanija>>(resKompanije.Content.ReadAsStringAsync().Result);

                    foreach (var komp in kompanije)
                    {
                        var zbir = 0;
                        foreach (var ocjena in ocjene)
                        {
                            if (ocjena.KompanijaId == komp.Id)
                            {
                                zbir += ocjena.OcjenaUsluge;
                            }
                        }

                        if (!kompanijeOcjene.ContainsKey(komp))
                        {
                            kompanijeOcjene.Add(komp, zbir);
                        }
                    }

                    var resPutnici = await _putnikService.Get(null);

                    if (resPutnici.IsSuccessStatusCode)
                    {
                        var putnici = JsonConvert.DeserializeObject<List<Model.Putnik>>(resPutnici.Content.ReadAsStringAsync().Result);

                        var najveca = 1;

                        foreach (var item2 in kompanije)
                        {
                            var rez = 0;
                            foreach (var item in kompanijeOcjene.ToList())
                            {

                                if (item2.Id == item.Key.Id)
                                    rez += item.Value;


                            }

                            rez = rez / putnici.Count;
                            if (rez > najveca)
                            {
                                najveca = rez;
                                lista.Insert(0, item2);
                            }
                            else
                            {
                                lista.Add(item2);
                            }
                        }

                    }

                }
            }

            model.ListaKomanija = lista;

            return PartialView(model);
            
        }

        public async Task<IActionResult> Ocjeni(int id, int ocjena)
        {

            var resPutnik = await _putnikService.Get(new PutnikSearchRequest() { KorisnickoIme = APIService.Username });

            if (resPutnik.IsSuccessStatusCode)
            {
                var putnik = JsonConvert.DeserializeObject<List<Model.Putnik>>(resPutnik.Content.ReadAsStringAsync().Result);

                if(putnik.Count > 0)
                {

                    if(await ProvjeriOcjenu(id, putnik[0].Id))
                    {
                        var newOcjena = new OcjenaUpsertRequest()
                        {
                            Komentar = "",
                            OcjenaUsluge = ocjena,
                            KompanijaId = id,
                            PutnikId = putnik[0].Id
                        };

                        TempData["poruka"] = $"Uspješno ste ocjenili kompaniju";
                        await _ocjeneService.Insert(newOcjena);

                        return RedirectToAction("Prikaz");
                    }
                    else
                    {
                        TempData["poruka"] = $"Već ste ocjenili ovu kompaniju";

                        return RedirectToAction("Prikaz");
                    }
                    
                }
            }

            return RedirectToAction("Prikaz");
        } 

        public async Task<bool> ProvjeriOcjenu(int kompId, int putId)
        {
            var resOcjene = await _ocjeneService.Get(new OcjenaSearchRequest() { KompanijaId = kompId });

            if (resOcjene.IsSuccessStatusCode)
            {
                var lista = JsonConvert.DeserializeObject<List<Model.Ocjena>>(resOcjene.Content.ReadAsStringAsync().Result);

                foreach (var item in lista)
                {
                    if (item.PutnikId == putId)
                        return false;
                }
            }

            return true;
        }
    }
}