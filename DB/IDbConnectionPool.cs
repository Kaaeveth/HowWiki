using System.Data;

namespace HowWiki.DB
{
    public interface IDbConnectionPool
    {
        IDbConnection RequestConnection();
        void ReleaseConnection(IDbConnection con);

        int POOL_SIZE { get; }
        int FreeConnections { get; }
    }
}
