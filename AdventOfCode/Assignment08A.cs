namespace AdventOfCode;

public class Assignment08A : Assignment, IAmAnAssignment
{
    public Assignment08A()
    {
        Load("Input/08.txt");
    }

    public class Tree
    {
        public int Height { get; set; }
        public bool IsVisible { get; set; } = false;
    }
    public List<List<Tree>> forest = new List<List<Tree>>();
    
    protected override void ReadLine(string line)
    {
        forest.Add(line.ToCharArray().Select(c => new Tree { Height = int.Parse(c.ToString())}).ToList());
    }

    public override void Process()
    {
        int count = 0;

        for (int i = 0; i < forest.Count; i++)
        {
            for (int j = 0; j < forest[i].Count; j++)
            {
                var tree = forest[i][j];
                if (i == 0 || j == 0 || i == forest.Count - 1 || j == forest.Count - 1)
                {
                    tree.IsVisible = true;
                    continue;
                }

                // Row
                if (forest[i].Take(j).Max(t => t.Height) < tree.Height)
                    tree.IsVisible = true;
                if (forest[i].Skip(j + 1).Max(t => t.Height) < tree.Height)
                    tree.IsVisible = true;
                
                // Column
                if (forest.Take(i).Select(r => r[j]).Max(t => t.Height) < tree.Height)
                    tree.IsVisible = true;
                if (forest.Skip(i + 1).Select(r => r[j]).Max(t => t.Height) < tree.Height)
                    tree.IsVisible = true;
                
            }
        }

        var result = forest.SelectMany(r => r.Where(t => t.IsVisible));
        Output = result.Count().ToString();
    }
}
