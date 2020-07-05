using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MyHealthCheck.Controllers
{
    public class IceAgeHealthCheck : Controller
    {
        public string Index()
        {
            return "ping " + DateTime.UtcNow.ToString("o");
        }
    }
}
