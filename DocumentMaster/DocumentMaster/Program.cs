using AutoMapper;
using DM.BLL.Authorization;
using DM.BLL.MapServices;
using DM.BLL.Services;
using DM.DAL.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

//builder.Services.AddDbContext<DMContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DMContext")
   // ?? throw new InvalidOperationException("Connection string 'DMContext' not found.")));

builder.Services.AddScoped<PersonService>();
builder.Services.AddScoped<DepartmentService>();
builder.Services.AddScoped<ProjectService>();
builder.Services.AddScoped<ActionService>();
builder.Services.AddScoped<FileService>();
builder.Services.AddScoped<PositionService>();


//Auto Mapper Config
var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AutoMappingProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);


builder.Services.AddCors(options =>
{
    options.AddPolicy("Open", builder => builder.AllowAnyOrigin().AllowAnyHeader());
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DM", Version = "v1" });
});

var app = builder.Build();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DM"));

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);

}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseSwagger(x=>x.SerializeAsV2=true);

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("Open");

app.UseRouting();

app.MapControllers();

app.Run();
