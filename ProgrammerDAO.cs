using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programo
{
	/// <summary>
	/// DAO for Programmers.
	/// </summary>
    public class ProgrammerDAO
    {
        public void Delete(Programmer element)
        {
			SqlConnection conn = DatabaseSingleton.GetInstance();

			using (SqlCommand command = new SqlCommand("Delete FROM Programmer where id = @id", conn))
			{
				command.Parameters.Add(new SqlParameter("@id", element.ID));
				command.ExecuteNonQuery();
				element.ID = 0;
			}
		}

        public IEnumerable<Programmer> GetAll()
        {
			SqlConnection conn = DatabaseSingleton.GetInstance();

			using (SqlCommand command = new SqlCommand("SELECT * FROM Programmer", conn))
			{
				SqlDataReader reader = command.ExecuteReader();
				while (reader.Read())
				{
					Programmer programmer = new Programmer
					{
						ID = Convert.ToInt32(reader[0].ToString()),
						username = reader[1].ToString()
					};
					yield return programmer;
				}
				reader.Close();
			}

		}

		public Programmer GetByID(int id)
        {
			Programmer programmer = null;
			SqlConnection conn = DatabaseSingleton.GetInstance();

			
			using (SqlCommand command = new SqlCommand("SELECT * FROM Programmer WHERE id = @Id", conn))
			{
				command.Parameters.Add(new SqlParameter("@id", id));
				SqlDataReader reader = command.ExecuteReader();

				while (reader.Read())
				{
					programmer = new Programmer
					{
						ID = Convert.ToInt32(reader[0].ToString()),
						username = reader[1].ToString()
					};
				}
				reader.Close();
				return programmer;
			}
		}
		public Programmer GetByUsername(string username)
		{
			Programmer programmer = null;
			SqlConnection conn = DatabaseSingleton.GetInstance();


			using (SqlCommand command = new SqlCommand("SELECT * FROM Programmer WHERE username = @username", conn))
			{
				command.Parameters.Add(new SqlParameter("@username", username));
				SqlDataReader reader = command.ExecuteReader();

				while (reader.Read())
				{
					programmer = new Programmer
					{
						ID = Convert.ToInt32(reader[0].ToString()),
						username = reader[1].ToString()
					};
				}
				reader.Close();
				return programmer;
			}
		}

		public void Save(Programmer element)
        {
			SqlConnection conn = DatabaseSingleton.GetInstance();

			SqlCommand command = null;

			if (element.ID < 1)
			{
				using (command = new SqlCommand("INSERT INTO Programmer VALUES (@username)", conn))
				{
					command.Parameters.Add(new SqlParameter("@username", element.username));
					command.ExecuteNonQuery();
					command.CommandText = "Select @@Identity";
					element.ID = Convert.ToInt32(command.ExecuteScalar());
				}
			}
			else
			{
				using (command = new SqlCommand("UPDATE Programmer SET username = @username " +
					"WHERE id = @id", conn))
				{
					command.Parameters.Add(new SqlParameter("@id", element.ID));
					command.Parameters.Add(new SqlParameter("@username", element.username));
					command.ExecuteNonQuery();
				}
			}

		}
	}
}
