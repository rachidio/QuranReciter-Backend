using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using HolyQuran.Core.Settings;
using HolyQuran.infrastructure.Extentions;
using HolyQuran.Data.Data;
using HolyQuran.Infrastructure.Services.Interfaces;
using HolyQuran.Core.Constants;
using HolyQuran.Infrastructure.Services;
using HolyQuran.Infrastructure.Services.Users;
using HolyQuran.Data.Models;
 
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
//builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddSingleton(new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new HolyQuran.infrastructure.Mapper.AutoMapper());
}).CreateMapper());
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(1);//You can set Time
});
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(config =>
{
    config.User.RequireUniqueEmail = true;
    config.Password.RequireDigit = false;
    config.Password.RequiredLength = 6;
    config.Password.RequireLowercase = false;
    config.Password.RequireNonAlphanumeric = false;
    config.Password.RequireUppercase = false;
    config.SignIn.RequireConfirmedEmail = false;
}).AddEntityFrameworkStores<ApplicationDbContext>()
                   .AddDefaultTokenProviders().AddDefaultUI();
builder.Services.AddControllersWithViews().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

;/*.AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);*/
//builder.Services.AddAuthentication(config =>
//{
//    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    config.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//.AddJwtBearer(options =>
//{
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        ValidIssuer = builder.Configuration["Jwt:Issure"],
//        ValidAudience = builder.Configuration["Jwt:Issure"],
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Signinkey"]))
//    };
//}
// ;
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Holy Quran API", Version = "v1" });
    //c.AddSecurityDefinition("Bearer",
    //    new OpenApiSecurityScheme
    //    {
    //        Description = "Please enter into field the word 'Bearer' following by space and JWT",
    //        Name = "Authorization",
    //        In = ParameterLocation.Header,
    //        Scheme = "Bearer"
    //    });
    //c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    //            {
    //                {
    //                    new OpenApiSecurityScheme
    //                    {
    //                        Reference = new OpenApiReference
    //                        {
    //                            Type = ReferenceType.SecurityScheme,
    //                            Id = "Bearer"
    //                        },
    //                        Scheme = "oauth2",
    //                        Name = "Bearer",
    //                        In = ParameterLocation.Header,
    //                    },
    //                    new List<string>()
    //                }
    //            });
});
//builder.Services.Registerservices();
builder.Services.AddScoped<IInterfaceServices, InterfaceServices>();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Holy Quran API");
    });
}
else if (app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Holy Quran API");
    });
}
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.SeedDb().Run();











