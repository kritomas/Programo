using System.Configuration;

namespace Programo
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Dictionary<string, DAOAction> actions = new Dictionary<string, DAOAction>();
			actions.Add("language", new DAOActionLanguage());
			actions.Add("programmer", new DAOActionProgrammer());
			actions.Add("project", new DAOActionProject());
			actions.Add("certify", new DAOActionCertification());
			actions.Add("assign", new DAOActionWork());
			actions.Add("feature", new DAOActionFeature());

			//try
			//{
				if (args.Length < 2)
				{
					// TODO
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
					Console.WriteLine("WRONG COMMAND KRONK");
				}
			/*}
			catch (Exception ex)
			{
				Console.WriteLine("Error:");
				Console.WriteLine(ex.Message);
			}*/
		}
	}
}
