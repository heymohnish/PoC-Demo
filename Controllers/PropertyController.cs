using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PoC_Demo.Model;
using PoC_Demo.Repository.Interface;
using PoC_Demo.Services;

namespace PoC_Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : Controller
    {
        private readonly IPropertyRepository _propertyRepository;

        public PropertyController(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }
        [Authorize]
        [HttpPost("AddProperty")]
        public async Task<IActionResult> AddProperty([FromBody] Property property)
        {
            var result = await _propertyRepository.AddProperty(property);
            return Ok(result);
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            var result = await _propertyRepository.AddUser(user);
            return Ok(result);
        }
        [HttpGet("GetAllProperty")]
        public async Task<IActionResult> GetAllProperty()
        {
            var result = await _propertyRepository.GetAllProperties();
            return Ok(result);
        }
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetPropertyById(int id)
        {
            var result = await _propertyRepository.GetPropertyById( id);
            return Ok(result);
        }
        [Authorize]
        [HttpGet("GetSeller/{id}")]
        public async Task<IActionResult> GetSellerById(int id)
        {
            var result = await _propertyRepository.GetUserById(id);
            return Ok(result);
        }
        [Authorize]
        [HttpGet("GetByProperty/{propertyId}")]
        public async Task<IActionResult> GetFavoritesByPropertyId(int propertyId)
        {
            var favorites = await _propertyRepository.GetFavoriteByPropertyId(propertyId);

            if (favorites == null)
            {
                return NotFound();
            }

            return Ok(favorites);
        }
        [Authorize]
        [HttpPost("AddFavorite")]
        public async Task<IActionResult> AddFavorite([FromBody] Favorite favorite)
        {
            var result = await _propertyRepository.AddFavorite(favorite);
            return Ok(result);
        }
        [Authorize]
        [HttpPost("AddInterest")]
        public async Task<IActionResult> AddInterest([FromBody] Interest interest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                bool success = await _propertyRepository.AddInterest(interest);

                if (!success)
                {
                    return StatusCode(500, "Failed to add interest.");
                }

                return Ok("Interest added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        // GET: api/Interest/Property/{propertyId}
        [Authorize]
        [HttpGet("Property/{propertyId}")]
        public async Task<IActionResult> GetInterestsByPropertyId(int propertyId)
        {
            try
            {
                List<Interest> interests = await _propertyRepository.GetInterestsByPropertyId(propertyId);

                if (interests == null)
                {
                    return NotFound();
                }

                return Ok(interests);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
