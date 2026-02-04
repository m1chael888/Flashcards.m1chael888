using Flashcards.m1chael888.Controllers;
using Flashcards.m1chael888.Infrastructure;
using Flashcards.m1chael888.Repositories;
using Flashcards.m1chael888.Services;
using Flashcards.m1chael888.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using static Flashcards.m1chael888.Enums.MainMenuViewEnums;

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
            collection.AddScoped<StudyController>();
            collection.AddScoped<ManageController>();

            collection.AddScoped<IMainMenuView, MainMenuView>();
            collection.AddScoped<IStudyView, StudyView>();
            collection.AddScoped<IManageView, ManageView>();

            collection.AddScoped<IStudyService, StudyService>();
            collection.AddScoped<IStackService, StackService>();
            collection.AddScoped<ICardService, CardService>();

            collection.AddScoped<IStackRepository>(x => new StackRepository(connectionString));
            collection.AddScoped<ICardRepository>(x => new CardRepository(connectionString));

            var provider = collection.BuildServiceProvider();

            var initializer = provider.GetRequiredService<IDbInitializer>();
            initializer.Initialize();

            var mainMenu = provider.GetRequiredService<IMainMenuView>();
            var studyController = provider.GetRequiredService<StudyController>();
            var manageController = provider.GetRequiredService<ManageController>();

            while (true)
            {
                Console.Clear();
                var choice = mainMenu.ShowMenu();

                switch (choice)
                {
                    case MainMenuOption.Study:
                        studyController.HandleStudyMenu();
                        break;
                    case MainMenuOption.Manage:
                        manageController.HandleManageMenu();
                        break;
                    case MainMenuOption.Exit:
                        Environment.Exit(0);
                        break;
                }
            }
        }
    } 
}