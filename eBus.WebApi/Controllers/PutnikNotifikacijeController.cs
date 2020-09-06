using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eBus.Model;
using eBus.Model.Requests;
using eBus.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eBus.WebApi.Controllers
{

    [AllowAnonymous]
    public class PutnikNotifikacijeController : BaseCRUDController<Model.PutnikNotifikacije, PutnikNotifikacijeSearchRequest, PutnikNotifikacijeUpsertRequest, PutnikNotifikacijeUpsertRequest>
    {
        public PutnikNotifikacijeController(ICRUDService<PutnikNotifikacije, PutnikNotifikacijeSearchRequest, PutnikNotifikacijeUpsertRequest, PutnikNotifikacijeUpsertRequest> service) : base(service)
        {
        }
    }
}