using AutoFixture;
using Moq;
using Order.Data;
using Order.Repositories.Repositories;
using Order.Repositories.Repositories.Interfaces;
using Shouldly;
using Xunit;

namespace Order.Repositories.Tests.Repositories;

public class OrderRepositoryTests
{
    private readonly OrderRepository _orderRepository;
    private readonly Data.Entities.Order _order;
    
    public OrderRepositoryTests()
    {
        // Setup
        var fixture = new Fixture();
        Mock<IDataContext> mockDbContext = new();
        Mock<IEntityStateManager> mockEntityStateManager = new();

        _order = fixture.Build<Data.Entities.Order>()
            .Create();

        IEnumerable<Data.Entities.Order> orders = new[]
        {
            _order
        };
        
        mockDbContext
            .Setup(x => x.Set<Data.Entities.Order>())
            .Returns(() =>
            {
                var mockDbSet = MockDbSet.GetMockDbSet(orders);
                return mockDbSet.Object;
            });
        
        _orderRepository = new OrderRepository(mockEntityStateManager.Object, mockDbContext.Object);
    }
    
    [Fact]
    public async Task CreateOrderAsync_ShouldReturnOrder()
    {
        var result = await _orderRepository.UpsertAsync(_order);

        result.ShouldBeAssignableTo<Data.Entities.Order>();
        result.ShouldBe(_order);
    }
}