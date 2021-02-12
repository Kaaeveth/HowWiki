using HowWiki.DB;
using System;
using System.Data.Common;

/*
 * Repository.cs
 * Basisklasse aller Repositories
 * Ein Repository stellt Logik fuer Datenbankabfragen bereit
 * 
 * Autor: Dominik Strutz
 */

namespace HowWiki.Repository
{
    public abstract class Repository : IDisposable
    {
        protected IDbConnectionPool dbConnectionPool;
        protected DbConnection dbConnection;

        public Repository(IDbConnectionPool connectionPool)
        {
            dbConnectionPool = connectionPool;
        }

        public void Dispose()
        {
            dbConnectionPool.ReleaseConnection(dbConnection);
        }
    }
}
