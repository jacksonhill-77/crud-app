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

        int chosenBook = BookHelper.GetIndexOfBookToModify("remove");
        List<string> lines = _fileService.ReadLinesFromFile(filePath).ToList();
        lines.RemoveAt(chosenBook);
        FileUtility.WriteLinesToFile(lines, filePath);
        Console.WriteLine("Book removed.");
        DisplayBooks();
    }

    public void UpdateBook()
    {
        // TODO: Move to Book UpdateBook(Book book)
        int bookIndex = BookHelper.GetIndexOfBookToModify("update");
        List<string> lines = _fileService.ReadLinesFromFile(filePath).ToList();
        string book = lines[bookIndex];
        string updatedBook = BookHelper.ChangeBookProperties(book);
        lines[bookIndex] = updatedBook;
        FileUtility.WriteLinesToFile(lines, filePath);

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

    // from BookHelper
    public static int GetIndexOfBookToModify(string modificationType)
    {
        throw new NotImplementedException();
        // TODO: Re-think
        // Console.WriteLine($"Please select the number of a book to {modificationType}:");
        // InteractionController.PrintLines(FileUtility.ReadLinesFromFile(filePath), filePath);
        // return int.Parse(Console.ReadLine()) - 1;
    }

    // from BookHelper
    static string ModifyBook(string book, int propertyIndex)
    {
        string updatedBook = ChangeSinglePropertyOfBook(book, propertyIndex);
        _interactionController.PrintUpdatedBookProperties(updatedBook);
        return updatedBook;
    }

    // from BookHelper
    static string ChangeSinglePropertyOfBook(string book, int propertyIndex)
    {
        var properties = book.Split(',');
        Console.WriteLine("\nPlease enter what you would like to update to: ");
        var newProperty = Console.ReadLine();
        properties[propertyIndex] = newProperty;
        var updatedBook = String.Join(",", properties);
        return updatedBook;
    }

    // from BookHelper
    public static string ChangeBookProperties(string book)
    {
        // TODO: Re-think
        // bool run = true;
        // while (run)
        // {
        //     Console.WriteLine("\nPlease select the part of the book you wish to update by selecting 1-3: ");
        //     Console.WriteLine(InteractionController.ConvertLineToPropertiesList(book));
        //     int chosenProperty = int.Parse(Console.ReadLine()) - 1;
        //     book = ModifyBook(book, chosenProperty);
        //     Console.WriteLine("\nDo you wish to continue editing? y/ n");
        //     string continueEditing = Console.ReadLine();
        //     if (continueEditing == "y")
        //     {
        //         continue;
        //     }
        //     else if (continueEditing == "n")
        //     {
        //         break;
        //     }
        // }
        //
        // return book;
        throw new NotImplementedException();
    }

    // from BookHelper
    public static List<String> ConvertBookListToJSON(List<Book> books)
    {
        List<String> output = new List<String>();
        foreach (var book in books)
        {
            output.Add(JsonConvert.SerializeObject(book));
        }
        return output;
    }
}

