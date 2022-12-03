namespace AdventOfCode;

public class Assignment02A : Assignment, IAmAnAssignment
{
    class Elve
    {
        public (string, string) Inventory { get; set; }
        public int Score { get; set; }
    }
    private readonly List<Elve> _list = new();

    public Assignment02A()
    {
        Load("Input/02.txt");
    }

    public override void Process()
    {
        foreach (var elf in _list)
        {
            switch (elf.Inventory.Item1)
            {
                case "A": // Rock
                    switch (elf.Inventory.Item2)
                    {
                        case "X": elf.Score += 3; // Scissors
                            break;
                        case "Y": elf.Score += 4; // Rock
                            break; 
                        case "Z": elf.Score += 8; // Paper
                            break; 
                    }
                    break;
                case "B": // Paper
                    switch (elf.Inventory.Item2)
                    {
                        case "X": elf.Score += 1; // Rock
                            break;
                        case "Y": elf.Score += 5; // Paper
                            break; 
                        case "Z": elf.Score += 9; // Scissors
                            break; 
                    }
                    break;

                case "C": // Scissors
                    switch (elf.Inventory.Item2)
                    {
                        case "X": elf.Score += 2; // Paper
                            break;
                        case "Y": elf.Score += 6; // Scissors
                            break; 
                        case "Z": elf.Score += 7; // Rock
                            break; 
                    }
                    break;
            }
        }
        
        Console.WriteLine($"Input {_list.Count}");
        Output = $"{_list.Select(l => l.Score).Sum()}";
    }

    protected override void ReadLine(string line)
    {
        _list.Add(new Elve { Inventory = (line.Split(' ')[0], line.Split(' ')[1])});
    }
}
