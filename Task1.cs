using System;

namespace MTSTest
{
	static class Program
	{
		static void Main(string[] args)
		{
			try
			{
				FailProcess();
			}
			catch
			{
				Console.WriteLine("In Catch");
			}

			Console.WriteLine("Failed to fail process!");
			Console.ReadKey();

		}

		private static void FailProcess()
		{
			//1
			//Environment.FailFast("sorry");
			
			//2
			//Environment.Exit(-1);
			
			//3
			FailProcess();
			
			//4
			//Process.GetCurrentProcess().Kill();
		}
	}
}