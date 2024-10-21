using CrudConsole;

string filePath = FilePaths.filePath;

bool run = true;

Console.WriteLine("Connecting to database...");
var dapperConnect = new DapperDBConnect();
dapperConnect.ReadDatabase();

Console.WriteLine("Welcome to the Simple Library.");
Interface.PrintOptions();

do
{
    string userInput = Console.ReadLine();
    CRUDLogic crudOperations = new CRUDLogic();
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
    Interface.PrintOptions();
} while (run == true);
