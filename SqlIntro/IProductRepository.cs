using System.Collections.Generic;

namespace SqlIntro
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts();
        void DeleteProduct(int id);
        void UpdateProduct(Product prod);
        void InsertProduct(Product prod);
        IEnumerable<Product> GetProductsWithReview();
        IEnumerable<Product> GetProductsAndReviews();


    }
}