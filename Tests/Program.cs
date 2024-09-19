using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
	public class Program
	{
		enum Cars
		{
			Mercedes,
			Audi,
			Volvo,
			Lada,
			BMW,
			Kia,
			Lexus,
			Toyota,
			Ford
		}
		public static void Main(string[] args)
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
		}
	}
}
