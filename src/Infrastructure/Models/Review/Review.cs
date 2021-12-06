namespace Infrastructure.Models.Reviews
{
    public class Review : BaseModel
    {
        public string ReviewText { get; set; }
        public int Rate { get; set; }
    }
}
