namespace AdventOfCode;

public class Assignment01B : Assignment, IAmAnAssignment
{
    class Elve
    {
        public Elve(int elveNumber)
        {
            ElveName = $"Elve {elveNumber}";
        }

        public string ElveName { get; }
        public List<int> Inventory { get; } = new List<int>();
    }
    
    private readonly List<Elve> _list = new();

    public Assignment01B()
    {
        Load("Input/01.txt");
    }

    public override void Process()
    {
        var elves = _list.OrderByDescending(e => e.Inventory.Sum()).Take(3).ToList();

        Output = $"Top three elves ({string.Join(", ", elves.Select(e => e.ElveName))}) carry {elves.Select(e => e.Inventory.Sum()).Sum()} calories";
    }

    protected override void ReadLine(string line)
    {
        if (int.TryParse(line, out int value))
        {
            if(_list.Count == 0)
                _list.Add(new Elve(_list.Count + 1));
                
            _list[^1].Inventory.Add(value);
        }
        else
        {
            _list.Add(new Elve(_list.Count + 1));
        }
    }
}
