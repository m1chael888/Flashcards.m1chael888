using Spectre.Console;
using static Flashcards.m1chael888.Enums.StudyViewEnums;
using static Flashcards.m1chael888.Enums.EnumExtension;

namespace Flashcards.m1chael888.Views
{
    public interface IStudyView
    {
        StudyMenuOption ShowMenu();
    }
    public class StudyView : IStudyView
    {
        public StudyMenuOption ShowMenu()
        {
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<StudyMenuOption>()
                    .Title("[lime]Study Menu::[/]")
                    .AddChoices(Enum.GetValues<StudyMenuOption>())
                    .UseConverter(x => GetDescription(x))
                    .HighlightStyle("lime")
                    .WrapAround()
                    );
            return choice;
        }
    }
}
