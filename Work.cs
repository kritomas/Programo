using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programo
{
	public class Work
	{
		public int ID = 0;
		public int project_id = 0;
		public int programmer_id = 0;
		public DateTime date_start = DateTime.UnixEpoch;
		public DateTime date_end = DateTime.UnixEpoch;

		public static void assign(string[] args)
		{
			if (args.Length < 5)
			{
				Console.WriteLine("assign programmer project from to");
				return;
			}
			ProgrammerDAO programmerDao = new ProgrammerDAO();
			Programmer programmer = programmerDao.GetByUsername(args[1]);
			if (programmer == null) throw new Exception("Programmer doesn't exist.");
			ProjectDAO projectDao = new ProjectDAO();
			Project project = projectDao.GetByName(args[2]);
			if (project == null) throw new Exception("Project doesn't exist.");

			WorkDAO dao = new WorkDAO();
			dao.Save(new Work
			{
				programmer_id = programmer.ID,
				project_id = project.ID,
				date_start = DateTime.Parse(args[3]),
				date_end = DateTime.Parse(args[4])
			});
		}
	}
}
