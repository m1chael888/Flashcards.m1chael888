using System.ComponentModel;

namespace Flashcards.m1chael888.Enums
{
    public static class ManageViewEnums
    {
        public enum ManageMenuOption
        {
            [Description("Create a stack")]
            CreateStack,
            [Description("View stacks")]
            ViewStacks,
            [Description("Update a stack")]
            UpdateStack,
            [Description("Delete a stack")]
            DeleteStack,
            [Description("Back")]
            Back
        }

        public enum ViewStacksOption
        {
            [Description("View cards in a stack")]
            ViewCards,
            [Description("Back")]
            Back
        }
    }
}
