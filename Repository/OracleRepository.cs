using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using HowWiki.DB;
using HowWiki.Repository;
using Oracle.ManagedDataAccess.Client;

namespace HowWiki.Repository
{
    public abstract class OracleRepository : IDisposable
    {
        protected readonly OracleConnection dbConnection;
        protected readonly IDbConnectionPool dbConnectionPool;

        public OracleRepository(IDbConnectionPool connectionPool)
        {
            dbConnectionPool = connectionPool;
            dbConnection = (OracleConnection)connectionPool.RequestConnection();
        }

        public void Dispose()
        {
            dbConnectionPool.ReleaseConnection(dbConnection);
        }

        public OracleCommand GetOracleCommand(string query, Dictionary<string, (string, OracleDbType)> parameters)
        {
            var cmd = new OracleCommand(query, dbConnection) //Neues Statement
            {
                CommandType = CommandType.Text
            };

            if (!(parameters is null))
                foreach (KeyValuePair<string, (string, OracleDbType)> param in parameters)
                {
                    cmd.Parameters.Add(param.Key, param.Value.Item2).Value = param.Value.Item1;
                }

            return cmd;
        }
    }
}
