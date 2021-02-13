using System.Collections.Generic;
using HowWiki.Models;
using HowWiki.Context;
using Microsoft.AspNetCore.Mvc;

/*
 * ArticleController.cs
 * Controller fuer die Artikel
 * 
 * Author: Dominik Strutz
*/

namespace HowWiki.Controllers
{
    [Route("api/[controller]")]
    public class ArticleController : ControllerBase
    {
        IArticleContext articleContext;

        public ArticleController(IArticleContext articleRepository)
        {
            this.articleContext = articleRepository;
        }

        // GET: api/<ArticleController>/list
        [HttpGet("list")]
        public IEnumerable<ArticleModel> Get()
        {
            return articleContext.GetArticles();
        }

        // GET api/<ArticleController>/show/{textId}
        [HttpGet("show/{textId}")]
        public ArticleModel Get(int textID)
        {
            ArticleModel article = articleContext.GetArticleById(textID);
            if (article == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return article;
        }

        // POST api/<ArticleController>/create
        [HttpPost("create")]
        public void Post()
        {
            articleContext.InsertArticle(new ArticleModel()
            {
                Title = Request.Form["title"],
                Content = Request.Form["content"],
                References = Request.Form["references"],
                Tags = Request.Form["tags"].ToString().Split(',')
            });
        }

    }
}
