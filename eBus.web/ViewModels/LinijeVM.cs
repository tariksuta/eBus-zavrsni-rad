using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBus.web.ViewModels
{
    public class LinijeVM
    {

        public List<Model.Grad> ListaGradova { get; set; }

        public List<Model.LinijaPodaci> PrikaziLinije { get; set; }

        public string Polaziste { get; set; }
        public string Odrediste { get; set; }
        public DateTime DatumPretrage { get; set; } =  DateTime.Now;
    }
}
