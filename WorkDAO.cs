using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programo
{
    public class WorkDAO
    {
        public void Delete(Work element)
        {
			SqlConnection conn = DatabaseSingleton.GetInstance();

			using (SqlCommand command = new SqlCommand("Delete FROM Work where id = @id", conn))
			{
				command.Parameters.Add("@id", (System.Data.SqlDbType)element.ID);
				command.ExecuteNonQuery();
				element.ID = 0;
			}
		}

        public IEnumerable<Work> GetAll()
        {
			SqlConnection conn = DatabaseSingleton.GetInstance();

			using (SqlCommand command = new SqlCommand("SELECT * FROM Work", conn))
			{
				SqlDataReader reader = command.ExecuteReader();
				while (reader.Read())
				{
					Work work = new Work
					{
						ID = Convert.ToInt32(reader[0].ToString()),
						project_id = Convert.ToInt32(reader[1].ToString()),
						programmer_id = Convert.ToInt32(reader[2].ToString()),
						date_start = DateTime.Parse(reader[3].ToString()),
						date_end = DateTime.Parse(reader[4].ToString())
					};
					yield return work;
				}
				reader.Close();
			}

		}

		public Work GetByID(int id)
        {
			Work work = null;
			SqlConnection conn = DatabaseSingleton.GetInstance();

			
			using (SqlCommand command = new SqlCommand("SELECT * FROM Work WHERE id = @Id", conn))
			{
				command.Parameters.Add(new SqlParameter("@id", id));
				SqlDataReader reader = command.ExecuteReader();

				while (reader.Read())
				{
					work = new Work
					{
						ID = Convert.ToInt32(reader[0].ToString()),
						project_id = Convert.ToInt32(reader[1].ToString()),
						programmer_id = Convert.ToInt32(reader[2].ToString()),
						date_start = DateTime.Parse(reader[3].ToString()),
						date_end = DateTime.Parse(reader[4].ToString())
					};
				}
				reader.Close();
				return work;
			}
		}

		public void Save(Work element)
        {
			SqlConnection conn = DatabaseSingleton.GetInstance();

			SqlCommand command = null;

			if (element.ID < 1)
			{
				using (command = new SqlCommand("INSERT INTO Work VALUES (@project_id, @programmer_id, @date_start, @date_end)", conn))
				{
					command.Parameters.Add(new SqlParameter("@project_id", element.project_id));
					command.Parameters.Add(new SqlParameter("@programmer_id", element.programmer_id));
					command.Parameters.Add(new SqlParameter("@date_start", element.date_start));
					command.Parameters.Add(new SqlParameter("@date_end", element.date_end));
					command.ExecuteNonQuery();
					//zjistim id posledniho vlozeneho zaznamu
					command.CommandText = "Select @@Identity";
					element.ID = Convert.ToInt32(command.ExecuteScalar());
				}
			}
			else
			{
				using (command = new SqlCommand("UPDATE Work SET project_id = @project_id, programmer_id = @programmer_id, date_start = @date_start, date_end = @date_end " +
					"WHERE id = @id", conn))
				{
					command.Parameters.Add(new SqlParameter("@id", element.ID));
					command.Parameters.Add(new SqlParameter("@project_id", element.project_id));
					command.Parameters.Add(new SqlParameter("@programmer_id", element.programmer_id));
					command.Parameters.Add(new SqlParameter("@date_start", element.date_start));
					command.Parameters.Add(new SqlParameter("@date_end", element.date_end));
					command.ExecuteNonQuery();
				}
			}

		}
	}
}
