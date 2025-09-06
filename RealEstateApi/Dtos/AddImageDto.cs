namespace RealEstateApi.Dtos
{
    public class AddImageDto
    {
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsMain { get; set; } = false;
    }
}