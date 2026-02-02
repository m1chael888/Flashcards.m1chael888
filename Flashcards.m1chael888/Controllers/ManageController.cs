using static Flashcards.m1chael888.Views.ManageViewEnums;
using Flashcards.m1chael888.Views;
using Spectre.Console;
using Flashcards.m1chael888.Services;

namespace Flashcards.m1chael888.Controllers
{
    public class ManageController
    {
        private IManageView _manageView;
        private IManageService _manageService;
        public ManageController(IManageView manageView, IManageService manageService)
        {
            _manageView = manageView;
            _manageService = manageService;
        }
        public void HandleManageMenu()
        {
            Console.Clear();
            var choice = CallManageMenu();

            switch (choice)
            {
                case ManageMenuOption.CreateStack:
                    CallCreateStack();
                    break;
                case ManageMenuOption.ViewStacks:

                    HandleManageMenu();
                    break;
                case ManageMenuOption.UpdateStack:

                    HandleManageMenu();
                    break;
                case ManageMenuOption.DeleteStack:

                    HandleManageMenu();
                    break;
                case ManageMenuOption.Back:
                    break;
            }
        }

        private ManageMenuOption CallManageMenu()
        {
            var choice = _manageView.ShowMenu();
            return choice;
        }

        private void CallCreateStack()
        {
            string stackName = _manageView.GetNewStack();
            _manageService.CallStackCreate(stackName);

            ReturnWithMsg("Stack saved successfully");
        }

        private void ReturnWithMsg(string msg = "")
        {
            Console.Clear();    
            if (msg != "") AnsiConsole.MarkupLine($"[lime]{msg}[/]");

            AnsiConsole.Status()
                .Spinner(Spinner.Known.Point)
                .SpinnerStyle("white")
                .Start("Press any key to return to menu", x =>
                {
                    Console.ReadKey();
                });
            HandleManageMenu();
        }
    }
}
