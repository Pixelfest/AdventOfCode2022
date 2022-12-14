namespace AdventOfCode
{
	public class Assignment06A : Assignment, IAmAnAssignment
	{
		private char[] chars;

		public Assignment06A()
		{
			Load("Input/06.txt");
		}

		public override void Process()
		{
			var previous = new List<char>();

			for (var i = 0; i < chars.Length; i++)
				if (i < 4)
				{
					previous.Add(chars[i]);
				}
				else
				{
					if (previous.GroupBy(item => item).Count() < 4)
					{
						previous.RemoveAt(0);
						previous.Add(chars[i]);
					}
					else
					{
						Output = i.ToString();
						break;
					}
				}
		}

		protected override void ReadLine(string line)
		{
			chars = line.ToCharArray();
		}
	}
}
