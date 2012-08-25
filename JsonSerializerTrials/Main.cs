using System;
using System.Diagnostics;
using JsonSerializer;

namespace JsonSerializerTrials
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Foo f = new Foo(5, 2.54E+10f);
			
			var sw = Stopwatch.StartNew();
			string jsonString = JSON.Serialize(f);
			TimeSpan elapsed = sw.Elapsed;
			
			if (jsonString == null)
			{
				Console.WriteLine("NULL JSON");
			}
			else
			{
				Console.WriteLine(string.Format("JSON = '{0}'\n{1}", jsonString, elapsed));
			}
					
		}
	}
}
