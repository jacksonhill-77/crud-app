namespace CrudApp.utils
{
    
    
    class FileUtility
    {

        static public void WriteLinesToFile(List<String> lines, string filePath)
        {
            
            using (StreamWriter outputFile = new StreamWriter(filePath))
            {
                foreach (var line in lines)
                    outputFile.WriteLine(line);
            }
        }

        static public string[] ReadLinesFromFile(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            return lines;
        }

    }
}
