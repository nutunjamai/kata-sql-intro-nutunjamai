using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks.Dataflow;
using Dapper;
using MySql.Data.MySqlClient;

namespace SqlIntro
{
    public class DapperProductRepository : IProductRepository
    {
        private readonly string _connectionString;

        public DapperProductRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Product> GetProducts()
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                return conn.Query<Product>("SELECT ProductID AS id, Name FROM product;");
            }
        }

        public void DeleteProduct(int id)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                conn.Execute("DELETE FROM product WHERE ProductId = @id", new { id });
            }
        }
        public void InsertProduct(Product prod)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                conn.Execute("INSERT INTO product (name) values(@name)", new { name = prod.Name });
            }
        }

        public void UpdateProduct(Product prod)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                conn.Execute("UPDATE product SET name = @name WHERE id = @id", new { name = prod.Name, id = prod.Id });
            }
        }

        public IEnumerable<Product> GetProductsWithReview()
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                return conn.Query<Product>("SELECT p.ProductId, p.Name, pr.Comments FROM product AS p " +
                                           "INNER JOIN productreview AS pr ON p.ProductId = pr.ProductId;");
            }
        }

        public IEnumerable<Product> GetProductsAndReviews()
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                return conn.Query<Product>("SELECT p.ProductId, p.Name, pr.Comments FROM product AS p " +
                                           "LEFT OUTER JOIN productreview AS pr ON p.ProductId = pr.ProductId;");
            }
        }
    }
}