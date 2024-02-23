using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programo
{
    public class FeatureDAO
    {
        public void Delete(Feature element)
        {
			SqlConnection conn = DatabaseSingleton.GetInstance();

			using (SqlCommand command = new SqlCommand("Delete FROM Feature where id = @id", conn))
			{
				command.Parameters.Add("@id", (System.Data.SqlDbType)element.ID);
				command.ExecuteNonQuery();
				element.ID = 0;
			}
		}

        public IEnumerable<Feature> GetAll()
        {
			SqlConnection conn = DatabaseSingleton.GetInstance();

			using (SqlCommand command = new SqlCommand("SELECT * FROM Feature", conn))
			{
				SqlDataReader reader = command.ExecuteReader();
				while (reader.Read())
				{
					Feature feature = new Feature
					{
						ID = Convert.ToInt32(reader[0].ToString()),
						project_id = Convert.ToInt32(reader[1].ToString()),
						name = reader[2].ToString(),
						is_complete = Convert.ToBoolean(reader[3].ToString())
					};
					yield return feature;
				}
				reader.Close();
			}

		}
		public IEnumerable<Feature> GetAllByProject(Project project)
		{
			SqlConnection conn = DatabaseSingleton.GetInstance();

			using (SqlCommand command = new SqlCommand("SELECT * FROM Feature WHERE project_id = @id", conn))
			{
				command.Parameters.Add(new SqlParameter("@id", project.ID));
				SqlDataReader reader = command.ExecuteReader();
				while (reader.Read())
				{
					Feature feature = new Feature
					{
						ID = Convert.ToInt32(reader[0].ToString()),
						project_id = Convert.ToInt32(reader[1].ToString()),
						name = reader[2].ToString(),
						is_complete = Convert.ToBoolean(reader[3].ToString())
					};
					yield return feature;
				}
				reader.Close();
			}
		}

		public Feature GetByID(int id)
        {
			Feature feature = null;
			SqlConnection conn = DatabaseSingleton.GetInstance();

			
			using (SqlCommand command = new SqlCommand("SELECT * FROM Feature WHERE id = @Id", conn))
			{
				command.Parameters.Add(new SqlParameter("@id", id));
				SqlDataReader reader = command.ExecuteReader();

				while (reader.Read())
				{
					feature = new Feature
					{
						ID = Convert.ToInt32(reader[0].ToString()),
						project_id = Convert.ToInt32(reader[1].ToString()),
						name = reader[2].ToString(),
						is_complete = Convert.ToBoolean(reader[3].ToString())
					};
				}
				reader.Close();
				return feature;
			}
		}

		public void Save(Feature element)
        {
			SqlConnection conn = DatabaseSingleton.GetInstance();

			SqlCommand command = null;

			if (element.ID < 1)
			{
				using (command = new SqlCommand("INSERT INTO Feature VALUES (@project_id, @name, @is_complete)", conn))
				{
					command.Parameters.Add(new SqlParameter("@project_id", element.project_id));
					command.Parameters.Add(new SqlParameter("@name", element.name));
					command.Parameters.Add(new SqlParameter("@is_complete", element.is_complete));
					command.ExecuteNonQuery();
					//zjistim id posledniho vlozeneho zaznamu
					command.CommandText = "Select @@Identity";
					element.ID = Convert.ToInt32(command.ExecuteScalar());
				}
			}
			else
			{
				using (command = new SqlCommand("UPDATE Feature SET project_id = @project_id, name = @name, is_complete = @is_complete " +
					"WHERE id = @id", conn))
				{
					command.Parameters.Add(new SqlParameter("@id", element.ID));
					command.Parameters.Add(new SqlParameter("@project_id", element.project_id));
					command.Parameters.Add(new SqlParameter("@name", element.name));
					command.Parameters.Add(new SqlParameter("@is_complete", element.is_complete));
					command.ExecuteNonQuery();
				}
			}

		}
	}
}
