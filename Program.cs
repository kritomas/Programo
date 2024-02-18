using System.Configuration;

namespace Programo
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Lang lang = new Lang
			{
				ID = 1,
				name = "Scratch"
			};
			LangDAO dao = new LangDAO();
			dao.Save(lang);
		}
	}
}
