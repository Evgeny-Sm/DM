using AutoMapper;
using DM.BLL.Authorization;
using DM.BLL.MapServices;
using DM.BLL.Services;
using DM.DAL.Models;
using DocumentMaster.BlazorServer.Authentication;
using DocumentMaster.BlazorServer.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Radzen;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureApplicationCookie(options => {
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(480);
    options.SlidingExpiration = true;
});



builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();
builder.Services.AddScoped<InvalidUserService>();

builder.Services.AddDbContext<DMContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DMContext")
    ?? throw new InvalidOperationException("Connection string 'DMContext' not found.")));

builder.Services.AddScoped<PersonService>();
builder.Services.AddScoped<DepartmentService>();
builder.Services.AddScoped<ProjectService>();
builder.Services.AddScoped<ActionService>();
builder.Services.AddScoped<FileService>();
builder.Services.AddScoped<PositionService>();
builder.Services.AddScoped<AccountService>();


//Auto Mapper Config
var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AutoMappingProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);



builder.Services.AddAuthenticationCore();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor().AddHubOptions(options =>
{
    // maximum message size of 100MB
    options.MaximumReceiveMessageSize = 100000000;
}); ;
builder.Services.AddScoped<ProtectedSessionStorage>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<HttpContextAccessor>();
builder.Services.AddHttpClient();
builder.Services.AddScoped<HttpClient>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Open", builder => builder.AllowAnyOrigin().AllowAnyHeader());
});

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);

}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseCookiePolicy();
app.UseAuthentication();

app.UseAuthorization();

app.UseRouting();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
