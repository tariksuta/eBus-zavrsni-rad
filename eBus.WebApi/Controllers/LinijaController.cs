﻿using System;
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
    //[Authorize(Roles ="Uposlenik,Administrator")]
    [AllowAnonymous] // dodao proba
    public class LinijaController : BaseCRUDController<Model.Linija, LinijaSearchRequest, LinijaUpsertRequest, LinijaUpsertRequest>
    {
        public LinijaController(ICRUDService<Linija, LinijaSearchRequest, LinijaUpsertRequest, LinijaUpsertRequest> service) : base(service)
        {
        }
    }
}