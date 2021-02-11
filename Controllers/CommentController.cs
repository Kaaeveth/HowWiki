﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HowWiki.Models;
using HowWiki.Repository;

namespace HowWiki.Controllers
{
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        ICommentRepository commentRepository;

        public CommentController(ICommentRepository commentRepository)
        {
            this.commentRepository = commentRepository;
        }

        // GET api/<CommentController>/:id
        [HttpGet("{id}")]
        public IEnumerable<CommentModel> Get(int id)
        {
            var res =  commentRepository.GetCommentsByTextId(id);
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
            commentRepository.CreateCommentForTextId(id, comment, 76);
        }
    }
}
