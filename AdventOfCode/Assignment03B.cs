using System.Text;

namespace AdventOfCode;

public class Assignment03B : Assignment, IAmAnAssignment
{
    private int count = 0;
    class Elve
    {
        public string Name { get; set; }
        public List<byte> Inventory1 { get; set; } = new List<byte>();
    }
    
    private readonly List<Elve> _list = new();

    public Assignment03B()
    {
        Load("Input/03.txt");
    }

    public override void Process()
    {
        var total = 0;

        for (int i = 0; i < _list.Count; i+=3)
        {
            var sharedItems = _list[i].Inventory1.Where(item1 => _list[i + 1].Inventory1.Contains(item1));
            var oneItem = sharedItems.Where(itemShared => _list[i + 2].Inventory1.Contains(itemShared)).FirstOrDefault();

            total += oneItem;
        }
        
        Output = $"The total is {total}";
    }

    protected override void ReadLine(string line)
    {
        var elve = new Elve
        {
            Name = $"Elve {++count}",
            Inventory1 = line.Select(Get).ToList()
        };
        _list.Add(elve);
    }

    private byte Get(Char a)
    {
        var b = Encoding.ASCII.GetBytes(a.ToString())[0];

        if (b > 96 && b < 123)
            b = (byte)(b - 96);
        else
            b = (byte)(b - 64 + 26);
        
        return b;
    }
}
