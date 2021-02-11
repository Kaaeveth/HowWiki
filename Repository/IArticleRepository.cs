using System.Collections.Generic;
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
