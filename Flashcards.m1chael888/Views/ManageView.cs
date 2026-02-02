using Spectre.Console;
using static Flashcards.m1chael888.Views.ManageViewEnums;
using Flashcards.m1chael888.Models;

namespace Flashcards.m1chael888.Views
{
    public interface IManageView
    {
        ManageMenuOption ShowMenu();
        string GetStackName(string msg, bool exists = false);
        ViewStacksOption DisplayStackList(List<StackModel> stacks); 
        StackModel DisplayStackPrompt(List<StackModel> stacks, string title);
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

        public string GetStackName(string msg, bool exists = false)
        {
            Console.Clear();
            AnsiConsole.MarkupLine($"[lime]{msg}[/]\n");
            if (exists) AnsiConsole.MarkupLine("[red]Enter a unique stack name!![/]");
            var input = AnsiConsole.Ask<string>("[lime]What would you like to call your stack??[/]");
            return input;
        }

        public ViewStacksOption DisplayStackList(List<StackModel> stacks)
        {
            AnsiConsole.MarkupLine("[lime]View Stacks::[/]\n");
            foreach (StackModel stack in stacks)
            {
                AnsiConsole.MarkupLine($"{stack.StackId}\t{stack.Name}");
            }

            var choice = AnsiConsole.Prompt(
                            new SelectionPrompt<ViewStacksOption>()
                            .Title("")
                            .AddChoices(Enum.GetValues<ViewStacksOption>())
                            .HighlightStyle("lime")
                            .WrapAround()
                            );
            return choice;
        }

        public StackModel DisplayStackPrompt(List<StackModel> stacks, string title)
        {
            var choice = AnsiConsole.Prompt(
                            new SelectionPrompt<StackModel>().Title($"[lime]{title}[/]")
                            .UseConverter(x => $"{x.StackId}\t{x.Name}")
                            .HighlightStyle("lime")
                            .WrapAround()
                            .AddChoices(stacks)
                            );
            return choice;
        }
    }
}
