using System.Collections.Generic;
using HowWiki.Models;

namespace HowWiki.Repository
{
    public interface ICommentRepository
    {
        IEnumerable<CommentModel> GetCommentsByTextId(int textId);
        void CreateCommentForTextId(int textId, string comment, int userid);
    }
}
