public class AssignmentReader01
{
    private readonly List<string> _list = new();
    private string _output = "There is no output.\nAt least not yet.";
    
    public AssignmentReader01(string path)
    {
        using StreamReader reader = new StreamReader(path);
        while (!reader.EndOfStream)
        {
            _list.Add(ReadLine(reader.ReadLine()));
        }
    }

    public void Process()
    {
        // Interesting things should happen here....
    }

    public void PrintInput()
    {
        foreach (var item in _list)
        {
            Console.WriteLine(item);
        }
    }

    public void PrintOutput()
    {
        Console.Write(_output);
    }

    public void SaveOutput(string path)
    {
        Directory.CreateDirectory("Output");
        using var streamWriter = new StreamWriter(path, options: new FileStreamOptions { Mode = FileMode.Create, Access = FileAccess.Write });
        
        streamWriter.Write(_output);
    }

    private string ReadLine(string line)
    {
        return line;
    }
}
