using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eBus.Model;
using eBus.Model.Requests;
using eBus.web.Helper;
using eBus.web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace eBus.web.Controllers
{
    public class LinijeController : Controller
    {
        private APIService _linijeService = new APIService("Linija");
        private APIService _gradoviService = new APIService("Grad");
        private APIService _kartaService = new APIService("Karta");
        private APIService _cijenaService = new APIService("Cijena");
        private APIService _sjedistaService = new APIService("Sjediste");
        private APIService _rezervacijeService  = new APIService("Rezervacija");
        private APIService _putnikService  = new APIService("Putnik");
        private APIService _preporukaService  = new APIService("Preporuka");

        public async Task<IActionResult> PretraziLinije(string polaziste, string odrediste, DateTime datum) //  LinijeVM podaci
        {
          
            

            var privremenaLinija = new List<LinijaPodaci>();

            var model = new LinijeVM();
            model.PrikaziLinije = new List<LinijaPodaci>();
            model.ListaGradova = new List<Grad>();
            model.DatumPretrage = datum;  //podaci.DatumPretrage;

            var searchLinije = new LinijaSearchRequest()
            {
                PoNazivu = true,
                NazivOdredista = odrediste, //podaci.Odrediste,
                NazivPolazista = polaziste //podaci.Polaziste
            };

            var resLinije = await _linijeService.Get(searchLinije);

            if (resLinije.IsSuccessStatusCode)
            {
                var result = resLinije.Content.ReadAsStringAsync().Result;
                var linije = JsonConvert.DeserializeObject<List<Model.Linija>>(result);

                var searchKarta = new KartaSearchRequest()
                {
                    PoAngzuje = false,
                    PoVozilu = false,
                    PoVremenu = false,
                    DatumIzdavanja = datum //podaci.DatumPretrage
                };

                var resKarta = await _kartaService.Get(searchKarta);

                if (resKarta.IsSuccessStatusCode)
                {

                    var resultK = resKarta.Content.ReadAsStringAsync().Result;
                      var karte = JsonConvert.DeserializeObject<List<Model.Karta>>(resultK);

                    //var karte = await vratiKartePoDatumu(datum);

                    //List<Model.Karta> karte = await vratiKartePoDatumu(podaci.DatumPretrage);

                    Dictionary<TimeSpan, int> linijeZaPrikazat = new Dictionary<TimeSpan, int>();


                  
                    foreach (var item in linije)
                    {

                        foreach (var karta in karte)
                        {
                            if(item.Id == karta.Angazuje.LinijaId) {

                                if (!linijeZaPrikazat.ContainsKey(karta.VrijemePolaska))
                                {

                                    var searchCijena = new CijenaSearchRequest()
                                    {
                                        KompanijaID = karta.Sjediste.Vozilo.KompanijaId,
                                        LinijaID = item.Id
                                    };

                                    var resCijena = await _cijenaService.Get(searchCijena);

                                    if (resCijena.IsSuccessStatusCode)
                                    {
                                        var resultC = resCijena.Content.ReadAsStringAsync().Result;
                                        var cijena = JsonConvert.DeserializeObject<List<Model.Cijena>>(resultC);

                                        var linijaPodaci = new LinijaPodaci()
                                        {
                                            AngazujeID = karta.AngazujeId,
                                            Cijena = cijena[0].Iznos,
                                            DatumPretrage =datum, //podaci.DatumPretrage,
                                            VoziloID = karta.Sjediste.VoziloId.Value,
                                            Kompanija = karta.Sjediste.Vozilo.Kompanija.Naziv,
                                            OdredisteNaziv =odrediste, //podaci.Odrediste,
                                            PolazisteNaziv = polaziste, //podaci.Polaziste,
                                            VrijemePolaska = karta.VrijemePolaska,
                                            kartaID = karta.Id
                                        };

                                        model.PrikaziLinije.Add(linijaPodaci);
                                    }

                                    linijeZaPrikazat.Add(karta.VrijemePolaska, karta.AngazujeId);
                                   
                                }

                               
                            
                            }

                            
                        }

                    }

                   
                }

               
            }

           // return View( model);

            return PartialView(model);
        }


        /* OVO NE KORISTIM */
        private async Task<List<Model.Karta>> vratiKartePoDatumu(DateTime datum)
        {
            var listaKarata = new List<Model.Karta>();

            var resKarte = await _kartaService.Get(null);

            if (resKarte.IsSuccessStatusCode)
            {
                var result = resKarte.Content.ReadAsStringAsync().Result;
                var sveKarte = JsonConvert.DeserializeObject<List<Model.Karta>>(result);

                foreach (var item in sveKarte)
                {
                    if(item.DatumIzdavanja.Date == datum.Date)
                    {
                        listaKarata.Add(item);
                    }
                }
            }

            return listaKarata;

        }

        /*------------------*/

        public async  Task<IActionResult> Prikaz()
        {

            var model = new LinijeVM();

            model.PrikaziLinije = await Preporuka();

            model.ListaGradova = new List<Model.Grad>();
            var resGrad = await _gradoviService.Get(null);

            if (resGrad.IsSuccessStatusCode)
            {
                var result = resGrad.Content.ReadAsStringAsync().Result;
                var gradovi = JsonConvert.DeserializeObject<List<Model.Grad>>(result);

                foreach (var item in gradovi)
                {
                    model.ListaGradova.Add(item);
                }
            }
            return   View(model);
        }

        public async Task<List<Model.Grad>> vratiGradove()
        {
            var lista = new List<Model.Grad>();
            var resGrad = await _gradoviService.Get(null);

            if (resGrad.IsSuccessStatusCode)
            {
                var result = resGrad.Content.ReadAsStringAsync().Result;
                var gradovi = JsonConvert.DeserializeObject<List<Model.Grad>>(result);

                foreach (var item in gradovi)
                {
                    lista.Add(item);
                }
            }

            return lista;
        }

        public async  Task<IActionResult> Detalji(int id)
        {
            LinijeDetaljiVM model = new LinijeDetaljiVM();

            var resKarta = await _kartaService.GetById(id);

            if (resKarta.IsSuccessStatusCode)
            {
                var result = resKarta.Content.ReadAsStringAsync().Result;
                var karta = JsonConvert.DeserializeObject<Model.Karta>(result);

                var resLinija = await _linijeService.GetById(karta.Angazuje.LinijaId);

                if (resLinija.IsSuccessStatusCode)
                {
                    var resultL = resLinija.Content.ReadAsStringAsync().Result;
                    var linija = JsonConvert.DeserializeObject<Model.Linija>(resultL);
             


                    var searchCijena = new CijenaSearchRequest()
                    {
                        KompanijaID = karta.Sjediste.Vozilo.KompanijaId,
                        LinijaID = karta.Angazuje.LinijaId
                    };

                    var resCijena = await _cijenaService.Get(searchCijena);

                    if (resCijena.IsSuccessStatusCode)
                    {

                        var resultC = resCijena.Content.ReadAsStringAsync().Result;
                        var cijena = JsonConvert.DeserializeObject<List<Model.Cijena>>(resultC);

                        model.Cijena = cijena[0].Iznos;
                        model.DatumPretrage = karta.DatumIzdavanja;
                        model.VrijemeDolaska = karta.VrijemeDolaska;
                        model.VrijemePolaska = karta.VrijemePolaska;
                        model.Vozilo = karta.Sjediste.Vozilo.Model;
                        model.voziloId = karta.Sjediste.VoziloId.Value;
                        model.Kompanija = karta.Sjediste.Vozilo.Kompanija.Naziv;
                        model.PolazisteNaziv = linija == null ? "Nema polaziste" : linija.Polaziste.Naziv;
                        model.OdredisteNaziv = linija == null ? "Nema odrediste" : linija.Odrediste.Naziv;
                      

                        
                    }
                }
            }

            return View(model);

        }

        public async Task<IActionResult> PrikaziSjedista(int id, DateTime datum,TimeSpan vrijeme)
        {
            var model = new SjedistaVM();
            model.Datum = datum;
            model.Vrijeme = vrijeme;
            model.listaSjedista = new List<SjedistaVM.Row>();
            var search = new SjedisteSearchRequest()
            {
                VoziloId = id
            };

            var resSjedista = await _sjedistaService.Get(search);

            if (resSjedista.IsSuccessStatusCode)
            {
                var result = resSjedista.Content.ReadAsStringAsync().Result;
                var sjedista = JsonConvert.DeserializeObject<List<Model.Sjediste>>(result);

                var resRezervacija = await _rezervacijeService.Get(null);

                if (resRezervacija.IsSuccessStatusCode)
                {
                    

                    var resultR = resRezervacija.Content.ReadAsStringAsync().Result;
                    var rezervacije = JsonConvert.DeserializeObject<List<Model.Rezervacija>>(resultR);

                    foreach (var item in sjedista)
                    {
                        model.listaSjedista.Add(new SjedistaVM.Row
                        {
                            Id = item.Id,
                            Red = item.Red,
                            Kolona = item.Kolona,
                            Rezervisano = false,
                            Lokacija = item.Red+" "+item.Kolona
                          


                        });
                    }

                    foreach (SjedistaVM.Row sjediste in model.listaSjedista)
                    {
                        foreach (var rezervacija in rezervacije)
                        {
                            if (rezervacija.Karta.SjedisteId == sjediste.Id && rezervacija.Karta.DatumIzdavanja.Date == datum.Date)
                            {

                                if (rezervacija.Otkazana == false)
                                {
                                   

                                    sjediste.Rezervisano = true;
                                }
                               

                            }

                        }
                    }

                   
                    
                }

            }
            return View(model); 
        }

        public async Task<IActionResult> Rezervisi(int sjedisteId, DateTime datum, TimeSpan vrijeme)
        {
            Model.Karta karta = null;
            Model.Sjediste s = null;

            Model.Karta kreiranaNovaKarta = null;
            var resSjediste =await  _sjedistaService.GetById(sjedisteId);

            if (resSjediste.IsSuccessStatusCode)
            {
                var resultS = resSjediste.Content.ReadAsStringAsync().Result;
                 s = JsonConvert.DeserializeObject<Model.Sjediste>(resultS);

                var searchKarta = new KartaSearchRequest()
                {
                    PoAngzuje = false,
                    PoVozilu = true,
                    Red = s.Red,
                    Kolona = s.Kolona,
                    PoDatumu = true,
                    DatumIzdavanja = datum,
                    IzSjedista = true,
                    PoVremenu = true,
                    VrijemePolaska = vrijeme
                };

                var resKarta = await _kartaService.Get(searchKarta);

                if (resKarta.IsSuccessStatusCode)
                {
                    var resultK = resKarta.Content.ReadAsStringAsync().Result;
                    var karte = JsonConvert.DeserializeObject<List<Model.Karta>>(resultK);

                    
                    if(karte.Count > 0)
                        karta = karte[0];

                    var reze = new RezervacijaUpsertRequest();

                    

                    if (karta == null)
                    {
                       
                        /* OVO SAM ZADNJE DODAO -----------------------------------------*/
                        var searchKarta2 = new KartaSearchRequest()
                        {
                            PoAngzuje = false,
                            PoVozilu = false,
                            DatumIzdavanja = datum,
                            VrijemePolaska = vrijeme
                        };

                        var karteRes = await _kartaService.Get(searchKarta2);

                        if (karteRes.IsSuccessStatusCode)
                        {
                            var lista = JsonConvert.DeserializeObject<List<Model.Karta>>(karteRes.Content.ReadAsStringAsync().Result);

                            if(lista.Count > 0)
                            {

                                if(datum.Date > DateTime.Now.Date)
                                {
                                    var upsert = new KartaUpsertRequest()
                                    {
                                        AngazujeId = lista[0].AngazujeId,
                                        DatumIzdavanja = datum,
                                        VrijemePolaska = vrijeme,
                                        VrijemeDolaska = lista[0].VrijemeDolaska,
                                        BrojKarte = GenerisiBrojKarte(9, true),
                                        SjedisteId = sjedisteId

                                    };

                                    var nova = await _kartaService.Insert(upsert);



                                    if (nova.IsSuccessStatusCode)
                                    {
                                        kreiranaNovaKarta = JsonConvert.DeserializeObject<Model.Karta>(nova.Content.ReadAsStringAsync().Result);

                                        if(await NovaKartaIRezervacija(kreiranaNovaKarta, s))
                                        {
                                            TempData["poruka"] = "Uspješno ste rezervisali sjedište";
                                            return RedirectToAction("PrikaziSjedista", new { id = s.VoziloId, datum = datum, vrijeme = vrijeme });
                                        }
                                    }
                                    else
                                    {
                                        TempData["poruka"] = "Trenutno karta nije kreirana";
                                        return RedirectToAction("PrikaziSjedista", new { id = s.VoziloId, datum = datum, vrijeme = vrijeme });
                                    }
                                }
                                else
                                {
                                    TempData["poruka"] = "Niste u mogućnosti da rezervišete";
                                    return RedirectToAction("PrikaziSjedista", new { id = s.VoziloId, datum = datum, vrijeme = vrijeme });
                                }

                               


                            }
                            else
                            {
                                TempData["poruka"] = "Trenutno karta nije kreirana";
                                return RedirectToAction("PrikaziSjedista", new { id = s.VoziloId, datum = datum, vrijeme = vrijeme });
                            }
                        }
                        else
                        {
                            TempData["poruka"] = "Trenutno karta nije kreirana";
                            return RedirectToAction("PrikaziSjedista", new { id = s.VoziloId, datum = datum, vrijeme = vrijeme });
                        }

                      /*-----------------------------------------------------------------------*/

                    }
                    else if (karta.DatumIzdavanja.Date < DateTime.Now.Date)
                    {
                        TempData["poruka"] = "Niste u mogučnosti rezervisati";

                        
                        return RedirectToAction("PrikaziSjedista", new { id = s.VoziloId, datum = datum, vrijeme = vrijeme });

                    }
                    else if (karta.DatumIzdavanja.Date == DateTime.Now.Date && karta.VrijemePolaska < DateTime.Now.TimeOfDay)
                    {
                        TempData["poruka"]="Vrijeme koje ste izabrali je isteklo";

                        return RedirectToAction("PrikaziSjedista", new { id = s.VoziloId, datum = datum, vrijeme = vrijeme });
                    }
                    else
                    {
                       if(await NovaKartaIRezervacija(karta, s))
                        {
                            TempData["poruka"] = "Uspješno ste rezervisali sjedište!";


                            return RedirectToAction("PrikaziSjedista", new { id = s.VoziloId, datum = datum, vrijeme = vrijeme });
                        }
                        else
                        {
                            TempData["poruka"] = "Niste u mogučnosti rezervisati";


                            return RedirectToAction("PrikaziSjedista", new { id = s.VoziloId, datum = datum, vrijeme = vrijeme });
                        }
                    }



                   

                   

                }

            }



            return RedirectToAction("PrikaziSjedista", new { id = s.VoziloId, datum = datum, vrijeme = vrijeme });
        }

        public async Task<bool> NovaKartaIRezervacija(Model.Karta karta1, Model.Sjediste s)
        {
            var reze = new RezervacijaUpsertRequest();
            var kartaRes = await _kartaService.GetById(karta1.Id);
            if (kartaRes.IsSuccessStatusCode)
            {
                var karta =  JsonConvert.DeserializeObject<Model.Karta>(kartaRes.Content.ReadAsStringAsync().Result);
                var cijenaRes = await _cijenaService.Get(new CijenaSearchRequest()
                {
                    KompanijaID = s.Vozilo.KompanijaId,
                    LinijaID = karta.Angazuje.LinijaId
                });

                if (cijenaRes.IsSuccessStatusCode)
                {
                    var resultC = cijenaRes.Content.ReadAsStringAsync().Result;
                    var cijena = JsonConvert.DeserializeObject<List<Model.Cijena>>(resultC);

                    if (karta != null)
                    {
                        reze.KartaId = karta.Id;
                        reze.DatumIsteka = karta.DatumIzdavanja.AddDays(1);

                    }
                    else
                    {
                        return false;
                    }




                    reze.DatumKreiranja = DateTime.Now;

                    reze.Otkazana = false;


                    var resPutnik = await _putnikService.Get(new PutnikSearchRequest() { KorisnickoIme = APIService.Username });
                    if (resPutnik.IsSuccessStatusCode)
                    {
                        var putnik = JsonConvert.DeserializeObject<List<Model.Putnik>>(resPutnik.Content.ReadAsStringAsync().Result);

                        reze.PutnikId = putnik[0].Id;

                        if (DateTime.Now.Date.Year - putnik[0].DatumRodjenja.Value.Date.Year > 50 || DateTime.Now.Date.Year - putnik[0].DatumRodjenja.Value.Date.Year < 18)
                        {
                            reze.CijenaSaPopustom = cijena[0].Popust == 0 ? cijena[0].Iznos : cijena[0].Iznos - (cijena[0].Iznos * decimal.Parse(cijena[0].Popust.Value.ToString()));
                        }
                        else
                        {
                            reze.CijenaSaPopustom = cijena[0].Iznos;
                        }
                    }
                    else
                    {
                        return false;
                    }




                    TempData["poruka"] = "Uspješno ste rezervisali";

                    await _rezervacijeService.Insert(reze);

                    return true;
                }
                return false;
            }
            return false;
           
        }

        private string GenerisiBrojKarte(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;

            string konacan = string.Empty;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
            {
                konacan = $"{builder.ToString().ToLower()}-{ DateTime.Now.Year.ToString()}";
            }
            else
            {
                konacan = builder.ToString();
            }

            return konacan;
        }

        public async Task<Model.Karta> KreirajKartu(KartaUpsertRequest novaKarta)
        {
            var resKarta = await _kartaService.Insert(novaKarta);

            Model.Karta karta = null;

            if (resKarta.IsSuccessStatusCode)
            {
                karta = JsonConvert.DeserializeObject<Model.Karta>(resKarta.Content.ReadAsStringAsync().Result);
            }

            return karta;
        }

        

        public async Task<List<Model.LinijaPodaci>> Preporuka()
        {
            var lista = new List<Model.LinijaPodaci>();

            if(APIService.Username != null)
            {
                var resPutnik = await _putnikService.Get(new PutnikSearchRequest() { KorisnickoIme = APIService.Username });

                if (resPutnik.IsSuccessStatusCode)
                {
                    var putnik = JsonConvert.DeserializeObject<List<Model.Putnik>>(resPutnik.Content.ReadAsStringAsync().Result);

                    var resPreporuka = await _preporukaService.GetById(putnik[0].Id);

                    if (resPreporuka.IsSuccessStatusCode)
                    {
                        var preporuke = JsonConvert.DeserializeObject<List<Model.LinijaPodaci>>(resPreporuka.Content.ReadAsStringAsync().Result);

                        foreach (var item in preporuke)
                        {
                            lista.Add(item);
                        }
                    }
                }
            }
           

            return lista;
        }
    }
}