using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PoC_Demo.DTO;
using PoC_Demo.Model;
using PoC_Demo.Repository;
using PoC_Demo.Repository.Interface;
using PoC_Demo.Services.Interface;


namespace PoC_Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly IProductRepository _productRepository;
        private readonly JWTSettings _jWTSettings;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;


        public LoginController(JWTSettings jWTSettings, ITokenService tokenService, IConfiguration configuration, IProductRepository productRepository)
        {
            _productRepository = productRepository;
            _configuration = configuration;
            _jWTSettings = jWTSettings;
            _tokenService = tokenService;
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(Login login)
        {
            var result = await _productRepository.ValidateUser(login);
            if(result)
            {
                var token = _tokenService.CreateToken();
                Response.Headers.Add("Authorization", "Bearer " + token);
                return Ok("Login Success");
            }
            else
            {
                return BadRequest("Wrong username/password");
            }
        }

    }
}
