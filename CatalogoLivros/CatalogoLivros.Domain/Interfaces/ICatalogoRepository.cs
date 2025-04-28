using CatalogoLivros.Domain.Entities;

namespace CatalogoLivros.Domain.Interfaces;

public interface ICatalogoRepository
{
    IEnumerable<Book> GetAll();
    Book GetById(int id);
}
