using SaaS.SubscriptionManager.Domain.Interfaces;
using Microsoft.EntityFrameworkCore; // Necesario para AddDbContext
using SaaS.SubscriptionManager.Infrastructure.Persistence; // Donde está ApplicationDbContext
using SaaS.SubscriptionManager.Infrastructure.Repositories; // Donde está SubscriptionRepository
using System.Reflection;
using FluentValidation;
using SaaS.SubscriptionManager.Application.Common.Behaviors;
using MediatR;
using SaaS.SubscriptionManager.Application.Subscriptions.Commands;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
// Esto le dice a .NET: "Cuando alguien pida IUnitOfWork, dale el DbContext actual"
builder.Services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

builder.Services.AddValidatorsFromAssembly(typeof(SaaS.SubscriptionManager.Application.Common.Behaviors.ValidationBehavior<,>).Assembly);

builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(typeof(SaaS.SubscriptionManager.Application.Subscriptions.Commands.CreateSubscriptionCommand).Assembly));

builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(CreateSubscriptionCommand).Assembly);

cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
});

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