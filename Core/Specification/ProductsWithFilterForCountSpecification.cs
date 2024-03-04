using System.Linq.Expressions;
using Core.Entities;
using Infrastructure;

namespace Core;

public class ProductWithFilterForCountSpecification : BaseSpecification<Product>
{

    // Phần này chỉ tính số lượng phần tử thôi nên k cần thêm Includes vào 
    public ProductWithFilterForCountSpecification(ProductSpecParams productParams) : base(x =>
    (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
        (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId)
        && (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)
    )
    {


    }


}
