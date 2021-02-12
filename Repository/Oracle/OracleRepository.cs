using System;
using System.Collections.Generic;
using System.Data;
using HowWiki.DB;
using Oracle.ManagedDataAccess.Client;

/*
 * OracleRepository.cs
 * Basisklasse aller Oracle Repositories.
 * Nur mit einem OracleConnectionPool verwenden.
 * 
 * Autor: Dominik Strutz
 */

namespace HowWiki.Repository.Oracle
{
    public abstract class OracleRepository : Repository
    {
        public OracleRepository(IDbConnectionPool connectionPool)
            :base(connectionPool)
        {
            if (connectionPool is OracleDbConnectionPool)
                dbConnection = (OracleConnection)connectionPool.RequestConnection();
            else
                throw new InvalidCastException("OracleRepository nur mit OracleDbConnectionPool verwenden");
        }

        public OracleCommand GetOracleCommand(string query, IDictionary<string, (string, OracleDbType)> parameters)
        {
            var cmd = new OracleCommand(query, (OracleConnection)dbConnection) //Neues Statement
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
