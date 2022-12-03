using TextCopy;

namespace AdventOfCode;

public abstract class Assignment : IAmAnAssignment
{
    protected string Output = "There is no output.\nAt least not yet.";

    public abstract void Process();
    protected abstract void ReadLine(string line);
    
    protected void Load(string fileInput)
    {
        using StreamReader reader = new StreamReader(fileInput);
        while (!reader.EndOfStream)
        {
            ReadLine(reader.ReadLine() ?? string.Empty);
        }
    }
    
    public void PrintOutput()
    {
        Console.Write(Output);
    }

    public void ToFileAndClipboard()
    {
        Directory.CreateDirectory("Output");
        using var streamWriter = new StreamWriter(Path.Combine("Output", $"{this.GetType().Name}.txt"), options: new FileStreamOptions { Mode = FileMode.Create, Access = FileAccess.Write });

        streamWriter.Write(Output);
        
        ClipboardService.SetText(Output);
    }
}
