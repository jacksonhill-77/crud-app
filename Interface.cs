using Newtonsoft.Json;

namespace CrudConsole
{
    public class Interface
    {

        static public void PrintOptions()
        {
            Console.WriteLine("\nPlease enter a number from the options below:\n");
            Console.WriteLine("1. Display current books in the library");
            Console.WriteLine("2. Add a book");
            Console.WriteLine("3. Remove a book");
            Console.WriteLine("4. Update a book's contents");
            Console.WriteLine("5. Close application");
        }
        static public void PrintLines(string[] lines, string filePath)
        {
            Console.WriteLine("\n");
            for (int i = 0; i < lines.Length; i++)
            {
                Console.WriteLine(ConvertLineToReadableInfo(lines[i], i));
            };
        }

        static public string ConvertLineToReadableInfo(string line, int index)
        {
            Book book = JsonConvert.DeserializeObject<Book>(line);
            return $"{index + 1}. Title: {book.Title}. Author: {book.Author}. Year published: {book.PublishYear}";
        }

        static public string ConvertLineToPropertiesList(string line)
        {
            string[] properties = line.Split(',');
            return $"1. Title: {properties[0]}\n2. Author: {properties[1]}\n3. Year published: {int.Parse(properties[2])}";
        }
    }
}