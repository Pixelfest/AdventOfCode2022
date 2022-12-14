namespace AdventOfCode
{
	public class Assignment07A : Assignment, IAmAnAssignment
	{
		private readonly Dir baseDir = new() { Name = "/" };
		private Dir currentDir;

		private bool dirmode;
//    private bool dirReadMode

		private bool skip = true;

		public Assignment07A()
		{
			Load("Input/07.txt");
		}

		public override void Process()
		{
			var total = 0;

			var selected = baseDir.Test();

			Output = selected.Select(s => s.Size()).Sum().ToString();
		}

		protected override void ReadLine(string line)
		{
			if (skip)
			{
				currentDir = baseDir;
				skip = false;
				return;
			}

			var commands = line.Split(" ");
			var command = commands[0];

			if (commands[0] == "$")
				dirmode = false;

			if (dirmode)
			{
				if (command == "dir")
					currentDir.dirs.Add(new Dir { Name = commands[1], parent = currentDir });
				else
					currentDir.files.Add(new Fil { Name = commands[1], Size = int.Parse(commands[0]) });
			}
			else
			{
				if (commands[1] == "cd")
				{
					if (commands[2] != "..")
						currentDir = currentDir.dirs.Single(d => d.Name == commands[2]);
					else
						currentDir = currentDir.parent;
				}
				else if (commands[1] == "ls")
				{
					dirmode = true;
				}
			}
		}

		private class Dir
		{
			public readonly List<Dir> dirs = new();
			public readonly List<Fil> files = new();
			public Dir parent { get; set; }
			public string Name { get; set; }

			public int Size()
			{
				var total = 0;

				foreach (var file in files) total += file.Size;

				foreach (var dir in dirs) total += dir.Size();

				return total;
			}

			public List<Dir> Test()
			{
				var list = new List<Dir>();

				foreach (var dir in dirs)
				{
					if (dir.Size() <= 100000) list.Add(dir);
					list.AddRange(dir.Test());
				}

				return list;
			}
		}

		private class Fil
		{
			public string Name { get; set; }
			public int Size { get; set; }
		}
	}
}
