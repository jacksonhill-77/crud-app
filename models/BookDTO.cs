namespace CrudApp.models
{
    public class Book
    {

        public int Id { get; set; }
        public string Title { get; set; } = null!;

        // how are the two null solutions different?
        public string? Author { get; set; }
        public int PublishYear { get; set; }

        // extra constructor not needed

        public override string ToString()
        {
            return $"{Title},{Author},{PublishYear}";
        }
    }
}

