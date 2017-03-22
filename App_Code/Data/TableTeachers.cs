using Modulewijzer.Models;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;


namespace Modulewijzer.Data
{
    public sealed class TableTeachers
    {


        /// <summary>
        /// Inserts the given teacher into the database.
        /// </summary>
        /// <param name="teacher">The teacher to insert.</param>
        public void Create(Teacher teacher)
        {
            // TODO: Place connection string somewhere else.
            using (var connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|Modulewijzer.mdf;Integrated Security=True"))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO Docenten (Voorletters, Achternaam)" +
                        "Values(@Initials, @LastName)";

                    SqlParameter[] parameters =
                    {
                        new SqlParameter("@Initials", SqlDbType.NVarChar) { Value = teacher.Initials },
                        new SqlParameter("@LastName", SqlDbType.NVarChar) {Value = teacher.LastName },
                    };
                    command.Parameters.AddRange(parameters);

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }

        }

        /// <summary>
        /// Updates the given teacher within the database.
        /// </summary>
        /// <param name="teacher">The teacher to update.</param>
        public void Update(Teacher teacher)
        {
            using (var connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|Modulewijzer.mdf;Integrated Security=True"))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "UPDATE Docenten SET Voorletters=@Initials, Achternaam=@LastName";

                    SqlParameter[] parameters =
                    {
                        new SqlParameter("@Initials", SqlDbType.NVarChar) { Value = teacher.Initials },
                        new SqlParameter("@LastName", SqlDbType.NVarChar) {Value = teacher.LastName },
                    };
                    command.Parameters.AddRange(parameters);

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        /// <summary>
        /// Deletes the given teacher from the database.
        /// </summary>
        /// <param name="teacher">The teacher to delete.</param>
        public void Delete(Teacher teacher)
        {

        }
    }
}
