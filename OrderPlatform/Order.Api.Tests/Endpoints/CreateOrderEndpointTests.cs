using AutoFixture;
using FastEndpoints;
using Moq;
using Order.Api.Endpoints;
using Order.Models;
using Order.Services.Interfaces;
using Shouldly;
using Xunit;

namespace Order.Api.Tests.Endpoints;

public class CreateOrderEndpointTests
{
    private readonly Fixture _fixture;
    private readonly Mock<IOrderService> _mockOrderService;

    public CreateOrderEndpointTests()
    {
        // Setup
        _fixture = new Fixture();
        _mockOrderService = new Mock<IOrderService>();
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
        
        _mockOrderService
            .Setup(s => 
                s.CreateAsync(It.IsAny<Data.Entities.Order>()))
            .ReturnsAsync(orderEntity);
        
        var ep = Factory.Create<CreateOrderEndpoint>(_mockOrderService.Object);

        // Act
        await ep.HandleAsync(request, default);
        
        var response = ep.Response;
        
        // Assert
        response.ShouldNotBeNull();
        response.Id.ShouldBe(orderId);
        
        _mockOrderService.Verify(s => 
            s.CreateAsync(It.IsAny<Data.Entities.Order>())
        , Times.Once);
    }
}