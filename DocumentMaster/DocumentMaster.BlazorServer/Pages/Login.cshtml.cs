using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using DocumentMaster.BlazorServer.ModelsDTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Models;

namespace BlazorServerSideApp.Pages
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        public string ReturnUrl { get; set; }
        private IConfiguration _config;
        public LoginModel(IConfiguration configuration)
        {
            _config = configuration;         
        }
        public async Task<IActionResult> OnGetAsync(string paramUsername, string paramPassword)
        {
            string returnUrl = Url.Content("~/");
            try
            {
                // Clear the existing external cookie
                await HttpContext
                    .SignOutAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme);
                HttpContext.Session.Remove("apiAuthMes");
            }
            catch { }

            // *** !!! This is where you would validate the user !!! ***
            var accountToCheck = new AccountDTO
            {
                Login = paramUsername,
                Password = paramPassword
            };
            var stringContent = JsonContent.Create(accountToCheck);
            string requestUri = _config.GetConnectionString("ApiHost") + "/account/token";
            var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
            request.Headers.Add("Accept", "application/json; charset=utf-8");
            request.Headers.Add("User-Agent", "HTTPClient-client");
            request.Content = stringContent;
            var client = new HttpClient();
            var responce = await client.SendAsync(request);
            if ((int)responce.StatusCode == 200)
            {
                var person = await responce.Content.ReadFromJsonAsync<Person1DTO>();
                HttpContext.Response.Cookies.Append("token", person.ShortToken);
                
                // In this example we just log the user in
                // (Always log the user in for this demo)
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, paramUsername),
                new Claim(ClaimTypes.Role, person.Role),
            };
                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    RedirectUri = this.Request.Host.Value
                };
                try
                {
                    await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
                    
                }

                catch (Exception ex)
                {
                    string error = ex.Message;
                }


            }
            else
            {
                HttpContext.Session.SetString("apiAuthMes", responce.StatusCode.ToString());
            }           
            
            return LocalRedirect(returnUrl);
        }
    }
}
