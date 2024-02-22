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
		public override void perform(string[] args)
		{
			if (args.Length < 2)
			{
				Console.WriteLine("create language");
				return;
			}
			switch (args[0])
			{
				case "create":
					Lang.create(args);
					break;
				case "delete":
					Lang.delete(args);
					break;
				case "rename":
					Lang.rename(args);
					break;
				case "list":
					Lang.list(args);
					break;
				default:
					throw new Exception("Unknown action");
			}
		}
	}

	class DAOActionProgrammer : DAOAction
	{
		public override void perform(string[] args)
		{
			if (args.Length < 2)
			{
				Console.WriteLine("create programmer");
				return;
			}
			switch (args[0])
			{
				case "create":
					Programmer.create(args);
					break;
				case "delete":
					Programmer.delete(args);
					break;
				case "rename":
					Programmer.rename(args);
					break;
				case "list":
					Programmer.list(args);
					break;
				default:
					throw new Exception("Unknown action");
			}
		}
	}

	class DAOActionProject : DAOAction
	{
		public override void perform(string[] args)
		{
			if (args.Length < 2)
			{
				Console.WriteLine("create project");
				return;
			}
			switch (args[0])
			{
				case "create":
					Project.create(args);
					break;
				case "delete":
					Project.delete(args);
					break;
				case "rename":
					Project.rename(args);
					break;
				case "list":
					Project.list(args);
					break;
				case "abandon":
					Project.abandon(args);
					break;
				case "unabandon":
					Project.unabandon(args);
					break;
				default:
					throw new Exception("Unknown action");
			}
		}
	}

	class DAOActionCertification : DAOAction
	{
		public override void perform(string[] args)
		{
			switch (args[0])
			{
				case "certify":
					Certification.certify(args);
					break;
				default:
					throw new Exception("Unknown action");
			}
		}
	}
	class DAOActionWork : DAOAction
	{
		public override void perform(string[] args)
		{
			switch (args[0])
			{
				case "assign":
					Work.assign(args);
					break;
				default:
					throw new Exception("Unknown action");
			}
		}
	}
}
