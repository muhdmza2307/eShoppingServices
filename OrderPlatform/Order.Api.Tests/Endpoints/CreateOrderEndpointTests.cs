using AutoFixture;
using FastEndpoints;
using Moq;
using Order.Api.Endpoints;
using Order.Models;
using Order.Repositories.Repositories.Interfaces;
using Shouldly;
using Xunit;

namespace Order.Api.Tests.Endpoints;

public class CreateOrderEndpointTests
{
    private readonly Fixture _fixture;
    private readonly Mock<IOrderRepository> _mockOrderRepository;

    public CreateOrderEndpointTests()
    {
        // Setup
        _fixture = new Fixture();
        _mockOrderRepository = new Mock<IOrderRepository>();
    }
    
    [Fact]
    public async Task HandleAsync_ShouldCreateOrderAndReturnResponse()
    {
        // Arrange
        var orderId = _fixture.Create<Guid>();
        var orderEntity = _fixture.Build<Data.Entities.Order>()
            .With(a => a.Id, orderId)
            .Create();
        var request = _fixture.Create<CreateOrderRequest>();
        
        _mockOrderRepository
            .Setup(s => 
                s.UpsertAsync(It.IsAny<Data.Entities.Order>()))
            .ReturnsAsync(orderEntity);
        
        var ep = Factory.Create<CreateOrderEndpoint>(_mockOrderRepository.Object);

        // Act
        await ep.HandleAsync(request, default);
        
        var response = ep.Response;
        
        // Assert
        response.ShouldNotBeNull();
        response.Id.ShouldBe(orderId);
        
        _mockOrderRepository.Verify(s => 
            s.UpsertAsync(It.IsAny<Data.Entities.Order>())
        , Times.Once);
    }
}