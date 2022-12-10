namespace AdventOfCode;

public class Assignment09B : Assignment, IAmAnAssignment
{
    public Assignment09B()
    {
        Load("Input/09.txt");
    }

    public class Move
    {
        public string Direction { get; set; }
        public int Count { get; set; }
    }
    
    public List<Move> Moves = new List<Move>();
    
    public class Position
    {
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;

        public Position(int x = 0, int y = 0)
        {
            this.X = x;
            this.Y = y;
        }
        
        public static Position FromMove(Position start, string direction)
        {
            var result = new Position { X = start.X, Y = start.Y };

            switch (direction)
            {
                case "U":
                    result.Y++;
                    break;
                case "D":
                    result.Y--;
                    break;
                case "L":
                    result.X--;
                    break;
                case "R":
                    result.X++;
                    break;
            }

            return result;
        }

        public override bool Equals(object? obj)
        {
            var other = (Position?)obj;
            if (other == null)
                return false;
            
            return X == other.X && Y == other.Y;
        }

        protected bool Equals(Position other)
        {
            return X == other.X && Y == other.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }

    public List<List<Position>> Rope { get; set; } = new List<List<Position>>();
    
    protected override void ReadLine(string line)
    {
        //Console.WriteLine($"Current line in file: {CurrentLine}");
        var split = line.Split(" ");
        
        Moves.Add(new Move{ Direction = split[0], Count = int.Parse(split[1])});
    }

    public int JustOne(int input)
    {
        if (input == 0)
            return 0;
        if (input > 0)
            return 1;

        return -1;
    }
    
    public override void Process()
    {
        int ropeLength = 10;

        for (int i = 0; i < ropeLength; i++)
        {
            Rope.Add(new List<Position> { new Position() });
        }

        foreach (var move in Moves)
        {
            for (int i = 0; i < move.Count; i++)
            {
                var head = Position.FromMove(Rope[0].Last(), move.Direction);
                Rope[0].Add(head);

                for (int j = 1; j < ropeLength; j++)
                {
                    var currentTail = Rope[j].Last();

                    var xD = Rope[j - 1].Last().X - currentTail.X;
                    var yD = Rope[j - 1].Last().Y - currentTail.Y;

                    if (Math.Abs(xD) < 2 && Math.Abs(yD) < 2)
                    {
                        // No movement of tail
                        Rope[j].Add(new Position(currentTail.X, currentTail.Y));
                    }
                    else
                    {
                        Rope[j].Add(new Position(currentTail.X + JustOne(xD), currentTail.Y + JustOne(yD)));
                    }
                }
            }
        }

        //Console.WriteLine($"Total lines in file: {TotalLines}");
        var count = Rope.Last().Distinct().Count();
        Output = count.ToString();
    }
}
