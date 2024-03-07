using Core;

namespace Infrastructure;

public class BasketRepository : IBasketRepository
{
    public Task<bool> DeteleBasketAsync(string basketId)
    {
        throw new NotImplementedException();
    }

    public Task<CustomerBasket> GetBasketAsync(string basketId)
    {
        throw new NotImplementedException();
    }

    public Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
    {
        throw new NotImplementedException();
    }
}
