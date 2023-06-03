using FiorelloOneToMany.Data;
using FiorelloOneToMany.Models;
using FiorelloOneToMany.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FiorelloOneToMany.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        public ProductService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context.Products.Include(m => m.Image).Where(m => m.SoftDelete).ToListAsync();
        }

        public async Task<Product> GetByIdAync(int? id) => await _context.Products.FindAsync(id);

        public Task<Product> GetByIdAsync(int? id)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> GetByIdWithImagesAsync(int? id)
        {
           return await _context.Products.Include(m => m.Image).FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
