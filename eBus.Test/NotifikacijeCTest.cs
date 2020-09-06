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
    public class NotifikacijeCTest
    {

       // [TestMethod]
    
        public async Task ProvjeraNotifikacija()
        {
            NotifikacijeController nc = new NotifikacijeController();

            ViewResult result = await nc.PrikaziNotifikacije() as ViewResult;

            var model = result.Model as NotifikacijeVM;


            Assert.AreEqual(0, model.ListaNOtifikacija.Count);
        }

        [TestMethod]
        public async Task ProvjeriBrojPregledaNovosti()
        {
            NovostiController nc = new NovostiController();

            var result = await nc.brojPregleda(2);

            Assert.AreEqual(1, result);
        }
    }
}
