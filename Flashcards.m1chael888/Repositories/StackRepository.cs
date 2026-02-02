using Microsoft.Data.SqlClient;
using Dapper;

namespace Flashcards.m1chael888.Repositories
{
    public interface IStackRepository
    {
        void Create(string stackName);
    }
    public class StackRepository : IStackRepository
    {
        private readonly string _connectionString;
        public StackRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Create(string stackName)
        {
            var sql = @"INSERT INTO Stacks (Name) VALUES (@Name)";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(sql, new
                {
                    Name = stackName
                });
            }
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
