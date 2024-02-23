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
			FeatureDAO featureDAO = new FeatureDAO();
			foreach (Project p in dao.GetAll())
			{
				Console.WriteLine(p);
				Console.WriteLine("Features:");
				foreach (Feature f in featureDAO.GetAllByProject(p))
				{
					Console.WriteLine(f);
				}
				Console.WriteLine();
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

		public static Project parseCSV(string[] line)
		{
			return new Project { name = line[0], is_abandoned = Convert.ToBoolean(line[1]) };
		}

		public static void import(string[] args)
		{
			if (args.Length < 3)
			{
				Console.WriteLine("import project csv_file");
				return;
			}
			using (TextFieldParser parser = new TextFieldParser(args[2]))
			{
				parser.TextFieldType = FieldType.Delimited;
				parser.SetDelimiters(",");
				while (!parser.EndOfData)
				{
					//Process row
					string[] fields = parser.ReadFields();
					ProjectDAO dao = new ProjectDAO();
					dao.Save(parseCSV(fields));
				}
			}
		}
	}
}
