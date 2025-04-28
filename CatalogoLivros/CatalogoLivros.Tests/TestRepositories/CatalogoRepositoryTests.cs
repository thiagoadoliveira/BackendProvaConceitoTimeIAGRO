using CatalogoLivros.Domain.Entities;
using CatalogoLivros.Infrastructure.Repositories;
using System.Text.Json;

namespace CatalogoLivros.Tests.TestRepositories;

public class CatalogoRepositoryTests
{
    private readonly CatalogoRepository _repository;

    public CatalogoRepositoryTests()
    {
        var booksJson = "[{\"Id\":1, \"Name\":\"Book 1\", \"Price\":10.0}, {\"Id\":2, \"Name\":\"Book 2\", \"Price\":20.0}]";
        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, PropertyNameCaseInsensitive = true };
        var books = JsonSerializer.Deserialize<List<Book>>(booksJson, options);

        // Injetando no repositório
        _repository = new CatalogoRepository(books);
    }

    [Fact]
    public void GetAll_ShouldReturnAllBooks()
    {
        var result = _repository.GetAll();

        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public void GetById_ShouldReturnBook_WhenBookExists()
    {
        var result = _repository.GetById(1);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Book 1", result.Name);
    }

    [Fact]
    public void GetById_ShouldReturnNull_WhenBookDoesNotExist()
    {
        var result = _repository.GetById(999);

        Assert.Null(result);
    }
}
