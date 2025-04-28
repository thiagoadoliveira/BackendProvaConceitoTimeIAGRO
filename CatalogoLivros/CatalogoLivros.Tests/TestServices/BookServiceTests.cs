using CatalogoLivros.Application.Services;
using CatalogoLivros.Domain.Entities;
using CatalogoLivros.Domain.Interfaces;
using Moq;

namespace CatalogoLivros.Tests.Services
{
    public class BookServiceTests
    {
        private readonly Mock<ICatalogoRepository> _repositoryMock;
        private readonly BookService _service;

        public BookServiceTests()
        {
            _repositoryMock = new Mock<ICatalogoRepository>();
            _service = new BookService(_repositoryMock.Object);
        }

        private List<Book> GetFakeBooks()
        {
            return new List<Book>
            {
                new Book { Id = 1, Name = "Book A", Price = 10m, Specifications = new Specification { Author = "Author 1", Genres = new List<string> { "Fantasy" }, Illustrator = new List<string> { "Illustrator 1" } } },
                new Book { Id = 2, Name = "Book B", Price = 20m, Specifications = new Specification { Author = "Author 2", Genres = new List<string> { "Sci-Fi" }, Illustrator = new List<string> { "Illustrator 2" } } },
                new Book { Id = 3, Name = "Book C", Price = 15m, Specifications = new Specification { Author = "Author 3", Genres = new List<string> { "Fantasy" }, Illustrator = new List<string> { "Illustrator 1" } } },
            };
        }

        [Fact]
        public void GetAll_ShouldReturnAllBooks()
        {
            // Arrange
            var fakeBooks = GetFakeBooks();
            _repositoryMock.Setup(r => r.GetAll()).Returns(fakeBooks);

            // Act
            var result = _service.GetAll(null);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public void GetById_ShouldReturnBook_WhenBookExists()
        {
            // Arrange
            var fakeBook = GetFakeBooks().First();
            _repositoryMock.Setup(r => r.GetById(1)).Returns(fakeBook);

            // Act
            var result = _service.GetById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Book A", result.Name);
        }

        [Fact]
        public void GetById_ShouldReturnNull_WhenBookDoesNotExist()
        {
            _repositoryMock.Setup(r => r.GetById(99)).Returns((Book)null);

            var result = _service.GetById(99);

            Assert.Null(result);
        }

        [Fact]
        public void GetByName_ShouldReturnBooksMatchingName()
        {
            // Arrange
            var fakeBooks = GetFakeBooks();
            _repositoryMock.Setup(r => r.GetAll()).Returns(fakeBooks);

            // Act
            var result = _service.GetByName("Book A", null);

            // Assert
            Assert.Single(result);
            Assert.Equal("Book A", result.First().Name);
        }

        [Fact]
        public void GetByAuthor_ShouldReturnBooksMatchingAuthor()
        {
            // Arrange
            var fakeBooks = GetFakeBooks();
            _repositoryMock.Setup(r => r.GetAll()).Returns(fakeBooks);

            // Act
            var result = _service.GetByAuthor("Author 2", null);

            // Assert
            Assert.Single(result);
            Assert.Equal("Author 2", result.First().Specifications.Author);
        }

        [Fact]
        public void GetByIlustrador_ShouldReturnBooksMatchingIllustrator()
        {
            // Arrange
            var fakeBooks = GetFakeBooks();
            _repositoryMock.Setup(r => r.GetAll()).Returns(fakeBooks);

            // Act
            var result = _service.GetByIlustrador("Illustrator 1", null);

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void GetByGenres_ShouldReturnBooksMatchingGenre()
        {
            // Arrange
            var fakeBooks = GetFakeBooks();
            _repositoryMock.Setup(r => r.GetAll()).Returns(fakeBooks);

            // Act
            var result = _service.GetByGenres("Fantasy", null);

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void GetByMaximumPrice_ShouldReturnBooksWithPriceLessThanOrEqual()
        {
            // Arrange
            var fakeBooks = GetFakeBooks();
            _repositoryMock.Setup(r => r.GetAll()).Returns(fakeBooks);

            // Act
            var result = _service.GetByMaximumPrice(15m, null);

            // Assert
            Assert.Equal(2, result.Count());
        }
    }
}
