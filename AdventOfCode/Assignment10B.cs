using System.Text;

namespace AdventOfCode;

public class Assignment10B : Assignment, IAmAnAssignment
{
    public Assignment10B()
    {
        Load("Input/10.txt");
    }

    private List<int> Instructions = new List<int> { };
    private List<int> Cycles = new List<int> {};
    
    protected override void ReadLine(string line)
    {
        if(line == "noop")
            Instructions.Add(0);
        else
        {
            Instructions.Add(int.Parse(line.Split(" ")[1]));
        }
        //Console.WriteLine($"Current line in file: {CurrentLine}");
    }
   
    public override void Process()
    {
        
        for (int i = 0; i < Instructions.Count; i++)
        {
            int valueAtStart = 1;
            if(i > 0)
                valueAtStart = Cycles.Last();

            Cycles.Add(valueAtStart);

            if (Instructions[i] != 0)
            {
                Cycles.Add(valueAtStart + Instructions[i]);
            }
        }

        int total = 0;

        List<List<bool>> matrix = new List<List<bool>>();
        List<bool> currentLine = new List<bool>();
        for (int i = 0; i < Cycles.Count; i += 1)
        {
            var value = 1;
            if(i > 0)
                value = Cycles[i-1];

            if (currentLine.Count == 40)
            {
                matrix.Add(currentLine);
                currentLine = new List<bool>();
            }
            
            currentLine.Add(currentLine.Count-1 <= value && currentLine.Count+1 >= value);
        }
        matrix.Add(currentLine);

        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < matrix.Count; i++)
        {
            builder.Append("\n");
            for (int j = 0; j < matrix[i].Count; j++)
            {
                builder.Append(matrix[i][j] ? "#" : ".");
            }
        }
        
        Output = builder.ToString();
    }
}
