using Microsoft.EntityFrameworkCore;
using RealEstateApi.Data;
using RealEstateApi.Dtos;
using RealEstateApi.Models;

namespace RealEstateApi.Services
{
    public class PropertyService
    {
        private readonly ApplicationDbContext _context;

        public PropertyService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Property> CreatePropertyAsync(CreatePropertyDto dto)
        {
            var property = new Property
            {
                Title = dto.Title,
                Description = dto.Description,
                Address = dto.Address,
                Price = dto.Price,
                CreatedAt = DateTime.UtcNow
            };

            _context.Properties.Add(property);
            await _context.SaveChangesAsync();

            return property;
        }

        public async Task<Property?> UpdatePropertyAsync(int id, UpdatePropertyDto dto)
        {
            var property = await _context.Properties.FindAsync(id);

            if (property == null)
                return null;

            property.Title = dto.Title;
            property.Description = dto.Description;
            property.Address = dto.Address;
            property.Price = dto.Price;
            property.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return property;
        }

        public async Task<Property?> UpdatePriceAsync(int id, decimal newPrice)
        {
            var property = await _context.Properties.FindAsync(id);

            if (property == null)
                return null;

            property.Price = newPrice;
            property.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return property;
        }

        public async Task<PropertyImage?> AddImageAsync(int propertyId, AddImageDto dto)
        {
            var property = await _context.Properties.FindAsync(propertyId);
            if (property == null)
                return null;

            var image = new PropertyImage
            {
                PropertyId = propertyId,
                ImageUrl = dto.ImageUrl,
                IsMain = dto.IsMain
            };

            _context.PropertyImages.Add(image);
            await _context.SaveChangesAsync();

            return image;
        }

        public async Task<List<Property>> GetPropertiesAsync(decimal? priceMin, decimal? priceMax, string? address)
        {
            var query = _context.Properties
                .Include(p => p.Images)
                .AsQueryable();

            if (priceMin.HasValue)
                query = query.Where(p => p.Price >= priceMin.Value);

            if (priceMax.HasValue)
                query = query.Where(p => p.Price <= priceMax.Value);

            if (!string.IsNullOrWhiteSpace(address))
                query = query.Where(p => p.Address.ToLower().Contains(address.ToLower()));

            return await query.ToListAsync();
        }


    }
}
