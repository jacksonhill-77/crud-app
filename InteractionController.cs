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

    // from BookHelper
    static void PrintUpdatedBookProperties(string updatedBook)
    {
        Console.WriteLine("\nUpdated. New properties are as follows: ");
        Console.WriteLine(ConvertLineToPropertiesList(updatedBook));
    }
    string ConvertLineToReadableInfo(string? author, List<Book> books)
    {
        return $"Author: {author}\n" + string.Join("\n", books.Select(ConvertLineToReadableInfo));
    }
    
    string ConvertLineToReadableInfo(Book book, int index)
    {
        return $"{index + 1}. Title: {book.Title}. Author: {book.Author}. Year published: {book.PublishYear}";
    }

    // from BookHelper
    public static List<Book> GetUsersListOfBooks()
    {
        bool run = true;
        List<Book> books = new List<Book>();

        do
        {
            books.Add(GetUserInputAsBook());
            Console.WriteLine("\nDo you wish to add another book? Y/N");
            string userResponse = Console.ReadLine();
            if (userResponse == "y")
            {
                continue;
            }
            else if (userResponse == "n")
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid response, please try again.");
            };

        } while (run);

        return books;
    }

    // from BookHelper
    static Book GetUserInputAsBook()
    {
        // dummy pid
        int pid = 0;

        Console.WriteLine("\nPlease enter the book title: ");
        string title = Console.ReadLine();

        Console.WriteLine("\nPlease enter the author name: ");
        string author = Console.ReadLine();

        Console.WriteLine("\nPlease enter the publish date: ");
        int publishDate = int.Parse(Console.ReadLine());

        Book book = new Book();
        book.Id = pid;
        book.Title = title;
        book.Author = author;
        book.PublishYear = publishDate;

        return book;
    }
}

}
