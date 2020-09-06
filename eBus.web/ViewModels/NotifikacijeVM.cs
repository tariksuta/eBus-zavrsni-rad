using eBus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBus.web.ViewModels
{
    public class NotifikacijeVM
    {
        //public List<Model.Notifikacije> ListaNOtifikacija { get; set; }

        public List<Row> ListaNOtifikacija { get; set; }

        public class Row
        {
            public int Id { get; set; }
            public string Naslov { get; set; }
            public DateTime? DatumSlanja { get; set; }
            public int? NovostId { get; set; }
            public bool Procitano { get; set; }

            public virtual Novosti Novost { get; set; }
        }
    }
}
