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
}
