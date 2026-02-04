using System.ComponentModel;

namespace Flashcards.m1chael888.Enums
{
    public class StudyViewEnums
    {
        public enum StudyMenuOption
        {
            [Description("Choose a stack to study from")]
            ChooseStack,
            [Description("View study history")]
            StudyHistory,
            [Description("Back")]
            Back
        }
    }
}
