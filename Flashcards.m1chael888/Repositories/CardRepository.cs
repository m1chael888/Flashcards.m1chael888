namespace Flashcards.m1chael888.Repositories
{
    public interface ICardRepository
    {
        void Create();
        void Read();
        void Update();
        void Delete();
    }
    public class CardRepository : ICardRepository
    {
        private readonly string _connectionString;
        public CardRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Create()
        {

        }

        public void Read()
        {

        }

        public void Update()
        {

        }

        public void Delete()
        {

        }
    }
}
