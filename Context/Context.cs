using HowWiki.DB;
using System;
using System.Data.Common;

/*
 * Context.cs
 * Basisklasse aller Contexts
 * Ein Context stellt Logik fuer Datenbankabfragen bereit
 * 
 * Autor: Dominik Strutz
 */

namespace HowWiki.Context
{
    public abstract class Context : IDisposable
    {
        protected IDbConnectionPool dbConnectionPool;
        protected DbConnection dbConnection;

        public Context(IDbConnectionPool connectionPool)
        {
            dbConnectionPool = connectionPool;
        }

        public void Dispose()
        {
            dbConnectionPool.ReleaseConnection(dbConnection);
        }
    }
}
