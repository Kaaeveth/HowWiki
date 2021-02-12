using System.Collections.Generic;
using HowWiki.Models;

/*
 * IArticleRepository.cs
 * Schnittstelle fuer ein ArticleRepository
 * 
 * Autor: Dominik Strutz
 */

namespace HowWiki.Repository
{
    public interface IArticleRepository
    {
        ArticleModel GetArticleById(int id);
        IEnumerable<ArticleModel> GetArticles();
        void InsertArticle(ArticleModel article);
    }
}
