using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using PetLabWeb.Models;

namespace PetLabWeb.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;
        private readonly IHttpContextAccessor _HttpContextAccessor;

        public LogoutModel(SignInManager<IdentityUser> signInManager, ILogger<LogoutModel> logger, IHttpContextAccessor httpContextAccessor)
        {
            _signInManager = signInManager;
            _logger = logger;
            _HttpContextAccessor = httpContextAccessor;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            SessionExtensionsHelp.RemoveObject(_HttpContextAccessor.HttpContext.Session, "UserSign");
            SessionExtensionsHelp.RemoveObject(_HttpContextAccessor.HttpContext.Session, "Token");
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToPage();
            }
        }
    }
}
