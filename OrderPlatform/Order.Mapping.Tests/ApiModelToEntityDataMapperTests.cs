using AutoFixture;
using Order.Models;
using Shouldly;
using Xunit;

namespace Order.Mapping.Tests;

public class ApiModelToEntityDataMapperTests
{
    private readonly Fixture _fixture;
    
    public ApiModelToEntityDataMapperTests()
    {
        // Setup
        _fixture = new Fixture();
    }

    [Fact]
    public void ToOrder_ShouldMapCreateOrderRequestToOrderEntity()
    {
        // Arrange
        var request = _fixture.Create<CreateOrderRequest>();

        // Act
        var order = request.ToOrder();

        // Assert
        order.ShouldNotBeNull();
        order.CustomerName.ShouldBe(request.CustomerName);
        order.ProductRefNo.ShouldBe(request.ProductRefNo);
        order.Quantity.ShouldBe(request.Quantity);
        order.TotalPrice.ShouldBe(request.TotalPrice);
    }
}