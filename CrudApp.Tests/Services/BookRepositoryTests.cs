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
        var newBook = new Book()
        {
            Id = 1,
            Title = "Test Title",
            Author = "Test Author",
        };
        var bookJSON = BookService.ConvertBookToJSON(newBook);

        var sut = testHelper
            .SetupAddBook(bookJSON)
            .CreateSut();

        // Act
        var response = sut.AddBook(bookJSON);

        // Assert
        response.Should().Be((true, null)!);
    }

    class TestHelper
    {
        private readonly MockRepository _mockRepository = new MockRepository(MockBehavior.Strict);
        readonly Mock<IFileService> _fileServiceMock;

        public TestHelper()
        {
            _fileServiceMock = _mockRepository.Create<IFileService>();
        }

        public TestHelper SetupAddBook(string bookJSON)
        {
            string filePath = "mockpath";

            _fileServiceMock
                .Setup(x => x.WriteLineToFile(bookJSON, filePath))
                .Returns((true, null));
            
            return this;
        }

        //public TestHelper SetupReadDatabase(List<Book> expectedBooks)
        //{
        //    _fileServiceMock
        //        .Setup(x => x.ReadDatabase())
        //        .Returns(expectedBooks);

        //    return this;
        //}

        //public TestHelper SetupAddBook(Book book)
        //{
        //    _bookRepositoryMock
        //        .Setup(x => x.AddBook(book.Title, book.Author, book.PublishYear));

        //    return this;
        //}

        //public TestHelper SetupUpdateBook(string titleOfBookToUpdate, Book updatedBook)
        //{
        //    _bookRepositoryMock
        //        .Setup(x => x.UpdateBook(titleOfBookToUpdate, updatedBook))
        //        .Returns((true, updatedBook));

        //    return this;
        //}

        public FileDbConnection CreateSut()
        {
            return new FileDbConnection(_fileServiceMock.Object);
        }
    }
}