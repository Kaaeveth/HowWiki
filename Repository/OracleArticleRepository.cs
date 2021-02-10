using HowWiki.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Oracle.ManagedDataAccess.Client;
using HowWiki.DB;
using System.Data;
using System.Data.Common;

namespace HowWiki.Repository
{
    public class OracleArticleRepository : OracleRepository, IArticleRepository
    {

        public OracleArticleRepository(IDbConnectionPool connectionPool)
            :base(connectionPool)
        {
        }

        public ArticleModel GetArticleById(int id)
        {
            using (var query = GetOracleCommand(queries[QUERY_KEYS.SELECT_ID], new Dictionary<string, (string, OracleDbType)>()
            {
                {":id", (id.ToString(), OracleDbType.Int32) }
            }))
            using (OracleDataReader reader = query.ExecuteReader())
                while (reader.Read())
                    return ParseModel(reader);
            return new ArticleModel();
        }

        public IEnumerable<ArticleModel> GetArticles()
        {
            IList<ArticleModel> articleModels = new List<ArticleModel>();

            using (var query = GetOracleCommand(queries[QUERY_KEYS.SELECT_ALL], null))
            using (OracleDataReader reader = query.ExecuteReader())
                while (reader.Read())
                    articleModels.Add(ParseModel(reader));

            return articleModels;
        }

        public void InsertArticle(ArticleModel article)
        {
            throw new NotImplementedException();
        }

        private ArticleModel ParseModel(OracleDataReader reader)
        {
            ArticleModel model = new ArticleModel()
            {
                //Spalten sind 0-basiert
                Id = reader.GetInt32(0),
                Title = reader.GetString(1),
                Content = reader.GetString(2),
                References = reader.GetString(3),
                AvgStars = reader.GetFloat(4),
                Author = reader.GetString(5),
                UsersRatedCount = reader.GetInt32(8),
                Tags = GetTagsById(reader.GetInt32(0)).ToArray()
            };

            return model;
        }

        private IEnumerable<string> GetTagsById(int id)
        {
            IList<string> tags = new List<string>();

            using (var query = GetOracleCommand(queries[QUERY_KEYS.SELECT_TAGS], new Dictionary<string, (string, OracleDbType)>()
            {
                {":id", (id.ToString(), OracleDbType.Int32) }
            }))
            using (OracleDataReader reader = query.ExecuteReader())
                while (reader.Read())
                {
                    //Spalten sind 0-basiert
                    tags.Add(reader.GetString(0));
                }

            return tags;
        }

        private enum QUERY_KEYS
        {
            SELECT_ALL,
            SELECT_ID,
            SELECT_TAGS
        }

        private readonly Dictionary<QUERY_KEYS, string> queries = new Dictionary<QUERY_KEYS, string>() 
        {
            {QUERY_KEYS.SELECT_ALL, "select * from article" },
            {QUERY_KEYS.SELECT_ID, "select * from article where textID = :id" },
            {QUERY_KEYS.SELECT_TAGS, "select tag from DS_Tags where id = :id" }
        };
    }
}
