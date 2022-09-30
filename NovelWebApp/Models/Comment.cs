namespace NovelWebApp.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string? CommentText { get; set; }
        public DateTime CreatedDate { get; set; }
        public float rating { get; set; }

        public Novel? Novel { get; set; }
    }
}
