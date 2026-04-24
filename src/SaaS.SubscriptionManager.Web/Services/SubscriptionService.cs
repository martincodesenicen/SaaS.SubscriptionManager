using SaaS.SubscriptionManager.Application.Subscriptions.Queries;
using System.Net.Http.Json;

namespace SaaS.SubscriptionManager.Web.Services;

public class SubscriptionService
{
    private readonly HttpClient _http;

    public SubscriptionService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<SubscriptionResponse>> GetSubscriptionsAsync()
    {
        // Esta ruta debe coincidir con la de tu Controller
        return await _http.GetFromJsonAsync<List<SubscriptionResponse>>("api/subscriptions") ?? new();
    }
}