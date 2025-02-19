using Order.Models;

namespace Order.Mapping;

public static class ApiModelToEntityDataMapper
{
    public static Data.Entities.Order ToOrder(this CreateOrderRequest request)
    {
        return new Data.Entities.Order
        {
            CustomerName = request.CustomerName,
            ProductRefNo = request.ProductRefNo,
            Quantity = request.Quantity,
            TotalPrice = request.TotalPrice
        };
    }
}