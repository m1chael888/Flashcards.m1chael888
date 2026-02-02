using Flashcards.m1chael888.Repositories;

namespace Flashcards.m1chael888.Services
{
    public interface IManageService
    {
        void CallStackCreate(string stackName);
    }
    public class ManageService : IManageService
    {
        private IStackRepository _stackRepository;
        public ManageService(IStackRepository stackRepository)
        {
            _stackRepository = stackRepository;
        }

        public void CallStackCreate(string stackName)
        {
            _stackRepository.Create(stackName);
        }

        public void CallStackRead()
        {

        }

        public void CallStackUpdate()
        {

        }

        public void CallStackDelete()
        {

        }
    }
}
