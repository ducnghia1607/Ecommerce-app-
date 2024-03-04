using System.Linq.Expressions;
using Core.Entities;
using Infrastructure;

namespace Core;

public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
{
    public ProductsWithTypesAndBrandsSpecification(ProductSpecParams productParams) : base(x =>
        (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
        (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId)
        && (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)
    )
    {
        AddInClude(p => p.ProductBrand);
        AddInClude(p => p.ProductType);
        AddOrderBy(p => p.Name);
        ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);
        if (!string.IsNullOrEmpty(productParams.Sort))
        {
            switch (productParams.Sort)
            {
                case "priceAsc":
                    AddOrderBy(p => p.Price);
                    break;
                case "priceDesc":
                    AddOrderByDescending(p => p.Price);
                    break;
                default:
                    AddOrderBy(p => p.Name);
                    break;
            }
        }

    }

    public ProductsWithTypesAndBrandsSpecification(int id) : base(p => p.ProductTypeId == id)
    {
        AddInClude(p => p.ProductBrand);
        AddInClude(p => p.ProductType);
    }
}
