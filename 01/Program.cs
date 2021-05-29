using System;
using System.IO;

namespace _01
{
	class Program
	{
		static void Main(string[] args)
		{
			const int MAX_DISTANCE = 2;
			Console.Write("Enter name:\t");
			var name = Console.ReadLine();
			var isError = string.IsNullOrEmpty(name);
			if (isError)
			{
				Console.WriteLine("Your name was not found.");
				return;
			}
			foreach (var c in name)
				if (!(char.IsLetter(c) || c == ' ' || c == '-'))
					isError = true;
			if (isError)
			{
				Console.WriteLine("Something went wrong. Check your input and retry.");
				return;
			}
			var allLines = (string[])null;
			if (File.Exists("us_names.txt"))
				allLines = System.IO.File.ReadAllLines("us_names.txt");
			else
				allLines = System.IO.File.ReadAllLines(
					$"{System.Reflection.Assembly.GetExecutingAssembly().Location}/us_names.txt");

			bool ContainsLower(string [] arr, string str)
			{
				foreach (var s in arr)
					if (s.ToLower() == str)
						return true;
				return false;
			}
			if (ContainsLower(allLines, name.ToLower()))
			{
				Console.WriteLine($"Hello, {name}!");
				return;
			}

			int LevenshteinDistance(string string1, string string2)
			{
				string1 = string1.ToLower();
				string2 = string2.ToLower();
				int diff;
				int[,] m = new int[string1.Length + 1, string2.Length + 1];

				for (int i = 0; i <= string1.Length; i++) 
					m[i, 0] = i;
				for (int j = 0; j <= string2.Length; j++)
					m[0, j] = j;

				for (int i = 1; i <= string1.Length; i++)
				{
					for (int j = 1; j <= string2.Length; j++)
					{
						diff = (string1[i - 1] == string2[j - 1]) ? 0 : 1;
						m[i, j] = Math.Min
							(Math.Min(m[i - 1, j] + 1, m[i, j - 1] + 1),
								m[i - 1, j - 1] + diff);
					}
				}
				return m[string1.Length, string2.Length];
			}
			var allDistances = new int[allLines.Length];
			var j = 0;
			for (var i = 0; i < allLines.Length; i++)
			{
				var distance = LevenshteinDistance(allLines[i], name);
				if (distance > MAX_DISTANCE)
					continue;
				allDistances[j] = distance;
				allLines[j++] = allLines[i];
			}

			if (j == 0)
			{
				Console.WriteLine($"Your name was not found.");
				return;
			}
			var distances = new int[j];
			Array.Copy(allDistances, distances, j);
			allDistances = null;

			var lines = new string[j];
			Array.Copy(allLines, lines, j);
			allLines = null;
			for (int dist = 0; dist <= MAX_DISTANCE; dist++)
				for (var k = 0; k < lines.Length; k++)
					if (dist == distances[k])
					{
						var needAnswer = true;
						while (needAnswer)
						{
							Console.WriteLine($"Did you mean “{lines[k]}”? Y/N");
							var ans = Console.ReadKey();
							if (ans.Key == ConsoleKey.Y)
							{
								Console.WriteLine($"\nHello, {lines[k]}!");
								return;
							}
							else
								needAnswer = false;
						}
					}
			Console.WriteLine($"\nYour name was not found.");
		}
	}
}
