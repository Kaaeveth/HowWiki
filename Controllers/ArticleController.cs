using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HowWiki.Models;
using HowWiki.Repository;
using Microsoft.AspNetCore.Mvc;


namespace HowWiki.Controllers
{
    [Route("api/[controller]")]
    public class ArticleController : ControllerBase
    {
        IArticleRepository articleRepository;

        public ArticleController(IArticleRepository articleRepository)
        {
            this.articleRepository = articleRepository;
        }

        // GET: api/<ArticleController>/list
        [HttpGet("list")]
        public IEnumerable<ArticleModel> Get()
        {
            return articleRepository.GetArticles();
        }

        // GET api/<ArticleController>/show/{textId}
        [HttpGet("show/{textId}")]
        public ArticleModel Get(int textID)
        {
            return articleRepository.GetArticleById(textID);
        }

        // POST api/<ArticleController>/create
        [HttpPost("create")]
        public void Post()
        {
            articleRepository.InsertArticle(new ArticleModel()
            {
                Title = Request.Form["title"],
                Content = Request.Form["content"],
                References = Request.Form["references"],
                Tags = Request.Form["tags"].ToString().Split(',')
            });
        }

    }
}
