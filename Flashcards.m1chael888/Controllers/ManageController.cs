using static Flashcards.m1chael888.Views.ManageViewEnums;
using Flashcards.m1chael888.Views;
using Spectre.Console;
using Flashcards.m1chael888.Services;
using Flashcards.m1chael888.Models;

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
                    CallStackCreate();
                    break;
                case ManageMenuOption.ViewStacks:
                    CallStacksRead();
                    break;
                case ManageMenuOption.UpdateStack:
                    CallStackUpdate();
                    break;
                case ManageMenuOption.DeleteStack:
                    CallStackDelete();
                    break;
                case ManageMenuOption.Back:
                    break;
            }
        }

        private ManageMenuOption CallManageMenu()
        {
            Console.Clear();
            var choice = _manageView.ShowMenu();
            return choice;
        }

        private void CallStackCreate()
        {
            string stackName = CallGetStackName("Create stack::");

            _manageService.StackCreate(stackName);
            ReturnToMenuWithMsg("Stack saved successfully");
        }

        private void CallStacksRead()
        {
            var stacks = GetStackList();
            var choice = _manageView.DisplayStackList(stacks);
            
            switch (choice)
            {
                case ViewStacksOption.ViewCards:
                    CallCardsRead();
                    break;
                case ViewStacksOption.Back:
                    HandleManageMenu();
                    break;
            }
        }

        private void CallCardsRead()
        {
            Console.Clear();
            var stacks = GetStackList();
            _manageView.DisplayStackPrompt(stacks, "Choose which stack's cards youd like to view::");
            // show cards
        }

        private void CallStackUpdate()
        {
            var stacks = GetStackList();
            var choice = _manageView.DisplayStackPrompt(stacks, "Choose a stack of cards to update::");
            choice.Name = CallGetStackName("Update stack::");

            _manageService.StackUpdate(choice);

            ReturnToMenuWithMsg("Stack updated successfully");
        }

        private void CallStackDelete()
        {
            var stacks = GetStackList();
            var choice = _manageView.DisplayStackPrompt(stacks, "Choose a stack of cards to delete::");
            _manageService.StackDelete(choice);

            ReturnToMenuWithMsg("Stack deleted successfully");
        }

        private List<StackModel> GetStackList()
        {
            var stacks = _manageService.StacksRead();
            return stacks;
        }
         
        private string CallGetStackName(string msg)
        {
            var stacks = GetStackList();
            string stackName = _manageView.GetStackName(msg);

            while (stacks.Where(x => x.Name == stackName).Count() > 0)
            {
                stackName = _manageView.GetStackName(msg, exists: true);
            }
            return stackName;
        }

        private void ReturnToMenuWithMsg(string msg)
        {
            Console.Clear();    
            AnsiConsole.MarkupLine($"[lime]{msg}[/]");

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
