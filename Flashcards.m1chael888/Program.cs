using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Flashcards.m1chael888.Infrastructure;

namespace Flashcards.m1chael888
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            var builder = new ConfigurationBuilder()
                .AddJsonFile("appSettings.json")
                .Build();
            var connectionString = builder.GetConnectionString("ConnectionString");

            var collection = new ServiceCollection();

            collection.AddScoped<IDbInitializer>(x => new DbInitializer(connectionString));

            var provider = collection.BuildServiceProvider();

            var initializer = provider.GetRequiredService<IDbInitializer>();
            initializer.Initialize();

            //start app
        }
    } 
}