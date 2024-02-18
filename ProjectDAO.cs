using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programo
{
    public class ProjectDAO
    {
        public void Delete(Project element)
        {
			SqlConnection conn = DatabaseSingleton.GetInstance();

			using (SqlCommand command = new SqlCommand("Delete FROM Project where id = @id", conn))
			{
				command.Parameters.Add("@id", (System.Data.SqlDbType)element.ID);
				command.ExecuteNonQuery();
				element.ID = 0;
			}
		}

        public IEnumerable<Project> GetAll()
        {
			SqlConnection conn = DatabaseSingleton.GetInstance();

			using (SqlCommand command = new SqlCommand("SELECT * FROM Project", conn))
			{
				SqlDataReader reader = command.ExecuteReader();
				while (reader.Read())
				{
					Project project = new Project
					{
						ID = Convert.ToInt32(reader[0].ToString()),
						name = reader[1].ToString(),
						is_abandoned = Convert.ToBoolean(reader[2].ToString())
					};
					yield return project;
				}
				reader.Close();
			}

		}

		public Project GetByID(int id)
        {
			Project project = null;
			SqlConnection conn = DatabaseSingleton.GetInstance();

			
			using (SqlCommand command = new SqlCommand("SELECT * FROM Project WHERE id = @Id", conn))
			{
				command.Parameters.Add(new SqlParameter("@id", id));
				SqlDataReader reader = command.ExecuteReader();

				while (reader.Read())
				{
					project = new Project
					{
						ID = Convert.ToInt32(reader[0].ToString()),
						name = reader[1].ToString(),
						is_abandoned = Convert.ToBoolean(reader[2].ToString())
					};
				}
				reader.Close();
				return project;
			}
		}

		public void Save(Project element)
        {
			SqlConnection conn = DatabaseSingleton.GetInstance();

			SqlCommand command = null;

			if (element.ID < 1)
			{
				using (command = new SqlCommand("INSERT INTO Project VALUES (@name, @is_abandoned)", conn))
				{
					command.Parameters.Add(new SqlParameter("@name", element.name));
					command.Parameters.Add(new SqlParameter("@is_abandoned", element.is_abandoned));
					command.ExecuteNonQuery();
					//zjistim id posledniho vlozeneho zaznamu
					command.CommandText = "Select @@Identity";
					element.ID = Convert.ToInt32(command.ExecuteScalar());
				}
			}
			else
			{
				using (command = new SqlCommand("UPDATE Project SET name = @name, is_abandoned = @is_abandoned " +
					"WHERE id = @id", conn))
				{
					command.Parameters.Add(new SqlParameter("@id", element.ID));
					command.Parameters.Add(new SqlParameter("@name", element.name));
					command.Parameters.Add(new SqlParameter("@is_abandoned", element.is_abandoned));
					command.ExecuteNonQuery();
				}
			}

		}
	}
}
