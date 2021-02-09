using HowWiki.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using Microsoft.Extensions.Configuration;

namespace HowWiki.Repository
{
    public class ArticleRepository : IArticleRepository, IDisposable
    {
        private readonly IConfiguration config;
        private OracleConnection con;

        public ArticleRepository(IConfiguration configuration)
        {
            config = configuration;
            con = new OracleConnection(config.GetConnectionString("DbConnection"));
            con.Open();
        }

        public void Dispose()
        {
            con.Close();
            con.Dispose();
        }

        public ArticleModel GetArticleById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ArticleModel> GetArticles()
        {
            IList<ArticleModel> articleModels = new List<ArticleModel>();

            using (var query = GetOracleCommand(QUERY_KEYS.SELECT_ALL, null))
            using (OracleDataReader reader = query.ExecuteReader())
                while (reader.Read())
                {
                    //Spalten sind 0-basiert
                    articleModels.Add(new ArticleModel()
                    {
                        Id = reader.GetInt32(0),
                        Title = reader.GetString(1),
                        Content = reader.GetString(2),
                        References = reader.GetString(3),
                        AvgStars = reader.GetFloat(4),
                        Author = reader.GetString(5),
                        UsersRatedCount = reader.GetInt32(8),
                        Tags = GetTagsById(reader.GetInt32(0)).ToArray()
                    });
                }

            return articleModels;
        }

        public void InsertArticle(ArticleModel article)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<string> GetTagsById(int id)
        {
            IList<string> tags = new List<string>();

            using (var query = GetOracleCommand(QUERY_KEYS.SELECT_TAGS, new Dictionary<string, string>()
            {
                {":id", id.ToString() }
            }))
            using (OracleDataReader reader = query.ExecuteReader())
                while (reader.Read())
                {
                    //Spalten sind 0-basiert
                    tags.Add(reader.GetString(0));
                }

            return tags;
        }

        //@paramters: key=Parameter in Form ":Name", value = Wert
        private OracleCommand GetOracleCommand(QUERY_KEYS query, Dictionary<string, string> parameters)
        {
            string sql = queries[query];

            if(!(parameters is null))
                foreach(KeyValuePair<string, string> param in parameters)
                {
                    sql = sql.Replace(param.Key, param.Value);
                }

            var cmd = new OracleCommand(sql, con) 
            { 
                CommandType = System.Data.CommandType.Text
            };

            return cmd;
        }

        private enum QUERY_KEYS
        {
            SELECT_ALL,
            SELECT_ID,
            SELECT_TAGS
        }

        private Dictionary<QUERY_KEYS, string> queries = new Dictionary<QUERY_KEYS, string>() 
        {
            {QUERY_KEYS.SELECT_ALL, "select * from article" },
            {QUERY_KEYS.SELECT_ID, "select * from article where textID = :id" },
            {QUERY_KEYS.SELECT_TAGS, "select tag from DS_Tags where id = :id" }
        };
    }
}
