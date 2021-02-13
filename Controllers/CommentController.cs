using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using HowWiki.Models;
using HowWiki.Context;

/*
 * CommentController.cs
 * Controller fuer Kommentare eines Artikels
 * Autor: Dominik Strutz
 */

namespace HowWiki.Controllers
{
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        ICommentContext commentContext;

        public CommentController(ICommentContext commentRepository)
        {
            this.commentContext = commentRepository;
        }

        // GET api/<CommentController>/:id
        [HttpGet("{id}")]
        public IEnumerable<CommentModel> Get(int id)
        {
            var res =  commentContext.GetCommentsByTextId(id);
            if(res == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return res;
        }

        // POST api/<CommentController>/:id/:comment
        [HttpPost("{id}/{comment}")]
        public void Put(int id, string comment)
        {
            commentContext.CreateCommentForTextId(id, comment, 76);
        }
    }
}
