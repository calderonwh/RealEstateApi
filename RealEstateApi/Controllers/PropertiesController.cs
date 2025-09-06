using Microsoft.AspNetCore.Mvc;
using RealEstateApi.Dtos;
using RealEstateApi.Services;
using RealEstateApi.Models;

namespace RealEstateApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertiesController : ControllerBase
    {
        private readonly PropertyService _service;

        public PropertiesController(PropertyService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<Property>> CreateProperty([FromBody] CreatePropertyDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _service.CreatePropertyAsync(dto);
            return CreatedAtAction(nameof(GetPropertyById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProperty(int id, [FromBody] UpdatePropertyDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _service.UpdatePropertyAsync(id, dto);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        [HttpPut("{id}/price")]
        public async Task<IActionResult> UpdatePrice(int id, [FromBody] UpdatePriceDto dto)
        {
            if (dto.Price <= 0)
                return BadRequest("El precio debe ser mayor a cero.");

            var updated = await _service.UpdatePriceAsync(id, dto.Price);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        [HttpPost("{id}/images")]
        public async Task<IActionResult> AddImage(int id, [FromBody] AddImageDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var image = await _service.AddImageAsync(id, dto);
            if (image == null)
                return NotFound("Propiedad no encontrada.");

            return Ok(image);
        }

        [HttpGet]
        public async Task<ActionResult<List<Property>>> GetProperties(
        [FromQuery] decimal? priceMin,
        [FromQuery] decimal? priceMax,
        [FromQuery] string? address)
        {
            var result = await _service.GetPropertiesAsync(priceMin, priceMax, address);
            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Property>> GetPropertyById(int id)
        {     
            return Ok();
        }
    }
}
