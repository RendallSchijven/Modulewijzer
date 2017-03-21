using Modulewijzer.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


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
                    command.CommandText = "INSERT INTO Modulewijzer (Naam, EC, Studiejaar, Periode) " +
                        "Values(@Name, @NumberEcs, @StudyYear, @Period)";

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
                    command.CommandText = "UPDATE Module SET Naam=@Name, EC=@NumberEcs, Studiejaar=@StudyYear, Periode=@Period";

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
