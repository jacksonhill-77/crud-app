using System.Collections.Generic;
using CrudApp.models;
using CrudApp.services;
using CrudApp.services.data;
using Moq;
using Xunit;

namespace DefaultNamespace;
/// <summary>
/// SUT = Subject Under Test
/// </summary>
public class BookServiceTests
{
    [Fact]
    public void CanAddBook()
    {
        // Setup
        var testHelper = new TestHelper();
        var expectedLines = new List<string> { "one", "two" };
        var sut = testHelper
            .SetupFileServiceRead(expectedLines)
            .SetupFileServiceWrite(expectedLines)
            .CreateSut();

        // Act
        // TODO: Add book, not empty list of books
        sut.AddBooks(new List<Book>()); 

        // Assert
        
    }
    
    class TestHelper
    {
        private readonly MockRepository _mockRepository = new MockRepository(MockBehavior.Strict);
        readonly Mock<IFileService> _fileServiceMock;
        readonly Mock<IBookRepository> _bookRepositoryMock;
        
        public TestHelper()
        {
            _fileServiceMock = _mockRepository.Create<IFileService>();
            _bookRepositoryMock = _mockRepository.Create<IBookRepository>();
        }
        
        public TestHelper SetupFileServiceRead(List<string> returnLines)
        {
            _fileServiceMock.Setup(x => 
                x.ReadLinesFromFile(It.IsAny<string>()))
                .Returns(returnLines);
            return this;
        }
        
        public TestHelper SetupFileServiceWrite(List<string> writeLines)
        {
            _fileServiceMock.Setup(x =>
                x.WriteLinesToFile(writeLines, It.IsAny<string>()));
            return this;
        }

        public BookService CreateSut()
        {
            return new BookService(_fileServiceMock.Object, _bookRepositoryMock.Object);
        }
    }
}