using API.Controllers;
using AutoMapper;
using Core;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API;


public class ProductsController : BaseApiController
{
    private readonly IGenericRepository<Product> _productsRepo;
    private readonly IGenericRepository<ProductBrand> _productBrandRepo;
    private readonly IGenericRepository<ProductType> _productTypeRepo;
    private readonly IMapper _mapper;

    public ProductsController(IGenericRepository<Product> productsRepo, IGenericRepository<ProductBrand> productBrandRepo,
    IGenericRepository<ProductType> productTypeRepo, IMapper mapper)
    {
        _mapper = mapper;
        _productTypeRepo = productTypeRepo;
        _productsRepo = productsRepo;
        _productBrandRepo = productBrandRepo;
    }

    [HttpGet]
    public async Task<ActionResult<Pagination<ProductDto>>> GetProducts([FromQuery] ProductSpecParams productParams)
    {
        var productSpec = new ProductsWithTypesAndBrandsSpecification(productParams);
        var products = await _productsRepo.ListAsync(productSpec);
        var countSpect = new ProductWithFilterForCountSpecification(productParams);
        var totalItems = await _productsRepo.CountAsync(countSpect);
        var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDto>>(products);
        return Ok(new Pagination<ProductDto>(productParams.PageSize, productParams.PageIndex, totalItems, data));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var spec = new ProductsWithTypesAndBrandsSpecification(id);
        var product = await _productsRepo.GetEntityWithSpec(spec);
        if (product == null) return NotFound(new ApiResponse(404));
        return Ok(_mapper.Map<Product, ProductDto>(product));
    }

    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
    {
        return Ok(await _productBrandRepo.ListAllSync());
    }


    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductTypes()
    {
        return Ok(await _productTypeRepo.ListAllSync());
    }
}
