using Dapper;
using Flashcards.m1chael888.Models;
using Microsoft.Data.SqlClient;

namespace Flashcards.m1chael888.Repositories
{
    public interface ICardRepository
    {
        void Create();
        List<CardModel> Read(int stackId);
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

        public List<CardModel> Read(int stackId)
        {
            var cards = new List<CardModel>();
            var sql = @"SELECT * FROM Cards WHERE StackId = @StackId";

            using (var connection = new SqlConnection(_connectionString))
            {
                cards = connection.Query<CardModel>(sql, new
                {
                    StackId = stackId
                }).ToList();
            }
            return cards;
        }

        public void Update()
        {

        }

        public void Delete()
        {

        }
    }
}
