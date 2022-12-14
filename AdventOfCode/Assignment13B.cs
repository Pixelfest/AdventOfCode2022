namespace AdventOfCode
{
	public class Assignment13B : Assignment, IAmAnAssignment
	{
		private readonly List<string> input = new();

		public Assignment13B()
		{
			Load("Input/13.txt");
		}

		public override void Process()
		{
			var index = 1;
			var total = 0;
			var current = 0;

			var list = new List<LOL>();

			foreach (var s in input)
				if (!string.IsNullOrEmpty(s))
					list.Add(LOL.Parse(s));

			list.Add(LOL.Parse("[[2]]"));
			list.Add(LOL.Parse("[[6]]"));

			list.Sort(new MyCustomComparer());

			var index1 = list.FindIndex(m => m.Original == "[[2]]") + 1;
			var index2 = list.FindIndex(m => m.Original == "[[6]]") + 1;

			Output = (index1 * index2).ToString();
		}

		protected override void ReadLine(string line)
		{
			input.Add(line);
		}


		public class LOL
		{
			public string Original { get; set; }
			public LOL Parent { get; set; }
			public List<LOL> Items { get; set; } = new();
			public int? Value { get; set; }

			public override string ToString()
			{
				if (Items.Any())
					return $"[{string.Join(',', Items.Select(s => s.ToString()).ToList())}]";
				if (Value.HasValue) return $"{Value}";

				return "[]";
			}

			public static LOL Parse(string s, LOL? currentLol = null)
			{
				if (currentLol == null)
				{
					currentLol = new LOL();
					currentLol.Original = s;
				}

				while (s.Length > 0)
					if (s.StartsWith("["))
					{
						s = s.Remove(0, 1);
						var newLol = new LOL { Parent = currentLol };
						currentLol.Items.Add(newLol);
						currentLol = newLol;
					}
					else if (s.StartsWith("]"))
					{
						s = s.Remove(0, 1);
						currentLol = currentLol.Parent;
					}
					else if (s.Split(",").Length > 0 && int.TryParse(s.Split(",")[0], out var value1))
					{
						currentLol.Items.Add(new LOL { Parent = currentLol, Value = value1 });
						s = s.Remove(0, value1.ToString().Length);
					}
					else if (s.StartsWith(","))
					{
						s = s.Remove(0, 1);
					}
					else if (int.TryParse(s.Split("]")[0], out var value2))
					{
						currentLol.Items.Add(new LOL { Parent = currentLol, Value = value2 });
						s = s.Remove(0, value2.ToString().Length);
					}

				return currentLol;
			}
		}

		public class MyCustomComparer : IComparer<LOL>
		{
			public int Compare(LOL? lol1, LOL? lol2)
			{
				if (!string.IsNullOrWhiteSpace(lol1.Original) && !string.IsNullOrWhiteSpace(lol2.Original))
					return Compare(LOL.Parse(lol1.Original, new LOL()), LOL.Parse(lol2.Original, new LOL()));

				if (lol1 == null || lol2 == null)
				{
					if (lol2 == null)
						return 1;
					if (lol1 == null)
						return -1;
				}

				// [1] versus 2 => make a list
				if (lol1.Value == null && lol2.Value != null)
				{
					lol2.Items.Insert(0, new LOL { Parent = lol2, Value = lol2.Value });
					lol2.Value = null;

					return Compare(lol1, lol2);
				}

				if (lol1.Value != null && lol2.Value == null)
				{
					lol1.Items.Insert(0, new LOL { Parent = lol1, Value = lol1.Value });
					lol1.Value = null;

					return Compare(lol1, lol2);
				}

				// 1 versus 2
				if (lol1.Value.HasValue && lol2.Value.HasValue)
				{
					var resultValue = lol1.Value.Value.CompareTo(lol2.Value.Value);

					if (resultValue == 0)
					{
						lol1.Parent.Items.RemoveAt(0); //.Value = null;
						lol2.Parent.Items.RemoveAt(0);

						return Compare(lol1.Parent, lol2.Parent);
					}

					return resultValue;
				}

				// [1] versus [] 
				if (lol1.Items.Any() && !lol2.Items.Any()) return 1;

				if (!lol1.Items.Any() && lol2.Items.Any()) return -1;

				if (!lol1.Value.HasValue && !lol2.Value.HasValue &&
				    !lol1.Items.Any() && !lol2.Items.Any())
				{
					lol1.Parent.Items.RemoveAt(0);
					lol2.Parent.Items.RemoveAt(0);

					return Compare(lol1.Parent, lol2.Parent);
				}

				var result = Compare(lol1.Items.First(), lol2.Items.First());

				if (result == 0)
				{
					lol1.Items.RemoveAt(0);
					lol2.Items.RemoveAt(0);

					return Compare(lol1, lol2);
				}

				return result;
			}
		}
	}
}
