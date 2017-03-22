using Modulewijzer.Interfaces;
using Modulewijzer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace Modulewijzer.DataAccess
{
    /// <summary>
    /// Provides methods for working with the `Docent` table within the database.
    /// </summary>
    public sealed class TabelDocent : IDBTable<Docent>
    {
        /// <summary>
        /// Inserts a new docent into the database.
        /// </summary>
        /// <param name="row">The docent to insert.</param>
        public void Create(Docent row)
        {
            using (var connection = new SqlConnection(DbConnection.ConnectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO [Docenten] ([Voorletters], [Achternaam], " +
                        "[Tussenvoegel]) VALUES(@Voorletters, @Achternaam, " +
                        "@Tussenvoegsel)";

                    command.Parameters.Add(new SqlParameter[]
                    {
                        new SqlParameter("@Voorletters", SqlDbType.NVarChar) { Value = row.Voorletters },
                        new SqlParameter("@Achternaam", SqlDbType.NVarChar) { Value = row.Achternaam },
                        new SqlParameter("@Tussenvoegsel", SqlDbType.NVarChar) { Value = row.Tussenvoegsel }
                    });

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        /// <summary>
        /// Updates the specified docent in the database.
        /// </summary>
        /// <param name="row">The docent to update.</param>
        public void Update(Docent row)
        {
            using (var connection = new SqlConnection(DbConnection.ConnectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "UPDATE [Docenten] SET [Voorletters]=@Voorletters, " +
                        "[Achternaam]=@Achternaam, [Tussenvoegsel]=@Tussenvoegsel";

                    command.Parameters.Add(new SqlParameter[]
                    {
                        new SqlParameter("@Voorletters", SqlDbType.NVarChar) { Value = row.Voorletters },
                        new SqlParameter("@Achternaam", SqlDbType.NVarChar) { Value = row.Achternaam },
                        new SqlParameter("@Tussenvoegsel", SqlDbType.NVarChar) { Value = row.Tussenvoegsel }
                    });

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        /// <summary>
        /// Deletes the docent with the given ID from the database.
        /// </summary>
        /// <param name="row">The ID of the docent to delete.</param>
        public void Delete(int rowId)
        {
            using (var connection = new SqlConnection(DbConnection.ConnectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM [Docenten] WHERE [DocentId]=@Id";
                    command.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = rowId });
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        /// <summary>
        /// Gets a docent with the given ID from the database.
        /// </summary>
        /// <param name="id">The docent's ID.</param>
        /// <returns></returns>
        public Docent GetById(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets all docents in the database.
        /// </summary>
        /// <returns>A collection of all docents in the database.</returns>
        public Docent[] GetAll()
        {
            var result = new List<Docent>();

            return result.ToArray();
        }
    }
}