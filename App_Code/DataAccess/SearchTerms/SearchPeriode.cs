using System.Data.SqlClient;
using System.Data;


namespace Modulewijzer.DataAccess
{
    public sealed class SearchPeriode : SearchTerm
    {
        private int m_periode;


        public override bool SetTerm(string term)
        {
            return int.TryParse(term, out m_periode);
        }

        public override SqlCommand GetCommand()
        {
            string query = "SELECT [ModulewijzerId], [Naam], [EC], [Studiejaar], [Periode] FROM [Modulewijzer]" +
                "WHERE [Periode] = @Periode";

            var cmd = new SqlCommand(query);
            cmd.Parameters.Add(new SqlParameter("@Periode", SqlDbType.Int) { Value = m_periode });

            return cmd;
        }
    }
}