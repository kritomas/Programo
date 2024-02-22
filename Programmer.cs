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
			LangDAO langDao = new LangDAO();
			foreach (Programmer p in dao.GetAll())
			{
				Console.WriteLine(p);
				foreach (Certification c in certificationDao.GetAllByProgrammer(p))
				{
					Lang l = langDao.GetByID(c.lang_id);
					Console.WriteLine(l + " " + c.date_start + " - " + c.date_end);
				}
				Console.WriteLine();
			}
		}
	}
}
