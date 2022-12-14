namespace AdventOfCode
{
	public class Assignment05A : Assignment, IAmAnAssignment
	{
		private Stack<char>[]? _list;
		private bool start = true;

		public Assignment05A()
		{
			Load("Input/05.txt");
		}

		public override void Process()
		{
			Output = string.Concat(_list.Select(stack => stack.Peek()));
		}

		protected override void ReadLine(string line)
		{
			if (string.IsNullOrWhiteSpace(line))
			{
				start = false;

				for (var i = 0; i < _list.Length; i++)
				{
					var reverse = new Stack<char>();

					while (_list[i].Count != 0) reverse.Push(_list[i].Pop());

					_list[i] = reverse;
				}

				return;
			}

			if (line[1] == '1')
				return;

			if (start)
			{
				if (_list == null)
				{
					_list = new Stack<char>[(line.Length + 1) / 4];
					for (var i = 0; i < _list.Length; i++) _list[i] = new Stack<char>();
				}

				for (var i = 1; i < line.Length; i += 4)
					if (char.IsLetter(line[i]))
						_list[(i - 1) / 4].Push(line[i]);
			}
			else
			{
				var split = line.Split(" ");
				var count = int.Parse(split[1]);
				var from = int.Parse(split[3]) - 1;
				var to = int.Parse(split[5]) - 1;

				for (var i = 0; i < count; i++)
				{
					var value = _list[from].Pop();
					_list[to].Push(value);
				}
			}
		}
	}
}
