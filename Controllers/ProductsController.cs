using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using TrinchPhotosAPI.Data;
using TrinchPhotosAPI.Data.Models;

namespace TrinchPhotosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public ProductsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Products>>> GetProducts()
        {
            var products = await _context.Products.Select(p => new Products
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Category = p.Category,
                DisplayImageURL = p.DisplayImageURL,
                CreatorId = p.CreatorId,
                StripePriceId = p.StripePriceId,
                Price = p.Price
            }).ToListAsync();

            foreach(var product in products)
            {
                try
                {
                    // Update price on database when product is retrieved
                    var service = new PriceService();
                    if(product.Price != service.Get(product.StripePriceId).UnitAmount.Value)
                    {
                        product.Price = service.Get(product.StripePriceId).UnitAmount.Value;
                        _context.Products.FirstAsync(p => p.Id == product.Id).Result.Price = product.Price;
                        _context.SaveChanges();
                        Console.WriteLine($"Updated price for product {product.Id} to {product.Price}");
                    }

                } catch(StripeException ex)
                {
                    Console.WriteLine($"Error retrieving price for product {product.Id}: {ex.Message}");
                }
            }

            return products;
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Products>> GetProducts(int id)
        {
            var products = await _context.Products.FindAsync(id);

            if (products == null)
            {
                return NotFound();
            }

            return new Products
            {
                Id = products.Id,
                Name = products.Name,
                Description = products.Description,
                Category = products.Category,
                DisplayImageURL = products.DisplayImageURL,
                CreatorId = products.CreatorId,
                StripePriceId = products.StripePriceId,
                Price = products.Price

            };
        }

        private bool ProductsExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
