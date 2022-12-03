using System.Text;

namespace AdventOfCode;

public class Assignment03A : Assignment, IAmAnAssignment
{
    private int count = 0;
    class Elve
    {
        public string Name { get; set; }
        public List<byte> Inventory1 { get; set; } = new List<byte>();
        public List<byte> Inventory2 { get; set; } = new List<byte>();
    }
    
    private readonly List<Elve> _list = new();

    public Assignment03A()
    {
        Load("Input/03.txt");
    }

    public override void Process()
    {
        var total = 0;
        foreach (var elf in _list)
        {
            var item = elf.Inventory1.FirstOrDefault(item1 => elf.Inventory2.Contains(item1));

            total += item;
        }
        Output = $"The total is {total}";
    }

    protected override void ReadLine(string line)
    {
        var half = line.Length / 2;

        var elve = new Elve
        {
            Name = $"Elve {++count}",
            Inventory1 = line.Take(half).Select(Get).ToList(),
            Inventory2 = line.Skip(half).Select(Get).ToList()
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
