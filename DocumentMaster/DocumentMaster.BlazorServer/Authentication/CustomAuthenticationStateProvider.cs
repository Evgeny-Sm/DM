﻿using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Claims;

namespace DocumentMaster.BlazorServer.Authentication
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {

        private readonly ProtectedLocalStorage _localStorage;   
        private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        public CustomAuthenticationStateProvider(ProtectedLocalStorage localStorage)
        {
            _localStorage = localStorage;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
           
            try
            {
                var userSessionStorageResult = await _localStorage.GetAsync<UserSession>("UserSession");
                var userSession = userSessionStorageResult.Success ? userSessionStorageResult.Value : null;
                if (userSession == null||(DateTime.Now.Subtract(userSession.DateTime)>TimeSpan.FromDays(1)))
                {
                    return await Task.FromResult(new AuthenticationState(_anonymous));
                }
                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Name,userSession.UserName),
                new Claim(ClaimTypes.Role,userSession.Role),
                new Claim("PersonId",userSession.PersonId.ToString()),
                new Claim("FirstName", userSession.FirstName),
                new Claim("LastName", userSession.LastName)
            }, "CustomAuth"
                    ));
                return await Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
            catch
            {
                return await Task.FromResult(new AuthenticationState(_anonymous));
            }           
        }
        public async Task UpdateAuthenticationState(UserSession userSession)
        {
            ClaimsPrincipal claimsPrincipal;
            if (userSession != null)
            {
                await _localStorage.SetAsync("UserSession", userSession);
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Name,userSession.UserName),
                new Claim(ClaimTypes.Role,userSession.Role),
                new Claim("PersonId",userSession.PersonId.ToString()),
                new Claim("FirstName", userSession.FirstName),
                new Claim("LastName", userSession.LastName)              
            }));
            }
            else
            {
                await _localStorage.DeleteAsync("UserSession");
                claimsPrincipal = _anonymous;
            }
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));

        }
    }
}
