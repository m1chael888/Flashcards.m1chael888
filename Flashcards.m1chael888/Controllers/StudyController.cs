using Flashcards.m1chael888.Views;
using Spectre.Console;
using static Flashcards.m1chael888.Views.StudyViewEnums;

namespace Flashcards.m1chael888.Controllers
{
    public class StudyController
    {
        private IStudyView _studyView;
        public StudyController(IStudyView studyView)
        {
            _studyView = studyView;
        }
        public void HandleStudyMenu()
        {
            Console.Clear();
            var choice = CallStudyMenu();

            switch (choice)
            {
                case StudyMenuOption.ChooseStack:

                    HandleStudyMenu();
                    break;
                case StudyMenuOption.Back:
                    break;
            }
        }

        private StudyMenuOption CallStudyMenu()
        {
            var choice = _studyView.ShowMenu();
            return choice;
        }
    }
}
