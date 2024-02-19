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

		public static void create(string[] args)
		{
			if (args.Length < 3)
			{
				Console.WriteLine("create project name");
				return;
			}
			ProjectDAO dao = new ProjectDAO();
			if (dao.GetByName(args[2]) != null) throw new Exception("Project already exists.");
			dao.Save(new Project { name = args[2] });
		}
		public static void delete(string[] args)
		{
			if (args.Length < 3)
			{
				Console.WriteLine("delete project name");
				return;
			}
			ProjectDAO dao = new ProjectDAO();
			Project p = dao.GetByName(args[2]);
			if (p == null) throw new Exception("Project doesn't exist.");
			dao.Delete(p);
		}
		public static void rename(string[] args)
		{
			if (args.Length < 4)
			{
				Console.WriteLine("rename project old_name new_name");
				return;
			}
			ProjectDAO dao = new ProjectDAO();
			Project p = dao.GetByName(args[2]);
			if (p == null) throw new Exception("Project doesn't exist.");
			p.name = args[3];
			dao.Save(p);
		}
		public static void list(string[] args)
		{
			ProjectDAO dao = new ProjectDAO();
			foreach (Project p in dao.GetAll())
			{
				Console.WriteLine(p);
			}
		}
		public static void abandon(string[] args)
		{
			if (args.Length < 3)
			{
				Console.WriteLine("abandon project name");
				return;
			}
			ProjectDAO dao = new ProjectDAO();
			Project p = dao.GetByName(args[2]);
			if (p == null) throw new Exception("Project doesn't exist.");
			p.is_abandoned = true;
			dao.Save(p);
		}
		public static void unabandon(string[] args)
		{
			if (args.Length < 3)
			{
				Console.WriteLine("unabandon project name");
				return;
			}
			ProjectDAO dao = new ProjectDAO();
			Project p = dao.GetByName(args[2]);
			if (p == null) throw new Exception("Project doesn't exist.");
			p.is_abandoned = false;
			dao.Save(p);
		}
	}
}
