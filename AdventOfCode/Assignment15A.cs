namespace AdventOfCode
{
	public class Assignment15A : Assignment, IAmAnAssignment
	{
		// 4822564 too low
		
		private List<Sensor> Sensors = new List<Sensor>();
		private int MaxX = int.MinValue;
		private int MinX = int.MaxValue;
		private int MaxY = int.MinValue;
		private int MinY = int.MaxValue;

		public Assignment15A()
		{
			Load("Input/15.txt");
		}

		public override void Process()
		{
			CreateMap();

			int lineToCheck = 2000000;
	
			var locations = CalculateLine(lineToCheck);
			//Console.WriteLine($"{string.Join("",locations.Select(l => l == 0 ? " " : l.ToString()))}");
			// for (int i = MinY; i <= MaxY; i++)
			// {
			// 	var locations = CalculateLine(i);
			// }

			Console.Write("\n");
			//10 ......###########################...
			//10     ..####B######################..

			Output = locations.Count(l => l == 1).ToString();
		}

		private List<int> CalculateLine(int lineToCheck)
		{
			List<(int min, int max, string name, int? beacon)> overlaps = new List<(int, int, string, int?)>();

			foreach (var sensor in Sensors)
			{
				var overlap = sensor.Taken(lineToCheck);

				if (overlap != null)
				{
					//Console.WriteLine($"Sensor '{overlap.Value.name}' overlaps from {overlap.Value.min} to {overlap.Value.max}");
					overlaps.Add(overlap.Value);
				}
			}

			List<int> locations = new List<int>();
			List<int> beacons = overlaps.Where(l => l.beacon != null).Select(l => l.beacon.Value).ToList();
			
			for (int i = MinX; i < MaxX; i++)
			{
				int result = 0;

				foreach (var overlap in overlaps)
				{
					if (i >= overlap.min && i <= overlap.max)
					{
						result = 1;
						break;
					}
				}

				if (beacons.Any(b => b == i))
					result = 2;
				
				locations.Add(result);
			}

			return locations;
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

			public (int min, int max, string name, int? beacon)? Taken(int row)
			{
				int? beaconLocation = null;
				if (row == ClosestBeacon.y)
					beaconLocation = ClosestBeacon.x;
				
				if (row >= Location.y - Range && row <= Location.y + Range)
				{
					int steps = Math.Abs(row - Location.y);
					int rowRange = Range - steps;
					return (Location.x - rowRange, Location.x + rowRange, SensorName, beaconLocation);
				}

				return null;
			}
		}
	}
	
	
}
