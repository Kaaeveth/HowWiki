using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HowWiki.Models;
using Microsoft.AspNetCore.Mvc;


namespace HowWiki.Controllers
{
    [Route("api/[controller]")]
    public class ArticleController : ControllerBase
    {
        // GET: api/<ArticleController>/list
        [HttpGet("list")]
        public IEnumerable<ArticleModel> Get()
        {
            //Testdaten
            return new ArticleModel[]{
                new ArticleModel
                {
                    Author = "Test",
                    Content = "Test",
                    AvgStars = 3f,
                    Id = 42,
                    References = "lel",
                    Title = "Hello",
                    UsersRatedCount = 3,
                    Tags = new string[] {"eins", "zwei", "Polizei"}
                }
            };
        }

        // api/<ArticleController>/show?textID=
        [HttpGet("show")]
        public string Get(int textID)
        {
            return "test";
        }

    }
}
