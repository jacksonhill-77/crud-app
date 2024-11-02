namespace CrudApp.models
{
    public class Book
    {

        public int Id { get; set; }
        // null forgiving operator - you are essentially just telling the compiler "I will make sure this is not null at runtime"
        public string Title { get; set; } = null!;

        // how are the two null solutions different?
        // the type? declaration is called a "nullable modifier", which means that it declares a variable of type "int" that can be nullable, and assigns it the value "null"
        public string? Author { get; set; }
        public int PublishYear { get; set; }

        // extra constructor not needed

        public override string ToString()
        {
            return $"{Title},{Author},{PublishYear}";
        }
    }
}

