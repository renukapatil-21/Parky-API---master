using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Ninject.Activation;
using ParkyAPI;
using ParkyAPI.Data;
using ParkyAPI.ParkyMapper;
using ParkyAPI.Repository;
using ParkyAPI.Repository.IRepository;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//------------------------------------------------------------------
builder.Services.AddCors();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(x =>
{
    x.UseSqlServer(connectionString);
});


builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
});

builder.Services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VVV");
//builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();


builder.Services.AddScoped<INationalParkRepository, NationalParkRepository>();
builder.Services.AddScoped<ITrailRepository, TrailRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddAutoMapper(typeof(ParkyMappings));


//------------------------------------------------------------------

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var appSettingsSection = builder.Configuration.GetSection("AppSettings");

builder.Services.Configure<AppSettings>(appSettingsSection);

var appSettings = appSettingsSection.Get<AppSettings>();
var key = Encoding.ASCII.GetBytes(appSettings.Secret);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
            .AddJwtBearer(x => {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

var app = builder.Build();

// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{
    app.UseSwagger();   
}*/
app.UseHttpsRedirection();

app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI();
/*app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/ParkyOpenAPISpec/swagger.json", "Parky API");
    options.RoutePrefix = "";
});*/
app.UseRouting();


//"launchUrl": "swagger",


/*app.UseSwaggerUI(options =>
{
    foreach (var desc in provider.ApiVersionDescriptions)
        options.SwaggerEndpoint($"/swagger/{desc.GroupName}/swagger.json",
            desc.GroupName.ToUpperInvariant());
    options.RoutePrefix = "";
});*/


app.UseCors(x => x
              .AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();


/*public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    readonly IApiVersionDescriptionProvider provider;

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) =>
    this.provider = provider;

    public void Configure(SwaggerGenOptions options)
    {
        app.UseSwaggerUI(options =>
        {
            foreach (var desc in provider.ApiVersionDescriptions)
                options.SwaggerEndpoint($"/swagger/{desc.GroupName}/swagger.json",
                    desc.GroupName.ToUpperInvariant());
            options.RoutePrefix = "";
        });
    }
}*/








/*using Microsoft.EntityFrameworkCore;
using ParkyAPI.Data;
using ParkyAPI.Repository.IRepository;
using AutoMapper;
using ParkyAPI.ParkyMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>
    (options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
builder.Services.AddScoped<INationalParkRepository, NationalParkRepository>();
builder.Services.AddScoped<ITrailRepository, TrailRepository>();


builder.Services.AddAutoMapper(typeof(ParkyMappings));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("ParkyOpenAPISpec",
        new Microsoft.OpenApi.Models.OpenApiInfo()
        {
            Title = "Parky API",
            Version = "1",
            Description = "Udemy Parky API",
            Contact = new Microsoft.OpenApi.Models.OpenApiContact()
            {
                Email = "abhishek.joshi@mindbodyonline.com",
                Name = "Abhishek Joshi"
            },
            License = new Microsoft.OpenApi.Models.OpenApiLicense()
            {
                Name = "MIT License"
            }
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("swagger/ParkyOpenAPISpec/swagger.json", "Parky API");
    options.RoutePrefix = "";
});

app.UseRouting();

*//*if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}*//*



app.UseAuthorization();

app.MapControllers();

app.Run();
*/
