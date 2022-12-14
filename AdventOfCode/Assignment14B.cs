using System.Drawing;
using System.Drawing.Imaging;
using System.Xml.Linq;

namespace AdventOfCode
{
	public class Assignment14B : Assignment, IAmAnAssignment
	{
		// 31721 too low
		private int[,] cave;
		private readonly List<List<(int x, int y)>> instructions = new();

		public Assignment14B()
		{
			Load("Input/14.txt");
		}

		public override void Process()
		{
			CreateMap();
			RenderCave("Startb.png");
			int result = SimulateSand(500);
			RenderCave("Finishb.png");
			// Take the max width of the lines
			// Take the max height of the lines + 1

			// First grain of sand to reach the bottom of the grid is the result

			Output = result.ToString();
		}

		private bool done = false;
		private int SimulateSand(int fallLocation)
		{
			int grainCounter = 0;
			while (!done)
			{
				grainCounter++;
				Console.WriteLine($"Graincount: {grainCounter}");
				(int x, int y) grain = (fallLocation, 0);
				bool stopped = false;
				while (!stopped)
				{
					(grain.x, grain.y, stopped) = Fall(grain);

					if (stopped && grain.x == fallLocation && grain.y == 0)
						done = true;
				}
			}

			return grainCounter;
		}

		private (int x, int y, bool couldFall) Fall((int x, int y) grain)
		{
			// end of the map
			if (grain.y + 1 == cave.GetLength(1))
			{
				cave[grain.x, grain.y] = 2;
				return (grain.x, grain.y, true);
			}

			if (cave[grain.x, grain.y + 1] == 0)
			{
				return (grain.x, grain.y + 1, false);
			}
		
			if (cave[grain.x - 1, grain.y + 1] == 0)
			{
				return (grain.x - 1, grain.y + 1, false);
			}
			
			if (cave[grain.x + 1, grain.y + 1] == 0)
			{
				return (grain.x + 1, grain.y + 1, false);
			}

			// Grain has stopped
			cave[grain.x, grain.y] = 2;
			return (grain.x, grain.y, true);
		}
		
		protected override void ReadLine(string line)
		{
			var coordinates = line.Split(" -> ");
			var currentLine = new List<(int, int)>();

			foreach (var coordinate in coordinates)
			{
				var xy = coordinate.Split(",");
				currentLine.Add((int.Parse(xy[0]), int.Parse(xy[1])));
			}

			instructions.Add(currentLine);
		}

		private void CreateMap()
		{
			var maxX = instructions.SelectMany(i => i.Select(c => c.x)).Max() + 1 + 300;
			var maxY = instructions.SelectMany(i => i.Select(c => c.y)).Max() + 2;

			cave = new int[maxX, maxY];

			foreach (var instruction in instructions)
			{
				(int x, int y)? currentLocation = null;

				foreach (var tuple in instruction)
				{
					if (currentLocation == null)
						currentLocation = tuple;

					DrawLine(currentLocation.Value, tuple);

					currentLocation = tuple;
				}
			}
		}

		private void DrawLine((int x, int y) from, (int x, int y) to)
		{
			var directionX = from.x != to.x;
			if (directionX)
			{
				int shift = to.x - from.x > 0 ? 1 : -1;
				for (int xd = from.x;; xd += shift)
				{
					cave[xd, from.y] = 1;
					if (xd == to.x)
						break;
				}
			}
			else
			{
				int shift = to.y - from.y > 0 ? 1 : -1;
				for (int yd = from.y;; yd += shift)
				{
					cave[from.x, yd] = 1;
					if (yd == to.y)
						break;
				}
			}
		}

		private void RenderCave(string fileName)
		{
			using Bitmap bmp = new(cave.GetLength(0), cave.GetLength(1));
			using var gfx = Graphics.FromImage(bmp);

			Dictionary<int, Color> mappings = new Dictionary<int, Color>
			{
				{ 0, Color.White }, 
				{ 1, Color.Black }, 
				{ 2, Color.Brown }
			};

			for (int y = 0; y < cave.GetLength(1); y++)
			{
				for (int x = 0; x < cave.GetLength(0); x++)
				{
					bmp.SetPixel(x, y, mappings[cave[x,y]]);
				}
			}
			
			bmp.Save(fileName, ImageFormat.Png);
			//Test
		}
	}
}
