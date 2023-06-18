namespace WebApplication2.Models
{

    public class MoveStats
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public MoveName[] Names { get; set; }
    }

    public class MoveName
    {
        public Language Language { get; set; }
        public string Name { get; set; }
    }

    public class Language
    {
        public string Name { get; set; }
    }

}