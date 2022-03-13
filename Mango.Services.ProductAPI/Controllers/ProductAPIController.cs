using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Models.Dto;
using Mango.Services.ProductAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mango.Services.ProductAPI.Controllers
{
    [Route("api/products")]
    public class ProductAPIController : ControllerBase
    {
        protected ResponseDto response;
        private IProductRepository productRepository;

        public ProductAPIController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
            this.response = new ResponseDto();
        }
       
        [HttpGet]
        public async Task<object> Get()
        {
            try
            {
                IEnumerable<ProductDto> productDtos = await this.productRepository.GetProducts();
                this.response.Result = productDtos;
            }
            catch (Exception ex)
            {
                this.response.IsSuccess = false;
                this.response.ErrorMessage
                    = new List<string>() { ex.ToString() };
            }

            return this.response;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<object> Get(int id)
        {
            try
            {
                ProductDto productDto = await this.productRepository.GetProductById(id);
                this.response.Result = productDto;
            }
            catch (Exception ex)
            {
                this.response.IsSuccess = false;
                this.response.ErrorMessage
                    = new List<string>() { ex.ToString() };
            }

            return this.response;
        }

        [HttpPost]
        public async Task<object> Post([FromBody] ProductDto productDto)
        {
            try
            {
                ProductDto model = await this.productRepository.CreateUpdateProduct(productDto);
                this.response.Result = model;
            }
            catch (Exception ex)
            {
                this.response.IsSuccess = false;
                this.response.ErrorMessage
                    = new List<string>() { ex.ToString() };
            }

            return this.response;
        }

        [HttpPut]
        public async Task<object> Put([FromBody] ProductDto productDto)
        {
            try
            {
                ProductDto model = await this.productRepository.CreateUpdateProduct(productDto);
                this.response.Result = model;
            }
            catch (Exception ex)
            {
                this.response.IsSuccess = false;
                this.response.ErrorMessage
                    = new List<string>() { ex.ToString() };
            }

            return this.response;
        }

        [HttpDelete]
        public async Task<object> Delete(int id)
        {
            try
            {
                bool isSuccess = await this.productRepository.DeleteProduct(id);
                this.response.Result = isSuccess;
            }
            catch (Exception ex)
            {
                this.response.IsSuccess = false;
                this.response.ErrorMessage
                    = new List<string>() { ex.ToString() };
            }

            return this.response;
        }
    }
}
