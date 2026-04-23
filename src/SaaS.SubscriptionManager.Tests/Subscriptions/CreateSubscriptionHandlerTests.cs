using Moq;
using SaaS.SubscriptionManager.Application.Subscriptions.Commands;
using SaaS.SubscriptionManager.Domain.Entities;
using SaaS.SubscriptionManager.Domain.Interfaces;
using Xunit;
namespace SaaS.SubscriptionManager.Tests.Subscriptions;
public class CreateSubscriptionHandlerTests
{
    [Fact]
    public async Task Handle_Should_CreateSubscription_And_ReturnGuid()
    {
        // ARRANGE
        var repoMock = new Mock<ISubscriptionRepository>();
        var handler = new CreateSubscriptionHandler(repoMock.Object);
        var command = new CreateSubscriptionCommand(Guid.NewGuid());

        // Ejecutamos la acción
        var result = await handler.Handle(command, CancellationToken.None);

        // Verificamos
        Assert.NotEqual(Guid.Empty, result);

        // Verificamos el llamado a AddAsync
        repoMock.Verify(r => r.AddAsync(It.IsAny<Subscription>()), Times.Once);
    }
}