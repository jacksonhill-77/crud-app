using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;

// the x service knows how to handle x
// when setting up database naming schemes, you can say all camelcase is converted to SQL conventions

namespace CrudConsole
{
    // repository pattern
    public interface IRepository
    {
        // create a test for reading database
        // public unneccessary 
        public void ReadDatabase();
        public void AddBook();
        public void EditBook();
        public void RemoveBook();
    }

    public class LibraryContext : DbContext
    {
        public DbSet<Book> simple_library { get; set; }

        public static String connectionString = "Server=localhost;User ID=sa;Password=9n8kZ81J0iuB;Initial Catalog=SIMPLE_LIBRARY;Integrated Security=false;TrustServerCertificate=True";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

    }

    public class DapperDBConnect : IDatabase
    {
        public static String connectionString = "Server=localhost;User ID=sa;Password=9n8kZ81J0iuB;Initial Catalog=SIMPLE_LIBRARY;Integrated Security=false;TrustServerCertificate=True";

        public void ReadDatabase()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var sql = "SELECT * FROM dbo.simple_library";

                var books = connection.Query<Book>(sql).ToList();

                foreach (Book book in books)
                {
                    Console.WriteLine(book.ToString());
                }
            }
        }

        public void AddBook()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                // get value from user

                var sql = "INSERT INTO dbo.simple_library";

                var books = connection.Query<Book>(sql).ToList();

                foreach (Book book in books)
                {
                    Console.WriteLine(book.ToString());
                }
            }
        }

        public void EditBook()
        {

        }

        public void RemoveBook()
        {

        }

    }

    public class EntityDBConnect
    {

        public static void QueryBooks()
        {
            using (var context = new LibraryContext())
            {
                Console.WriteLine("Testing...");
                var books = context.simple_library.ToList();
                foreach (var book in books)
                {
                    Console.WriteLine(book);
                }
            }
        }
    }
}
