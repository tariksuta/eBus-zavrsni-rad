using eBus.web.Controllers;
using eBus.web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eBus.Test
{
    [TestClass]
    public class LicniPodaciCTest
    {
        [TestMethod]
        public async Task ProvjeriIzmjenuKorisnickogImena()
        {
            LicniPodaciController lp = new LicniPodaciController();



            var result = await lp.provjeriKorisnickoIme("mobile", 1);



            bool isValid = result;

            Assert.AreEqual(false, isValid);
        }

        [TestMethod]
        public async Task ProvjeriValidnostIzmjenePodataka()
        {
            LicniPodaciController lp = new LicniPodaciController();

            var podaci = new LicniPodaciUrediVM();
            podaci.DatumRegistracije = DateTime.Now;
            podaci.DatumRodjenja = DateTime.Now;
            podaci.Email = "proba@mail.com";
            podaci.Ime = "Proba";
            podaci.Prezime = "Proba";

            podaci.Lozinka = "nova1";
            podaci.PotvrdiLozinku = "nova";

            podaci.KorisnickoIme = "Tare18";
            podaci.Slika = new byte[16];
            podaci.StaraSlika = "testtest";

            ViewResult result = await lp.Snimi(podaci) as ViewResult;

            Assert.AreEqual(result.ViewData["error_poruka"], "Lozinke se ne podudaraju");


        }
    }
}
