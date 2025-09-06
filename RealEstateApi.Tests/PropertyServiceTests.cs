using Microsoft.EntityFrameworkCore;
using RealEstateApi.Data;
using RealEstateApi.Dtos;
using RealEstateApi.Models;
using RealEstateApi.Services;

namespace RealEstateApi.Tests
{
    public class PropertyServiceTests
    {
        private ApplicationDbContext _context = null!;
        private PropertyService _service = null!;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // evita conflictos entre tests
                .Options;

            _context = new ApplicationDbContext(options);
            _service = new PropertyService(_context);
        }

        [Test]
        public async Task CreatePropertyAsync_ShouldAddNewProperty()
        {
            var dto = new CreatePropertyDto
            {
                Title = "Casa de pruebas",
                Description = "Una prueba unitaria muy bonita",
                Address = "Calle Falsa 123",
                Price = 123456
            };

            var result = await _service.CreatePropertyAsync(dto);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Title, Is.EqualTo("Casa de pruebas"));
            Assert.That(await _context.Properties.CountAsync(), Is.EqualTo(1));
        }

        [Test]
        public async Task UpdatePropertyAsync_ShouldModifyExistingProperty()
        {
            var property = new Property
            {
                Title = "Original",
                Description = "Desc",
                Address = "Dirección",
                Price = 100
            };

            _context.Properties.Add(property);
            await _context.SaveChangesAsync();

            var dto = new UpdatePropertyDto
            {
                Title = "Actualizado",
                Description = "Nueva desc",
                Address = "Nueva dirección",
                Price = 200
            };

            var result = await _service.UpdatePropertyAsync(property.Id, dto);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Title, Is.EqualTo("Actualizado"));
            Assert.That(result.Price, Is.EqualTo(200));
        }

        [Test]
        public async Task UpdatePriceAsync_ShouldChangePrice()
        {
            var property = new Property
            {
                Title = "Casa",
                Description = "Desc",
                Address = "Dirección",
                Price = 500
            };

            _context.Properties.Add(property);
            await _context.SaveChangesAsync();

            var result = await _service.UpdatePriceAsync(property.Id, 999);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Price, Is.EqualTo(999));
        }

        [Test]
        public async Task AddImageAsync_ShouldAddImageToProperty()
        {
            var property = new Property
            {
                Title = "Casa",
                Description = "Desc",
                Address = "Dirección",
                Price = 500
            };

            _context.Properties.Add(property);
            await _context.SaveChangesAsync();

            var dto = new AddImageDto
            {
                ImageUrl = "https://image.test.jpg",
                IsMain = true
            };

            var result = await _service.AddImageAsync(property.Id, dto);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.ImageUrl, Is.EqualTo(dto.ImageUrl));
            Assert.That(result.IsMain, Is.True);
        }

        [Test]
        public async Task GetPropertiesAsync_ShouldReturnFilteredResults()
        {
            _context.Properties.AddRange(
                new Property { Title = "Barata", Address = "Calle A", Price = 100 },
                new Property { Title = "Media", Address = "Calle B", Price = 300 },
                new Property { Title = "Cara", Address = "Calle C", Price = 1000 }
            );
            await _context.SaveChangesAsync();

            var results = await _service.GetPropertiesAsync(priceMin: 200, priceMax: 800, address: "B");

            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].Title, Is.EqualTo("Media"));
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }
    }
}
