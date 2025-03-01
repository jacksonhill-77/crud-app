namespace CrudApp.services;

public interface IFileService
{
    void WriteLinesToFile(List<string> lines, string filePath);
    List<string> ReadLinesFromFile(string filePath);
}

public class FileService : IFileService
{
    public void WriteLinesToFile(List<string> lines, string filePath)
    {
        using var outputFile = new StreamWriter(filePath);
        lines.ForEach(outputFile.WriteLine);
    }

    public List<string> ReadLinesFromFile(string filePath)
    {
        return File.ReadAllLines(filePath).ToList();
    }
}