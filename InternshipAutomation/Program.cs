using InternshipAutomation.Application.Repository.GeneralRepository;
using InternshipAutomation.Domain.User;
using InternshipAutomation.Persistance.Context;
using IntershipOtomation.Domain.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IGeneralRepository, GeneralRepository<InternshipAutomationDbContext>>();
builder.Services.AddIdentity<User, AppRole>().AddEntityFrameworkStores<InternshipAutomationDbContext>();
builder.Services.AddDbContext<InternshipAutomationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerLocalhost"));
});
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();