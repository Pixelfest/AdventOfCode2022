// See https://aka.ms/new-console-template for more information
void Empty()
{
    Console.WriteLine("");
}
void Title(string title)
{
    const int titleLength = 60;
    Console.WriteLine(new string('=', titleLength));
    Console.Write(new string('=', (titleLength - title.Length) / 2 - 2));
    Console.Write($" {title} ");
    Console.Write(new string('=', titleLength - 2 - title.Length - ((titleLength - title.Length) / 2 - 2)));
    Console.Write("\n");
    Console.WriteLine(new string('=', titleLength));
}

Title("Hello");
Empty();

var assignmentReader = new AssignmentReader01("Input/01.txt");

Title("Input");
assignmentReader.PrintInput();
Empty();

Title("Output");
assignmentReader.PrintOutput();
Empty();
Empty();

Console.WriteLine("Saving output...");
assignmentReader.SaveOutput("Output/01.txt");
Console.WriteLine("Done, bye!");

Console.ReadLine();
