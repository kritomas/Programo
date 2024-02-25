using System.Configuration;

namespace Programo
{
	internal class Program
	{
		/// <summary>
		/// Prints usage information.
		/// </summary>
		static void usage()
		{
			Console.WriteLine("Usage:");
			Console.WriteLine("create language [name]");
			Console.WriteLine("delete language [name]");
			Console.WriteLine("rename language [old_name] [new_name]");
			Console.WriteLine("create programmer [name]");
			Console.WriteLine("delete programmer [name]");
			Console.WriteLine("rename programmer [old_name] [new_name]");
			Console.WriteLine("import programmer [csv_file]");
			Console.WriteLine("create project [name]");
			Console.WriteLine("delete project [name]");
			Console.WriteLine("rename project [old_name] [new_name]");
			Console.WriteLine("abandon project [name]");
			Console.WriteLine("unabandon project [name]");
			Console.WriteLine("import project [csv_file]");
			Console.WriteLine("certify [programmer] [language] [from] [to]");
			Console.WriteLine("assign [programmer] [project] [from] [to]");
			Console.WriteLine("create feature [project] [name] [is_complete]");
			Console.WriteLine("complete feature [project] [name]");
			Console.WriteLine("uncomplete feature [project] [name]");
		}

		static void Main(string[] args)
		{
			Dictionary<string, DAOAction> actions = new Dictionary<string, DAOAction>();
			actions.Add("language", new DAOActionLanguage());
			actions.Add("programmer", new DAOActionProgrammer());
			actions.Add("project", new DAOActionProject());
			actions.Add("certify", new DAOActionCertification());
			actions.Add("assign", new DAOActionWork());
			actions.Add("feature", new DAOActionFeature());

			try
			{
				if (args.Length < 2)
				{
					throw new Exception("Not enough arguments.");
				}
				else if (actions.ContainsKey(args[1]))
				{
					actions[args[1]].perform(args);
				}
				else if (actions.ContainsKey(args[0]))
				{
					actions[args[0]].perform(args);
				}
				else
				{
					throw new Exception("WRONG COMMAND KRONK");
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error:");
				Console.WriteLine(ex.Message);
				usage();
			}
		}
	}
}
