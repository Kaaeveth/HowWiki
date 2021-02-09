using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HowWiki.Models;

namespace HowWiki.Repository
{
    public interface IArticleRepository
    {
        ArticleModel GetArticleById(int id);
        IEnumerable<ArticleModel> GetArticles();
        void InsertArticle(ArticleModel article);
    }
}
