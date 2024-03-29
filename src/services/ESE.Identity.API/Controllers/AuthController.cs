﻿using ESE.Core.Messages.Integration;
using ESE.Identity.API.Models;
using ESE.MessageBus;
using ESE.WebApi.Core.Controllers;
using ESE.WebApi.Core.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ESE.Identity.API.Controllers
{
    [ApiController]
    [Route("api/identity")]
    public class AuthController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;

        private readonly IMessageBus _bus;

        public AuthController(SignInManager<IdentityUser> signInManager,
                              UserManager<IdentityUser> userManager,
                              IOptions<AppSettings> appSettings,
                              IMessageBus bus)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
            _bus = bus;
        }

        [HttpPost("new-account")]
        public async Task<ActionResult> Register(UserRegister userRegister)
        {
            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }

            var user = new IdentityUser
            {
                UserName = userRegister.Email,
                Email = userRegister.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, userRegister.Password);

            if (result.Succeeded)
            {
                try
                {
                    var clientResult = await RegisterClient(userRegister);

                    if (!clientResult.ValidationResult.IsValid)
                    {
                        await _userManager.DeleteAsync(user);
                        return CustomResponse(clientResult.ValidationResult);
                    }

                    return CustomResponse(await GetJwt(userRegister.Email));
                }
                catch (Exception ex)
                {
                    // Trate a exceção de maneira apropriada (log, retornar uma resposta específica, etc.)
                    // Aqui você pode incluir um log, por exemplo:
                    CustomResponse("Erro ao registrar cliente após o registro do usuário.");

                    // Retorne uma resposta de erro genérica ou específica, dependendo do seu caso.
                    return CustomResponse("Erro ao registrar o cliente.");
                }
            }

            foreach (var error in result.Errors)
            {
                AddProcessingError(error.Description);
            }

            return CustomResponse();
        }


        [HttpPost("autenticated")]
        public async Task<ActionResult> Login(UserLogin userLogin)
        {

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _signInManager.PasswordSignInAsync(userLogin.Email, userLogin.Password, false, false); // False pra persistência e true para bloqueio do usuário após 5 tentativas(padrão 5min)

            if (result.Succeeded)
            {
                return CustomResponse(await GetJwt(userLogin.Email));
            }

            if (result.IsLockedOut)
            {
                AddProcessingError("Usuário bloqueado temporariamente por tentativa inválidas.");
                return CustomResponse();
            }

            AddProcessingError("Usuário ou senhas incorretos");
            return CustomResponse();
        }

        private async Task<UserResponseLogin> GetJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);

            var identityClaims = await GetClaimsUser(claims, user);
            var encodeToken = EncodedToken(identityClaims);

            return GetResponseToken(encodeToken, user, claims);
        }

        private async Task<ClaimsIdentity> GetClaimsUser(ICollection<Claim> claims, IdentityUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            return identityClaims;
        }

        private string EncodedToken(ClaimsIdentity identityClaims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.ValidOn,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpirationHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            return tokenHandler.WriteToken(token);
        }

        private UserResponseLogin GetResponseToken(string encodedToken, IdentityUser user, IEnumerable<Claim> claims)
        {
            return new UserResponseLogin
            {
                AccessToken = encodedToken,
                ExpireIn = TimeSpan.FromHours(_appSettings.ExpirationHours).TotalSeconds,
                UserToken = new UserToken
                {
                    Id = user.Id,
                    Email = user.Email,
                    Claims = claims.Select(c => new UserClaim { Value = c.Value, Type = c.Type, })
                }
            };
        }


        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);


        private async Task<ResponseMessage> RegisterClient(UserRegister userRegister)
        {
            var user = await _userManager.FindByEmailAsync(userRegister.Email);

            var userRegistered = new UserRegistratedIntegrationEvent(Guid.Parse(user.Id), userRegister.Name, userRegister.Email, userRegister.Cpf);

            try
            {
                return await _bus.RequestAsync<UserRegistratedIntegrationEvent, ResponseMessage>(userRegistered);
            }
            catch
            {
                await _userManager.DeleteAsync(user);
                throw;
            }
        }
    }
}
