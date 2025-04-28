namespace CatalogoLivros.Application.DTOs
{
    public class BookSpecificationsDto
    {
        public string OriginallyPublished { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public int PageCount { get; set; }
        public List<string> Illustrator { get; set; } = new();
        public List<string> Genres { get; set; } = new();
    }
}
