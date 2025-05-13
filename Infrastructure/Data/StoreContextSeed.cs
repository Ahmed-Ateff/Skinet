using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context)
        {
            if (!context.Products.Any())
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(),".." ,"Infrastructure", "Data", "SeedData", "products.json");
                var productsData =  File.ReadAllText(path);
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                if (products == null) return;
                context.Products.AddRange(products);
                await context.SaveChangesAsync();
            }
        }
    }
}
