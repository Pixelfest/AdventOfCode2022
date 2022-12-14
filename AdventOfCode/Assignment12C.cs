using System.Drawing;
using System.Drawing.Imaging;

namespace AdventOfCode
{
	public class Assignment12C : Assignment, IAmAnAssignment
	{
		private (int x, int y) end;
		private int index;
		private bool init;

		private MapPoint[,] map;
		private (int x, int y) start;

		public Assignment12C()
		{
			Load("Input/12.txt");
		}

		private int MapSizeX { get; set; }
		private int MapSizeY { get; set; }


		public override void Process()
		{
			var currentPosition = end;

			AnalyzePoint(currentPosition, (currentPosition.x + 1, currentPosition.y));
			AnalyzePoint(currentPosition, (currentPosition.x, currentPosition.y - 1));
			AnalyzePoint(currentPosition, (currentPosition.x, currentPosition.y + 1));
			AnalyzePoint(currentPosition, (currentPosition.x - 1, currentPosition.y));

			using Bitmap bmp = new(MapSizeX, MapSizeY);
			using var gfx = Graphics.FromImage(bmp);
			//gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

			var colors = new List<Color>();
			for (var i = 0; i < 255; i++)
				colors.Add(Color.FromArgb(255, i, 0));
			for (var i = 0; i < 255; i++)
				colors.Add(Color.FromArgb(255 - i, 255, 0));
			for (var i = 0; i < 255; i++)
				colors.Add(Color.FromArgb(0, 255, i));
			for (var i = 0; i < 255; i++)
				colors.Add(Color.FromArgb(0, 255 - i, 255));
			for (var i = 0; i < 255; i++)
				colors.Add(Color.FromArgb(i, 0, 255));
			//for (int i = 0; i < 255; i++)
			//    colors.Add(Color.FromArgb(0, 255-i, i));

			var maxDistance = map.Cast<MapPoint>().Select(m => m.DistanceToEnd).Max();
			var per = (colors.Count - 1) / (float)maxDistance;
			for (var y = 0; y < map.GetLength(1); y++)
			for (var x = 0; x < map.GetLength(0); x++)
			{
				var m = map[x, y];

				//var value = (int)((255f / 26) * m.Elevation);
				//bmp.SetPixel(x, y, colors[(int)((colors.Count/26f)*m.Elevation)]);
				var color = m.DistanceToEnd < 0 ? Color.FromArgb(255, 0, 0, 0) : colors[(int)(per * m.DistanceToEnd)];
				bmp.SetPixel(x, y, color);
			}

			bmp.Save("output.png", ImageFormat.Png);


			Console.WriteLine("Result");
			// for (int y = 0; y < map.GetLength(1); y++)
			// {
			//     for (int x = 0; x < map.GetLength(0); x++)
			//     {
			//         var note = (x, y) == end ? "E" : (x, y) == start ? "S" : null;
			//         Console.Write($"{note ?? (map[x, y].DistanceToEnd < 0 ? "." : (map[x, y].DistanceToEnd.ToString().Last().ToString()))}");
			//     }
			//
			//     Console.Write("\n");
			// }

			Console.WriteLine($"Part 1: {map[start.x, start.y].DistanceToEnd}");
			Console.WriteLine(
				$"Part 2: {map.Cast<MapPoint>().Where(m => m.Elevation == 0 && m.DistanceToEnd != -1).Select(m => m.DistanceToEnd).Min().ToString()}");
			Output = map.Cast<MapPoint>().Where(m => m.Elevation == 0 && m.DistanceToEnd != -1)
				.Select(m => m.DistanceToEnd).Min().ToString();
		}

		protected override void ReadLine(string line)
		{
			MapSizeX = line.Length;
			MapSizeY = TotalLines;

			if (!init)
			{
				init = true;
				map = new MapPoint[MapSizeX, MapSizeY];
			}

			for (var i = 0; i < MapSizeX; i++)
			{
				map[i, index] = new MapPoint();

				if (line[i] == 'S')
				{
					start = (i, index);
					map[i, index].Elevation = 'a' - 'a';
					map[i, index].DistanceToEnd = -1;
				}
				else if (line[i] == 'E')
				{
					end = (i, index);
					map[i, index].Elevation = 'z' - 'a';
					map[i, index].DistanceToEnd = 0;
				}
				else
				{
					map[i, index].Elevation = line[i] - 'a';
				}
			}

			index++;
		}

		public bool IsValidPath((int x, int y) here, (int x, int y) there)
		{
			return
				map[here.x, here.y].Elevation <= map[there.x, there.y].Elevation ||
				map[here.x, here.y].Elevation - map[there.x, there.y].Elevation == 1;
		}

		public void AnalyzePoint((int x, int y) here, (int x, int y) there)
		{
			if (
				there.x < 0 || there.y < 0 || there.x > MapSizeX - 1 || there.y > MapSizeY - 1 ||
				(map[there.x, there.y].DistanceToEnd != -1 &&
				 map[there.x, there.y].DistanceToEnd <= map[here.x, here.y].DistanceToEnd + 1) ||
				!IsValidPath(here, there))
				return;

			// Path is valid and is not already defined
			var currentSteps = map[here.x, here.y].DistanceToEnd;

			currentSteps++;

			map[there.x, there.y].DistanceToEnd = currentSteps;

			if ((there.x, there.y + 1) != here)
				AnalyzePoint(there, (there.x, there.y + 1));
			if ((there.x, there.y - 1) != here)
				AnalyzePoint(there, (there.x, there.y - 1));
			if ((there.x + 1, there.y) != here)
				AnalyzePoint(there, (there.x + 1, there.y));
			if ((there.x - 1, there.y) != here)
				AnalyzePoint(there, (there.x - 1, there.y));
		}

		public struct MapPoint
		{
			public MapPoint()
			{
				Elevation = 0;
				DistanceToEnd = -1;
			}

			public int Elevation { get; set; }
			public int DistanceToEnd { get; set; }
		}
	}
}
