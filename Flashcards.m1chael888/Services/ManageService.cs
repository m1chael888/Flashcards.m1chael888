using Flashcards.m1chael888.Repositories;
using Flashcards.m1chael888.Models;

namespace Flashcards.m1chael888.Services
{
    public interface IManageService
    {
        void StackCreate(string stackName);
        List<StackModel> StacksRead();
        void StackUpdate(StackModel stack);
        void StackDelete(StackModel stack);
        void CardCreate();
        List<CardDto> CardsRead(StackModel stack);
        void CardUpdate();
        void CardDelete();

    }
    public class ManageService : IManageService
    {
        private IStackRepository _stackRepository;
        private ICardRepository _cardRepository;
        public ManageService(IStackRepository stackRepository, ICardRepository cardRepository)
        {
            _stackRepository = stackRepository;
            _cardRepository = cardRepository;
        }

        public void StackCreate(string stackName)
        {
            _stackRepository.Create(stackName);
        }

        public List<StackModel> StacksRead()
        {
            return _stackRepository.Read();
        }

        public void StackUpdate(StackModel stack)
        {
            _stackRepository.Update(stack);
        }

        public void StackDelete(StackModel stack)
        {
            _stackRepository.Delete(stack.StackId);
        }

        public void CardCreate()
        {
            _cardRepository.Create();
        }

        public List<CardDto> CardsRead(StackModel stack)
        {
            List<CardModel> cardModels = _cardRepository.Read(stack.StackId);
            var cardDtos = new List<CardDto>();

            foreach (var card in cardModels)
            {
                var cardDto = new CardDto();
                cardDto.CardId = card.CardId;
                cardDto.Front = card.Front;
                cardDto.Back = card.Back;
            }
            return cardDtos;
        }

        public void CardUpdate()
        {
            _cardRepository.Update();
        }

        public void CardDelete()
        {
            _cardRepository.Delete();
        }

    }
}
