using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PoC_Demo.Model;
using PoC_Demo.Repository;
using PoC_Demo.Repository.Interface;
using PoC_Demo.Services;

namespace PoC_Demo.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly EmailService _emailService;

        public ProductController(IProductRepository productRepository, EmailService emailService) 
        {
            _emailService = emailService;
            _productRepository = productRepository;
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct(Product product)
        {
            var result = await _productRepository.AddProductAsync(product);
            _ = _emailService.SendMail(product, "New Product added");
            return result ? Ok("Product added successfully") :BadRequest("Something went wrong");
        }


        [HttpGet("GetProduct")]
        public async Task<IActionResult> GetProduct()
        {
            var result = await _productRepository.GetProducts();
            return Ok(result);
        }

        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            var result = await _productRepository.UpdateProductAsync(product);
            return Ok(result);
        }

        [HttpDelete("RemoveProduct")]
        public async Task<IActionResult> RemoveProduct(int id)
        {
            var result = await _productRepository.RemoveProduct(id);
            return Ok(result);
        }
    }
}
