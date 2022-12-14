using TextCopy;

namespace AdventOfCode
{
	public abstract class Assignment : IAmAnAssignment
	{
		protected int CurrentLine;
		protected string Output = "There is no output.\nAt least not yet.";
		protected int TotalLines;

		public abstract void Process();

		public void PrintOutput()
		{
			Console.Write(Output);
		}

		public void ToFileAndClipboard()
		{
			Directory.CreateDirectory("Output");
			using var streamWriter = new StreamWriter(Path.Combine("Output", $"{GetType().Name}.txt"),
				new FileStreamOptions { Mode = FileMode.Create, Access = FileAccess.Write });

			streamWriter.Write(Output);

			ClipboardService.SetText(Output);
		}

		protected abstract void ReadLine(string line);

		protected void Load(string fileInput)
		{
			TotalLines = File.ReadAllLines(fileInput).Length;

			using var reader = new StreamReader(fileInput);
			while (!reader.EndOfStream)
			{
				ReadLine(reader.ReadLine() ?? string.Empty);
				CurrentLine++;
			}
		}
	}
}
