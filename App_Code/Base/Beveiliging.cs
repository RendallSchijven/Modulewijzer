using System.Web;
using System.Data.SqlClient;
using Modulewijzer.DataAccess;
using System.Data;

/// <summary>
/// Every action that takes place on an authentication basis.
/// </summary>
public static class Beveiliging
{
    /// <summary>
    /// Gets random salt from bcrypt, then hashes password
    /// </summary>
    private static string HashPassword(string password)
    {
        //workfactor 13 seems the sweet spot ~1-2sec
        var salt = BCrypt.Net.BCrypt.GenerateSalt(13);
        return BCrypt.Net.BCrypt.HashPassword(password, salt);
    }

    /// <summary>
    /// Creates an account
    /// </summary>
    public static string CreateAccount(string afkorting, string wachtwoord)
    {
        string hashPass = HashPassword(wachtwoord);
        using (var connection = new SqlConnection(DbConnection.ConnectionString))
        {
            connection.Open();

            /// <summary>
            /// Checks if the afkorting already exists
            /// </summary>
            using (var CheckAfkorting = connection.CreateCommand())
            {
                CheckAfkorting.CommandText = "EXEC TryAfkorting @Afkorting";

                CheckAfkorting.Parameters.Add(new SqlParameter("@Afkorting", SqlDbType.NVarChar) { Value = afkorting });

                using (var reader = CheckAfkorting.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader.GetString(0) != null)
                        {
                            return "Afkorting is al in gebruik. Kies een andere.";
                        }
                    }
                }
            }

            /// <summary>
            /// Creates the new user
            /// </summary>
            using (var Gebruiker_aanmaken = connection.CreateCommand())
            {
                Gebruiker_aanmaken.CommandText = "EXEC CreateGebruiker @Afkorting, @Wachtwoord";

                Gebruiker_aanmaken.Parameters.AddRange(new SqlParameter[]
                {
                        new SqlParameter("@Afkorting", SqlDbType.NVarChar) { Value = afkorting },
                        new SqlParameter("@Wachtwoord", SqlDbType.NVarChar) { Value = hashPass },
                });
                Gebruiker_aanmaken.ExecuteNonQuery();

            }
            connection.Close();
        }
        Beveiliging.Login(afkorting, wachtwoord);
        return "";
    }

    /// <summary>
    /// Login to blyat
    /// </summary>
    public static string Login(string afkorting, string wachtwoord)
    {
        using (var connection = new SqlConnection(DbConnection.ConnectionString))
        {
            connection.Open();

            //Checks if there is account with this afkorting, and password. if so, login
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "EXEC Inloggen @Afkorting";
                command.Parameters.Add(new SqlParameter("@Afkorting", SqlDbType.NVarChar) { Value = afkorting });

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //if (reader.GetString(1) == null) return "Geen account met deze afkorting";
                        if (!BCrypt.Net.BCrypt.Verify(wachtwoord, reader.GetString(2))) return "Wachtwoord is incorrect";

                        //Set sessions
                        HttpContext.Current.Session["Gebruiker_Id"] = reader.GetInt32(0);
                        HttpContext.Current.Session["Afkorting"] = reader.GetString(1);
                        HttpContext.Current.Response.Redirect("Default");
                    }
                }
            }

            connection.Close();
        }
        return "";
    }

    /// <summary>
    /// Logout from website
    /// </summary>
    public static void Logout()
    {
        HttpContext.Current.Session.Clear();
        HttpContext.Current.Session.Abandon();
        HttpContext.Current.Response.Redirect("Default");
    }

    /// <summary>
    /// Check for sessions
    /// </summary>
    public static bool IsAuthenticated()
    {
        if (HttpContext.Current.Session["Gebruiker_Id"] == null) return false;
        return true;
    }
}