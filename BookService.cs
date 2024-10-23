using System;
using CrudConsole;

// the x service knows how to handle x

public class CRUDLogic
{
    // These 4 methods become part of the database class

    private string filePath = FilePaths.filePath;

    public void DisplayBooks()
    {
        Console.WriteLine("Current books in library:");
        Interface.PrintLines(FileInteracter.ReadLinesFromFile(filePath), filePath);
    }

    public void AddBooks()
    {
        List<String> books = FileInteracter.ReadLinesFromFile(filePath).ToList();
        List<String> newBooks = BookOperations.ConvertBookListToJSON(BookOperations.GetUsersListOfBooks());

        books.AddRange(newBooks);

        FileInteracter.WriteLinesToFile(books, filePath);
    }

    public void RemoveBook()
    {
        int chosenBook = BookOperations.GetIndexOfBookToModify("remove");
        List<string> lines = FileInteracter.ReadLinesFromFile(filePath).ToList();
        lines.RemoveAt(chosenBook);
        FileInteracter.WriteLinesToFile(lines, filePath);
        Console.WriteLine("Book removed.");
        DisplayBooks();
    }

    public void UpdateBook()
    {
        int bookIndex = BookOperations.GetIndexOfBookToModify("update");
        List<string> lines = FileInteracter.ReadLinesFromFile(filePath).ToList();
        string book = lines[bookIndex];
        string updatedBook = BookOperations.ChangeBookProperties(book);
        lines[bookIndex] = updatedBook;
        FileInteracter.WriteLinesToFile(lines, filePath);

    }
}
