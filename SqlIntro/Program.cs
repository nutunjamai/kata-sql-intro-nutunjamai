using System;
using System.Configuration;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace SqlIntro
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
#if DEBUG
                .AddJsonFile($"appsettings.Debug.json")
#else
                .AddJsonFile($"appsettings.Release.json")
#endif
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var repo = new DapperProductRepository(connectionString);

            foreach (var prod in repo.GetProducts())
            {
                Console.WriteLine("Product Name:" + prod.Name);
            }

            foreach (var prod in repo.GetProductsWithReview())
            {
                Console.WriteLine("\nProduct Name:" + prod.Name + "\nProduct Review:" + prod.Comments);
            }

            foreach (var prod in repo.GetProductsAndReviews())
            {
                Console.WriteLine("\nProduct Name:" + prod.Name + "\nProduct Review (if available):" + prod.Comments);
            }

            Console.ReadLine();
        }
    }
}