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
                    command.CommandText = "INSERT INTO [Docenten] ([Voorletters], [Achternaam], [Tussenvoegsel])" +
                        "VALUES(@Voorletters, @Achternaam, @Tussenvoegsel)";

                    command.Parameters.AddRange(new SqlParameter[]
                    {
                        new SqlParameter("@Voorletters", SqlDbType.NVarChar) { Value = row.Voorletters.ToUpper() },
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
                        "[Achternaam]=@Achternaam, [Tussenvoegsel]=@Tussenvoegsel WHERE [DocentId]=@Id";

                    command.Parameters.AddRange(new SqlParameter[]
                    {
                        new SqlParameter("@Id", SqlDbType.Int) {Value = row.Id },
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

        public int GetLink(int DocentId, int ModuleId)
        {
            int result = 0;
            using (var connection = new SqlConnection(DbConnection.ConnectionString))
            {
                connection.Open();

                using (var checkLink = connection.CreateCommand())
                {
                    checkLink.CommandText = "EXEC IsDocentLinked @ModulewijzerId, @DocentId";

                    checkLink.Parameters.AddRange(new SqlParameter[]
                    {
                                    new SqlParameter("@ModulewijzerId", SqlDbType.Int) { Value = ModuleId },
                                     new SqlParameter("@DocentId", SqlDbType.Int) { Value = DocentId },
                    });

                    using (var reader = checkLink.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = reader.GetInt32(0);
                        }
                    }
                    //result = (int)checkLink.ExecuteScalar();
                }
                connection.Close();
            }
            return result;
        }

        /// <summary>
        /// Assigns a docent to a modulewijzer
        /// </summary>
        public void Toevoegen(int ModulewijzerId, int DocentId)
        {
            using (var connection = new SqlConnection(DbConnection.ConnectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "EXEC LinkDocent @ModulewijzerId, @DocentId";

                    command.Parameters.AddRange(new SqlParameter[]
                    {
                        new SqlParameter("@ModulewijzerId", SqlDbType.Int) { Value = ModulewijzerId },
                        new SqlParameter("@DocentId", SqlDbType.Int) { Value =  DocentId},
                    });

                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        /// <summary>
        /// Unassigns a docent to a modulewijzer
        /// </summary>
        public void Verwijderen(int ModulewijzerId, int DocentId)
        {
            using (var connection = new SqlConnection(DbConnection.ConnectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "EXEC DropLinkDocenten @ModulewijzerId, @DocentId";

                    command.Parameters.AddRange(new SqlParameter[]
                    {
                        new SqlParameter("@ModulewijzerId", SqlDbType.Int) { Value = ModulewijzerId },
                        new SqlParameter("@DocentId", SqlDbType.Int) { Value = DocentId },
                    });

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
            string achternaam = "";
            string voorletters = "";
            string tussenvoegsel = "";

            using (var connection = new SqlConnection(DbConnection.ConnectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "EXEC GetDocent @docentId";
                    command.Parameters.AddWithValue("@docentId", id);
                    command.ExecuteNonQuery();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            voorletters = reader.GetString(0);
                            achternaam = reader.GetString(1);
                            tussenvoegsel = reader.GetString(2);
                        }
                    }

                }
                connection.Close();
            }

            var docent = new Docent()
            {
                Achternaam = achternaam,
                Tussenvoegsel = tussenvoegsel,
                Voorletters = voorletters
            };

            return docent;
        }
    }
}