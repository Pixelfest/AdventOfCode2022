namespace AdventOfCode;

public class Assignment09A : Assignment, IAmAnAssignment
{
    public Assignment09A()
    {
        Load("Input/08.txt");
    }

    protected override void ReadLine(string line)
    {
        //Console.WriteLine($"Current line in file: {CurrentLine}");
    }

    public override void Process()
    {
        //Console.WriteLine($"Total lines in file: {TotalLines}");
        Output = ""; //
    }
}
