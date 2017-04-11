using Modulewijzer.Interfaces;
using Modulewijzer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace Modulewijzer.DataAccess
{
    /// <summary>
    /// Provides methods for working with the `Competentie` table within the database.
    /// </summary>
    public sealed class TabelCompetentie : IDBTable<Competentie>
    {
        /// <summary>
        /// Inserts a new competentie into the database.
        /// </summary>
        /// <param name="row">The competentie to insert.</param>
        public void Create(Competentie row)
        {
            using (var connection = new SqlConnection(DbConnection.ConnectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO [Competenties] ([Naam], [Niveau], [Competentie_beschrijving], [Competentie_groep]) VALUES" +
                        "(@Naam, @Niveau, @Beschrijving, @Groep)";

                    command.Parameters.AddRange(new SqlParameter[]
                    {
                        new SqlParameter("@Naam", SqlDbType.NVarChar) { Value = row.Naam },
                        new SqlParameter("@Niveau", SqlDbType.Int) { Value = row.Niveau },
                        new SqlParameter("@Beschrijving", SqlDbType.NVarChar) { Value = row.Beschrijving },
                        new SqlParameter("@Groep", SqlDbType.NVarChar) { Value = row.Groep }
                    });

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        /// <summary>
        /// Updates the specified competentie in the database.
        /// </summary>
        /// <param name="row">The compentie to update.</param>
        public void Update(Competentie row)
        {
            using (var connection = new SqlConnection(DbConnection.ConnectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "UPDATE [Competenties] SET [Naam]=@Naam, [Niveau]=@Niveau, " +
                        "[Competentie_beschrijving]=@Beschrijving, [Competentie_groep]=@Groep WHERE [CompetentieId]=@Id";

                    command.Parameters.AddRange(new SqlParameter[]
                    {
                        new SqlParameter("@Id" , SqlDbType.Int) {Value = row.Id },
                        new SqlParameter("@Naam", SqlDbType.NVarChar) { Value = row.Naam },
                        new SqlParameter("@Niveau", SqlDbType.Int) { Value = row.Niveau },
                        new SqlParameter("@Beschrijving", SqlDbType.NVarChar) { Value = row.Beschrijving },
                        new SqlParameter("@Groep", SqlDbType.NVarChar) { Value = row.Groep }
                    });

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        /// <summary>
        /// Deletes the competentie with the given ID from the database.
        /// </summary>
        /// <param name="row">The ID of the competentie to delete.</param>
        public void Delete(int rowId)
        {
            using (var connection = new SqlConnection(DbConnection.ConnectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM [Competenties] WHERE [CompetentieId]=@Id";
                    command.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = rowId });
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        /// <summary>
        /// Gets the link between a modulewijzer and a competentie
        /// </summary>
        public int GetLink(int CompetentieId, int ModuleId)
        {
            int result = 0;
            using (var connection = new SqlConnection(DbConnection.ConnectionString))
            {
                connection.Open();

                using (var checkLink = connection.CreateCommand())
                {
                    checkLink.CommandText = "EXEC IsCompetentieLinked @ModulewijzerId, @CompetentieId";

                    checkLink.Parameters.AddRange(new SqlParameter[]
                    {
                                    new SqlParameter("@ModulewijzerId", SqlDbType.Int) { Value = ModuleId },
                                     new SqlParameter("@CompetentieId", SqlDbType.Int) { Value = CompetentieId },
                    });

                    using (var reader = checkLink.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = reader.GetInt32(0);
                        }
                    }
                }
                connection.Close();
            }
            return result;
        }

        /// <summary>
        /// Gets all competentie groups
        /// </summary>
        public List<string> GetGroepen()
        {
            List<string> groepen = new List<string>();

            using (var connection = new SqlConnection(DbConnection.ConnectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();

                    command.CommandText = "EXEC GetGroepen";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            groepen.Add(reader.GetString(0));
                        }
                    }

                }
                connection.Close();
            }

            return groepen;
        }

        /// <summary>
        /// Assigns a competentie to a modulewijzer
        /// </summary>
        public void Toevoegen(int ModulewijzerId, int CompetentieId)
        {
            using (var connection = new SqlConnection(DbConnection.ConnectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "EXEC LinkCompetentie @ModulewijzerId, @CompetentieId";

                    command.Parameters.AddRange(new SqlParameter[]
                    {
                        new SqlParameter("@ModulewijzerId", SqlDbType.Int) { Value = ModulewijzerId },
                        new SqlParameter("@CompetentieId", SqlDbType.Int) { Value = CompetentieId },
                    });

                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        /// <summary>
        /// Unassigns a competentie to a modulewijzer
        /// </summary>
        public void Verwijderen(int ModulewijzerId, int CompetentieId)
        {
            using (var connection = new SqlConnection(DbConnection.ConnectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "EXEC DropLinkCompetentie @ModulewijzerId, @CompetentieId";

                    command.Parameters.AddRange(new SqlParameter[]
                    {
                        new SqlParameter("@ModulewijzerId", SqlDbType.Int) { Value = ModulewijzerId },
                        new SqlParameter("@CompetentieId", SqlDbType.Int) { Value = CompetentieId },
                    });

                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        /// <summary>
        /// Gets a competentie with the given ID from the database.
        /// </summary>
        /// <param name="id">The competentie's ID.</param>
        /// <returns></returns>
        public Competentie GetById(int id)
        {
            string naam = "";
            int niveau = 0;
            string competentie_beschrijving = "";
            string competentie_groep = "";

            using (var connection = new SqlConnection(DbConnection.ConnectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "EXEC GetCompetentie @competentieId";
                    command.Parameters.AddWithValue("@competentieId", id);
                    command.ExecuteNonQuery();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            naam = reader.GetString(0);
                            niveau = reader.GetInt32(1);
                            competentie_beschrijving = reader.GetString(2);
                            competentie_groep = reader.GetString(3);
                        }
                    }

                }
                connection.Close();
            }

            var competentie = new Competentie()
            {
                Naam = naam,
                Niveau = niveau,
                Beschrijving = competentie_beschrijving,
                Groep = competentie_groep
            };

            return competentie;
        }
    }
}
