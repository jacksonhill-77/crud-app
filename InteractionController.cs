using CrudApp.models;
using CrudApp.services;

namespace CrudApp;

/// <summary>
/// The role of the InteractionController is to implement all console read and write operations
/// </summary>
/// <param name="bookService">Class that implements IBookService</param>
public class InteractionController(IBookService bookService)
{
    private readonly IBookService _bookService = bookService;
    private bool _isRunning = false;

    public void StartInteraction()
    {
        _isRunning = true;
        do
        {
            var userInput = Console.ReadLine();
            switch (userInput)
            {
                case "1":
                    DisplayBooks();
                    break;
                case "2":
                    //crudOperations.AddBooks();
                    break;
                case "3":
                    //crudOperations.RemoveBook();
                    break;
                case "4":
                    //crudOperations.UpdateBook();
                    break;
                case "5":
                    _isRunning = false;
                    break;
                default:
                    Console.WriteLine("Please choose a number between 1 - 4");
                    break;

            }
            PrintOptions();
        } while (_isRunning == true);
    }
    
    void PrintOptions()
    {
        Console.WriteLine("\nPlease enter a number from the options below:\n");
        Console.WriteLine("1. Display current books in the library");
        Console.WriteLine("2. Add a book");
        Console.WriteLine("3. Remove a book");
        Console.WriteLine("4. Update a book's contents");
        Console.WriteLine("5. Close application");
    }

    void DisplayBooks()
    {
        PrintBooks(_bookService.FetchBooks());
    }
    
    void PrintBooks(List<Book> books)
    {
        Console.WriteLine("\n");

        books
            .GroupBy(x => x.Author, (s, enumerable) => new { Author = s, Books = enumerable.ToList() })
            .Select(x => ConvertLineToReadableInfo(x.Author, x.Books))
            .ToList()
            .ForEach(Console.WriteLine);
    }

    string ConvertLineToReadableInfo(string? author, List<Book> books)
    {
        return $"Author: {author}\n" + string.Join("\n", books.Select(ConvertLineToReadableInfo));
    }
    
    string ConvertLineToReadableInfo(Book book, int index)
    {
        return $"{index + 1}. Title: {book.Title}. Author: {book.Author}. Year published: {book.PublishYear}";
    }
    
}
