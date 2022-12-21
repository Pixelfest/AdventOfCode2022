using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace AdventOfCode
{
	public class Assignment21B : Assignment, IAmAnAssignment
	{
		private Dictionary<string, Instructions> baseInstructions = new Dictionary<string, Instructions>();
		public Assignment21B()
		{
			Load("Input/21.txt");
		}
		
		public static T CreateDeepCopy<T>(T obj)
		{
			using (var ms = new MemoryStream())
			{
				IFormatter formatter = new BinaryFormatter();
				formatter.Serialize(ms, obj);
				ms.Seek(0, SeekOrigin.Begin);
				return (T)formatter.Deserialize(ms);
			}
		}


		public override void Process()
		{
			bool answer = false;
			while(!answer)
			{
				// Yes, I'm going to figure this out manually :-)
				var value = Console.ReadLine();
				long number = 0;
				
				if(!long.TryParse(value, out number))
					continue;
				
				var instructions = baseInstructions.ToDictionary(k => k.Key, k => (Assignment21B.Instructions)k.Value.Clone());

				instructions["humn"].Value = number;
				
				while (!instructions["root"].Value.HasValue)
				{
					foreach (var key in instructions.Keys)
					{
						var currentEntry = instructions[key];
						if (!currentEntry.Value.HasValue)
						{
							if (!currentEntry.Num1.HasValue && instructions[currentEntry.NameNum1].Value.HasValue)
								currentEntry.Num1 = instructions[currentEntry.NameNum1].Value;
							if (!currentEntry.Num2.HasValue && instructions[currentEntry.NameNum2].Value.HasValue)
								currentEntry.Num2 = instructions[currentEntry.NameNum2].Value;

							if (currentEntry.Num1.HasValue && currentEntry.Num2.HasValue)
								currentEntry.Value =
									currentEntry.Operation(currentEntry.Num1.Value, currentEntry.Num2.Value);
						}
					}
				}

				Console.WriteLine($"Num1: {instructions["root"].Num1}, Num2: {instructions["root"].Num2}");
				
				if(instructions["root"].Num1 > instructions["root"].Num2)
					Console.WriteLine("Num1 > Num2");
				if(instructions["root"].Num1 < instructions["root"].Num2)
					Console.WriteLine("Num1 < Num2");
				
				Console.WriteLine($"Num1: {instructions["root"].Num1}, Num2: {instructions["root"].Num2}");
				
				if (instructions["root"].Num1 == instructions["root"].Num2)
				{
					Console.WriteLine("Num1 == Num2!");
					Output = instructions["humn"].Value.ToString();
					answer = true;
					break;
				}
			}
		}

		protected override void ReadLine(string line)
		{
			var split = line.Split(" ");
			var name = split[0].Replace(":", "");
			
			if (split.Length == 2)
			{
				baseInstructions[name] = new Instructions
				{
					Name = name,
					Value = int.Parse(split[1])
				};
			}
			else
			{
				Func<long, long, long> func;
				
				switch (split[2])
				{
					case "*":
						func = (a, b) => a * b;
						break;
					case "/":
						func = (a, b) => a / b;
						break;
					case "-":
						func = (a, b) => a - b;
						break;
					default:
						func = (a, b) => a + b;
						break;
				}
				
				baseInstructions[name] = new Instructions
				{
					Name = name,
					Operation = func,
					NameNum1 = split[1],
					NameNum2 = split[3]
				};
			}
			//Console.WriteLine($"Current line in file: {CurrentLine}");
		}
		
		public class Instructions : ICloneable
		{
			public string Name { get; set; }
			public long? Value { get; set; }
			public Func<long, long, long> Operation { get; set; }
			
			public string NameNum1 { get; set; }
			public long? Num1 { get; set; }
			
			public string NameNum2 { get; set; }
			public long? Num2 { get; set; }
			public object Clone()
			{
				return new Instructions
				{
					Name = Name,
					Value = Value,
					Operation = Operation,
					NameNum1 = NameNum1,
					NameNum2 = NameNum2
				};
			}
		}
	}
}
