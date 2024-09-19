using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
	public class NegativeTests
	{
		public void NegativeTestsMethod()
		{
			// В switch есть default-ветвь
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
				default:
					Console.WriteLine("default");
					break;
			}

			// В switch используются все члены перечисления
			Cities myCity = Cities.Tula;
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
				case Cities.Murmansk:
					Console.WriteLine("Murmansk");
					break;
			}

			// В switch не используется больше 4 констант из перечисления
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
			}

			ObjectExistState myObject = ObjectExistState.NotExist;
			switch (myObject) 
			{
				case ObjectExistState.Exist:
					Console.WriteLine("Object exist");
					break;
			}
		}
	}
}
