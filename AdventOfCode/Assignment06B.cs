namespace AdventOfCode
{
	public class Assignment06B : Assignment, IAmAnAssignment
	{
		private char[] chars;

		public Assignment06B()
		{
			Load("Input/06.txt");
		}

		public override void Process()
		{
			var previous = new List<char>();

			for (var i = 0; i < chars.Length; i++)
				if (i < 14)
				{
					previous.Add(chars[i]);
				}
				else
				{
					if (previous.GroupBy(item => item).Count() < 14)
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
