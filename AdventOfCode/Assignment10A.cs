namespace AdventOfCode
{
	public class Assignment10A : Assignment, IAmAnAssignment
	{
		private readonly List<int> Cycles = new();

		private readonly List<int> Instructions = new();

		public Assignment10A()
		{
			Load("Input/10.sample.txt");
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

			for (var i = 19; i < Cycles.Count; i += 40)
			{
				var value = 1;
				if (i > 0)
					value = Cycles[i - 1];

				var cycle = i + 1;
				total += cycle * value;
			}

			Output = total.ToString();
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
