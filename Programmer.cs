﻿using Microsoft.VisualBasic.FileIO;
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

		public static void create(string[] args)
		{
			if (args.Length < 3)
			{
				Console.WriteLine("create programmer name");
				return;
			}
			ProgrammerDAO dao = new ProgrammerDAO();
			if (dao.GetByUsername(args[2]) != null) throw new Exception("Programmer already exists.");
			dao.Save(new Programmer { username = args[2] });
		}
		public static void delete(string[] args)
		{
			if (args.Length < 3)
			{
				Console.WriteLine("delete programmer name");
				return;
			}
			ProgrammerDAO dao = new ProgrammerDAO();
			Programmer p = dao.GetByUsername(args[2]);
			if (p == null) throw new Exception("Programmer doesn't exist.");
			dao.Delete(p);
		}
		public static void rename(string[] args)
		{
			if (args.Length < 4)
			{
				Console.WriteLine("rename programmer old_name new_name");
				return;
			}
			ProgrammerDAO dao = new ProgrammerDAO();
			Programmer p = dao.GetByUsername(args[2]);
			if (p == null) throw new Exception("Programmer doesn't exist.");
			p.username = args[3];
			dao.Save(p);
		}
		public static void list(string[] args)
		{
			ProgrammerDAO dao = new ProgrammerDAO();
			CertificationDAO certificationDao = new CertificationDAO();
			WorkDAO workDao = new WorkDAO();
			LangDAO langDao = new LangDAO();
			ProjectDAO projectDao = new ProjectDAO();
			foreach (Programmer p in dao.GetAll())
			{
				Console.WriteLine(p);
				Console.WriteLine("Certificates:");
				foreach (Certification c in certificationDao.GetAllByProgrammer(p))
				{
					Lang l = langDao.GetByID(c.lang_id);
					Console.WriteLine(l + " " + c.date_start + " - " + c.date_end);
				}
				Console.WriteLine("Projects:");
				foreach (Work w in workDao.GetAllByProgrammer(p))
				{
					Project proj = projectDao.GetByID(w.project_id);
					Console.WriteLine(proj + " " + w.date_start + " - " + w.date_end);
				}
				Console.WriteLine();
			}
		}

		public static Programmer parseCSV(string[] line)
		{
			return new Programmer { username = line[0] };
		}

		public static void import(string[] args)
		{
			if (args.Length < 3)
			{
				Console.WriteLine("import programmer csv_file");
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
					ProgrammerDAO dao = new ProgrammerDAO();
					dao.Save(parseCSV(fields));
				}
			}
		}
	}
}
