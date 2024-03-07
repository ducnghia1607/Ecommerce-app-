namespace Core;

public interface IBasketRepository
{
    Task<CustomerBasket> GetBasketAsync(string basketId);

    Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket);

    Task<bool> DeteleBasketAsync(string basketId);
}
