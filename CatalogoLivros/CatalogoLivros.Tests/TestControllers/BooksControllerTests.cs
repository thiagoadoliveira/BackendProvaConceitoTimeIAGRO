using CatalogoLivros.API.Controllers;
using CatalogoLivros.Application.DTOs;
using CatalogoLivros.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CatalogoLivros.Tests.Controllers;

public class BooksControllerTests
{
    private readonly Mock<IBookService> _serviceMock;
    private readonly BooksController _controller;

    public BooksControllerTests()
    {
        _serviceMock = new Mock<IBookService>();
        _controller = new BooksController(_serviceMock.Object);
    }

    [Fact]
    public void GetAll_ReturnsOkResult_WithListOfBooks()
    {
        var books = new List<BookDto>
        {
            new BookDto { Id = 1, Name = "Livro Teste", Price = 10 }
        };

        _serviceMock.Setup(service => service.GetAll(It.IsAny<string>())).Returns(books);

        var result = _controller.GetAll("asc");

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedBooks = Assert.IsAssignableFrom<IEnumerable<BookDto>>(okResult.Value);
        Assert.Single(returnedBooks);
    }

    [Fact]
    public void GetById_BookExists_ReturnsOkResult()
    {
        var book = new BookDto { Id = 1, Name = "Livro Teste", Price = 10M };
        _serviceMock.Setup(service => service.GetById(1)).Returns(book);

        var result = _controller.GetById(1);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedBook = Assert.IsType<BookDto>(okResult.Value);
        Assert.Equal(1, returnedBook.Id);
    }

    [Fact]
    public void GetById_BookDoesNotExist_ReturnsNotFound()
    {
        _serviceMock.Setup(service => service.GetById(1)).Returns((BookDto?)null);

        var result = _controller.GetById(1);

        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

    [Fact]
    public void CalcularFrete_BookExists_ReturnsOkResultWithFreightPrice()
    {
        var book = new BookDto { Id = 1, Name = "Livro Teste", Price = 5.99M };
        _serviceMock.Setup(service => service.GetById(1)).Returns(book);

        var result = _controller.CalcularFrete(1);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var message = Assert.IsType<string>(okResult.Value);
        Assert.Contains("O valor do Frete será:", message);
    }

    [Fact]
    public void CalcularFrete_BookDoesNotExist_ReturnsNotFound()
    {
        _serviceMock.Setup(service => service.GetById(1)).Returns((BookDto?)null);

        var result = _controller.CalcularFrete(1);

        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

    [Fact]
    public void GetByName_BookExists_ReturnsOkResult()
    {
        var books = new List<BookDto> { new BookDto { Id = 1, Name = "Livro Teste" } };
        _serviceMock.Setup(service => service.GetByName(It.IsAny<string>(), It.IsAny<string>())).Returns(books);

        var result = _controller.GetByName("Teste", "asc");

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedBooks = Assert.IsAssignableFrom<IEnumerable<BookDto>>(okResult.Value);
        Assert.Single(returnedBooks);
    }

    [Fact]
    public void GetByAuthor_BookDoesNotExist_ReturnsNotFound()
    {
        _serviceMock.Setup(service => service.GetByAuthor(It.IsAny<string>(), It.IsAny<string>())).Returns((IEnumerable<BookDto>?)null);

        var result = _controller.GetByAuthor("Autor Inexistente", "asc");

        Assert.IsType<NotFoundObjectResult>(result.Result);
    }
}
