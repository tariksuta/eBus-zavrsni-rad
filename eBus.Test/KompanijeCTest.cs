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
    public class KompanijeCTest
    {
        [TestMethod]
        public async Task ProvjeraSvihKompanija()
        {
            KompanijeController kc = new KompanijeController();

            ViewResult result = await kc.Prikaz() as ViewResult;

            var model = result.Model as KompanijeVM;

            Assert.AreEqual(3, model.SveKompanije.Count);
        }
        [TestMethod]
        public async Task ProvjeraUspjesnostiOcjene()
        {
            KompanijeController kc = new KompanijeController();

            var result = await kc.ProvjeriOcjenu(1, 2);

            Assert.IsTrue(result);

        }
    }

    
}
