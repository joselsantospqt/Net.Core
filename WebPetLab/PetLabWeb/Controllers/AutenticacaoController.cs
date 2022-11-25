using Microsoft.AspNetCore.Authorization;
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

        public IActionResult AccessDenied()
        {
            return Redirect("/Identity/Account/AccessDenied");
        }

        [Authorize]
        public IActionResult Logoff()
        {
            return Redirect("/Identity/Account/Logout");
        }

        [Authorize]
        public IActionResult Delete()
        {
            return Redirect("/Identity/Account/Manage/DeletePersonalData");
        }

        [Authorize]
        public IActionResult PainelAdm()
        {
            return Redirect("/Identity/Account/Manage");
        }

        public IActionResult Home()
        {
            return View();
        }

        public IActionResult Sobre()
        {
            return View();
        }

        public IActionResult Servicos()
        {
            return View();
        }

        public IActionResult Contatos()
        {
            return View();
        }

        public IActionResult Parceiros()
        {
            return View();
        }
    }
}
