using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBus.Model.Requests
{
    public class AngazujeSearchRequest
    {
        public int? LinijaId { get; set; }

        public int? VoziloId { get; set; }

        public bool ZaLiniju { get; set; }
        public DateTime Datum { get; set; }
        
        public bool PoMjestu { get; set; }

        public string Polaziste { get; set; }
        public string Odrediste { get; set; }
    }
}
