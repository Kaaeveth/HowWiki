using HowWiki.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HowWiki.DB;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace HowWiki.Repository
{
    public class OracleCommentRepository : OracleRepository, ICommentRepository
    {

        public OracleCommentRepository(IDbConnectionPool connectionPool)
            :base(connectionPool)
        {
        }

        public IEnumerable<CommentModel> GetCommentsByTextId(int textId)
        {
            IList<CommentModel> comments = new List<CommentModel>();

            using (var cmd = GetOracleCommand("select * from kommentare where textId = :id order by erstellt desc", new Dictionary<string, (string, OracleDbType)>()
            {
                {":id", (textId.ToString(), OracleDbType.Int32) }
            }))
            using(OracleDataReader reader = cmd.ExecuteReader())
            {
                while(reader.Read())
                    comments.Add(ParseModel(reader));
            }

            return comments;
        }

        private CommentModel ParseModel(OracleDataReader reader)
        {
            return new CommentModel()
            {
                Comment = reader.GetString(0),
                Created = reader.GetDateTime(1),
                Author = reader.GetString(2)
            };
        }

        public void CreateCommentForTextId(int textId, string comment, int userId)
        {
            using(var cmd = GetOracleCommand("insert into DS_Kommentare values(:userid, :textid, sysdate, :inhalt)", new Dictionary<string, (string, OracleDbType)>()
            {
                {":userid", (userId.ToString(), OracleDbType.Int32) },
                {":textid", (textId.ToString(), OracleDbType.Int32) },
                {":inhalt", (comment, OracleDbType.NVarchar2) }
            }))
            {
                cmd.ExecuteNonQuery();
            }
        }
    }
}
