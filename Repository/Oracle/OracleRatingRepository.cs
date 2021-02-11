using HowWiki.DB;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;

namespace HowWiki.Repository.Oracle
{
    public class OracleRatingRepository : OracleRepository, IRatingRepository
    {
        public OracleRatingRepository(IDbConnectionPool connectionPool)
            :base(connectionPool)
        {
        }

        public void InsertRating(int textId, int stars, bool helpful)
        {
            using(var cmd = GetOracleCommand(newRatingSql, 
                new Dictionary<string, (string, OracleDbType)>
            {
                    {":tid", (textId.ToString(), OracleDbType.Int32) },
                    {":stars", (stars.ToString(), OracleDbType.Int32) },
                    {":hp", (helpful ? "1" : "0", OracleDbType.Int32) }
            }))
            {
                cmd.ExecuteNonQuery();
            }
        }

        private readonly string newRatingSql = @"merge into DS_Bewertung tgt 
                                                using (select :tid ""tid"", 76, :stars ""stars"", :hp ""hp"", sysdate from dual) src 
                                                on(tgt.textid=src.""tid"" and tgt.userid=76) 
                                                when matched then 
                                                update set tgt.sterne= src.""stars"", tgt.hilfreich=src.""hp"", tgt.zeit=sysdate 
                                                when not matched then 
                                                insert values (src.""tid"", 76, src.""stars"", src.""hp"", sysdate) ";
    }
}
