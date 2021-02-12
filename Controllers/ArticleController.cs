﻿using System.Collections.Generic;
using HowWiki.Models;
using HowWiki.Repository;
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
            ArticleModel article = articleRepository.GetArticleById(textID);
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
