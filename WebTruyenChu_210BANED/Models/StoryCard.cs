namespace WebTruyenChu_210BANED.Models
{
    public class StoryCard
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string Genre { get; set; }
        public string Author { get; set; }
        public int FirstChapterId { get; set; }
        public int LatestChapterId { get; set; }
    }

}
