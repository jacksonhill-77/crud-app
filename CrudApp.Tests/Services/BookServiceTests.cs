using System.Collections.Generic;
using CrudApp.models;
using CrudApp.services;
using CrudApp.services.data;
using FluentAssertions;
using Moq;
using Xunit;

namespace DefaultNamespace;
/// <summary>
/// SUT = Subject Under Test
/// </summary>
public class BookServiceTests
{
    // [Fact]
    // public void CanAddBook()
    // {
    //     // Setup
    //     var testHelper = new TestHelper();
    //     var expectedLines = new List<string> { "one", "two" };
    //     var sut = testHelper
    //         .SetupFileServiceRead(expectedLines)
    //         .SetupFileServiceWrite(expectedLines)
    //         .CreateSut();
    //
    //     // Act
    //     // TODO: Add book, not empty list of books
    //     sut.AddBooks(new List<Book>()); 
    //
    //     // Assert
    //     
    // }
    
    
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
        var sut = testHelper
            .SetupGetBookByTitle(newBook.Title, false)
            .SetupAddBook(newBook)
            .CreateSut();

        // Act
        var response = sut.AddBook(newBook); 

        // Assert
        response.Should().Be((true, null));
    }
    
    [Fact]
    public void CanAddBook_ReportsDuplicate()
    {
        // Setup
        var testHelper = new TestHelper();
        var newBook = new Book()
        {
            Title = "Existing title",
        };
        var sut = testHelper
            .SetupGetBookByTitle(newBook.Title, true)
            .CreateSut();

        // Act
        var response = sut.AddBook(newBook); 

        // Assert
        response.Should().Be((false, "Already exists"), "Because the business checks for this exact response string");
    }
    
    
    class TestHelper
    {
        private readonly MockRepository _mockRepository = new MockRepository(MockBehavior.Strict);
        readonly Mock<IBookRepository> _bookRepositoryMock;
        
        public TestHelper()
        {
            _bookRepositoryMock = _mockRepository.Create<IBookRepository>();
        }

        public TestHelper SetupGetBookByTitle(string expectedTitle, bool doesExist)
        {
            _bookRepositoryMock
                .Setup(x => x.GetBookByTitle(expectedTitle))
                .Returns(doesExist
                    ? new Book()
                    {
                        Title = expectedTitle
                    }
                    : null);
            return this;
        }
        
        public TestHelper SetupAddBook(Book book)
        {
            _bookRepositoryMock
                .Setup(x => x.AddBook(book.Title, book.Author, book.PublishYear));
            
            return this;
        }


        public BookService CreateSut()
        {
            return new BookService(_bookRepositoryMock.Object);
        }
    }
}