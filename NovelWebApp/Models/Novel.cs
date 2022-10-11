namespace NovelWebApp.Models
{
    public class Novel
    {
        public int NovelId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Photo { get; set; }
        public List<Chapter>? Chapters { get; set; }
        public List<Comment>? Comments { get; set; }


    }
}
