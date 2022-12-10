namespace AdventOfCode;

public class Assignment09A : Assignment, IAmAnAssignment
{
    public Assignment09A()
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

    public List<Position> HeadPositions { get; set; } = new List<Position> { new Position() };
    public List<Position> TailPositions { get; set; } = new List<Position> { new Position() };
    
    protected override void ReadLine(string line)
    {
        //Console.WriteLine($"Current line in file: {CurrentLine}");
        var split = line.Split(" ");
        
        Moves.Add(new Move{ Direction = split[0], Count = int.Parse(split[1])});
    }

    public override void Process()
    {
        foreach (var move in Moves)
        {
            for (int i = 0; i < move.Count; i++)
            {
                var head = Position.FromMove(HeadPositions.Last(), move.Direction);
                HeadPositions.Add(head);

                var currentTail = TailPositions.Last();

                var xD = head.X - currentTail.X;
                var yD = head.Y - currentTail.Y;

                if (Math.Abs(xD) == 2 && Math.Abs(yD) == 0)
                {
                    // move along X
                    TailPositions.Add(new Position(currentTail.X + xD / 2, currentTail.Y));
                }
                else if (Math.Abs(xD) == 0 && Math.Abs(yD) == 2)
                {
                    // move along Y
                    TailPositions.Add(new Position(currentTail.X, currentTail.Y + yD / 2));
                }
                else if (Math.Abs(xD) == 2 && Math.Abs(yD) == 1)
                {
                    // Diagonally direction X
                    TailPositions.Add(new Position(currentTail.X + xD / 2, currentTail.Y + yD));
                }
                else if (Math.Abs(xD) == 1 && Math.Abs(yD) == 2)
                {
                    // Diagonally direction Y
                    TailPositions.Add(new Position(currentTail.X + xD, currentTail.Y + yD / 2));
                    
                }
                else // if (Math.Abs(xD) < 2 && Math.Abs(yD) < 2)
                {
                    // No movement of tail
                    TailPositions.Add(new Position(currentTail.X, currentTail.Y));
                }
            }
        }
        
        //Console.WriteLine($"Total lines in file: {TotalLines}");
        var count = TailPositions.Distinct().Count();
        Output = count.ToString();
    }
}
