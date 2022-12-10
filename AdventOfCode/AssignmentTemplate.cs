namespace AdventOfCode;

public class AssignmentTemplate : Assignment, IAmAnAssignment
{
    public AssignmentTemplate()
    {
        Load("Input/09.txt");
    }
    
    protected override void ReadLine(string line)
    {
        //Console.WriteLine($"Current line in file: {CurrentLine}");
    }
   
    public override void Process()
    {
       
        Output = "";
    }
}
