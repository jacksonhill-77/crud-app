using CrudApp;
using CrudApp.services.data;
using CrudApp.utils;
using CrudApp.services;

// var filePath = FilePathsUtility.filePath;
//
// bool run = true;
// Console.WriteLine("Connecting to database...");
// var dapperConnect = new DapperDbConnection();
// dapperConnect.ReadDatabase();
//
// Console.WriteLine("Welcome to the Simple Library.");


var interactionController = new InteractionController(new BookService(new FileService()));
interactionController.StartInteraction();