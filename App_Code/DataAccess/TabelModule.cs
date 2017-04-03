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
        /// Gets a module with the given ID from the database.
        /// </summary>
        /// <param name="id">The module's ID.</param>
        /// <returns></returns>
        public Module GetById(int id)
        {
            throw new NotImplementedException();
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