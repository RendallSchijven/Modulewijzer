using Modulewijzer.Interfaces;
using Modulewijzer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace Modulewijzer.DataAccess
{
    /// <summary>
    /// Provides methods for working with the `Module` table within the database.
    /// </summary>
    public sealed class TabelModule : IDBTable<Module>
    {
        /// <summary>
        /// Inserts a new module into the database.
        /// </summary>
        /// <param name="row">The module to insert.</param>
        public void Create(Module row)
        {
            using (var connection = new SqlConnection(DbConnection.ConnectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO [Modulewijzer] ([Naam], [EC], [Studiejaar], " +
                        "[Periode], [Werkvorm], [Leeruitkomsten], [Literatuur], [Planning]) VALUES " +
                        "(@Naam, @AantalEcs, @StudieJaar, @Periode, @Werkvorm, @Leeruitkomsten, " +
                        "@Literatuur, @Planning)";

                    command.Parameters.AddRange(new SqlParameter[]
                    {
                        new SqlParameter("@Naam", SqlDbType.NVarChar) { Value = row.Naam },
                        new SqlParameter("@AantalEcs", SqlDbType.Int) { Value = row.AantalEcs },
                        new SqlParameter("@StudieJaar", SqlDbType.Int) { Value = row.StudieJaar },
                        new SqlParameter("@Periode", SqlDbType.Int) { Value = row.Periode },
                        new SqlParameter("@Werkvorm", SqlDbType.NVarChar) { Value = row.Werkvorm },
                        new SqlParameter("@Leeruitkomsten", SqlDbType.NVarChar) { Value = row.Leeruitkomsten },
                        new SqlParameter("@Literatuur", SqlDbType.NVarChar) { Value = row.Literatuur },
                        new SqlParameter("@Planning", SqlDbType.NVarChar) { Value = row.Planning }
                    });

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        /// <summary>
        /// Updates the specified module in the database.
        /// </summary>
        /// <param name="row">The module to update.</param>
        public void Update(Module row)
        {
            using (var connection = new SqlConnection(DbConnection.ConnectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "UPDATE [Modulewijzer] SET [Naam]=@Naam, [EC]=@AantalEcs, " +
                        "[Studiejaar]=@StudieJaar, [Periode]=@Periode, [Werkvorm]=@Werkvorm, " +
                        "[Leeruitkomsten]=@Leeruitkomsten, [Literatuur]=@Literatuur, [Planning]=@Planning " +
                        "WHERE [ModulewijzerId]=@Id";

                    command.Parameters.AddRange(new SqlParameter[]
                    {
                        new SqlParameter("@Id",SqlDbType.Int) { Value = row.Id },
                        new SqlParameter("@Naam", SqlDbType.NVarChar) { Value = row.Naam },
                        new SqlParameter("@AantalEcs", SqlDbType.Int) { Value = row.AantalEcs },
                        new SqlParameter("@StudieJaar", SqlDbType.Int) { Value = row.StudieJaar },
                        new SqlParameter("@Periode", SqlDbType.Int) { Value = row.Periode },
                        new SqlParameter("@Werkvorm", SqlDbType.NVarChar) { Value = row.Werkvorm },
                        new SqlParameter("@Leeruitkomsten", SqlDbType.NVarChar) { Value = row.Leeruitkomsten },
                        new SqlParameter("@Literatuur", SqlDbType.NVarChar) { Value = row.Literatuur },
                        new SqlParameter("@Planning", SqlDbType.NVarChar) { Value = row.Planning }
                    });

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        /// <summary>
        /// Deletes the module with the given ID from the database.
        /// </summary>
        /// <param name="row">The ID of the module to delete.</param>
        public void Delete(int rowId)
        {
            using (var connection = new SqlConnection(DbConnection.ConnectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM [Modulewijzer] WHERE ModulewijzerId=@Id";
                    command.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = rowId });
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        /// <summary>
        /// Gets the docenten linked to the module
        /// </summary>
        public List<string> GetLinkedDocenten(int Id)
        {
            List<string> docenten = new List<string>();

            using (var connection = new SqlConnection(DbConnection.ConnectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();

                    command.CommandText = "EXEC GetLinkedDocenten @ModuleId";
                    command.Parameters.Add(new SqlParameter("@ModuleId", SqlDbType.Int) { Value = Id });

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string voorletters = reader.GetString(1);
                            string achternaam = reader.GetString(2);
                            string tussenvoegsel = reader.GetString(3);
                            docenten.Add($"{voorletters}. {tussenvoegsel} {achternaam}, ");
                        }
                    }

                }
                connection.Close();
            }
            return docenten;
        }

        /// <summary>
        /// Gets the competenties linked to the module
        /// </summary>
        public Dictionary<string, string> GetLinkedCompetenties(int Id)
        {
            Dictionary<string, string> competenties = new Dictionary<string, string>();

            using (var connection = new SqlConnection(DbConnection.ConnectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();

                    command.CommandText = "EXEC GetLinkedCompetenties @ModuleId";
                    command.Parameters.Add(new SqlParameter("@ModuleId", SqlDbType.Int) { Value = Id });

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string comp_naam = reader.GetString(1);
                            string niveau = Convert.ToString(reader.GetInt32(2));
                            string competentie_beschrijving = reader.GetString(3);
                            competenties.Add($"{comp_naam} {niveau}", $"{competentie_beschrijving}");
                        }
                    }

                }
                connection.Close();
            }
            return competenties;
        }

        /// <summary>
        /// Gets a module with the given ID from the database.
        /// </summary>
        /// <param name="id">The module's ID.</param>
        /// <returns></returns>
        public Module GetById(int id)
        {
            string naam = "";
            int EC = 0;
            int studiejaar = 0;
            int periode = 0;
            string werkvorm = "";
            string leeruitkomsten = "";
            string literatuur = "";
            string planning = "";

            using (var connection = new SqlConnection(DbConnection.ConnectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "EXEC GetModuleData @ModulewijzerId";
                    command.Parameters.AddWithValue("@ModulewijzerId", id);
                    command.ExecuteNonQuery();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            naam = reader.GetString(0);
                            EC = reader.GetInt32(1);
                            studiejaar = reader.GetInt32(2);
                            periode = reader.GetInt32(3);
                            werkvorm = reader.GetString(4);
                            leeruitkomsten = reader.GetString(5);
                            literatuur = reader.GetString(6);
                            planning = reader.GetString(7);
                        }
                    }

                }
                connection.Close();
            }

            var module = new Module()
            {
                Naam = naam,
                AantalEcs = EC,
                StudieJaar = studiejaar,
                Periode = periode,
                Werkvorm = werkvorm,
                Leeruitkomsten = leeruitkomsten,
                Literatuur = literatuur,
                Planning = planning
            };

            return module;
        }


        public string GreaterZero(int ec, int studiejaar, int periode)
        {
            if (ec < 0)
            {
                return "Aantal EC`s moet groter zijn dan 0.";
            }
            if (studiejaar < 0)
            {
                return "Studiejaar moet groter zijn dan 0.";
            }
            if (periode < 0)
            {
                return "Periode moet groter zijn dan 0.";
            }
            return "";
        }

        public List<Module> Search(SearchTerm term)
        {
            using (var connection = new SqlConnection(DbConnection.ConnectionString))
            {
                connection.Open();

                var cmd = term.GetCommand();
                cmd.Connection = connection;
                return GetModulesFromReader(cmd.ExecuteReader());
            }
        }

        #region Private methods.
        public List<Module> GetModulesFromReader(SqlDataReader reader)
        {
            var result = new List<Module>();
            while (reader.Read())
            {
                result.Add(new Module()
                {
                    Id = reader.GetInt32(0),
                    Naam = reader.GetString(1),
                    AantalEcs = reader.GetInt32(2),
                    StudieJaar = reader.GetInt32(3),
                    Periode = reader.GetInt32(4),
                    //Werkvorm = reader.GetString(5),
                    //Leeruitkomsten = reader.GetString(6),
                    //Literatuur = reader.GetString(7),
                    //Planning = reader.GetString(8)
                });
            }
            return result;
        }
        #endregion
    }
}