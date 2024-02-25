using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programo
{
	class DAOAction
	{
		public virtual void perform(string[] args)
		{
			throw new InvalidOperationException("Undefined");
		}
	}

	class DAOActionLanguage : DAOAction
	{
		public static void create(string[] args)
		{
			if (args.Length < 3)
			{
				throw new Exception("Not enough arguments.");
			}
			LangDAO dao = new LangDAO();
			if (dao.GetByName(args[2]) != null) throw new Exception("Language already exists.");
			dao.Save(new Lang { name = args[2] });
		}
		public static void delete(string[] args)
		{
			if (args.Length < 3)
			{
				throw new Exception("Not enough arguments.");
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
				throw new Exception("Not enough arguments.");
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

		public override void perform(string[] args)
		{
			if (args.Length < 2)
			{
				throw new Exception("Not enough arguments.");
			}
			switch (args[0])
			{
				case "create":
					create(args);
					break;
				case "delete":
					delete(args);
					break;
				case "rename":
					rename(args);
					break;
				case "list":
					list(args);
					break;
				default:
					throw new Exception("Unknown action");
			}
		}
	}

	class DAOActionProgrammer : DAOAction
	{
		public static void create(string[] args)
		{
			if (args.Length < 3)
			{
				throw new Exception("Not enough arguments.");
			}
			ProgrammerDAO dao = new ProgrammerDAO();
			if (dao.GetByUsername(args[2]) != null) throw new Exception("Programmer already exists.");
			dao.Save(new Programmer { username = args[2] });
		}
		public static void delete(string[] args)
		{
			if (args.Length < 3)
			{
				throw new Exception("Not enough arguments.");
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
				throw new Exception("Not enough arguments.");
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

		public static void import(string[] args)
		{
			if (args.Length < 3)
			{
				throw new Exception("Not enough arguments.");
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
					dao.Save(Programmer.parseCSV(fields));
				}
			}
		}

		public override void perform(string[] args)
		{
			if (args.Length < 2)
			{
				throw new Exception("Not enough arguments.");
			}
			switch (args[0])
			{
				case "create":
					create(args);
					break;
				case "delete":
					delete(args);
					break;
				case "rename":
					rename(args);
					break;
				case "list":
					list(args);
					break;
				case "import":
					import(args);
					break;
				default:
					throw new Exception("Unknown action");
			}
		}
	}

	class DAOActionProject : DAOAction
	{
		public static void create(string[] args)
		{
			if (args.Length < 3)
			{
				throw new Exception("Not enough arguments.");
			}
			ProjectDAO dao = new ProjectDAO();
			if (dao.GetByName(args[2]) != null) throw new Exception("Project already exists.");
			dao.Save(new Project { name = args[2] });
		}
		public static void delete(string[] args)
		{
			if (args.Length < 3)
			{
				throw new Exception("Not enough arguments.");
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
				throw new Exception("Not enough arguments.");
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
				throw new Exception("Not enough arguments.");
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
				throw new Exception("Not enough arguments.");
			}
			ProjectDAO dao = new ProjectDAO();
			Project p = dao.GetByName(args[2]);
			if (p == null) throw new Exception("Project doesn't exist.");
			p.is_abandoned = false;
			dao.Save(p);
		}

		public static void import(string[] args)
		{
			if (args.Length < 3)
			{
				throw new Exception("Not enough arguments.");
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
					dao.Save(Project.parseCSV(fields));
				}
			}
		}

		public override void perform(string[] args)
		{
			if (args.Length < 2)
			{
				throw new Exception("Not enough arguments.");
			}
			switch (args[0])
			{
				case "create":
					create(args);
					break;
				case "delete":
					delete(args);
					break;
				case "rename":
					rename(args);
					break;
				case "list":
					list(args);
					break;
				case "abandon":
					abandon(args);
					break;
				case "unabandon":
					unabandon(args);
					break;
				case "import":
					import(args);
					break;
				default:
					throw new Exception("Unknown action");
			}
		}
	}

	class DAOActionCertification : DAOAction
	{
		public static void certify(string[] args)
		{
			if (args.Length < 5)
			{
				throw new Exception("Not enough arguments.");
			}
			ProgrammerDAO programmerDao = new ProgrammerDAO();
			Programmer programmer = programmerDao.GetByUsername(args[1]);
			if (programmer == null) throw new Exception("Programmer doesn't exist.");
			LangDAO langDao = new LangDAO();
			Lang lang = langDao.GetByName(args[2]);
			if (lang == null) throw new Exception("Language doesn't exist.");

			CertificationDAO dao = new CertificationDAO();
			dao.Save(new Certification
			{
				programmer_id = programmer.ID,
				lang_id = lang.ID,
				date_start = DateTime.Parse(args[3]),
				date_end = DateTime.Parse(args[4])
			});
		}

		public override void perform(string[] args)
		{
			switch (args[0])
			{
				case "certify":
					certify(args);
					break;
				default:
					throw new Exception("Unknown action");
			}
		}
	}
	class DAOActionWork : DAOAction
	{
		public static void assign(string[] args)
		{
			if (args.Length < 5)
			{
				throw new Exception("Not enough arguments.");
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

		public override void perform(string[] args)
		{
			switch (args[0])
			{
				case "assign":
					assign(args);
					break;
				default:
					throw new Exception("Unknown action");
			}
		}
	}

	class DAOActionFeature : DAOAction
	{
		public static void create(string[] args)
		{
			if (args.Length < 5)
			{
				throw new Exception("Not enough arguments.");
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
				throw new Exception("Not enough arguments.");
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
				throw new Exception("Not enough arguments.");
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

		public override void perform(string[] args)
		{
			switch (args[0])
			{
				case "create":
					create(args);
					break;
				case "complete":
					complete(args);
					break;
				case "uncomplete":
					uncomplete(args);
					break;
				default:
					throw new Exception("Unknown action");
			}
		}
	}
}
