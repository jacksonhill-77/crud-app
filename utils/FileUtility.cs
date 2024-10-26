namespace CrudConsole
{
    class FileUtility
    {
        static public void WriteLinesToFile(List<String> lines, string filePath)
        {
            using (StreamWriter outputFile = new StreamWriter(filePath))
            {
                foreach (string line in lines)
                    outputFile.WriteLine(line);
            }
        }

        static public string[] ReadLinesFromFile(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            return lines;
        }

    }
}
