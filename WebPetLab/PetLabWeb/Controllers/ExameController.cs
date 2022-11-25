using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetLabWeb.Controllers
{
    public class ExameController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
