namespace Flashcards.m1chael888.Repositories
{
    public interface ICardRepository
    {

    }
    public class CardRepository : ICardRepository
    {
        private readonly string _connectionString;
        public CardRepository(string connectionString)
        {
            _connectionString = connectionString;
        }


    }
}
