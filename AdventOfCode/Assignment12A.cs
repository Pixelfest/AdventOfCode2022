namespace AdventOfCode;

public class Assignment12A : Assignment, IAmAnAssignment
{
    public Assignment12A()
    {
        Load("Input/12.txt");
    }

    public struct MapPoint
    {
        public MapPoint()
        {
            Elevation = 0;
            DistanceToEnd = -1;
            Tried = false;
        }

        public bool Tried { get; set; }
        public int Elevation { get; set; }
        public int DistanceToEnd { get; set; }
    }

    private MapPoint[,] map;
    private (int x, int y) start;
    private (int x, int y) end;
    private (int x, int y) currentPosition;
    private bool init;
    private int index;
    private int stepCount;
    private int MapSizeX { get; set; }
    private int MapSizeY { get; set; }

    protected override void ReadLine(string line)
    {
        MapSizeX = line.Length;
        MapSizeY = TotalLines;

        if (!init)
        {
            init = true;
            map = new MapPoint[MapSizeX, MapSizeY];
        }

        for (int i = 0; i < MapSizeX; i++)
        {
            map[i, index] = new MapPoint();

            if (line[i] == 'S')
            {
                start = (i, index);
                map[i, index].Elevation = 'a' - 'a';
                map[i, index].DistanceToEnd = 0;
                map[i, index].Tried = true;
            }
            else if (line[i] == 'E')
            {
                end = (i, index);
                map[i, index].Elevation = 'z' - 'a';
                map[i, index].DistanceToEnd = -1;
            }
            else
                map[i, index].Elevation = line[i] - 'a';
        }

        index++;
        //Console.WriteLine($"Current line in file: {CurrentLine}");
    }

    public bool IsValidPath((int x, int y) here, (int x, int y) there)
    {
        //return Math.Abs(map[here.x, here.y].Elevation - map[there.x, there.y].Elevation) <= 1;
        return
            map[here.x, here.y].Elevation >= map[there.x, there.y].Elevation ||
            map[here.x, here.y].Elevation - map[there.x, there.y].Elevation == -1;
    }

    private bool solved = false;

    public bool AnalyzePoint((int x, int y) here, (int x, int y) there)
    {
        if (
            there.x < 0 || there.y < 0 || there.x > MapSizeX - 1 || there.y > MapSizeY - 1 ||
            //map[there.x, there.y].Tried ||
            (map[there.x, there.y].DistanceToEnd != -1 && map[there.x, there.y].DistanceToEnd <= map[here.x, here.y].DistanceToEnd+1) ||
            !IsValidPath(here, there))
            return false;

        // Path is valid and is not already defined
        int currentSteps = map[here.x, here.y].DistanceToEnd;
        
       // Console.WriteLine($"{(there)}, {currentSteps}");

        currentSteps++;

        map[there.x, there.y].DistanceToEnd = currentSteps;

        //if (there == end)
        //    return true;
        //else
        //{
            bool result = false;

           if ((there.x, there.y + 1) != here)
                result = AnalyzePoint(there, (there.x, there.y + 1));
            if (!result && (there.x, there.y - 1) != here)
                result = AnalyzePoint(there, (there.x, there.y - 1));
            if (!result && (there.x + 1, there.y) != here)
                result = AnalyzePoint(there, (there.x + 1, there.y));
            if (!result && (there.x - 1, there.y) != here)
                result = AnalyzePoint(there, (there.x - 1, there.y));
 
            map[here.x, here.y].Tried = true;

            return result;
        //}
    }


    public override void Process()
    {
        currentPosition = start;

        Console.WriteLine($"Solved: {AnalyzePoint(currentPosition, (currentPosition.x + 1, currentPosition.y))}");
       Console.WriteLine($"Solved: {AnalyzePoint(currentPosition, (currentPosition.x, currentPosition.y - 1))}");
         Console.WriteLine($"Solved: {AnalyzePoint(currentPosition, (currentPosition.x, currentPosition.y + 1))}");
        Console.WriteLine($"Solved: {AnalyzePoint(currentPosition, (currentPosition.x - 1, currentPosition.y))}");


        // for (int y = 0; y < map.GetLength(1); y++)
        // {
        //     for (int x = 0; x < map.GetLength(0); x++)
        //     {
        //         var note = (x, y) == end ? "E" : (x, y) == start ? "S" : " ";
        //
        //         Console.Write($"{map[x, y].Elevation:D4}{note} ");
        //     }
        //
        //     Console.Write("\n");
        // }
        //
        Console.WriteLine("Result");
        for (int y = 0; y < map.GetLength(1); y++)
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                var note = (x, y) == end ? "E" : (x, y) == start ? "S" : null;
                //Console.Write($"{map[x, y].DistanceToEnd:D4}{note} ");
                Console.Write($"{note ?? (map[x, y].DistanceToEnd < 0 ? "." : (map[x, y].DistanceToEnd.ToString().Last().ToString()))}");
            }
        
            Console.Write("\n");
        }

        
        //Output = map[start.x, start.y].DistanceToEnd.ToString();
        Output = map[end.x, end.y].DistanceToEnd.ToString();
    }
}
