using CatalogoLivros.Application.Interfaces;
using CatalogoLivros.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoLivros.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IBookService _service;

    public BooksController(IBookService bookService)
    {
        _service = bookService;
    }

    [HttpGet("GetAll/{ordenacao}")]
    public ActionResult<IEnumerable<Book>> GetAll(string ordenacao)
    {
        try
        {
            return Ok(_service.GetAll(ordenacao));
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("GetById/{id}")]
    public ActionResult<IEnumerable<Book>> GetById(int id)
    {
        try
        {
            var book = _service.GetById(id);
            if (book is null)
            {
                return NotFound("Livro não encontrado");
            }
            return Ok(book);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("GetByName/{Name}/{ordenacao}")]
    public ActionResult<IEnumerable<Book>> GetByName(string Name, string ordenacao)
    {
        try
        {
            var book = _service.GetByName(Name, ordenacao);
            if (book is null)
            {
                return NotFound("Livro não encontrado");
            }
            return Ok(book);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("GetByAuthor/{Author}/{ordenacao}")]
    public ActionResult<IEnumerable<Book>> GetByAuthor(string Author, string ordenacao)
    {
        try
        {
            var book = _service.GetByAuthor(Author, ordenacao);
            if (book is null)
            {
                return NotFound("Livro não encontrado");
            }
            return Ok(book);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("GetByIlustrador/{Ilustrador}/{ordenacao}")]
    public ActionResult<IEnumerable<Book>> GetByIlustrador(string Ilustrador, string ordenacao)
    {
        try
        {
            var book = _service.GetByIlustrador(Ilustrador, ordenacao);
            if (book is null)
            {
                return NotFound("Livro não encontrado");
            }
            return Ok(book);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("GetByGenres/{Genres}/{ordenacao}")]
    public ActionResult<IEnumerable<Book>> GetByGenres(string Genres, string ordenacao)
    {
        try
        {
            var book = _service.GetByGenres(Genres, ordenacao);
            if (book is null)
            {
                return NotFound("Livro não encontrado");
            }
            return Ok(book);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    } //maximum price

    [HttpGet("GetByMaximumPrice/{MaximumPrice}/{ordenacao}")]
    public ActionResult<IEnumerable<Book>> GetByMaximumPrice(decimal MaximumPrice, string ordenacao)
    {
        try
        {
            var book = _service.GetByMaximumPrice(MaximumPrice, ordenacao);
            if (book is null)
            {
                return NotFound("Livro não encontrado");
            }
            return Ok(book);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    } //maximum price

    [HttpGet("CalcularFrete/{id}")]
    public ActionResult<string> CalcularFrete(int id)
    {
        try
        {
            var livro = _service.GetById(id);
            if (livro == null) { return NotFound("Livro não encontrado"); }
            return Ok($" O valor do Frete será: " + livro.FreightPrice.ToString("C", new System.Globalization.CultureInfo("en-US")));
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
