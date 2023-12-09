namespace API.DTO
{
    public class ReviewDto
    {
        public int ProductId { get; set; }
        public string UserEmail { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
    }
}
