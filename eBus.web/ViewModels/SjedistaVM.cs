using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBus.web.ViewModels
{
    public class SjedistaVM
    {

        public DateTime Datum { get; set; }
        public TimeSpan Vrijeme { get; set; }
        public List<Row> listaSjedista { get; set; }
        public class Row
        {
            public int Id { get; set; }
            public int Red { get; set; }
            public int Kolona { get; set; }

            public bool Rezervisano { get; set; }

            public string Lokacija { get; set; }
        }

       
    }
}
