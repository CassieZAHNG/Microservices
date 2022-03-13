using AutoMapper;
using Mango.Services.ProductAPI.DbContexts;
using Mango.Services.ProductAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mango.Services.ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext db;
        private IMapper mapper;

        public ProductRepository(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<ProductDto> CreateUpdateProduct(ProductDto productDto)
        {
            Product product = this.mapper.Map<ProductDto, Product>(productDto);
            if(product.ProductId > 0)
            {
                this.db.Products.Update(product);
            }
            else
            {
                this.db.Products.Add(product);
            }
            await this.db.SaveChangesAsync();
            return this.mapper.Map<Product, ProductDto>(product);
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            try
            {
                Product product = await this.db.Products.FirstOrDefaultAsync(u => u.ProductId == productId);
                if(product == null)
                {
                    return false;
                }
                this.db.Products.Remove(product);
                await this.db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ProductDto> GetProductById(int productId)
        {
            Product product = await this.db.Products.Where(x=>x.ProductId==productId).FirstOrDefaultAsync();
            return this.mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            List<Product> productList = await this.db.Products.ToListAsync();
            return this.mapper.Map<List<ProductDto>>(productList);
        }
    }
}
