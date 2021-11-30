namespace Backend.Entities
{
    public class Blog
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string ImageUrl { get; set; }
        public string ImageAlt { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}