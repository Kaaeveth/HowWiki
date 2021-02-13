using System.Collections.Generic;
using HowWiki.Models;

/*
 * IArticleContext.cs
 * Schnittstelle fuer ein ArticleContext
 * 
 * Autor: Dominik Strutz
 */

namespace HowWiki.Context
{
    public interface IArticleContext
    {
        ArticleModel GetArticleById(int id);
        IEnumerable<ArticleModel> GetArticles();
        void InsertArticle(ArticleModel article);
    }
}
