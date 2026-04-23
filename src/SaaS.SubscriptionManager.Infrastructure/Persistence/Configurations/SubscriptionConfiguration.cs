using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaaS.SubscriptionManager.Domain.Entities;
namespace SaaS.SubscriptionManager.Infrastructure.Persistence.Configurations;
public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder.ToTable("Subscriptions"); // nombre de la tabla
        builder.HasKey(s => s.Id); // Primary key
        builder.Property(s => s.UserId).IsRequired(); // marcar userid como requerido
        builder.Property(s => s.Status).HasConversion<string>().HasMaxLength(20).IsRequired(); // pasar de enum a string en la bd
        builder.Property(s => s.ExpirationDate).IsRequired(false); // puede ser null
    }
}