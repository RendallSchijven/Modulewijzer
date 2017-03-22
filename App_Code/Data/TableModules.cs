using Modulewijzer.Models;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;


namespace Modulewijzer.Data
{
    public sealed class TableModules
    {
        /// <summary>
        /// Inserts the given module into the database.
        /// </summary>
        /// <param name="competence">The module to insert.</param>
        public void Create(Module module)
        {
            // TODO: Place connection string somewhere else.
            using (var connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|Modulewijzer.mdf;Integrated Security=True"))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO Modulewijzer (Naam, EC, Studiejaar, Periode, Werkvorm, Leeruitkomsten, Literatuur, Planning)" +
                        "Values(@Name, @NumberEcs, @StudyYear, @Period, @Method, @Outcomes, @Literature, @Schedule)";

                    SqlParameter[] parameters =
                    {
                        new SqlParameter("@Name", SqlDbType.NVarChar) { Value = module.Name },
                        new SqlParameter("@NumberEcs", SqlDbType.Int) {Value = module.NumberEcs },
                        new SqlParameter("@StudyYear", SqlDbType.Int) { Value = module.StudyYear },
                        new SqlParameter("@Period", SqlDbType.Int) { Value = module.Period },
                        new SqlParameter("@Method", SqlDbType.NVarChar) {Value = module.Method },
                        new SqlParameter("@Outcomes", SqlDbType.NVarChar) {Value = module.Outcomes },
                        new SqlParameter("@Literature", SqlDbType.NVarChar) {Value = module.Literature },
                        new SqlParameter("@Schedule", SqlDbType.NVarChar) {Value = module.Schedule }
                    };
                    command.Parameters.AddRange(parameters);

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        public List<Module> GetAll()
        {
            var modules = new List<Module>();


            // TODO: Place connection string somewhere else.
            using (var connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|Modulewijzer.mdf;Integrated Security=True"))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT ModulewijzerId, Naam, EC, Studiejaar, Periode, Werkvorm, Leeruitkomsten, Literatuur, Planning  FROM Modulewijzer";

                    using (var reader = command.ExecuteReader())
                    {
                        modules.Add(new Module()
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            NumberEcs = reader.GetInt32(2),
                            StudyYear = reader.GetInt32(3),
                            Period = reader.GetInt32(4),
                            Method = reader.GetString(5),
                            Outcomes = reader.GetString(6),
                            Literature = reader.GetString(7),
                            Schedule = reader.GetString(8)
                        });
                    }
                }

                connection.Close();
            }
            return modules;
        }

        public void AddTeacher(Module module, Teacher teacher)
        {
            // TODO: Place connection string somewhere else.
            using (var connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|Modulewijzer.mdf;Integrated Security=True"))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO [ModuleDocent] ([ModuleId], [DocentId]) VALUES (@ModuleId, @DocentId)";

                    SqlParameter[] parameters =
                    {
                        new SqlParameter("@ModuleId", SqlDbType.Int) { Value = module.Id },
                        new SqlParameter("@DocentId",SqlDbType.Int) { Value = teacher.Id }
                    };
                    command.Parameters.AddRange(parameters);

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        /// <summary>
        /// Updates the given module within the database.
        /// </summary>
        /// <param name="competence">The module to update.</param>
        public void Update(Module module)
        {
            // TODO: Place connection string somewhere else.
            using (var connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|Modulewijzer.mdf;Integrated Security=True"))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "UPDATE Modulewijzer SET Naam=@Name, EC=@NumberEcs, Studiejaar=@StudyYear, Periode=@Period, Werkvorm=@Method, Leeruitkomsten=@Outcomes, Literatuur=@Literature, Planning=@Schedule WHERE ModulewijzerId=@ModuleId";

                    SqlParameter[] parameters =
                    {
                        new SqlParameter("@ModuleId", SqlDbType.Int) {Value = module.Id },
                        new SqlParameter("@Name", SqlDbType.NVarChar) { Value = module.Name },
                        new SqlParameter("@NumberEcs", SqlDbType.Int) {Value = module.NumberEcs },
                        new SqlParameter("@StudyYear", SqlDbType.Int) { Value = module.StudyYear },
                        new SqlParameter("@Period", SqlDbType.Int) { Value = module.Period },
                        new SqlParameter("@Method", SqlDbType.NVarChar) {Value = module.Method },
                        new SqlParameter("@Outcomes", SqlDbType.NVarChar) {Value = module.Outcomes },
                        new SqlParameter("@Literature", SqlDbType.NVarChar) {Value = module.Literature },
                        new SqlParameter("@Schedule", SqlDbType.NVarChar) {Value = module.Schedule }
                    };
                    command.Parameters.AddRange(parameters);

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }



            using (var connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|Modulewijzer.mdf;Integrated Security=True"))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT DocentId, Voorletters, Achternaam FROM Docenten";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string initials = reader.GetString(1);
                            string lastname = reader.GetString(2);
                        }
                    }

                }

                connection.Close();
            }

        }

        /// <summary>
        /// Deletes the given module from the database.
        /// </summary>
        /// <param name="competence">The module to delete.</param>
        public void Delete(Module module)
        {
            using (var connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|Modulewijzer.mdf;Integrated Security=True"))
            {
                connection.Open();

                connection.Close();
            }
        }
    }
}