using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programo
{
	public class Feature
	{
		public int ID = 0;
		public int project_id = 0;
		public string name = "";
		public bool is_complete = false;

		public override string ToString()
		{
			return name + (is_complete ? " (complete)" : "");
		}

		public static void create(string[] args)
		{
			if (args.Length < 5)
			{
				Console.WriteLine("create feature project name is_complete");
				return;
			}
			ProjectDAO projectDao = new ProjectDAO();
			Project project = projectDao.GetByName(args[2]);
			if (project == null) throw new Exception("Project doesn't exist.");
			FeatureDAO dao = new FeatureDAO();
			Feature parsed = new Feature
			{
				project_id = project.ID,
				name = args[3],
				is_complete = Convert.ToBoolean(args[4])
			};
			if (dao.GetByIDName(parsed) != null) throw new Exception("Feature already exists.");
			dao.Save(parsed);
		}
		public static void complete(string[] args)
		{
			if (args.Length < 4)
			{
				Console.WriteLine("complete feature project name");
				return;
			}
			ProjectDAO projectDao = new ProjectDAO();
			Project project = projectDao.GetByName(args[2]);
			if (project == null) throw new Exception("Project doesn't exist.");
			FeatureDAO dao = new FeatureDAO();
			Feature parsed = new Feature
			{
				project_id = project.ID,
				name = args[3]
			};
			Feature feature = dao.GetByIDName(parsed);
			if (feature == null) throw new Exception("Feature doesn't exist.");
			feature.is_complete = true;
			dao.Save(feature);
		}
		public static void uncomplete(string[] args)
		{
			if (args.Length < 4)
			{
				Console.WriteLine("complete feature project name");
				return;
			}
			ProjectDAO projectDao = new ProjectDAO();
			Project project = projectDao.GetByName(args[2]);
			if (project == null) throw new Exception("Project doesn't exist.");
			FeatureDAO dao = new FeatureDAO();
			Feature parsed = new Feature
			{
				project_id = project.ID,
				name = args[3]
			};
			Feature feature = dao.GetByIDName(parsed);
			if (feature == null) throw new Exception("Feature doesn't exist.");
			feature.is_complete = false;
			dao.Save(feature);
		}
	}
}
