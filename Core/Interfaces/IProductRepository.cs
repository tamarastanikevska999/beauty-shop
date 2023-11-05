using Core.Entities;
using Core.Util;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetProductByIdAsync(int id);
        Task<PagedProductList> GetProductsAsync(int? id, int? fromAmount, int? toAmount , string name, string type, string brand, Paging paging, Sorting sorting);
        Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync(string name);
        Task<IReadOnlyList<ProductType>> GetProductTypesAsync(string name);
    }

}