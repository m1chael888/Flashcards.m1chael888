using Flashcards.m1chael888.Views;
using Spectre.Console;
using static Flashcards.m1chael888.Views.MainMenuViewEnums;

namespace Flashcards.m1chael888.Controllers
{
    public interface IFlashcardsController
    {
        void HandleMainMenuOption();
    }
    public class FlashcardsController : IFlashcardsController
    {
        private IMainMenuView _mainMenuView;
        public FlashcardsController(IMainMenuView mainMenuView)
        {
            _mainMenuView = mainMenuView;
        }

        public void HandleMainMenuOption()
        {
            Console.Clear();
            var choice = CallMainMenu();

            switch (choice)
            {
                case MainMenuOption.Study:
                    Console.WriteLine("under construction");
                    ReturnToMenu();
                    break;
                case MainMenuOption.Manage:
                    Console.WriteLine("under construction");
                    ReturnToMenu();
                    break;
                case MainMenuOption.Exit:
                    Environment.Exit(0);
                    break;
            }
        }

        private MainMenuOption CallMainMenu()
        {
            var choice = _mainMenuView.Call();
            return choice;
        }

        private void ReturnToMenu()
        {
            AnsiConsole.Status()
                .Spinner(Spinner.Known.Point)
                .SpinnerStyle("white")
                .Start("Press any key to return to menu", x =>
                {
                    Console.ReadKey();
                });
            HandleMainMenuOption();
        }
    }
}
