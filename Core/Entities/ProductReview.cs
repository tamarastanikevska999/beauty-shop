namespace Core.Entities
{
    public class ProductReview : BaseEntity
    {
        public int ProductId { get; set; }
        public string UserEmail { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
    }
}