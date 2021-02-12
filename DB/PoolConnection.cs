using System.Data;

/*
 * PoolConnection.cs
 * Stellt eine Verbindung im DbConnectionPool dar.
 * Autor: Dominik Strutz
 */

namespace HowWiki.DB
{
    public class PoolConnection<T> where T : IDbConnection
    {
        public T Connection { get; set; }
        public bool Taken { get; set; }

        public PoolConnection()
        {
            Taken = false;
        }
    }
}
