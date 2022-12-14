namespace AdventOfCode
{
	public class Assignment01A : Assignment, IAmAnAssignment
	{
		private readonly List<Elve> _list = new();

		public Assignment01A()
		{
			Load("Input/01.txt");
		}

		public override void Process()
		{
			var elve = _list.MaxBy(e => e.Inventory.Sum());

			Output = $"{elve?.ElveName} carries {elve?.Inventory.Sum()} calories";
		}

		protected override void ReadLine(string line)
		{
			if (int.TryParse(line, out var value))
			{
				if (_list.Count == 0)
					_list.Add(new Elve(_list.Count + 1));

				_list[^1].Inventory.Add(value);
			}
			else
			{
				_list.Add(new Elve(_list.Count + 1));
			}
		}

		private class Elve
		{
			public Elve(int elveNumber)
			{
				ElveName = $"Elve {elveNumber}";
			}

			public string ElveName { get; }
			public List<int> Inventory { get; } = new();
		}
	}
}
