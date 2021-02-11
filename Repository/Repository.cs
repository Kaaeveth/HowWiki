using HowWiki.DB;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace HowWiki.Repository
{
    public abstract class Repository : IDisposable
    {
        protected IDbConnectionPool dbConnectionPool;
        protected DbConnection dbConnection;

        public Repository( IDbConnectionPool connectionPool)
        {
            dbConnectionPool = connectionPool;
        }

        public void Dispose()
        {
            dbConnectionPool.ReleaseConnection(dbConnection);
        }
    }
}
