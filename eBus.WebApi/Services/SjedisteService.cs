using AutoMapper;
using eBus.Model;
using eBus.Model.Requests;
using eBus.WebApi.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBus.WebApi.Services
{
    public class SjedisteService : BaseCRUDService<Model.Sjediste, SjedisteSearchRequest, Database.Sjediste, SjedisteUpsertRequest, SjedisteUpsertRequest>
    {
        public SjedisteService(_170048Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public override List<Model.Sjediste> Get(SjedisteSearchRequest search)
        {
            var query = _context.Sjediste.Include(x => x.Vozilo).AsQueryable();

            if(search != null)
            {
                if (search.VoziloId.HasValue)
                {
                    query = query.Where(l => l.VoziloId == search.VoziloId.Value);
                }
            }

            var lista = query.ToList();

            return _mapper.Map<List<Model.Sjediste>>(lista);
        }

        public override Model.Sjediste GetById(int id)
        {
            var data = _context.Sjediste.Include(l => l.Vozilo).Where(s => s.Id == id).FirstOrDefault();

            return _mapper.Map<Model.Sjediste>(data);
        }
    }
}
