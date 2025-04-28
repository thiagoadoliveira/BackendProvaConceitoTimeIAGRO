using CatalogoLivros.Application.DTOs;
using CatalogoLivros.Domain.Entities;

namespace CatalogoLivros.Application.Mappers;

public static class BookMapper
{
    public static BookDto ToDto(Book book)
    {
        return new BookDto
        {
            Id = book.Id,
            Name = book.Name,
            Price = book.Price,
            FreightPrice = book.Price * 0.20m,
            Specifications = new BookSpecificationsDto
            {
                Author = book.Specifications.Author,
                OriginallyPublished = book.Specifications.OriginallyPublished,
                PageCount = book.Specifications.PageCount,
                Illustrator = NormalizeToList(book.Specifications.Illustrator),
                Genres = NormalizeToList(book.Specifications.Genres)
            }
        };
    }

    private static List<string> NormalizeToList(object? value)
    {
        return value switch
        {
            string s => new List<string> { s },
            IEnumerable<string> list => list.ToList(),
            _ => new List<string>()
        };
    }
}
