using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace Callisto.SharedKernel
{
    /// <summary>
    /// Defines the <see cref="DbConnectionFactory" />
    /// </summary>
    public static class DbConnectionFactory
    {
        /// <summary>
        /// Defines the Connections
        /// </summary>
        private static Dictionary<string, SqlConnection> Connections = new Dictionary<string, SqlConnection>();

        /// <summary>
        /// The GetSQLConnection
        /// </summary>
        /// <param name="connectionString">The <see cref="string"/></param>
        /// <returns>The <see cref="DbConnection"/></returns>
        public static DbConnection GetSQLConnection(string connectionString)
        {
            if (!Connections.Any(c => c.Key == connectionString))
            {
                Connections.TryAdd(connectionString, new SqlConnection(connectionString));
            }

            return Connections.First(c => c.Key == connectionString).Value;
        }
    }
}
