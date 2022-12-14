using System.Text;

namespace AdventOfCode
{
	public class Assignment03A : Assignment, IAmAnAssignment
	{
		private readonly List<Elf> _list = new();

		public Assignment03A()
		{
			Load("Input/03.txt");
		}

		public override void Process()
		{
			var total = 0;

			foreach (var elf in _list)
			{
				var item = elf.Inventory1.FirstOrDefault(item1 => elf.Inventory2.Contains(item1));

				total += item;
			}

			Output = $"The total is {total}";
		}

		protected override void ReadLine(string line)
		{
			var half = line.Length / 2;

			var elf = new Elf
			{
				Inventory1 = line.Take(half).Select(Get).ToList(), Inventory2 = line.Skip(half).Select(Get).ToList()
			};
			_list.Add(elf);
		}

		private byte Get(char a)
		{
			var b = Encoding.ASCII.GetBytes(a.ToString())[0];

			if (b > 96 && b < 123)
				b = (byte)(b - 96);
			else
				b = (byte)(b - 64 + 26);

			return b;
		}

		private class Elf
		{
			public List<byte> Inventory1 { get; init; } = new();
			public List<byte> Inventory2 { get; init; } = new();
		}
	}
}
