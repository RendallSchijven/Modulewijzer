using System.Data.SqlClient;


namespace Modulewijzer.DataAccess
{
    public abstract class SearchTerm
    {
        public abstract bool SetTerm(string term);
        public abstract SqlCommand GetCommand();
    }
}
