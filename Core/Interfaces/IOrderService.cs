

using Core.Entities.OrderAggregate;

namespace Core.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, AddressOrder shipToAddress, string basketId);

        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();

        Task<Order> GetOrderUserById(int id, string buyerEmail);

        Task<IReadOnlyList<Order>> GetOrderForUser(string buyerEmail);
    }
}