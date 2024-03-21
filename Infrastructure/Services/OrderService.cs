
using Core;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Core.Specification;
namespace Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _uow;
        private readonly IBasketRepository _basketRepo;
        public OrderService(IUnitOfWork uow, IBasketRepository basketRepo)
        {
            _uow = uow;
            _basketRepo = basketRepo;
        }

        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, AddressOrder shipToAddress, string basketId)
        {
            // Get basket from repo
            var basket = await _basketRepo.GetBasketAsync(basketId);
            if (basket == null) return null;

            var items = new List<OrderItem>();
            //get item for the product repo
            foreach (var item in basket.Items)
            {
                // Lấy thông tin product từ database để có thể dùng chính xác price được lưu trong db 
                var productItem = await _uow.Repository<Product>().GetByIdAsync(item.Id);
                var productOrder = new ProductItemOrder(productItem.Id, productItem.Name, productItem.PictureUrl);
                var orderItem = new OrderItem(productOrder, productItem.Price, item.Quantity);

                items.Add(orderItem);
            }
            // Get deliverymethod 
            var deliveryMethod = await _uow.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);
            // calculate  subtotal 

            var subtotal = items.Sum(item => item.Quantity * item.Price);

            // check paymentIntent
            var spec = new OrderByPaymentIntentSpecification(basket.PaymentIntentId);
            var order = await _uow.Repository<Order>().GetEntityWithSpec(spec);
            if (order != null)
            {
                order.DeliveryMethod = deliveryMethod;
                order.ShipToAddress = shipToAddress;
                order.PaymentIntentId = basket.PaymentIntentId;
                order.Subtotal = subtotal;
                _uow.Repository<Order>().Update(order);
            }
            else
            {
                //create  order
                order = new Order(buyerEmail, items, shipToAddress, deliveryMethod, subtotal, basket.PaymentIntentId);
                _uow.Repository<Order>().Add(order);
            }


            // save to db
            var result = await _uow.Complete();
            if (result <= 0) return null;

            // delete basket;


            return order;
        }



        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            return await _uow.Repository<DeliveryMethod>().ListAllSync();
        }

        public async Task<IReadOnlyList<Order>> GetOrderForUser(string buyerEmail)
        {
            var spec = new OrdersWithItemsAndDeliverySpecification(buyerEmail);

            return await _uow.Repository<Order>().ListAsync(spec);
        }

        public async Task<Order> GetOrderUserById(int id, string buyerEmail)
        {
            var spec = new OrdersWithItemsAndDeliverySpecification(id, buyerEmail);
            return await _uow.Repository<Order>().GetEntityWithSpec(spec);
        }
    }
}