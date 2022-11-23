using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetLabWeb.Controllers
{
    public class AutenticacaoController : Controller
    {
        public IActionResult Login()
        {
            return Redirect("/Identity/Account/Login");
        }

        public IActionResult Delete()
        {
            return Redirect("/Identity/Account/Manage/DeletePersonalData");
        }

        public IActionResult PainelAdm()
        {
            return Redirect("/Identity/Account/Manage");
        }
    }
}
