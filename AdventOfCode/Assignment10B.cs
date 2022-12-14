using System.Text;

namespace AdventOfCode
{
	public class Assignment10B : Assignment, IAmAnAssignment
	{
		private readonly List<int> Cycles = new();

		private readonly List<int> Instructions = new();

		public Assignment10B()
		{
			Load("Input/10.txt");
		}

		public override void Process()
		{
			for (var i = 0; i < Instructions.Count; i++)
			{
				var valueAtStart = 1;
				if (i > 0)
					valueAtStart = Cycles.Last();

				Cycles.Add(valueAtStart);

				if (Instructions[i] != 0) Cycles.Add(valueAtStart + Instructions[i]);
			}

			var total = 0;

			var matrix = new List<List<bool>>();
			var currentLine = new List<bool>();
			for (var i = 0; i < Cycles.Count; i += 1)
			{
				var value = 1;
				if (i > 0)
					value = Cycles[i - 1];

				if (currentLine.Count == 40)
				{
					matrix.Add(currentLine);
					currentLine = new List<bool>();
				}

				currentLine.Add(currentLine.Count - 1 <= value && currentLine.Count + 1 >= value);
			}

			matrix.Add(currentLine);

			var builder = new StringBuilder();
			for (var i = 0; i < matrix.Count; i++)
			{
				builder.Append("\n");
				for (var j = 0; j < matrix[i].Count; j++) builder.Append(matrix[i][j] ? "#" : ".");
			}

			Output = builder.ToString();
		}

		protected override void ReadLine(string line)
		{
			if (line == "noop")
				Instructions.Add(0);
			else
				Instructions.Add(int.Parse(line.Split(" ")[1]));
			//Console.WriteLine($"Current line in file: {CurrentLine}");
		}
	}
}
