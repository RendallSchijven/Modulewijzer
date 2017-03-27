using System.Data.SqlClient;
using System.Data;


namespace Modulewijzer.DataAccess
{
    public sealed class SearchNaam : SearchTerm
    {
        private string m_naam;


        public override bool SetTerm(string term)
        {
            m_naam = term;
            return true;
        }

        public override SqlCommand GetCommand()
        {
            string query = "SELECT [ModulewijzerId], [Naam], [EC], [Studiejaar], [Periode] FROM [Modulewijzer]" +
                "WHERE [Naam] LIKE @Naam";

            var cmd = new SqlCommand(query);
            cmd.Parameters.Add(new SqlParameter("@Naam", SqlDbType.NVarChar) { Value = $"%{m_naam}%" });

            return cmd;
        }
    }
}
