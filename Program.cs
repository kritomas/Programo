using System.Configuration;

namespace Programo
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Certification cert = new Certification
			{
				lang_id = 1,
				programmer_id = 1,
				date_start = DateTime.Parse("2013-09-01"),
				date_end = DateTime.Parse("2020-05-01"),
			};
			CertificationDAO dao = new CertificationDAO();
			dao.Save(cert);
		}
	}
}
