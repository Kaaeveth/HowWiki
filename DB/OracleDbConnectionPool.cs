using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;

/*
 * OracleDbConnectionPool.cs
 * ConnectionPool fuer eine Oracle Datenbank
 * 
 * Autor: Dominik Strutz
 */

namespace HowWiki.DB
{
    public class OracleDbConnectionPool : IDbConnectionPool, IDisposable
    {
        public int POOL_SIZE { get; }                       // Anzahl an Verbindungen
        private int takenConnectionsCount;
        private PoolConnection<OracleConnection>[] pool;
        private readonly IConfiguration config;

        public OracleDbConnectionPool(IConfiguration configuration, int poolSize = 30)
        {
            POOL_SIZE = poolSize;
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
                pool[i].Connection.KeepAlive = true;
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
            lock (pool)
            {
                //Ist die Verbindung im Pool?
                foreach(PoolConnection<OracleConnection> con in pool)
                {
                    if(ReferenceEquals(con.Connection, connection))
                    {
                        con.Taken = false;
                        takenConnectionsCount--;
                        return;
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
            lock (pool)
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
