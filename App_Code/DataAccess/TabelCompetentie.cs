﻿using Modulewijzer.Interfaces;
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
                    command.CommandText = "INSERT INTO [Competenties] ([Naam], [Niveau], [Competentie_beschrijving]) VALUES" +
                        "(@Naam, @Niveau, @Beschrijving)";

                    command.Parameters.AddRange(new SqlParameter[]
                    {
                        new SqlParameter("@Naam", SqlDbType.NVarChar) { Value = row.Naam },
                        new SqlParameter("@Niveau", SqlDbType.Int) { Value = row.Niveau },
                        new SqlParameter("@Beschrijving", SqlDbType.NVarChar) { Value = row.Beschrijving }
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
                        "[Competentie_beschrijving]=@Beschrijving WHERE [Id]=@Id";

                    command.Parameters.AddRange(new SqlParameter[]
                    {
                        new SqlParameter("@Naam", SqlDbType.NVarChar) { Value = row.Naam },
                        new SqlParameter("@Niveau", SqlDbType.Int) { Value = row.Niveau },
                        new SqlParameter("@Beschrijving", SqlDbType.NVarChar) { Value = row.Beschrijving }
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
                    command.CommandText = "DELETE FROM [Competentie] WHERE [Id]=@Id";
                    command.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = rowId });
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets all competenties in the database.
        /// </summary>
        /// <returns>A collection of all competenties in the database.</returns>
        public Competentie[] GetAll()
        {
            var result = new List<Competentie>();

            return result.ToArray();
        }
    }
}