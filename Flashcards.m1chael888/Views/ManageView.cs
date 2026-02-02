using Spectre.Console;
using static Flashcards.m1chael888.Views.ManageViewEnums;

namespace Flashcards.m1chael888.Views
{
    public interface IManageView
    {
        ManageMenuOption ShowMenu();
        string GetNewStack();
    }
    public class ManageView : IManageView
    {
        public ManageMenuOption ShowMenu()
        {
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<ManageMenuOption>()
                    .Title("[lime]Manage Menu::[/]")
                    .AddChoices(Enum.GetValues<ManageMenuOption>())
                    .HighlightStyle("lime")
                    .WrapAround()
                    );
            return choice;
        }

        public string GetNewStack()
        {
            AnsiConsole.MarkupLine("[lime]Creating a stack::[/]\n");
            var input = AnsiConsole.Ask<string>("[lime]What would you like to call your new stack??[/]");
            return input;
        }
    }
}
