using SaaS.SubscriptionManager.Domain.Interfaces;
using Microsoft.EntityFrameworkCore; // Necesario para AddDbContext
using SaaS.SubscriptionManager.Infrastructure.Persistence; // Donde está ApplicationDbContext
using SaaS.SubscriptionManager.Infrastructure.Repositories; // Donde está SubscriptionRepository
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// 1. Configurar la Base de Datos (SQL Server)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Registrar el Repositorio y Unit of Work
builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
// Esto le dice a .NET: "Cuando alguien pida IUnitOfWork, dale el DbContext actual"
builder.Services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

// 3. Registrar MediatR (Escanea el proyecto Application para encontrar Handlers)
builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(typeof(SaaS.SubscriptionManager.Application.Subscriptions.Commands.CreateSubscriptionCommand).Assembly));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();