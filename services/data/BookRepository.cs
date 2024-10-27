using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using CrudApp.models;

// when setting up database naming schemes, you can say all camelcase is converted to SQL conventions

namespace CrudApp.services.data
{
    public interface IRepository
    {
        // create a test for reading database
        // public unneccessary 
        // the classes based on this interface only return from database, rather than return and print. so readdatabase shouldn't be void 
        // the interface class should deal with the printing 
        List<Book> ReadDatabase();
        void AddBook();
        void EditBook();
        void RemoveBook();
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

    public class DapperDbConnection : IRepository
    {
        public static String connectionString = "Server=localhost;User ID=sa;Password=9n8kZ81J0iuB;Initial Catalog=SIMPLE_LIBRARY;Integrated Security=false;TrustServerCertificate=True";

        public List<Book> ReadDatabase()
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

            return new List<Book>();
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

    public class FileDbConnection
    {
        List<Book> ReadDatabase()
        {
            var books = new List<Book>();
            return books;
        }

        void AddBook(Book book)
        {

        }

        void EditBook()
        {

        }

        void RemoveBook()
        {

        }

    }
}
