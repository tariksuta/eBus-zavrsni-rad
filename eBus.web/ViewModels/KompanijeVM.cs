using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBus.web.ViewModels
{
    public class KompanijeVM
    {
        public List<Model.Kompanija> ListaKomanija { get; set; }
        public List<Model.Kompanija> SveKompanije { get; set; }

        public int KompanijaID { get; set; }
        public int Ocjena { get; set; } = 1;
    }
}
