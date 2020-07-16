using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using ElectroCo.Data;
using ElectroCo.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Security.Claims;

namespace ElectroCo.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ChangePasswordModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly ApplicationDbContext db;

        public ChangePasswordModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            db = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            public string UserId { get; set; }

            [Required(ErrorMessage = "A {0} é de preenchimento obrigatório")]
            [StringLength(100, MinimumLength = 6, ErrorMessage = "A {0} deve ter ao menos {2} e no máximo {1} caracteres.")]
            [DataType(DataType.Password)]
            [Display(Name = "Password Atual")]
            public string ActualPassword { get; set; }

            [Required(ErrorMessage = "A {0} é de preenchimento obrigatório")]
            [StringLength(100, MinimumLength = 6, ErrorMessage = "A {0} deve ter ao menos {2} e no máximo {1} caracteres.")]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirme password")]
            [Compare("Password", ErrorMessage = "A password e a confirmação não corresponde.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            
            if (ModelState.IsValid )
            {
                var user = await _userManager.FindByIdAsync(Input.UserId);
                if (user == null)
                {
                    TempData["error"] = "Não existe informação";
                    return LocalRedirect("returnUrl");
                }
                var result = await _userManager.ChangePasswordAsync(user, Input.ActualPassword, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User updated password.");
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    if (error.Code == "PasswordMismatch")
                    {
                        ModelState.AddModelError(string.Empty, "A password actual está errada");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
