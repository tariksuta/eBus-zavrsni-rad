using eBus.web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eBus.Test
{
    [TestClass]
    public class RezervacijeCTest
    {

        [TestMethod]
        public async Task ProvjeriDetaljeRezervacije()
        {
            RezervacijeController rc = new RezervacijeController();

            PartialViewResult result = await rc.Detalji(2) as PartialViewResult;

            var model = result.Model as Model.Rezervacija;

            Assert.AreEqual(false, model.Otkazana);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public async Task ProvjeriOtkazivanjeRezervacije()
        {
            RezervacijeController rc = new RezervacijeController();
            
                ViewResult res = await rc.Otkazi(2) as ViewResult;

            Assert.AreEqual(res.ViewData["poruka"], "Uspješno ste otkazali rezervaciju");

        }
    }
}
