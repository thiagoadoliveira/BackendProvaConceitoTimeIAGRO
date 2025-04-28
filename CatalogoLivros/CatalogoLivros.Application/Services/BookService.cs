using CatalogoLivros.Application.DTOs;
using CatalogoLivros.Application.Interfaces;
using CatalogoLivros.Application.Mappers;
using CatalogoLivros.Domain.Entities;
using CatalogoLivros.Domain.Interfaces;

namespace CatalogoLivros.Application.Services;

public class BookService : IBookService
{
    private readonly ICatalogoRepository _repository;

    public BookService(ICatalogoRepository repo)
    {
        _repository = repo;
    }

    public IEnumerable<BookDto> GetAll(string? ordenacao)
    {
        var books = _repository.GetAll();
        books = Ordenar(books, ordenacao);
        return books.Select(BookMapper.ToDto).ToList();
    }

    public BookDto? GetById(int id)
    {
        var book = _repository.GetById(id);
        return book is not null ? BookMapper.ToDto(book) : null;
    }

    public IEnumerable<BookDto> GetByName(string Name, string ordenacao)
    {
        var books = _repository
        .GetAll()
        .Where(b => b.Name.Contains(Name, StringComparison.OrdinalIgnoreCase));

        books = Ordenar(books, ordenacao);
        return books.Select(BookMapper.ToDto).ToList();
    }

    public IEnumerable<BookDto> GetByAuthor(string Author, string ordenacao)
    {
        var books = _repository
        .GetAll()
        .Where(b => b.Specifications.Author.Contains(Author, StringComparison.OrdinalIgnoreCase));

        books = Ordenar(books, ordenacao);
        return books.Select(BookMapper.ToDto).ToList();
    }

    public IEnumerable<BookDto> GetByIlustrador(string Ilustrador, string ordenacao)
    {
        var books = _repository
        .GetAll()
        .Where(b => b.Specifications.Illustrator != null && b.Specifications.Illustrator.Any(i => i.Contains(Ilustrador, StringComparison.OrdinalIgnoreCase)));

        books = Ordenar(books, ordenacao);
        return books.Select(BookMapper.ToDto).ToList();
    }

    public IEnumerable<BookDto> GetByGenres(string Genres, string ordenacao)
    {
        var books = _repository
        .GetAll()
        .Where(b => b.Specifications.Genres != null && b.Specifications.Genres.Any(i => i.Contains(Genres, StringComparison.OrdinalIgnoreCase)));

        books = Ordenar(books, ordenacao);
        return books.Select(BookMapper.ToDto).ToList();
    }

    public IEnumerable<BookDto> GetByMaximumPrice(decimal MaximumPrice, string ordenacao)
    {
        var books = _repository
        .GetAll()
        .Where(l => l.Price <= MaximumPrice);

        books = Ordenar(books, ordenacao);
        return books.Select(BookMapper.ToDto).ToList();
    }

    private IEnumerable<Book> Ordenar(IEnumerable<Book> books, string? ordenacao)
    {
        if (string.IsNullOrEmpty(ordenacao)) return books;

        return ordenacao.ToLower() switch
        {
            "asc" => books.OrderBy(b => b.Price),
            "desc" => books.OrderByDescending(b => b.Price),
            _ => books
        };
    }
}
