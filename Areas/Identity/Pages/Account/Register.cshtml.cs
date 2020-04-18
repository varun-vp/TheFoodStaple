using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace TheFoodStapleEx.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IPasswordValidator<IdentityUser> _passwordValidator;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager,
            IPasswordValidator<IdentityUser> passwordValidator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
            _passwordValidator = passwordValidator;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            
            [EmailAddress]
            [Display(Name = "Email")]
            
            [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"), Required, StringLength(30)]
            public string Email { get; set; }

            
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]

            //[RegularExpression(@"^([A-Z])[a-zA-Z!@#$%][0-9]{4,8}$"), Required]
            [DataType(DataType.Password)]
            [Required]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required(ErrorMessage="Role cannot be empty")]
            public string Role { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ViewData["roles"] = _roleManager.Roles.Where(p=>p.Name!="Admin").ToList();
            
            ReturnUrl = returnUrl;
            
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (string.IsNullOrEmpty(Input.Role))                                                   //check whether the role is null or not
            {
                TempData["roleEmpty"] = "Role Cannot be Empty";
                return LocalRedirect("~/Identity/Account/Register");
            }
            
            returnUrl = returnUrl ?? Url.Content("~/");
            var role = _roleManager.FindByIdAsync(Input.Role).Result;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            


            var currentUser = await _userManager.FindByNameAsync(Input.Email);
            if (currentUser!=null)                                                                  //check whether the email id exist or not
            {
                TempData["emailExisted"] = "Email ID already registered with us";
                
                return LocalRedirect("~/Identity/Account/Register");
            }
            
            if (ModelState.IsValid)
                {

                if (role == null)
                {
                    TempData["roleEmpty"] = "Role Cannot be Empty";
                    return LocalRedirect("~/Identity/Account/Register");
                }
                var user = new IdentityUser { UserName = Input.Email, Email = Input.Email };


                if (!Regex.IsMatch(Input.Password, "[a-z]"))
                {
                    TempData["passwordStrength"] = "Password strength is poor must contains atleast one character from [a-z]";
                    return LocalRedirect("~/Identity/Account/Register");
                }
                if (!Regex.IsMatch(Input.Password, "[A-Z]"))
                {
                    TempData["passwordStrength"] = "Password strength is poor must contains atleast one character from [A-Z]";
                    return LocalRedirect("~/Identity/Account/Register");
                }
                if (!Regex.IsMatch(Input.Password, "[0-9]"))
                {
                    TempData["passwordStrength"] = "Password strength is poor must contains atleast one character from [0-9]";
                    return LocalRedirect("~/Identity/Account/Register");
                }
                if (!Regex.IsMatch(Input.Password, "[!@#$%^&*]"))
                {
                    TempData["passwordStrength"] = "Password strength is poor must contain atleast special character [!@#$%^&*-_?><]";
                    return LocalRedirect("~/Identity/Account/Register");
                }


                //var pswd = _passwordValidator.ValidateAsync(_userManager, user, Input.Password);

                //if ( pswd == null)                                                                  //check the status of passowrd validator but not working
                //{
                //    TempData["passwordStrength"] = "Passwordstrengthis poor";
                //    return LocalRedirect("~/Identity/Account/Register");
                //}
                var result = await _userManager.CreateAsync(user, Input.Password);
                
                if(result==null)                                                                   //check the result contains null or not
                {
                    TempData["passwordStrength"] = "Passwordstrengthis poor";
                    return LocalRedirect("~/Identity/Account/Register");
                }
                
                //var currentUser = await _userManager.FindByNameAsync(Input.Email);
                //if(currentUser==null)
                //{
                //    return Page();
                //}
                if (result.Succeeded)
                {
                        _logger.LogInformation("User created a new account with password.");

                        await _userManager.AddToRoleAsync(user, role.Name);
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
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
