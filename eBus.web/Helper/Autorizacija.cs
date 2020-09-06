using eBus.Model.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace eBus.web.Helper
{
    public class AutorizacijaAttribute : TypeFilterAttribute//filter https://www.c-sharpcorner.com/article/working-with-filters-in-asp-net-core-mvc/
    {
        public AutorizacijaAttribute(bool putnik) : base(typeof(MyAuthorizeImpl))
        {
            Arguments = new object[] { putnik };
        }
    }

    public class MyAuthorizeImpl : IAsyncActionFilter
    {
        private readonly APIService _putnikService = new APIService("Putnik");

        private readonly bool _putnik;

        public MyAuthorizeImpl(bool putnik)
        {
            _putnik = putnik;
        }

       

        public async Task OnActionExecutionAsync(ActionExecutingContext filterContext, ActionExecutionDelegate next)
        {
            if (APIService.Username == null)
            {
                if (filterContext.Controller is Controller controller)
                {
                    controller.TempData["error_poruka"] = "Niste logirani";
                }
                filterContext.Result = new RedirectToActionResult("PrijaviSe", "Login", new { area = "" });
                return;
            }


            List<Model.Putnik> k = null;
            HttpResponseMessage res = await _putnikService.Get(new PutnikSearchRequest() { KorisnickoIme = APIService.Username });
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                k = JsonConvert.DeserializeObject<List<Model.Putnik>>(result);
            }
            if (k == null)
            {
                if (filterContext.Controller is Controller controller)
                {
                    controller.TempData["error_poruka"] = "Niste logirani";
                }
                filterContext.Result = new RedirectToActionResult("PrijaviSe", "Login", new { area = "" });
                return;
            }
            //prava pristupa
            if (_putnik)
            {
                await next();
                return;
            }

            if (filterContext.Controller is Controller c1)
            {
                c1.TempData["error_poruka"] = "Nemate pravo pristupa";
            }
            filterContext.Result = new RedirectToActionResult("Index", "Home", new { area = "" });
            return;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            //throw new NotImplementException();
        }

    }

}
