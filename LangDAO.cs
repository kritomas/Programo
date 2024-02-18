using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programo
{
    public class LangDAO
    {
        public void Delete(Lang element)
        {
			SqlConnection conn = DatabaseSingleton.GetInstance();

			using (SqlCommand command = new SqlCommand("Delete FROM Lang where id = @id", conn))
			{
				command.Parameters.Add("@id", (System.Data.SqlDbType)element.ID);
				command.ExecuteNonQuery();
				element.ID = 0;
			}
		}

        public IEnumerable<Lang> GetAll()
        {
			SqlConnection conn = DatabaseSingleton.GetInstance();

			using (SqlCommand command = new SqlCommand("SELECT * FROM Lang", conn))
			{
				SqlDataReader reader = command.ExecuteReader();
				while (reader.Read())
				{
					Lang lang = new Lang
					{
						ID = Convert.ToInt32(reader[0].ToString()),
						name = reader[1].ToString()
					};
					yield return lang;
				}
				reader.Close();
			}

		}

		public Lang GetByID(int id)
        {
			Lang lang = null;
			SqlConnection conn = DatabaseSingleton.GetInstance();

			
			using (SqlCommand command = new SqlCommand("SELECT * FROM Lang WHERE id = @Id", conn))
			{
				command.Parameters.Add(new SqlParameter("@id", id));
				SqlDataReader reader = command.ExecuteReader();

				while (reader.Read())
				{
					lang = new Lang
					{
						ID = Convert.ToInt32(reader[0].ToString()),
						name = reader[1].ToString()
					};
				}
				reader.Close();
				return lang;
			}
		}

		public void Save(Lang element)
        {
			SqlConnection conn = DatabaseSingleton.GetInstance();

			SqlCommand command = null;

			if (element.ID < 1)
			{
				using (command = new SqlCommand("INSERT INTO Lang VALUES (@name)", conn))
				{
					command.Parameters.Add(new SqlParameter("@name", element.name));
					command.ExecuteNonQuery();
					//zjistim id posledniho vlozeneho zaznamu
					command.CommandText = "Select @@Identity";
					element.ID = Convert.ToInt32(command.ExecuteScalar());
				}
			}
			else
			{
				using (command = new SqlCommand("UPDATE Lang SET name = @name " +
					"WHERE id = @id", conn))
				{
					command.Parameters.Add(new SqlParameter("@id", element.ID));
					command.Parameters.Add(new SqlParameter("@name", element.name));
					command.ExecuteNonQuery();
				}
			}

		}
	}
}
