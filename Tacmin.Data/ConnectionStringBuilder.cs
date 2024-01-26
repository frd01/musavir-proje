using Tacmin.Core;

namespace Tacmin.Data
{
    public class ConnectionStringBuilder
    {
        public static string MainDbConStr()
        {
            return SqlConStrUserCred(
                Engine.Settings.db.server.ToString(),
                Engine.Settings.db.name.ToString(),
                Engine.Settings.db.username.ToString(),
                Engine.Settings.db.password.ToString()
                );
        }

        public static string SqlConStrUserCred(string server, string db, string username, string pass)
        {
            return $"Server={server}; Database={db}; user id={username}; password={pass}; MultipleActiveResultSets=true";
        }

        public static string SqlConStrTrusted(string server, string db)
        {
            return $"Server={server}; Database={db}; Trusted_Connection=True; MultipleActiveResultSets=true";
        }
    }
}
