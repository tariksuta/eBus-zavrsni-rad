using eBus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBus.web.ViewModels
{
    public class LinijeDetaljiVM
    {
        //public LinijaPodaci Podaci { get; set; }

        public string OdredisteNaziv { get; set; }
        public string PolazisteNaziv { get; set; }

        public decimal Cijena { get; set; }

        public string Kompanija { get; set; }

        public string Vozilo { get; set; }

        public int voziloId { get; set; }



       
        public DateTime DatumPretrage { get; set; }

        public TimeSpan VrijemePolaska { get; set; }

        public TimeSpan VrijemeDolaska { get; set; }


       
    }
}
