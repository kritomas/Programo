using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Programo
{
	public class Programmer
	{
		public int ID = 0;
		public string username = "";

		public override string ToString()
		{
			return username;
		}

		/// <summary>
		/// Parses a single CSV line.
		/// </summary>
		/// <param name="line">The line fields to parse.</param>
		/// <returns>The parsed Programmer.</returns>
		public static Programmer parseCSV(string[] line)
		{
			return new Programmer { username = line[0] };
		}
	}
}
