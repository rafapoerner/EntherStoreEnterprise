using ESE.WebApp.MVC.Models;
using ESE.WebApp.MVC.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ESE.WebApp.MVC.Controllers
{

    public class IdentityController : Controller
    {

        private readonly IAutenticatedService _autenticatedService;

        public IdentityController(IAutenticatedService autenticatedService)
        {
            _autenticatedService = autenticatedService;
        }

        [HttpGet]
        [Route("new-account")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("new-account")]
        public async Task<IActionResult> Register(UserRegister userRegister)
        {
            if (!ModelState.IsValid) return View(userRegister);

            // API - Register
            var response = await _autenticatedService.Register(userRegister);

            //if (false) return View(userRegister);

            // Realizar Registro na app
            await ConnectLogin(response);

            return RedirectToAction("Index", "Home");

        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UserLogin userLogin)
        {
            if (!ModelState.IsValid) return View(userLogin);

            // API - Register
            var response = await _autenticatedService.Login(userLogin);

            //if (false) return View(userLogin);

            // Realizar Login na app
            await ConnectLogin(response);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("sair")]
        public IActionResult Logout()
        {
            return RedirectToAction("Index", "Home");
        }

        private async Task ConnectLogin(UserResponseLogin response)
        {
            var token = GetTokenFormated(response.AccessToken);

            var claims = new List<Claim>();
            claims.Add(new Claim("JWT", response.AccessToken));
            claims.AddRange(token.Claims);

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
                IsPersistent = true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }

        private static JwtSecurityToken GetTokenFormated(string jwtToken)
        {
            return new JwtSecurityTokenHandler().ReadToken(jwtToken) as JwtSecurityToken; // esse último JwtSecurityToken é um cast.
        }

    }
}
