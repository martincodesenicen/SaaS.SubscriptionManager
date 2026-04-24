using Microsoft.EntityFrameworkCore;
using SaaS.SubscriptionManager.Domain.Entities;
using SaaS.SubscriptionManager.Domain.Interfaces;
using SaaS.SubscriptionManager.Infrastructure.Persistence;
namespace SaaS.SubscriptionManager.Infrastructure.Repositories;
public class SubscriptionRepository : ISubscriptionRepository
{
    private readonly ApplicationDbContext _context;
    public SubscriptionRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Subscription?> GetByIdAsync(Guid id)
    {
        return await _context.Subscriptions.FindAsync(id);
    }
    public async Task AddAsync(Subscription subscription)
    {
        await _context.Subscriptions.AddAsync(subscription);
    }
    public async Task UpdateAsync(Subscription subscription)
    {
        _context.Subscriptions.Update(subscription);
        await _context.SaveChangesAsync(); 
    }
}