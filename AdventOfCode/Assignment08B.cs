namespace AdventOfCode
{
	public class Assignment08B : Assignment, IAmAnAssignment
	{
		public List<List<Tree>> forest = new();

		public Assignment08B()
		{
			Load("Input/08.txt");
		}

		public override void Process()
		{
			var count = 0;
			var highest = 0;
			for (var i = 0; i < forest.Count; i++)
			for (var j = 0; j < forest[i].Count; j++)
			{
				var tree = forest[i][j];
				if (i == 0 || j == 0 || i == forest.Count - 1 || j == forest.Count - 1)
				{
					tree.IsVisible = true;
					continue;
				}

				// Row
				var left = forest[i].Take(j).ToList();
				var right = forest[i].Skip(j + 1).ToList();

				// Column
				var top = forest.Take(i).Select(r => r[j]).ToList();
				var bottom = forest.Skip(i + 1).Select(r => r[j]).ToList();

				tree.SetScore(left, right, top, bottom);

				if (tree.Score > highest)
					highest = tree.Score;
			}

			Output = highest.ToString();
		}

		protected override void ReadLine(string line)
		{
			Console.WriteLine(CurrentLine);
			forest.Add(line.ToCharArray().Select(c => new Tree { Height = int.Parse(c.ToString()) }).ToList());
		}

		public class Tree
		{
			public int Height { get; set; }
			public bool IsVisible { get; set; }
			public int Score { get; set; }

			public void SetScore(List<Tree> left, List<Tree> right, List<Tree> top, List<Tree> bottom)
			{
				Score = 0;

				if (left.Count == 0 || right.Count == 0 || top.Count == 0 || bottom.Count == 0)
					return;

				left.Reverse();
				top.Reverse();

				Score = GetScore(left);
				Score *= GetScore(right);
				Score *= GetScore(top);
				Score *= GetScore(bottom);
			}

			public int GetScore(List<Tree> input)
			{
				var score = 0;
				for (var i = 0; i < input.Count; i++)
				{
					score++;

					if (input[i].Height >= Height)
						break;
				}

				return score;
			}
		}
	}
}
