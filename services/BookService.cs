using System;
using CrudApp.services.data;
using CrudApp.utils;
using CrudApp.controllers;

namespace CrudApp.services
{
    public class BookService
    {

        private string filePath = FilePathsUtility.filePath;

        public void DisplayBooks()
        {
            Console.WriteLine("Current books in library:");
            InteractionController.PrintLines(FileUtility.ReadLinesFromFile(filePath), filePath);
        }

        public void AddBooks()
        {
            List<String> books = FileUtility.ReadLinesFromFile(filePath).ToList();
            List<String> newBooks = BookHelper.ConvertBookListToJSON(BookHelper.GetUsersListOfBooks());

            books.AddRange(newBooks);

            FileUtility.WriteLinesToFile(books, filePath);
        }

        public void RemoveBook()
        {
            int chosenBook = BookHelper.GetIndexOfBookToModify("remove");
            List<string> lines = FileUtility.ReadLinesFromFile(filePath).ToList();
            lines.RemoveAt(chosenBook);
            FileUtility.WriteLinesToFile(lines, filePath);
            Console.WriteLine("Book removed.");
            DisplayBooks();
        }

        public void UpdateBook()
        {
            int bookIndex = BookHelper.GetIndexOfBookToModify("update");
            List<string> lines = FileUtility.ReadLinesFromFile(filePath).ToList();
            string book = lines[bookIndex];
            string updatedBook = BookHelper.ChangeBookProperties(book);
            lines[bookIndex] = updatedBook;
            FileUtility.WriteLinesToFile(lines, filePath);

        }
    }
}

