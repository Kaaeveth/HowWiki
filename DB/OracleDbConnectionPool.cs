using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace HowWiki.DB
{
    public class OracleDbConnectionPool : IDbConnectionPool, IDisposable
    {
        public int POOL_SIZE { get; } = 30; // Anzahl an Verbindungen
        private int takenConnectionsCount;
        private PoolConnection<OracleConnection>[] pool;
        private readonly object lockObject = new object();  //ThreadLock
        private readonly IConfiguration config;

        public OracleDbConnectionPool(IConfiguration configuration)
        {
            pool = new PoolConnection<OracleConnection>[POOL_SIZE];
            takenConnectionsCount = 0;
            config = configuration;

            //Verbindungen aufbauen
            for(int i = 0; i < POOL_SIZE; i++)
            {
                pool[i] = new PoolConnection<OracleConnection>()
                {
                    Connection = new OracleConnection(config.GetConnectionString("DbConnection"))
                };
                pool[i].Connection.Open();
            }
        }

        public void Dispose()
        {
            foreach (PoolConnection<OracleConnection> con in pool)
            {
                con.Connection.Close();
                con.Connection.Dispose();
            }
        }

        public int FreeConnections 
        {   get
            {
                return POOL_SIZE - takenConnectionsCount;
            }
        }

        public void ReleaseConnection(IDbConnection connection)
        {
            lock (lockObject)
            {
                //Ist die Verbindung im Pool?
                foreach(PoolConnection<OracleConnection> con in pool)
                {
                    if(ReferenceEquals(con.Connection, con))
                    {
                        con.Taken = false;
                        takenConnectionsCount--;
                    }
                }
            }

            //Die Verbindung wurde nicht aus den Pool genommen
            //Schliessen der Verbindung
            connection.Close();
            connection.Dispose();
        }

        public IDbConnection RequestConnection()
        {
            lock (lockObject)
            {
                //Eine freie Verbindung suchen
                foreach(PoolConnection<OracleConnection> con in pool)
                {
                    if (!con.Taken)
                    {
                        con.Taken = true;
                        takenConnectionsCount++;
                        return con.Connection;
                    }
                }
            }

            //Keine Verbindung gefunden
            //Erstellen einer neuen
            return new OracleConnection(config.GetConnectionString("DbConnection"));
        }
    }
}
