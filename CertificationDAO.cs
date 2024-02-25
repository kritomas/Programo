using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programo
{
	/// <summary>
	/// DAO for Certificaions.
	/// </summary>
    public class CertificationDAO
    {
        public void Delete(Certification element)
        {
			SqlConnection conn = DatabaseSingleton.GetInstance();

			using (SqlCommand command = new SqlCommand("Delete FROM Certification where id = @id", conn))
			{
				command.Parameters.Add("@id", (System.Data.SqlDbType)element.ID);
				command.ExecuteNonQuery();
				element.ID = 0;
			}
		}

        public IEnumerable<Certification> GetAll()
        {
			SqlConnection conn = DatabaseSingleton.GetInstance();

			using (SqlCommand command = new SqlCommand("SELECT * FROM Certification", conn))
			{
				SqlDataReader reader = command.ExecuteReader();
				while (reader.Read())
				{
					Certification certification = new Certification
					{
						ID = Convert.ToInt32(reader[0].ToString()),
						lang_id = Convert.ToInt32(reader[1].ToString()),
						programmer_id = Convert.ToInt32(reader[2].ToString()),
						date_start = DateTime.Parse(reader[3].ToString()),
						date_end = DateTime.Parse(reader[4].ToString())
					};
					yield return certification;
				}
				reader.Close();
			}
		}
		/// <summary>
		/// Gets all Certifications of the specified Programmer.
		/// </summary>
		/// <param name="programmer"></param>
		/// <returns></returns>
		public IEnumerable<Certification> GetAllByProgrammer(Programmer programmer)
		{
			SqlConnection conn = DatabaseSingleton.GetInstance();

			using (SqlCommand command = new SqlCommand("SELECT * FROM Certification WHERE programmer_id = @id", conn))
			{
				command.Parameters.Add(new SqlParameter("@id", programmer.ID));
				SqlDataReader reader = command.ExecuteReader();
				while (reader.Read())
				{
					Certification certification = new Certification
					{
						ID = Convert.ToInt32(reader[0].ToString()),
						lang_id = Convert.ToInt32(reader[1].ToString()),
						programmer_id = Convert.ToInt32(reader[2].ToString()),
						date_start = DateTime.Parse(reader[3].ToString()),
						date_end = DateTime.Parse(reader[4].ToString())
					};
					yield return certification;
				}
				reader.Close();
			}
		}

		public Certification GetByID(int id)
        {
			Certification certification = null;
			SqlConnection conn = DatabaseSingleton.GetInstance();

			
			using (SqlCommand command = new SqlCommand("SELECT * FROM Certification WHERE id = @Id", conn))
			{
				command.Parameters.Add(new SqlParameter("@id", id));
				SqlDataReader reader = command.ExecuteReader();

				while (reader.Read())
				{
					certification = new Certification
					{
						ID = Convert.ToInt32(reader[0].ToString()),
						lang_id = Convert.ToInt32(reader[1].ToString()),
						programmer_id = Convert.ToInt32(reader[2].ToString()),
						date_start = DateTime.Parse(reader[3].ToString()),
						date_end = DateTime.Parse(reader[4].ToString())
					};
				}
				reader.Close();
				return certification;
			}
		}

		public void Save(Certification element)
        {
			SqlConnection conn = DatabaseSingleton.GetInstance();

			SqlCommand command = null;

			if (element.ID < 1)
			{
				using (command = new SqlCommand("INSERT INTO Certification VALUES (@lang_id, @programmer_id, @date_start, @date_end)", conn))
				{
					command.Parameters.Add(new SqlParameter("@lang_id", element.lang_id));
					command.Parameters.Add(new SqlParameter("@programmer_id", element.programmer_id));
					command.Parameters.Add(new SqlParameter("@date_start", element.date_start));
					command.Parameters.Add(new SqlParameter("@date_end", element.date_end));
					command.ExecuteNonQuery();
					command.CommandText = "Select @@Identity";
					element.ID = Convert.ToInt32(command.ExecuteScalar());
				}
			}
			else
			{
				using (command = new SqlCommand("UPDATE Certification SET lang_id = @lang_id, programmer_id = @programmer_id, date_start = @date_start, date_end = @date_end " +
					"WHERE id = @id", conn))
				{
					command.Parameters.Add(new SqlParameter("@id", element.ID));
					command.Parameters.Add(new SqlParameter("@lang_id", element.lang_id));
					command.Parameters.Add(new SqlParameter("@programmer_id", element.programmer_id));
					command.Parameters.Add(new SqlParameter("@date_start", element.date_start));
					command.Parameters.Add(new SqlParameter("@date_end", element.date_end));
					command.ExecuteNonQuery();
				}
			}

		}
	}
}
