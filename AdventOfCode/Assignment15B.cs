namespace AdventOfCode
{
	public class Assignment15B : Assignment, IAmAnAssignment
	{
		// 4822564 too low
		
		private List<Sensor> Sensors = new List<Sensor>();
		private int MaxX = int.MinValue;
		private int MinX = int.MaxValue;
		private int MaxY = int.MinValue;
		private int MinY = int.MaxValue;

		public Assignment15B()
		{
			Load("Input/15.txt");
		}

		public override void Process()
		{
			CreateMap();
			MinY = 0;
			MinX = 0;
			MaxY = 4000000;
			MaxX = 4000000;
//			int lineToCheck = 2000000;
			int locX = 0;
			int locY = 0;

			Parallel.For(0, 4000000, (y, state) =>
			{
				if (y % 1000 == 0)
					Console.WriteLine(y);

				var locations = CalculateLine(y);

				if (locations != null)
				{
					locX = locations.Value;//.ToList().IndexOf(false);
					locY = y;
					Output = ((long)4000000 * (long)locX + locY).ToString();
					Console.WriteLine("SOLUTION: " + Output);
					state.Break();
					//break;
				}
			});
				
			// for (int y = 0; y <= 4000000; y++)
			// {
			// 	if(y % 100 == 0)
			// 		Console.WriteLine(y);
			// 	
			// 	var locations = CalculateLine(y);
			//
			// 	if (locations.Any(l => !l))
			// 	{
			// 		locX = locations.ToList().IndexOf(false);
			// 		locY = y;
			// 		break;
			// 	}
			// }

			Output = ((long)4000000 * (long)locX + locY).ToString();
			//Console.WriteLine($"{string.Join("",locations.Select(l => l == 0 ? " " : l.ToString()))}");
			// for (int i = MinY; i <= MaxY; i++)
			// {
			// 	var locations = CalculateLine(i);
			// }

			Console.Write("\n");
			//10 ......###########################...
			//10     ..####B######################..

			//Output = locations.Count(l => l == 1).ToString();
		}
		public static IEnumerable<Range> Collapse(IEnumerable<Range> ranges)
		{
			if(ranges == null || !ranges.Any())
				yield break;

			var orderdList = ranges.OrderBy(r => r.Start.Value);
			var firstRange = orderdList.First();

			var min = firstRange.Start;
			var max = firstRange.End;

			foreach (var current in orderdList.Skip(1))
			{
				if (current.End.Value > max.Value && current.Start.Value > max.Value)
				{
					yield return new Range(min, max);
					min = current.Start;
				}
				max = max.Value > current.End.Value ? max : current.End;
			}
			yield return new Range(min, max);
		}
		
		private int? CalculateLine(int lineToCheck)
		{
			List<Range> overlaps = new List<Range>();

			foreach (var sensor in Sensors)
			{
				var overlap = sensor.Taken(lineToCheck);

				if (overlap != null)
				{
					int min = overlap.Value.min < 0 ? 0 : overlap.Value.min;
					int max = overlap.Value.max > 4000000 ? 4000000 : overlap.Value.max;
					//Console.WriteLine($"Sensor '{overlap.Value.name}' overlaps from {overlap.Value.min} to {overlap.Value.max}");
					overlaps.Add(new Range(min, max));
				}
			}

			var newOverlaps = Collapse(overlaps);

			if (newOverlaps.Count() > 1)
			{
				return newOverlaps.First().End.Value + 1;
			}
			else
			{
				return null;
			}
			
			bool[] locations = new bool[MaxX];

			// for (int j = 0; j < MaxX; j++)
			// {
			// 	locations[j] = overlaps.Any(o => o.min < j && o.max > j);
			// }

			// overlaps = overlaps.OrderBy(o => o.min).ToList();
			// var list = new List<(int x, int y)>  { (int.MaxValue, int.MinValue)};
			// foreach ((int min, int max) overlap in overlaps)
			// {
			// 	if(overlap.min < list[list.Count].x)
			// }
			
			// foreach (var overlap in overlaps)
			// {
			// 	
			// 	//if (!locations.Any(l => !l))
			// 	//	return locations;
			// 	
			// 	for (int i = Math.Max(overlap.min, 0); i < Math.Min(overlap.max, 4000000); i++)
			// 		locations[i] = true;
			// }
			//List<int> locations = new List<int>();
			//List<int> beacons = overlaps.Where(l => l.beacon != null).Select(l => l.beacon.Value).ToList();
			
			// for (int i = MinX; i < MaxX; i++)
			// {
			// 	int result = 0;
			//
			// 	foreach (var overlap in overlaps)
			// 	{
			// 		if (i >= overlap.min && i <= overlap.max)
			// 		{
			// 			result = 1;
			// 			break;
			// 		}
			// 	}
			//
			// 	//if (beacons.Any(b => b == i))
			// 	//	result = 2;
			//
			// 	locations.Add(result);
			//
			// 	if (result == 0)
			// 		break;
			// }
			//
//			return locations;
		}

		private void CreateMap()
		{
			foreach (var sensor in Sensors)
			{
				if (sensor.Location.x - sensor.Range < MinX)
					MinX = sensor.Location.x - sensor.Range;
				if (sensor.Location.x + sensor.Range > MaxX)
					MaxX = sensor.Location.x + sensor.Range;

				if (sensor.Location.y - sensor.Range < MinY)
					MinY = sensor.Location.y - sensor.Range;
				if (sensor.Location.y + sensor.Range > MaxY)
					MaxY = sensor.Location.y + sensor.Range;
			}
		}

		protected override void ReadLine(string line)
		{
			// Sensor at x=2, y=18: closest beacon is at x=-2, y=15
			var split = line.Split(" ");
			var sensor = new Sensor
			{
				SensorName = line
			};

			int lx = int.Parse(split[2].Replace("x=", "").Replace(",", ""));
			int ly = int.Parse(split[3].Replace("y=", "").Replace(":", ""));
			
			sensor.Location = (lx, ly);
			
			int bx = int.Parse(split[8].Replace("x=", "").Replace(",", ""));
			int by = int.Parse(split[9].Replace("y=", ""));

			sensor.ClosestBeacon = (bx, by);
			
			Sensors.Add(sensor);
			//Console.WriteLine($"Current line in file: {CurrentLine}");
		}

		public class Sensor
		{
			public string SensorName { get; set; }
			public (int x, int y) Location { get; set; }
			public (int x, int y) ClosestBeacon { get; set; }

			public int Range
			{
				get
				{
					return Math.Abs(Location.x - ClosestBeacon.x) + Math.Abs(Location.y - ClosestBeacon.y);
				}
			}

			public (int min, int max)? Taken(int row)
			{
				if (row >= Location.y - Range && row <= Location.y + Range)
				{
					int steps = Math.Abs(row - Location.y);
					int rowRange = Range - steps;
					return (Location.x - rowRange, Location.x + rowRange);
				}

				return null;
			}
		}
	}
	
	
}
