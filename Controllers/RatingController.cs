using HowWiki.Context;
using Microsoft.AspNetCore.Mvc;

/*
 * RatingController.cs
 * Controller fuer die Bewertungen eines Artikels.
 * Autor: Dominik Strutz
 */

namespace HowWiki.Controllers
{
    [Route("api/[controller]")]
    public class RatingController : ControllerBase
    {
        IRatingContext ratingContext;

        public RatingController(IRatingContext ratingRepository)
        {
            this.ratingContext = ratingRepository;
        }

        //PUT api/<ArticleController>/rate/{textId}/{stars}/{helpful}
        [HttpPut("rate/{textId}/{stars}/{helpful}")]
        public void Put(int textId, int stars, bool helpful)
        {
            ratingContext.InsertRating(textId, stars, helpful);
        }
    }
}
