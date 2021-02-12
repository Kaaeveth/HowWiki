using System.Collections.Generic;
using HowWiki.Models;

/*
 * ICommentRepository.cs
 * Schnittstelle fuer ein CommentRepository
 * 
 * Autor: Dominik Strutz
 */

namespace HowWiki.Repository
{
    public interface ICommentRepository
    {
        IEnumerable<CommentModel> GetCommentsByTextId(int textId);
        void CreateCommentForTextId(int textId, string comment, int userid);
    }
}
