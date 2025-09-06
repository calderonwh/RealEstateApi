namespace RealEstateApi.Dtos
{
    public class CreatePropertyDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Address { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
