using System.Configuration;

namespace Programo
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Work work = new Work
			{
				project_id = 1,
				programmer_id = 1,
				date_start = DateTime.Parse("2021-11-01"),
				date_end = DateTime.Parse("2023-10-25"),
			};
			WorkDAO dao = new WorkDAO();
			dao.Save(work);
		}
	}
}
