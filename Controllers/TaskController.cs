using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PoC_Demo.Repository;
using PoC_Demo.Repository.Interface;

namespace PoC_Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public TaskController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet("GetTask")]
        public async Task<IActionResult> GetTask(DateTime? date)
        {
            var result= await _productRepository.GetTask(date);
            return result.Count != 0 ? Ok(result) : BadRequest("Could you give me the exact date...?");
        }
    }
}
