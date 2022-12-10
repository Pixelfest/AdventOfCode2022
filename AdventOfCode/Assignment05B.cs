namespace AdventOfCode;

public class Assignment05B : Assignment, IAmAnAssignment
{
    public Assignment05B()
    {
        Load("Input/05.txt");
    }

    private Stack<Char>[]? _list = null;
    bool start = true;

    protected override void ReadLine(string line)
    {
        if (string.IsNullOrWhiteSpace(line))
        {
            start = false;

            for (int i = 0; i < _list.Length; i++)
            {
                var reverse = new Stack<Char>();
                
                while (_list[i].Count != 0)
                {
                    reverse.Push(_list[i].Pop());
                }

                _list[i] = reverse;
            }

            return;
        }

        if (line[1] == '1')
            return;
        
        if (start)
        {
            if (_list == null)
            {
                _list = new Stack<Char>[(line.Length + 1) / 4];
                for (int i = 0; i < _list.Length; i++)
                {
                    _list[i] = new Stack<char>();
                }
            }

            for (int i = 1; i < line.Length; i += 4)
            {
                if (char.IsLetter(line[i]))
                {
                    _list[(i - 1) / 4].Push(line[i]);
                }
            }
        }
        else
        {
            var split = line.Split(" ");
            int count = int.Parse(split[1]);
            int from =  int.Parse(split[3]) - 1;
            int to =  int.Parse(split[5]) - 1;

            Stack<Char> temp = new Stack<char>();
                
            for (int i = 0; i < count; i++)
            {
                
                var value = _list[from].Pop();
                temp.Push(value);
            }

            while (temp.Count > 0)
            {
                _list[to].Push(temp.Pop());
            }
        }
    }
    
    public override void Process()
    {
        Output = string.Concat(_list.Select(stack => stack.Peek()));
    }
}
