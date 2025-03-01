using System.Collections.Generic;
using CrudApp.models;
using CrudApp.services;
using CrudApp.services.data;
using FluentAssertions;
using Moq;
using Xunit;
using System.Linq;

namespace CrudApp.Tests.Repositories;
/// <summary>
/// SUT = Subject Under Test
/// </summary>
public class BookRepositoryTests
{
    [Fact]
    public void CanAddBook_WithSuccess()
    {
        // Setup
        var testHelper = new TestHelper();
        var fileService = new FileService();
        var filePath = "mockpath";
        var fileDbConnection = new FileDbConnection(new FileService(), filePath);
        var newBook = new Book()
        {
            Id = 1,
            Title = "Test Title",
            Author = "Test Author",
            PublishYear = 1900,
        };

        var bookJSON = fileDbConnection.ConvertBookToJSON(newBook);

        var sut = testHelper
            .SetupAddBook(bookJSON)
            .CreateSut();

        // Act
        var response = sut.AddBook(newBook);

        // Assert
        response.Should().Be((true, null)!);
    }

    [Fact]
    public void CanAddBook_WithCorrectJSON()
    {
        // Setup
        var testHelper = new TestHelper();
        var fileService = new FileService();
        var filePath = "mockpath";
        var fileDbConnection = new FileDbConnection(new FileService(), filePath);
        var newBook = new Book()
        {
            Id = 1,
            Title = "Test Title",
            Author = "Test Author",
            PublishYear = 1900,
        };

        var bookJSON = fileDbConnection.ConvertBookToJSON(newBook);

        var sut = testHelper
            .SetupAddBook(bookJSON)
            .CreateSut();

        // Act
        var response = sut.AddBook(newBook);

        // Assert
        testHelper._convertedJSON.Should().Be(bookJSON);
    }

    [Fact]
    public void CanRemoveBook_WithSuccess()
    {
        // Setup
        var testHelper = new TestHelper();
        var fileService = new FileService();
        var filePath = "mockpath";
        var fileDbConnection = new FileDbConnection(new FileService(), filePath);
        var book1 = new Book()
        {
            Id = 1,
            Title = "Book 1",
            Author = "Author 1",
            PublishYear = 1901,
        };

        var book2 = new Book()
        {
            Id = 2,
            Title = "Book 2",
            Author = "Author 2",
            PublishYear = 1902,
        };

        var books = new List<Book> 
        {   book1,
            book2
        };

        var booksPreRemoval = books.ConvertAll(book => fileDbConnection.ConvertBookToJSON(book));
        var booksPostRemoval = booksPreRemoval
            .Skip(1)
            .ToList();

        var sut = testHelper
            .SetupRemoveBook(booksPreRemoval, booksPostRemoval)
            .CreateSut();

        // Act
        sut.RemoveBook(book1.Title);

        // Assert
        testHelper._booksPostRemoval.Should().BeEquivalentTo(booksPostRemoval);
    }

    class TestHelper
    {
        private readonly MockRepository _mockRepository = new MockRepository(MockBehavior.Strict);
        readonly Mock<IFileService> _fileServiceMock;
        public string _convertedJSON = "";
        public List<string> _booksPostRemoval = new List<string>();
        string _filePath = "mockpath";

        public TestHelper()
        {
            _fileServiceMock = _mockRepository.Create<IFileService>();
        }

        public TestHelper SetupAddBook(string bookJSON)
        {

            _fileServiceMock
                .Setup(x => x.WriteLineToFile(bookJSON, _filePath))
                .Callback<string, string>((bookJSON, filePath) => _convertedJSON = bookJSON)
                .Returns((true, null));
            
            return this;
        }

        public TestHelper SetupRemoveBook(List<string> booksPreRemoval, List<string> booksPostRemoval)
        {
            _fileServiceMock
                .Setup(x => x.ReadLinesFromFile(_filePath))
                .Returns((booksPreRemoval));

            _fileServiceMock
                .Setup(x => x.WriteLinesToFile(booksPostRemoval, _filePath))
                .Callback<List<string>, string>((booksPostRemoval, filePath) => _booksPostRemoval = booksPostRemoval)
                .Returns((true, null));

            return this;
        }

        //public TestHelper SetupReadDatabase(List<Book> expectedBooks)
        //{
        //    string filePath = "mockpath";

        //    _fileServiceMock
        //        .Setup(x => x.ReadLinesFromFile(filePath))
        //        .Returns(expectedBooks);

        //    return this;
        //}

        public FileDbConnection CreateSut()
        {
            return new FileDbConnection(_fileServiceMock.Object, _filePath);
        }
    }
}