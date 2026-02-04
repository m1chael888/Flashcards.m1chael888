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
        void CardCreate(CardModel card);
        List<CardDto> CardsRead(StackModel stack);
        void CardUpdate(CardModel card);
        void CardDelete(int cardId);

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

        public void CardCreate(CardModel card)
        {
            _cardRepository.Create(card);
        }

        public List<CardDto> CardsRead(StackModel stack)
        {
            List<CardModel> cardModels = _cardRepository.Read(stack.StackId);
            var cardDtos = new List<CardDto>();

            for (int i = 0; i < cardModels.Count(); i++)
            {
                var cardDto = new CardDto();
                cardDto.DisplayId = i + 1;
                cardDto.CardId = cardModels[i].CardId;
                cardDto.Front = cardModels[i].Front;
                cardDto.Back = cardModels[i].Back;
                cardDtos.Add(cardDto);
            }
            return cardDtos;
        }

        public void CardUpdate(CardModel card)
        {
            _cardRepository.Update(card);
        }

        public void CardDelete(int cardId)
        {
            _cardRepository.Delete(cardId);
        }
    }
}
