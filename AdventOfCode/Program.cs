namespace AdventOfCode;

static class Program
{
    public static void Main(string[] args)
    {
        Run(new Assignment09A());

        Console.WriteLine("Bye!");
    }

    private static void Run(IAmAnAssignment assignment)
    {
        Utils.Empty();
        Utils.Title(assignment.GetType().Name);

        assignment.Process();
        assignment.PrintOutput();
        assignment.ToFileAndClipboard();
        Utils.Empty();
        Utils.Empty();
    }
}
