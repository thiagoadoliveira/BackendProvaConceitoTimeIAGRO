using CatalogoLivros.Domain.Entities;
using CatalogoLivros.Domain.Interfaces;
using System.Text.Json;

namespace CatalogoLivros.Infrastructure.Repositories;

public class CatalogoRepository : ICatalogoRepository
{

    private readonly List<Book> _books;

    public CatalogoRepository()
    {
        var json = File.ReadAllText("Data/books.json");
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        };
        _books = JsonSerializer.Deserialize<List<Book>>(json, options);
    }

    // Construtor para testes, permitindo passar os livros diretamente
    public CatalogoRepository(List<Book> books)
    {
        _books = books;
    }

    public IEnumerable<Book> GetAll() => _books;

    public Book GetById(int id)
    {
        return _books.FirstOrDefault(l => l.Id == id);
    }

}
