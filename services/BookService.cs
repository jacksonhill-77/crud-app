using CrudApp.models;
using CrudApp.utils;
using Newtonsoft.Json;

namespace CrudApp.services;
public interface IBookService
{
    void AddBooks(List<Book> books);
    void RemoveBook(Book book);
    List<Book> FetchBooks();
    Book UpdateBook(Book book);
}

public class BookService(IFileService fileService) : IBookService
{
    private readonly IFileService _fileService = fileService;

    private string filePath = FilePathsUtility.filePath;

    public void RemoveBook()
    {
        // TODO: Move to void RemoveBook(Book book)
        
        // int chosenBook = BookHelper.GetIndexOfBookToModify("remove");
        // List<string> lines = _fileService.ReadLinesFromFile(filePath).ToList();
        // lines.RemoveAt(chosenBook);
        // FileUtility.WriteLinesToFile(lines, filePath);
        // Console.WriteLine("Book removed.");
        // DisplayBooks();
    }

    public void UpdateBook()
    {
        // TODO: Move to Book UpdateBook(Book book)
        // int bookIndex = BookHelper.GetIndexOfBookToModify("update");
        // List<string> lines = _fileService.ReadLinesFromFile(filePath).ToList();
        // string book = lines[bookIndex];
        // string updatedBook = BookHelper.ChangeBookProperties(book);
        // lines[bookIndex] = updatedBook;
        // FileUtility.WriteLinesToFile(lines, filePath);

    }

    public void AddBooks(List<Book> newBooks)
    {
        var books = _fileService.ReadLinesFromFile(filePath);
        
        books.AddRange(newBooks.Select(JsonConvert.SerializeObject));

        FileUtility.WriteLinesToFile(books, filePath);
    }

    public void RemoveBook(Book book)
    {
        throw new NotImplementedException();
    }

    public List<Book> FetchBooks()
    {
        throw new NotImplementedException();
    }

    public Book UpdateBook(Book book)
    {
        throw new NotImplementedException();
    }
}