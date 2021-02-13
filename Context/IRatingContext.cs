/*
 * IRatingContext.cs
 * Schnittstelle fuer ein RatingContext, welches Bewertungen aktualisiert / einfuegt
 * Bewertungen selber werden nicht abgefragt, sondern aggregiert mit jedem Artikel.
 * 
 * Autor: Dominik Strutz
 */

namespace HowWiki.Context
{
    public interface IRatingContext
    {
        void InsertRating(int textId, int stars, bool helpful);
    }
}
