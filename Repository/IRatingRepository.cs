/*
 * IRatingRepository.cs
 * Schnittstelle fuer ein RatingRepository, welches Bewertungen aktualisiert / einfuegt
 * Bewertungen selber werden nicht abgefragt, sondern aggregiert mit jedem Artikel.
 * 
 * Autor: Dominik Strutz
 */

namespace HowWiki.Repository
{
    public interface IRatingRepository
    {
        void InsertRating(int textId, int stars, bool helpful);
    }
}
