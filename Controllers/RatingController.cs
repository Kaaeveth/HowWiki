﻿using HowWiki.Repository;
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
        IRatingRepository ratingRepository;

        public RatingController(IRatingRepository ratingRepository)
        {
            this.ratingRepository = ratingRepository;
        }

        //PUT api/<ArticleController>/rate/{textId}/{stars}/{helpful}
        [HttpPut("rate/{textId}/{stars}/{helpful}")]
        public void Put(int textId, int stars, bool helpful)
        {
            ratingRepository.InsertRating(textId, stars, helpful);
        }
    }
}
