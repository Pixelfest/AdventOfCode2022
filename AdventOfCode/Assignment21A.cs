namespace AdventOfCode
{
	public class Assignment21A : Assignment, IAmAnAssignment
	{
		private Dictionary<string, Instructions> instructions = new Dictionary<string, Instructions>();
		public Assignment21A()
		{
			Load("Input/21.txt");
		}

		public override void Process()
		{
			while (!instructions["root"].Value.HasValue)
			{
				foreach (var key in instructions.Keys)
				{
					var currentEntry = instructions[key];
					if (!currentEntry.Value.HasValue)
					{
						if (!currentEntry.Num1.HasValue && instructions[currentEntry.NameNum1].Value.HasValue)
							currentEntry.Num1 = instructions[currentEntry.NameNum1].Value;
						if (!currentEntry.Num2.HasValue && instructions[currentEntry.NameNum2].Value.HasValue)
							currentEntry.Num2 = instructions[currentEntry.NameNum2].Value;

						if (currentEntry.Num1.HasValue && currentEntry.Num2.HasValue)
							currentEntry.Value = currentEntry.Operation(currentEntry.Num1.Value, currentEntry.Num2.Value);
					}
				}
			}
			
			Output = instructions["root"].Value.ToString();
		}

		protected override void ReadLine(string line)
		{
			var split = line.Split(" ");
			var name = split[0].Replace(":", "");
			
			if (split.Length == 2)
			{
				instructions[name] = new Instructions
				{
					Name = name,
					Value = int.Parse(split[1])
				};
			}
			else
			{
				Func<long, long, long> func;
				
				switch (split[2])
				{
					case "*":
						func = (a, b) => a * b;
						break;
					case "/":
						func = (a, b) => a / b;
						break;
					case "-":
						func = (a, b) => a - b;
						break;
					default:
						func = (a, b) => a + b;
						break;
				}
				
				instructions[name] = new Instructions
				{
					Name = name,
					Operation = func,
					NameNum1 = split[1],
					NameNum2 = split[3]
				};
			}
			//Console.WriteLine($"Current line in file: {CurrentLine}");
		}

		public class Instructions
		{
			public string Name { get; set; }
			public long? Value { get; set; }
			public Func<long, long, long> Operation { get; set; }
			
			public string NameNum1 { get; set; }
			public long? Num1 { get; set; }
			
			public string NameNum2 { get; set; }
			public long? Num2 { get; set; }
		}
	}
}
