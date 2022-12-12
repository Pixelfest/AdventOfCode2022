namespace AdventOfCode;

public class Assignment12B : Assignment, IAmAnAssignment
{
    public Assignment12B()
    {
        Load("Input/12.txt");
    }

    public struct MapPoint
    {
        public MapPoint()
        {
            Elevation = 0;
            DistanceToEnd = -1;
        }

        public int Elevation { get; set; }
        public int DistanceToEnd { get; set; }
    }

    private MapPoint[,] map;
    private (int x, int y) start;
    private (int x, int y) end;
    private (int x, int y) currentPosition;
    private bool init;
    private int index;
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
                map[i, index].DistanceToEnd = -1;
            }
            else if (line[i] == 'E')
            {
                end = (i, index);
                map[i, index].Elevation = 'z' - 'a';
                map[i, index].DistanceToEnd = 0;
            }
            else
                map[i, index].Elevation = line[i] - 'a';
        }

        index++;
    }

    public bool IsValidPath((int x, int y) here, (int x, int y) there)
    {
        return
            map[here.x, here.y].Elevation <= map[there.x, there.y].Elevation ||
            map[here.x, here.y].Elevation - map[there.x, there.y].Elevation == 1;
    }

    public void AnalyzePoint((int x, int y) here, (int x, int y) there)
    {
        if (
            there.x < 0 || there.y < 0 || there.x > MapSizeX - 1 || there.y > MapSizeY - 1 ||
            (map[there.x, there.y].DistanceToEnd != -1 && map[there.x, there.y].DistanceToEnd <= map[here.x, here.y].DistanceToEnd + 1) ||
            !IsValidPath(here, there))
            return;

        // Path is valid and is not already defined
        int currentSteps = map[here.x, here.y].DistanceToEnd;

        currentSteps++;

        map[there.x, there.y].DistanceToEnd = currentSteps;

        if ((there.x, there.y + 1) != here)
            AnalyzePoint(there, (there.x, there.y + 1));
        if ((there.x, there.y - 1) != here)
            AnalyzePoint(there, (there.x, there.y - 1));
        if ((there.x + 1, there.y) != here)
            AnalyzePoint(there, (there.x + 1, there.y));
        if ((there.x - 1, there.y) != here)
            AnalyzePoint(there, (there.x - 1, there.y));
    }


    public override void Process()
    {
        currentPosition = end;

        AnalyzePoint(currentPosition, (currentPosition.x + 1, currentPosition.y));
        AnalyzePoint(currentPosition, (currentPosition.x, currentPosition.y - 1));
        AnalyzePoint(currentPosition, (currentPosition.x, currentPosition.y + 1));
        AnalyzePoint(currentPosition, (currentPosition.x - 1, currentPosition.y));

        Console.WriteLine("Result");
        for (int y = 0; y < map.GetLength(1); y++)
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                var note = (x, y) == end ? "E" : (x, y) == start ? "S" : null;
                Console.Write($"{note ?? (map[x, y].DistanceToEnd < 0 ? "." : (map[x, y].DistanceToEnd.ToString().Last().ToString()))}");
            }

            Console.Write("\n");
        }

        Console.WriteLine($"Part 1: {map[start.x, start.y].DistanceToEnd}");
        Console.WriteLine($"Part 2: {map.Cast<MapPoint>().Where(m => m.Elevation == 0 && m.DistanceToEnd != -1).Select(m => m.DistanceToEnd).Min().ToString()}");
        Output = map.Cast<MapPoint>().Where(m => m.Elevation == 0 && m.DistanceToEnd != -1).Select(m => m.DistanceToEnd).Min().ToString();
    }
}
