namespace AdventOfCode;

public class Assignment13A : Assignment, IAmAnAssignment
{
    public Assignment13A()
    {
        Load("Input/13.sample.txt");
    }

    private List<string> input = new List<string>();
    protected override void ReadLine(string line)
    {
        input.Add(line);
    }

    public override void Process()
    {
        int index = 1;
        int total = 0;
        int current = 0;
        
        while (true)
        {
            var string1 = input[current];
            var string2 = input[current + 1];

            if (Compare(string1, string2) > 1)
            {
                total += index;
            }

            index++;
            current += 3;

            if (current > input.Count)
                break;
        }
      
        Output = total.ToString();
    }

    public int Compare(LOL lol1, LOL lol2)
    {
        // [1] versus 2 => make a list
        if (lol1.Value == null && lol2.Value != null)
        {
            lol2.Items.Insert(0, new LOL() { Value = lol2.Value });
            lol2.Value = null;

            return Compare(lol1, lol2);
        }
        else if (lol1.Value != null && lol2.Value == null)
        {
            lol1.Items.Insert(0, new LOL() { Value = lol1.Value });
            lol1.Value = null;

            return Compare(lol1, lol2);
        }

        // 1 versus 2
        if (lol1.Value.HasValue && lol2.Value.HasValue)
        {
            return lol1.Value.Value.CompareTo(lol2.Value.Value);
        }

        // [1] versus [1,2] 
        if (lol1.Items.Any() && !lol2.Items.Any())
        {
            return 1;
        }
        if (!lol1.Items.Any() && lol2.Items.Any())
        {
            return -1;
        }
        
        // Others
        while(lol1.Items.Any())
        return Compare(lol1.Items.First(), lol2.Items.First());
    }

    public LOL Parse(LOL currentLol, string s)
    {
        while (s.Length > 0)
        {
            if (s.StartsWith("["))
            {
//                s = s.Remove(s.Length - 1);
                s = s.Remove(0, 1);
                var newLol = new LOL { Parent = currentLol };
                currentLol.Items.Add(newLol);
                currentLol = newLol;
            }
            else if (s.StartsWith("]"))
            {
                s = s.Remove(0, 1);
                currentLol = currentLol.Parent;
            }
            else if (s.Split(",").Length > 0 && int.TryParse(s.Split(",")[0], out int value))
            {
                currentLol.Items.Add(new LOL { Value = value});
                s = s.Remove(0, value.ToString().Length);
            }
        }

        return currentLol;
    }
    
    public class LOL
    {
        public LOL Parent { get; set; }
        public List<LOL> Items { get; set; } = new List<LOL>();
        public int? Value { get; set; }
    }
}
