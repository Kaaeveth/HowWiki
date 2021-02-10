using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HowWiki.Models
{
    public class CommentModel
    {
        public string Author { get; set; }
        public string Comment { get; set; }
        public DateTime Created { get; set; }
    }
}
