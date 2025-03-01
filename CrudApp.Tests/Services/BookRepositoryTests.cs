using System.Collections.Generic;
using CrudApp.models;
using CrudApp.services;
using CrudApp.services.data;
using FluentAssertions;
using Moq;
using Xunit;

namespace CrudApp.Tests.Services;
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
    public void CanUpdateBook_WithSuccess()
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

    class TestHelper
    {
        private readonly MockRepository _mockRepository = new MockRepository(MockBehavior.Strict);
        readonly Mock<IFileService> _fileServiceMock;
        public string _convertedJSON = "";
        string _filePath = "mockpath";

        public TestHelper()
        {
            _fileServiceMock = _mockRepository.Create<IFileService>();
        }

        public TestHelper SetupAddBook(string bookJSON)
        {
            string filePath = "mockpath";

            _fileServiceMock
                .Setup(x => x.WriteLineToFile(bookJSON, filePath))
                .Callback<string, string>((bookJSON, filePath) => _convertedJSON = bookJSON)
                .Returns((true, null));
            
            return this;
        }

        public TestHelper SetupUpdateBook(string titleOfBookToUpdate, Book updatedBook)
        {
            _fileServiceMock
                .Setup(x => x.UpdateBook(titleOfBookToUpdate, updatedBook))
                .Returns((true, updatedBook));

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