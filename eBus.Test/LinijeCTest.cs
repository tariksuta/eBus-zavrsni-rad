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
    public class LinijeCTest
    {


        [TestMethod]
        public async Task PretreziLinije()
        {
            LinijeController lc = new LinijeController();

            var datume = new DateTime(2020, 8, 25);

            PartialViewResult result = await lc.PretraziLinije("Mostar", "Medugorje", datume) as PartialViewResult;

            var model = result.Model as LinijeVM;



            Assert.AreEqual(0, model.PrikaziLinije.Count);

        }

        [TestMethod]
        public async Task ProvjeriDetaljeLinije()
        {
            LinijeController lc = new LinijeController();

            ViewResult result = await lc.Detalji(61) as ViewResult;

            var model = result.Model as LinijeDetaljiVM;



            Assert.AreEqual("Mostar", model.PolazisteNaziv);
            Assert.AreEqual("Medugorje", model.OdredisteNaziv);


        }

        [TestMethod]

        public async Task ProvjeriSjedista()
        {
            LinijeController lc = new LinijeController();

            var datum = new DateTime(2020, 8, 25);
            var vrijeme = new TimeSpan(9, 0, 0);
            ViewResult result = await lc.PrikaziSjedista(2, datum, vrijeme) as ViewResult;

            var model = result.Model as SjedistaVM;

            Assert.AreEqual(44, model.listaSjedista.Count);
            Assert.AreEqual(true, model.listaSjedista[3].Rezervisano);
        }

        //[TestMethod]
        //public async Task ProvjeriOpcijuRezervisi()
        //{
        //    LinijeController lc = new LinijeController();

        //    var datum = new DateTime(2020, 8, 13);
        //    var vrijeme = new TimeSpan(9, 0, 0);

        //    ViewResult result = await lc.Rezervisi(3, datum, vrijeme) as ViewResult;



        //    Assert.AreEqual(result.TempData["poruka"], "Niste u mogučnosti rezervisati");
        //}

    }
}
