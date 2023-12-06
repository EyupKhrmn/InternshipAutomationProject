using System.Text;
using InternshipAutomation.Application.Repository.GeneralRepository;
using InternshipAutomation.Domain.User;
using InternshipAutomation.Persistance.Context;
using InternshipAutomation.Security.Token;
using IntershipOtomation.Domain.Entities.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

#region Swagger Token Entegrations

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",new OpenApiInfo
    {
        Version = "v1",
        Title = "Deneme",
        Description = "Deneme açıklaması"
    });
    
    c.AddSecurityDefinition("Bearer",new OpenApiSecurityScheme
    {
        Description = "TOken bazlı doprulama",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    
    c.AddSecurityRequirement(new OpenApiSecurityRequirement{    {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            },
            new string[] {}
        }
    });
});

#endregion

#region Custom Services

builder.Services.AddScoped<IGeneralRepository, GeneralRepository<InternshipAutomationDbContext>>();

builder.Services.AddScoped<IDecodeTokenService, DecodeTokenService>();

builder.Services.AddIdentity<User, AppRole>().AddEntityFrameworkStores<InternshipAutomationDbContext>();

#endregion

#region Authentication

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Token:Issuer"],
        ValidAudience = builder.Configuration["Token:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
        ClockSkew = TimeSpan.Zero
    };
});

#endregion

#region Cookie Settings

//builder.Services.ConfigureApplicationCookie(_ =>
//{
//    _.Cookie = new CookieBuilder
//    {
//        Name = "InternshipAutomationCookie",
//        HttpOnly = false,
//        Expiration = TimeSpan.FromMinutes(2),
//        SameSite = SameSiteMode.Lax,
//        SecurePolicy = CookieSecurePolicy.Always
//    };
//    _.SlidingExpiration = true;
//    _.ExpireTimeSpan = TimeSpan.FromMinutes(2);
//});

#endregion

builder.Services.AddDbContext<InternshipAutomationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();