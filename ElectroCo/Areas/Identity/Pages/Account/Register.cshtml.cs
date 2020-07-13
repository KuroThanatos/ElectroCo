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

namespace ElectroCo.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext db;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            db = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "This field is required")]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "This field is required")]
            [StringLength(40, ErrorMessage = "It {0} can't have more than {1} characters.")]
            [RegularExpression("[A-ZÓÂÍ][a-zçáéíóúàèìòùãõäëïöüâêîôûñ]+(( | d[ao](s)? | e |-|'| d')[A-ZÓÂÍ][a-zçáéíóúàèìòùãõäëïöüâêîôûñ]+){1,3}",
                          ErrorMessage = "Should write between 2 and 4 names, starting Uppercase, followed by lowercase.")]
            [Display(Name = "Name")]
            public string Name { get; set; }

            [Required(ErrorMessage = "This field is required")]
            [StringLength(9, MinimumLength = 9, ErrorMessage = "Must have exactly {1} digits in {0}.")]
            [RegularExpression("[12567][0-9]{8}", ErrorMessage = "Should write a number, with 9 digits, starting with 1, 2, 5, 6 or 7.")]
            [Display(Name = "NIF")]
            public string NIF { get; set; }

            [Required(ErrorMessage = "This field is required")]
            [StringLength(9, MinimumLength = 9, ErrorMessage = "Must have exactly {1} digits in {0}.")]
            [RegularExpression("[29][0-9]{8}", ErrorMessage = "Should write a number, with 9 digits, starting with 2 or 9.")]
            [Display(Name = "Phone")]
            public string Phone { get; set; }

            [Required(ErrorMessage = "This field is required")]
            [Display(Name = "Address")]
            public string Address { get; set; }

            [Required(ErrorMessage = "This field is required")]
            [StringLength(8, MinimumLength = 8, ErrorMessage = "Must have exactly {1} digits and - after 4 first digits in {0}.")]
            [RegularExpression("[0-9]{4}-[0-9]{3}", ErrorMessage = "Should write a number, with 8 digits, with 4 digits, hifen and more 3 digits")]
            [Display(Name = "Postal Code")]
            public string CP { get; set; }

            [Required(ErrorMessage = "This field is required")]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            ///Pode ser interessante
            public Clientes Cliente { get; set; }

        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            // ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = Input.Email, Email = Input.Email };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    var result1 = await _userManager.AddToRoleAsync(user, "cliente");
                    _logger.LogInformation("User created a new account with password.");

                    Clientes novoCliente = new Clientes
                    {
                        Name = Input.Name,
                        Email = Input.Email,
                        UserId = user.Id,
                        NIF = Input.NIF,
                        Telefone = Input.Phone,
                        Morada = Input.Address,
                        CodigoPostal = Input.CP
                    };

                    try
                    {
                        db.Add(novoCliente);
                        await db.SaveChangesAsync();
                    }
                    catch (Exception)
                    {
                        // escrever uma msg de erro para visualização futura, guardando os dados da var. ex
                        // na BD, numa tabela de 'Erros'
                        // num ficheiro de texto

                        // eventualmente, se não é possível criar o Cliente
                        // deverá ser eliminado o User que se acabou de criar
                        return RedirectToAction("Index", "Home");
                    }

                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    //var callbackUrl = Url.Page(
                    //    "/Account/ConfirmEmail",
                    //    pageHandler: null,
                    //    values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                    //    protocol: Request.Scheme);

                    //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
