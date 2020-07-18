using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using App_Empresas_Common;
using App_Empresas_Services_Impl.Services;
using App_Empresas_Services_Spec;
using App_Empresas_Services_Spec.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace App_Empresas.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<object> Post(
            [FromBody]UserDto usuario,
            [FromServices] SigningConfigurations signingConfigurations,
            [FromServices] TokenConfigurations tokenConfigurations)
        {
            bool credenciaisValidas = false;
            
            if(usuario != null && !string.IsNullOrEmpty(usuario.UserID))
            {
                var usuarioBase = await _userService.GetUser(usuario.UserID);

                credenciaisValidas = (usuarioBase != null && usuario.UserID == usuarioBase.UserID
                    && usuario.AccessKey == usuarioBase.AccessKey);
            }

            if (credenciaisValidas)
            {
                var identity = new ClaimsIdentity(
                    new GenericIdentity(usuario.UserID, "Login"),
                    new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, usuario.UserID)
                    }
                );

                var dataCriacao = DateTime.Now;
                var dataExpiracao = dataCriacao + TimeSpan.FromSeconds(tokenConfigurations.Seconds);

                var handler = new JwtSecurityTokenHandler();
                var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                {
                    Issuer = tokenConfigurations.Issuer,
                    Audience = tokenConfigurations.Audience,
                    SigningCredentials = signingConfigurations.SigningCredentials,
                    Subject = identity,
                    NotBefore = dataCriacao,
                    Expires = dataExpiracao
                });

                var token = handler.WriteToken(securityToken);

                var retorno = new
                {
                    authenticated = true,
                    created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                    expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                    accessToken = token,
                    message = "OK"
                };

                return Task.FromResult(retorno).Result;
            }
            else
            {
                var retorno = new
                {
                    authenticated = false,
                    message = "Falha ao autenticar"
                };

                return Task.FromResult(retorno).Result;
            }

        }

    }
}