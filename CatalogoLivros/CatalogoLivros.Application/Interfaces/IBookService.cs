using CatalogoLivros.Application.DTOs;

namespace CatalogoLivros.Application.Interfaces
{
    public interface IBookService
    {
        IEnumerable<BookDto> GetAll(string? ordenacao);
        BookDto? GetById(int id);
        IEnumerable<BookDto> GetByName(string Name, string ordenacao);
        IEnumerable<BookDto> GetByAuthor(string Author, string ordenacao);
        IEnumerable<BookDto> GetByIlustrador(string Ilustrador, string ordenacao);
        IEnumerable<BookDto> GetByGenres(string Genres, string ordenacao);
        IEnumerable<BookDto> GetByMaximumPrice(decimal MaximumPrice, string ordenacao);

    }
}
