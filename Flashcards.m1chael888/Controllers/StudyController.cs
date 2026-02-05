using static Flashcards.m1chael888.Enums.StudyViewEnums;
using Flashcards.m1chael888.Services;
using Flashcards.m1chael888.Models;
using Flashcards.m1chael888.Views;
using Spectre.Console;

namespace Flashcards.m1chael888.Controllers
{
    public class StudyController
    {
        private IStudyView _studyView;
        private IStackService _stackService;
        private ICardService _cardService;
        public StudyController(IStudyView studyView, IStackService stackService, ICardService cardService)
        {
            _studyView = studyView;
            _stackService = stackService;
            _cardService = cardService;
        }
        public void HandleStudyMenu()
        {
            Console.Clear();
            var choice = CallStudyMenu();

            switch (choice)
            {
                case StudyMenuOption.ChooseStack:
                    CallChooseStack();
                    break;
                case StudyMenuOption.StudyHistory:

                    break;
                case StudyMenuOption.Back:
                    break;
            }
        }

        private void CallChooseStack()
        {
            
        }

        private List<StackModel> GetStackList()
        {
            var stacks = _stackService.StacksRead();
            return stacks;
        }

        private StudyMenuOption CallStudyMenu()
        {
            var choice = _studyView.ShowMenu();
            return choice;
        }
    }
}
