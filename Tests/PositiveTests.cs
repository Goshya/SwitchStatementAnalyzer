using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
	public class PositiveTests
	{
		public void TestMethod()
		{
			Cars myCar = Cars.Mercedes;
			switch (myCar)
			{
				case Cars.Mercedes:
					Console.WriteLine("Mercedes");
					break;
				case Cars.Lada:
					Console.WriteLine("Lada");
					break;
				case Cars.Volvo:
					Console.WriteLine("Volvo");
					break;
				case Cars.Lexus:
					Console.WriteLine("Lexus");
					break;
				case Cars.Toyota:
					Console.WriteLine("Toyota");
					break;
			}

			Cities myCity = new Cities();
			switch (myCity)
			{
				case Cities.Tula:
					Console.WriteLine("Tula");
					break;
				case Cities.Moscow:
					Console.WriteLine("Moscow");
					break;
				case Cities.London:
					Console.WriteLine("London");
					break;
				case Cities.Rome:
					Console.WriteLine("Rome");
					break;
				case Cities.Omsk:
					Console.WriteLine("Omsk");
					break;
			}

			Languages myLanguage = Languages.Csharp;
			switch (myLanguage)
			{
				case Languages.Csharp:
					Console.WriteLine("Csharp");
					break;
				case Languages.Cpp:
					Console.WriteLine("Cpp");
					break;
				case Languages.Assembler:
					Console.WriteLine("Assembler");
					break;
				case Languages.Fsharp:
					Console.WriteLine("Fsharp");
					break;
				case Languages.Java:
					Console.WriteLine("Java");
					break;
			}
		}
	}
}
