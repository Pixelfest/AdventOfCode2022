namespace AdventOfCode
{
	public class Assignment04B : Assignment, IAmAnAssignment
	{
		private readonly List<Helper> _list = new();

		public Assignment04B()
		{
			Load("Input/04.txt");
		}

		public override void Process()
		{
			var total = 0;

			foreach (var helper in _list)
				if (helper.Elf1.Any(item => helper.Elf2.Contains(item)) ||
				    helper.Elf2.Any(item => helper.Elf1.Contains(item)))
					total++;

			Output = $"{total}";
		}

		protected override void ReadLine(string line)
		{
			var helper = new Helper { Elf1 = Get(line.Split(",")[0]), Elf2 = Get(line.Split(",")[1]) };

			_list.Add(helper);
		}

		private List<int> Get(string s)
		{
			var from = int.Parse(s.Split("-")[0]);
			var to = int.Parse(s.Split("-")[1]);

			return Enumerable.Range(from, to - from + 1).ToList();
		}

		private class Helper
		{
			public List<int> Elf1 { get; init; } = new();
			public List<int> Elf2 { get; init; } = new();
		}
	}
}
