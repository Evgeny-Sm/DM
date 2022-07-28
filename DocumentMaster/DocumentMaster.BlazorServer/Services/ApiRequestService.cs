﻿namespace DocumentMaster.BlazorServer.Services
{
    public class ApiRequestService<T> where T : class
    {
        IConfiguration _configuration;
        IHttpContextAccessor _httpContextAccessor;


        public ApiRequestService(IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = contextAccessor;
        }
        public async Task<HttpResponseMessage> CreateAuthRequest(string endpoint, HttpMethod httpMethod, T content)
        {
            string requestUri = _configuration.GetConnectionString("ApiHost") + endpoint;
            var request = new HttpRequestMessage(httpMethod, requestUri);
            request.Headers.Add("Accept", "application/json; charset=utf-8");
            request.Headers.Add("User-Agent", "HTTPClient-client");


            string token = _httpContextAccessor.HttpContext.Request.Cookies["token"];
            request.Headers.Add("Authorization", $"Bearer {token}");

            var stringContent = JsonContent.Create(content);
            request.Content = stringContent;

            var client = new HttpClient();
            var responce = await client.SendAsync(request);

            return responce;
        }
        public Task<HttpResponseMessage> CreateAuthRequest(string endpoint, HttpMethod httpMethod)
        {
            string requestUri = _configuration.GetConnectionString("ApiHost") + endpoint;
            var request = new HttpRequestMessage(httpMethod, requestUri);
            request.Headers.Add("Accept", "application/json; charset=utf-8");
            request.Headers.Add("User-Agent", "HTTPClient-client");



            string token = _httpContextAccessor.HttpContext.Request.Cookies["token"];

            request.Headers.Add("Authorization", $"Bearer {token}");

            var client = new HttpClient();
            var responce = client.SendAsync(request).Result;

            return Task.FromResult(responce);
        }
        public Task<HttpResponseMessage> CreateAuthRequest(string endpoint, IFormFile file)
        {
            string requestUri = _configuration.GetConnectionString("ApiHost") + endpoint;
            var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
            request.Headers.Add("Accept", "multipart/form-data");
            request.Headers.Add("User-Agent", "HTTPClient-client");
            request.ContentType



            string token = _httpContextAccessor.HttpContext.Request.Cookies["token"];

            request.Headers.Add("Authorization", $"Bearer {token}");
            request.Content.Fil = file;

            var client = new HttpClient();
            var responce = client.SendAsync(request).Result;

            return Task.FromResult(responce);
        }
    }
}
