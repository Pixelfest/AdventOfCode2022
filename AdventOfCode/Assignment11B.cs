using System.Numerics;
using System.Reflection.Metadata;
using System.Text;

namespace AdventOfCode;

public class Assignment11B : Assignment, IAmAnAssignment
{
    // 109230 fout te laag
    // 165459765 te laag
    
    private List<Monkey> monkeys { get; set; } = new List<Monkey>();
    private long numberOfRounds;
    
    public Assignment11B()
    {
        Load("Input/11.txt");
        numberOfRounds = 10000;
      }

    protected override void ReadLine(string line)
    {
        if (line.StartsWith("Monkey"))
        {
            monkeys.Add(new Monkey());
            return;
        }

        if (string.IsNullOrWhiteSpace(line))
            return;

        var currentMonkey = monkeys.Last();
        switch (line[8])
        {
            case 'n':
                currentMonkey.SetItems(line);
                break;
            case 'i':
                currentMonkey.SetOperation(line);
                break;
            case 'd':
                currentMonkey.SetTest(line);
                break;
            case 'r':
                currentMonkey.SetTrue(line);
                break;
            case 'a':
                currentMonkey.SetFalse(line);
                break;
        }

        // Keeping values in check by removing all excess dividable by the total of divide-checks of all monkeys multiplies.
        int modulo = 1;
        foreach (var monkey in monkeys)
        {
            modulo *= monkey.DivideBy;
        }

        foreach (var monkey in monkeys)
        {
            monkey.Modulo = modulo;
        }
        //Console.WriteLine($"Current line in file: {CurrentLine}");
    }

    public override void Process()
    {
        for (int round = 1; round <= numberOfRounds; round++)
        {
            Console.WriteLine($"Round {round}");
            for (int monkey = 0; monkey < monkeys.Count; monkey++)
            {
                var currentMonkey = monkeys[monkey];
                while (currentMonkey.HasItems)
                {
                    (int nextMonkey, long item) = currentMonkey.Handle();
                    monkeys[nextMonkey].Items.Enqueue(item);
                }
            }
        }

        var mostInspections = monkeys.Select(m => m.InspectionCount).OrderByDescending(m => m).Take(2).ToList();
        
        Output = (mostInspections[0] * mostInspections[1]).ToString();
    }

    public class Monkey
    {
        public Func<long, long> Operation { get; set; }
        public Func<long, bool> Test { get; set; }

        public int TestIsTrue { get; set; }
        public int TestIsFalse { get; set; }
        public int DivideBy { get; set; }
        public int Modulo { get; set; }

        public long InspectionCount { get; set; }

        public Queue<long> Items { get; set; } = new Queue<long>();

        public bool HasItems => Items.Count > 0;
        public (int, long) Handle()
        {
            InspectionCount++;
            
            var currentItem = Items.Dequeue();

            currentItem = Operation(currentItem);

            currentItem = currentItem % Modulo;
            
            var dividable = Test(currentItem);
            
            if (dividable)
            {
                //Console.WriteLine("Item is dividable by whatever");
                return (TestIsTrue, currentItem);
            }
            else
            {
                return (TestIsFalse, currentItem);
            }
        }

        public void SetItems(string s)
        {
            var bla = s.Replace("  Starting items: ", "");

            foreach (var item in bla.Split(", "))
            {
                Items.Enqueue(int.Parse(item));
            }
        }

        public void SetOperation(string s)
        {
            var operation = s.Split(" ");
            var symbol = operation[^2];
            var number = operation[^1];

            if (int.TryParse(number, out int num))
            {
                if (symbol == "*")
                    Operation = (i) => i * num;
                else
                    Operation = (i) => i + num;
            }
            else
            {
                if (symbol == "*")
                    Operation = (i) => i * i;
                else
                    Operation = (i) => i + i;
            }
        }

        public void SetTest(string s)
        {
            DivideBy = int.Parse(s.Split(" ").Last());
            
            Test = (i) => i % DivideBy == 0;
        }

        public void SetTrue(string s)
        {
            TestIsTrue = int.Parse(s.Split(" ").Last());
        }

        public void SetFalse(string s)
        {
            TestIsFalse = int.Parse(s.Split(" ").Last());
        }
    }
}
