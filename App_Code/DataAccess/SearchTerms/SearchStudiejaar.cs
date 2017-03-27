using System.Data.SqlClient;
using System.Data;


namespace Modulewijzer.DataAccess
{
    public sealed class SearchStudieJaar : SearchTerm
    {
        private int m_studiejaar;


        public override bool SetTerm(string term)
        {
            return int.TryParse(term, out m_studiejaar);
        }

        public override SqlCommand GetCommand()
        {
            string query = "SELECT [ModulewijzerId], [Naam], [EC], [Studiejaar], [Periode] FROM [Modulewijzer]" +
                "WHERE [Studiejaar] = @StudieJaar";

            var cmd = new SqlCommand(query);
            cmd.Parameters.Add(new SqlParameter("@StudieJaar", SqlDbType.Int) { Value = m_studiejaar });

            return cmd;
        }
    }
}
