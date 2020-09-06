using eBus.web.Controllers;
using eBus.web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eBus.Test
{
    [TestClass]
    public class LoginCTest
    {

        [TestMethod]
        public async Task ProvjeriLoginPodatke()
        {
            LoginController lc = new LoginController();

            var podaci = new LoginVM();

            podaci.KorisnickoIme = "mobile";
            podaci.Lozinka = "test2";

            ViewResult result = await lc.Prijava(podaci) as ViewResult;

            Assert.AreEqual(result.ViewData["error_poruka2"], "Pogrešno korisničko ime ili lozinka! ");
        }

       
    }
}
