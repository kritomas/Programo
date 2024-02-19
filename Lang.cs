using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programo
{
	public class Lang
	{
		public int ID = 0;
		public string name = "";

		public override string ToString()
		{
			return name;
		}

		public static void create(string[] args)
		{
			if (args.Length < 3)
			{
				Console.WriteLine("create language name");
				return;
			}
			LangDAO dao = new LangDAO();
			if (dao.GetByName(args[2]) != null) throw new Exception("Language already exists.");
			dao.Save(new Lang { name = args[2] });
		}
		public static void delete(string[] args)
		{
			if (args.Length < 3)
			{
				Console.WriteLine("delete language name");
				return;
			}
			LangDAO dao = new LangDAO();
			Lang l = dao.GetByName(args[2]);
			if (l == null) throw new Exception("Language doesn't exist.");
			dao.Delete(l);
		}
		public static void rename(string[] args)
		{
			if (args.Length < 4)
			{
				Console.WriteLine("rename language old_name new_name");
				return;
			}
			LangDAO dao = new LangDAO();
			Lang l = dao.GetByName(args[2]);
			if (l == null) throw new Exception("Language doesn't exist.");
			l.name = args[3];
			dao.Save(l);
		}
		public static void list(string[] args)
		{
			LangDAO dao = new LangDAO();
			foreach (Lang l in dao.GetAll())
			{
				Console.WriteLine(l);
			}
		}
	}
}
