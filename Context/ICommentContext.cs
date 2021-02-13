using System.Collections.Generic;
using HowWiki.Models;

/*
 * ICommentContext.cs
 * Schnittstelle fuer ein CommentContext
 * 
 * Autor: Dominik Strutz
 */

namespace HowWiki.Context
{
    public interface ICommentContext
    {
        IEnumerable<CommentModel> GetCommentsByTextId(int textId);
        void CreateCommentForTextId(int textId, string comment, int userid);
    }
}
