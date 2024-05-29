using Meta.Books.WebApi.Dto;
using Meta.Books.WebSite.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Meta.Books.WebSite.Pages.User
{
    public class Login : PageModel
    {
        [BindProperty]
        public LoginDto LoginDto { get; set; }
        
        public List<string> Errors { get; set; } = new List<string>();

        private readonly IUserService _service;

        public Login(IUserService service)
        {
            _service = service;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        Errors.Add(error.ErrorMessage);
                    }
                }
                return Page();
            }

            var response = await _service.Login(LoginDto);

            if (response.data)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, LoginDto.email)
                    // Puedes agregar más claims aquí si es necesario
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                return RedirectToPage("/Home"); // Asegúrate de que esta ruta sea correcta
            }
            else
            {
                Errors.Add("Invalid login attempt. Please check your email and password.");
                return Page();
            }
        }
    }
}
