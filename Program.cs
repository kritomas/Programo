using System.Configuration;

namespace Programo
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Project project = new Project
			{
				name = "Glomeria",
				is_abandoned = true
			};
			ProjectDAO dao = new ProjectDAO();
			dao.Save(project);
		}
	}
}
