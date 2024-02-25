using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programo
{
	public class Project
	{
		public int ID = 0;
		public string name = "";
		public bool is_abandoned = false;

		public override string ToString()
		{
			return name + (is_abandoned ? " (abandoned)" : "");
		}

		public static Project parseCSV(string[] line)
		{
			return new Project { name = line[0], is_abandoned = Convert.ToBoolean(line[1]) };
		}
	}
}
