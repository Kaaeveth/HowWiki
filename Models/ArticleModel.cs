namespace HowWiki.Models
{
    public class ArticleModel
    {
        public string Author {get; set;}
        public string Content { get; set; }
        public string Title { get; set; }
        public string References { get; set; }
        public float AvgStars { get; set; }
        public int UsersRatedCount { get; set; }
        public int Id { get; set; }
        public string[] Tags { get; set; }
    }
}
