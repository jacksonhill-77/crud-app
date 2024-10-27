using CrudApp.services.data;
using CrudApp.controllers;
using CrudApp.utils;
using CrudApp.services;

string filePath = FilePathsUtility.filePath;

bool run = true;

Console.WriteLine("Connecting to database...");
var dapperConnect = new DapperDbConnection();
dapperConnect.ReadDatabase();

Console.WriteLine("Welcome to the Simple Library.");
InteractionController.PrintOptions();

do
{
    string userInput = Console.ReadLine();
    BookService crudOperations = new BookService();
    switch (userInput)
    {
        case "1":
            crudOperations.DisplayBooks();
            break;
        case "2":
            crudOperations.AddBooks();
            break;
        case "3":
            crudOperations.RemoveBook();
            break;
        case "4":
            crudOperations.UpdateBook();
            break;
        case "5":
            run = false;
            break;
        default:
            Console.WriteLine("Please choose a number between 1 - 4");
            break;

    }
    InteractionController.PrintOptions();
} while (run == true);
