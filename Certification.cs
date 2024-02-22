using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programo
{
	public class Certification
	{
		public int ID = 0;
		public int lang_id = 0;
		public int programmer_id = 0;
		public DateTime date_start = DateTime.UnixEpoch;
		public DateTime date_end = DateTime.UnixEpoch;

		public static void certify(string[] args)
		{
			if (args.Length < 5)
			{
				Console.WriteLine("certify programmer language from to");
				return;
			}
			ProgrammerDAO programmerDao = new ProgrammerDAO();
			Programmer programmer = programmerDao.GetByUsername(args[1]);
			if (programmer == null) throw new Exception("Programmer doesn't exist.");
			LangDAO langDao = new LangDAO();
			Lang lang = langDao.GetByName(args[2]);
			if (lang == null) throw new Exception("Language doesn't exist.");

			CertificationDAO dao = new CertificationDAO();
			dao.Save(new Certification { programmer_id = programmer.ID,
				lang_id = lang.ID,
				date_start = DateTime.Parse(args[3]),
				date_end = DateTime.Parse(args[4])});
		}
	}
}
