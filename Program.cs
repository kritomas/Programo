using System.Configuration;

namespace Programo
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Dictionary<string, DAOAction> actions = new Dictionary<string, DAOAction>();
			actions.Add("language", new DAOActionLanguage());

			if (args.Length < 2 || !actions.ContainsKey(args[1]))
			{
				Console.WriteLine("create language");
				return;
			}

			try
			{
				actions[args[1]].perform(args);
			} catch (Exception ex)
			{
				Console.WriteLine("Error:");
				Console.WriteLine(ex.Message);
			}
		}
	}
}
