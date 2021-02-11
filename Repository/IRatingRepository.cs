namespace HowWiki.Repository
{
    public interface IRatingRepository
    {
        void InsertRating(int textId, int stars, bool helpful);
    }
}
