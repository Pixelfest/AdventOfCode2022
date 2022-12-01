using AdventOfCode;

Utils.Title("Welcome to AdventOfCode 2022");

Menu:

var assignments = Utils.LoadAssignments();
var count = 0;
foreach (var assignment in assignments)
{
    Console.WriteLine($"{++count}: {assignment.Name}");
}
Console.WriteLine("E: Exit");
Console.Write("Chose assignment: ");
var input = Console.ReadLine();

if (int.TryParse(input, out int value))
{
    var type = assignments[value - 1];
    var assignment = (IAmAnAssignment)Activator.CreateInstance(type);

    Utils.Title(type.Name);
    
    assignment.Process();
    assignment.PrintOutput();
    assignment.SaveOutput();
    Utils.Empty();
    Utils.Empty();
    goto Menu;
}

if (input.ToUpperInvariant() == "E")
    goto Exit;

Utils.Empty();
Console.WriteLine("I don't understand.");
goto Menu;

Exit:
Console.WriteLine("Bye!");
