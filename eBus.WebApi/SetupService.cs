using eBus.WebApi.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBus.WebApi
{
    public class SetupService
    {
        public static void Init(_170048Context context)
        {
           //if(context.Drzava.Count() == 0) // ovo sam dodao i radi
            context.Database.Migrate();
        }
    }
}
