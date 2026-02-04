using Flashcards.m1chael888.Models;
using Spectre.Console;
using static Flashcards.m1chael888.Enums.EnumExtension;
using static Flashcards.m1chael888.Enums.ManageViewEnums;
using static System.Net.Mime.MediaTypeNames;

namespace Flashcards.m1chael888.Views
{
    public interface IManageView
    {
        ManageMenuOption ShowMenu();
        string GetStackName(string msg, bool exists = false);
        ViewStacksOption DisplayStackList(List<StackModel> stacks); 
        StackModel DisplayStackPrompt(List<StackModel> stacks, string title);
        void DisplayCardList(List<CardDto> cards, string stackName);
        ViewCardsOption DisplayCardMenu();
        string GetCardFront(string operation, bool error = false);
        string GetCardBack(string operation, bool error = false);
        CardDto DisplayCardPrompt(List<CardDto> cards, string title);
    }
    public class ManageView : IManageView
    {
        public ManageMenuOption ShowMenu()
        {
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<ManageMenuOption>()
                    .Title("[lime]Manage Menu::[/]")
                    .AddChoices(Enum.GetValues<ManageMenuOption>())
                    .UseConverter(x => GetDescription(x))
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
            AnsiConsole.MarkupLine("[lime]Id\tName[/]");
            foreach (StackModel stack in stacks)
            {
                AnsiConsole.MarkupLine($"{stack.StackId}\t{stack.Name}");
            }

            var choice = AnsiConsole.Prompt(
                            new SelectionPrompt<ViewStacksOption>()
                            .Title("")
                            .AddChoices(Enum.GetValues<ViewStacksOption>())
                            .UseConverter(x => GetDescription(x))
                            .HighlightStyle("lime")
                            .WrapAround()
                            );
            return choice;
        }

        public StackModel DisplayStackPrompt(List<StackModel> stacks, string title)
        {
            var choice = AnsiConsole.Prompt(
                            new SelectionPrompt<StackModel>()
                            .Title($"[lime]{title}[/]")
                            .UseConverter(x => $"{x.StackId}\t{x.Name}")
                            .HighlightStyle("lime")
                            .WrapAround()
                            .AddChoices(stacks)
                            );
            return choice;
        }

        public ViewCardsOption DisplayCardMenu()
        {
            var choice = AnsiConsole.Prompt(
                            new SelectionPrompt<ViewCardsOption>()
                            .AddChoices(Enum.GetValues<ViewCardsOption>())
                            .UseConverter(x => GetDescription(x))
                            .HighlightStyle("lime")
                            .WrapAround()
                            );
            return choice;
        }

        public string GetCardFront(string operation, bool error = false)
        {
            Console.Clear();
            AnsiConsole.MarkupLine($"[lime]{operation} a card::[/]\n");
            return AnsiConsole.Ask<string>("[lime]Enter the front of the card (question)[/]");
        }

        public string GetCardBack(string operation, bool error = false)
        {
            Console.Clear();
            AnsiConsole.MarkupLine($"[lime]{operation} a card::[/]\n");
            return AnsiConsole.Ask<string>("[lime]Enter the back of the card (answer)[/]");
        }

        public void DisplayCardList(List<CardDto> cards, string stackName)
        {
            AnsiConsole.MarkupLine($"[lime]Cards in {stackName}::[/]\n");
            AnsiConsole.MarkupLine("[lime]Id[/]");
            foreach (CardDto card in cards)
            {
                string front = card.Front; string back = card.Back;
                AnsiConsole.MarkupLine($"{card.DisplayId}\t{CheckLength(front).PadRight(28)}\t{CheckLength(back).PadRight(28)}");
            }
            Console.WriteLine();
        }

        public CardDto DisplayCardPrompt(List<CardDto> cards, string title)
        {
            Console.Clear();
            return AnsiConsole.Prompt(
                new SelectionPrompt<CardDto>()
                .Title($"[lime]{title}[/]")
                .UseConverter(x => $"{x.DisplayId}\t{CheckLength(x.Front).PadRight(28)}\t{CheckLength(x.Back).PadRight(28)}")
                .AddChoices(cards)
                .HighlightStyle("lime")
                .WrapAround()
                );
        }

        string CheckLength(string myString)
        {
            if (myString.Length > 28) myString = myString.Substring(0, 25) + "...";
            return myString;
        }
    }
}
