namespace AdventOfCode
{
	public class Assignment11A : Assignment, IAmAnAssignment
	{
		private readonly int numberOfRounds;

		public Assignment11A()
		{
			Console.WriteLine(2 / 3);
			Load("Input/11.txt");
			numberOfRounds = 20;
		}
		// 99540 fout te laag
		// 101115 fout te hoog

		private List<Monkey> monkeys { get; } = new();

		public override void Process()
		{
			for (var round = 1; round <= numberOfRounds; round++)
			for (var monkey = 0; monkey < monkeys.Count; monkey++)
			{
				var currentMonkey = monkeys[monkey];
				while (currentMonkey.HasItems)
				{
					(var nextMonkey, var item) = currentMonkey.Handle();
					monkeys[nextMonkey].Items.Enqueue(item);
				}
			}

			var mostInspections = monkeys.Select(m => m.InspectionCount).OrderByDescending(m => m).Take(2).ToList();

			Output = (mostInspections[0] * mostInspections[1]).ToString();
		}

		protected override void ReadLine(string line)
		{
			if (line.StartsWith("Monkey"))
			{
				monkeys.Add(new Monkey());
				return;
			}

			if (string.IsNullOrWhiteSpace(line))
				return;

			var currentMonkey = monkeys.Last();
			switch (line[8])
			{
				case 'n':
					currentMonkey.SetItems(line);
					break;
				case 'i':
					currentMonkey.SetOperation(line);
					break;
				case 'd':
					currentMonkey.SetTest(line);
					break;
				case 'r':
					currentMonkey.SetTrue(line);
					break;
				case 'a':
					currentMonkey.SetFalse(line);
					break;
			}
			//Console.WriteLine($"Current line in file: {CurrentLine}");
		}

		public class Monkey
		{
			public Func<long, long> Operation { get; set; }
			public Func<long, bool> Test { get; set; }

			public int TestIsTrue { get; set; }
			public int TestIsFalse { get; set; }

			public int InspectionCount { get; set; }

			public Queue<long> Items { get; set; } = new();

			public bool HasItems => Items.Count > 0;

			public (int, long) Handle()
			{
				InspectionCount++;

				var currentItem = Items.Dequeue();

				currentItem = Operation(currentItem);

				if (currentItem < 3)
					Console.WriteLine("Item < 3");

				currentItem = currentItem / 3;

				if (currentItem == 0)
					Console.WriteLine("Worry level is 0");

				var dividable = Test(currentItem);

				if (dividable)
					//Console.WriteLine("Item is dividable by whatever");
					return (TestIsTrue, currentItem);
				return (TestIsFalse, currentItem);
			}

			public void SetItems(string s)
			{
				var bla = s.Replace("  Starting items: ", "");

				foreach (var item in bla.Split(", ")) Items.Enqueue(int.Parse(item));
			}

			public void SetOperation(string s)
			{
				var operation = s.Split(" ");
				var symbol = operation[^2];
				var number = operation[^1];

				if (symbol == "*")
				{
					if (number == "old")
						Operation = i => i * i;
					else
						Operation = i => i * int.Parse(number);
				}
				else if (symbol == "+")
				{
					if (number == "old")
						Operation = i => i + i;
					else
						Operation = i => i + int.Parse(number);
				}
			}

			public void SetTest(string s)
			{
				var divideBy = int.Parse(s.Split(" ").Last());

				Test = i => i % divideBy == 0;
			}

			public void SetTrue(string s)
			{
				TestIsTrue = int.Parse(s.Split(" ").Last());
			}

			public void SetFalse(string s)
			{
				TestIsFalse = int.Parse(s.Split(" ").Last());
			}
		}
	}
}
