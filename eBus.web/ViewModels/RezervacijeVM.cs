using eBus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBus.web.ViewModels
{
    public class RezervacijeVM
    {

        //public List<Model.Rezervacija> ListaRezervacija { get; set; }

        public List<Row> ListaRezervacija { get; set; }


        public class Row
        {
            public int Id { get; set; }
            public DateTime DatumKreiranja { get; set; }
            public DateTime DatumIsteka { get; set; }
            public bool? Otkazana { get; set; }
            public int PutnikId { get; set; }
            public int KartaId { get; set; }
            public byte[] Qrcode { get; set; }

            public bool Vazeca { get; set; }

            public virtual Karta Karta { get; set; }
            public virtual Putnik Putnik { get; set; }
        }
    }
}
