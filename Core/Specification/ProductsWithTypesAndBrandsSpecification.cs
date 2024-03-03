using System.Linq.Expressions;
using Core.Entities;
using Infrastructure;

namespace Core;

public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
{
    public ProductsWithTypesAndBrandsSpecification()
    {
        AddInClude(p => p.ProductBrand);
        AddInClude(p => p.ProductType);
    }

    public ProductsWithTypesAndBrandsSpecification(int id) : base(p => p.ProductTypeId == id)
    {
        AddInClude(p => p.ProductBrand);
        AddInClude(p => p.ProductType);
    }
}
