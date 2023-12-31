using Core.Entities;
using Core.Interfaces;
using Core.Util;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRepository: IProductRepository
    {
        private readonly StoreContext _context;
        public ProductRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.ProductType)
                .Include(p => p.ProductBrand)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PagedProductList> GetProductsAsync(int? id, int? fromAmount, int? toAmount, string name, string type, string brand, Paging paging, Sorting sorting)
        {
            var products = _context.Products
                .Include(p => p.ProductType)
                .Include(p => p.ProductBrand)
                .AsQueryable();
            if (id != null && id > 0)
            {
                products = products.Where(x => x.Id == id);
            }
            if(!String.IsNullOrEmpty(name)){
                products = products.Where(x => !string.IsNullOrEmpty(x.Name) && x.Name.ToLower().StartsWith(name.ToLower()));
            }
            if(!String.IsNullOrEmpty(type)){
                products = products.Where(x => !string.IsNullOrEmpty(x.ProductType.Name) && x.ProductType.Name.ToLower().StartsWith(type.ToLower()));
            }
            if(!String.IsNullOrEmpty(brand)){
                products = products.Where(x => !string.IsNullOrEmpty(x.ProductBrand.Name) && x.ProductBrand.Name.ToLower().StartsWith(brand.ToLower()));
            }
            if (fromAmount != null && fromAmount > 0)
            {
                products = products.Where(x => x.Price >= fromAmount);
            }
            if (toAmount != null && toAmount > 0)
            {
                products = products.Where(x => x.Price <= toAmount);
            }

            string sortBy = !String.IsNullOrEmpty(sorting.SortType)? sorting.SortType : "id";
            string sortOrder = sorting.SortOrder == "asc" ? "asc" : "desc";

            if (sortOrder == "desc")
            {
                if (sortBy == "price")
                    products = products.OrderByDescending(x => x.Price);
                else if (sortBy == "type")
                    products = products.OrderByDescending(x => x.ProductType);
                else if (sortBy == "brand")
                    products = products.OrderByDescending(x => x.ProductBrand);
                else
                    products = products.OrderByDescending(x => x.Id);
            }
            else
            {
                if (sortBy == "price")
                    products = products.OrderBy(x => x.Price);
                else if (sortBy == "type")
                    products = products.OrderBy(x => x.ProductType);
                else if (sortBy == "brand")
                    products = products.OrderBy(x => x.ProductBrand);
                else
                    products = products.OrderBy(x => x.Id);
            }

            int totalCount = products.Count();
            int page = paging.PageNumber > 0 ? paging.PageNumber : 1;
            int pageSize = paging.PageSize > 0 ? paging.PageSize : 10;

            if (paging != null)
            {
                products = products.Skip(pageSize * (page - 1)).Take(pageSize);
            }

            var list = await products.ToListAsync();
            PagedProductList paged = new PagedProductList();
            decimal tmp = (decimal)totalCount / pageSize;
            paged.TotalCount = totalCount;
            paged.PageSize = pageSize;
            paged.TotalPages = (int)Math.Ceiling(tmp);
            paged.Page = page;
            paged.Items = list;
            paged.SortBy = sortBy;
            paged.SortOrder = sortOrder;

            return paged;
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync(string name)
        {
            var types = await _context.ProductTypes.OrderBy(x => x.Name).ToListAsync();
            if(!String.IsNullOrEmpty(name)){
                types = types.Where(x => !string.IsNullOrEmpty(x.Name) && x.Name.ToLower().StartsWith(name.ToLower())).ToList();
            }
            return types;
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync(string name)
        {
            var brands = await _context.ProductBrands.OrderBy(x => x.Name).ToListAsync();
            if(!String.IsNullOrEmpty(name)){
                brands = brands.Where(x => !string.IsNullOrEmpty(x.Name) && x.Name.ToLower().StartsWith(name.ToLower())).ToList();
            }
            return brands;
        }

        public async Task<IReadOnlyList<ProductReview>> GetProductReviewsAsync(int id)
        {
            return await _context.ProductReviews.Where(a=>a.ProductId==id).ToListAsync();
        }

        public async Task<Product> GetTopRatedProductAsync()
        {
            var reviews = await _context.ProductReviews.ToListAsync();
            if (reviews.Count() == 0)
                return null;
            var prod = reviews[0].ProductId;
            float max = 0;
            foreach(var r in reviews)
            {
                var avg = GetProductRatingAsync(r.Id).Result;
                if (avg > max)
                {
                    max = avg;
                    prod = r.Id;
                }
            }
            var product = GetProductByIdAsync(prod).Result;
            return product;

        }

        public async Task<float> GetProductRatingAsync(int productId)
        {
            var reviews = await _context.ProductReviews.Where(a=>a.ProductId==productId).ToListAsync();
            float sum = 0;
            foreach (var r in reviews)
                sum = sum + r.Rating;
            if (reviews.Count() > 0)
                return sum / reviews.Count;
            else
                return 0;
        }

        public async Task AddProductReviewAsync(int productId, string userEmail, string comment, int rating)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == productId);

            if (product != null)
            {
                ProductReview review = new ProductReview
                {
                    ProductId = product.Id,
                    UserEmail = userEmail,
                    Comment = comment,
                    Rating = rating
                };

                product.Reviews.Add(review);
                _context.ProductReviews.Add(review);
                _context.Products.Update(product);

                await _context.SaveChangesAsync(); // Use async SaveChanges
            }
        }
    }
}