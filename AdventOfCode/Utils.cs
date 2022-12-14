namespace AdventOfCode
{
	public static class Utils
	{
		public static void Empty()
		{
			Console.WriteLine("");
		}

		public static void Title(string title)
		{
			const int titleLength = 60;
			Console.WriteLine(new string('=', titleLength));
			Console.Write(new string('=', (titleLength - title.Length) / 2 - 2));
			Console.Write($" {title} ");
			Console.Write(new string('=', titleLength - 2 - title.Length - ((titleLength - title.Length) / 2 - 2)));
			Console.Write("\n");
			Console.WriteLine(new string('=', titleLength));
		}

		public static List<Type> LoadAssignments()
		{
			var assignments = new List<Type>();
			var assignmentTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes())
				.Where(t => typeof(IAmAnAssignment).IsAssignableFrom(t));
			foreach (var assignmentType in assignmentTypes)
				if (!assignmentType.IsInterface && !assignmentType.IsAbstract)
					assignments.Add(assignmentType);

			return assignments;
		}
	}
}
