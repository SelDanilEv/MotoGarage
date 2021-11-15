namespace Infrastructure.Models.Menu
{
    public class NavMenuItem : BaseModel
    {
        public NavMenuItem()
        {
            Link = new ActionLink();
        }

        public string Name { get; set; }

        public ActionLink Link { get; set; } 

        public int AccessLevel { get; set; }

        public int Priority { get; set; }
    }
}
