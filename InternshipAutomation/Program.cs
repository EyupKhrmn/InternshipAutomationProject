using System.Reflection;
using System.Text;
using FluentValidation.AspNetCore;
using InternshipAutomation.Application.Mail;
using InternshipAutomation.Application.Repository.GeneralRepository;
using InternshipAutomation.Domain.User;
using InternshipAutomation.Persistance.Context;
using InternshipAutomation.Persistance.LogService;
using InternshipAutomation.Security.Token;
using IntershipOtomation.Domain.Entities.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using QuestPDF.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddFluentValidation(v =>
{
    v.ImplicitlyValidateChildProperties = true;
    v.ImplicitlyValidateRootCollectionElements = true;
    v.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
});
builder.Services.AddEndpointsApiExplorer();

QuestPDF.Settings.License = LicenseType.Community;

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

#region Authentication & Authorization

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
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"] ?? string.Empty)),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(IdentityData.AdminUserPolicyName, p => 
        p.RequireClaim(IdentityData.UserRankClaimsName, IdentityData.AdminUserRankName));

    options.AddPolicy(IdentityData.TeacherUserPolicyName, p =>
        p.RequireClaim(IdentityData.UserRankClaimsName, IdentityData.TeacherUserRankName));
    
    options.AddPolicy(IdentityData.StudentUserPolicyName, p => 
        p.RequireClaim(IdentityData.UserRankClaimsName,IdentityData.StudentUserRankName));
    
    options.AddPolicy(IdentityData.CompanyUserPolicyName, p => 
        p.RequireClaim(IdentityData.UserRankClaimsName,IdentityData.CompanyUserRankName));
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

#region Mail Operations

builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));

builder.Services.AddScoped<IEmailSender,MailSender>();

#endregion

#region Log Operations

builder.Services.AddScoped<ILogService,LogService>();
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File(builder.Configuration["Serilog:Path"] ?? string.Empty,rollingInterval: RollingInterval.Day)
    .CreateLogger();


#endregion

#region DbContext Operations

builder.Services.AddDbContext<InternshipAutomationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerLocalhost"));
});

#endregion

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
