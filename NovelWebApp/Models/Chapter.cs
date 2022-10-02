
namespace NovelWebApp.Models
{
    public class Chapter
    {
        public int ChapterId { get; set; }
        public string? Title { get; set; }
        public int ChapterNumber { get; set; }
        public string? chapterStory { get; set; }
        public int NovelId { get; set; }
        public Novel? Novel { get; set; }


    }
}