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
                    command.CommandText = "INSERT INTO Modulewijzer (Naam, EC, Studiejaar, Periode, Period) " +
                        "Values(@Name, @NumberEcs, @StudyDate, @EndDate, @Period)";

                    SqlParameter[] parameters =
                    {
                        new SqlParameter("@Name", SqlDbType.NVarChar) { Value = module.Name },
                        new SqlParameter("@NumberEcs", SqlDbType.Int) {Value = module.NumberEcs },
                        new SqlParameter("@StudyYear", SqlDbType.Int) { Value = module.StudyYear },
                        new SqlParameter("@Period", SqlDbType.Int) { Value = module.Period }
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
                    command.CommandText = "SELECT * FROM Modulewijzer";

                    using (var reader = command.ExecuteReader())
                    {
                        modules.Add(new Module()
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            NumberEcs = reader.GetInt32(2),
                            StudyYear = reader.GetInt32(3),
                            Period = reader.GetInt32(4)
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
                    command.CommandText = "UPDATE Module SET Name=@Name, @NumberEcs=@NumberEcs, StudyDate=@StudyDate, EndDate=@EndDate, Period=@Period";

                    SqlParameter[] parameters =
{
                        new SqlParameter("@Name", SqlDbType.NVarChar) { Value = module.Name },
                        new SqlParameter("@NumberEcs", SqlDbType.Int) {Value = module.NumberEcs },
                        new SqlParameter("@StudyDate", SqlDbType.Int) { Value = module.StudyDate },
                        new SqlParameter("@Period", SqlDbType.Int) { Value = module.Period }
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
                    command.CommandText = "SELECT * FROM Docenten";

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
            using (var connection = new SqlConnection("ModuleWijzer"))
            {
                connection.Open();

                connection.Close();
            }
        }
    }
}