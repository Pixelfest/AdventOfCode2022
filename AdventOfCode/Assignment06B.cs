namespace AdventOfCode;

public class Assignment06B : Assignment, IAmAnAssignment
{
    public Assignment06B()
    {
        Load("Input/06.txt");
    }

    private char[] chars;

    protected override void ReadLine(string line)
    {
        chars = line.ToCharArray();
    }
    
    public override void Process()
    {
        List<Char> previous = new List<Char>();
        
        for (int i = 0; i < chars.Length; i++)
        {
            if (i < 14)
            {
                previous.Add(chars[i]);
            }
            else
            {
                if(previous.GroupBy(item => item).Count() < 14)
                {
                    previous.RemoveAt(0);
                    previous.Add(chars[i]);
                }
                else
                {
                    Output = (i).ToString();
                    break;
                }
            }
        }
        
        
    }
}
