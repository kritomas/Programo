using System.Configuration;

namespace Programo
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Programmer programmer = new Programmer
			{
				username = "kritomas"
			};
			ProgrammerDAO dao = new ProgrammerDAO();
			dao.Save(programmer);
		}
	}
}
