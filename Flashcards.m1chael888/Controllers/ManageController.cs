using static Flashcards.m1chael888.Enums.ManageViewEnums;
using Flashcards.m1chael888.Views;
using Spectre.Console;
using Flashcards.m1chael888.Services;
using Flashcards.m1chael888.Models;

namespace Flashcards.m1chael888.Controllers
{
    public class ManageController
    {
        private IManageView _manageView;
        private IStackService _stackService;
        private ICardService _cardService;
        public ManageController(IManageView manageView, IStackService stackService, ICardService cardService)
        {
            _manageView = manageView;
            _stackService = stackService;
            _cardService = cardService;
        }
        public void HandleManageMenu()
        {
            Console.Clear();
            var choice = CallManageMenu();

            switch (choice)
            {
                case ManageMenuOption.CreateStack:
                    CallStackCreate();
                    break;
                case ManageMenuOption.ViewStacks:
                    CallStacksRead();
                    break;
                case ManageMenuOption.UpdateStack:
                    CallStackUpdate();
                    break;
                case ManageMenuOption.DeleteStack:
                    CallStackDelete();
                    break;
                case ManageMenuOption.Back:
                    break;
            }
        }

        private ManageMenuOption CallManageMenu()
        {
            Console.Clear();
            var choice = _manageView.ShowMenu();
            return choice;
        }

        private void CallStackCreate()
        {
            string stackName = CallGetStackName("Create stack::");

            _stackService.StackCreate(stackName);
            ReturnToManageMenu("Stack saved successfully");
        }

        private void CallStacksRead()
        {
            Console.Clear();
            var stacks = GetStackList();
            var choice = _manageView.DisplayStackList(stacks);
            
            switch (choice)
            {
                case ViewStacksOption.ViewCards:
                    CallCardsRead();
                    break;
                case ViewStacksOption.Back:
                    HandleManageMenu();
                    break;
            }
        }

        private void CallStackUpdate()
        {
            var stacks = GetStackList();
            var choice = _manageView.DisplayStackPrompt(stacks, "Choose a stack of cards to update::");
            choice.Name = CallGetStackName("Update stack::");

            _stackService.StackUpdate(choice);

            ReturnToManageMenu("Stack updated successfully");
        }

        private void CallStackDelete()
        {
            var stacks = GetStackList();
            var choice = _manageView.DisplayStackPrompt(stacks, "Choose a stack of cards to delete::");
            _stackService.StackDelete(choice);

            ReturnToManageMenu("Stack deleted successfully");
        }

        private void CallShowCards(List<CardDto> cards, StackModel choice)
        {
            Console.Clear();
            if (cards.Count() > 0)
            {
                _manageView.DisplayCardList(cards, choice.Name);
            }
            else
            {
                AnsiConsole.MarkupLine("[lime]This stack is empty!! Create some cards[/]\n");
            }
            HandleCardsReadMenu(choice);
        }

        private void HandleCardsReadMenu(StackModel choice)
        {
            switch (_manageView.DisplayCardMenu())
            {
                case ViewCardsOption.CreateCard:
                    CallCardCreate(choice);
                    break;
                case ViewCardsOption.UpdateCard:
                    CallCardUpdate(choice);
                    break;
                case ViewCardsOption.DeleteCard:
                    CallCardDelete(choice);
                    break;
                case ViewCardsOption.Back:
                    CallStacksRead();
                    break;
            }
        }

        void CallCardCreate(StackModel choice)
        {
            var card = new CardModel();
            var front = _manageView.GetCardFront("Creating");
            var back = _manageView.GetCardBack("Creating");
            card.Front = front;
            card.Back = back;
            card.StackId = choice.StackId;

            _cardService.CardCreate(card);
            ReturnToCardList(choice, "Card created successfully =)");
        }

        private void CallCardsRead()
        {
            Console.Clear();
            var stacks = GetStackList();
            var choice = _manageView.DisplayStackPrompt(stacks, "Choose which stack's cards youd like to view::");
            var cards = _cardService.CardsRead(choice);

            CallShowCards(cards, choice);
        }

        void CallCardUpdate(StackModel choice)
        {
            var cards = GetCardList(choice);
            if (cards.Count() == 0)
            {
                CallShowCards(cards, choice);
            }
            else
            {
                var card = _manageView.DisplayCardPrompt(cards, "Choose a card to update::");
                var front = _manageView.GetCardFront("Updating");
                var back = _manageView.GetCardBack("Updating");
                var updatedCard = new CardModel();
                updatedCard.CardId = card.CardId;
                updatedCard.Front = front;
                updatedCard.Back = back;
                updatedCard.StackId = choice.StackId;

                _cardService.CardUpdate(updatedCard);
                ReturnToCardList(choice, "Card updated successfully =)");
            }
        }

        void CallCardDelete(StackModel choice)
        {
            var cards = GetCardList(choice);
            if (cards.Count() == 0)
            {
                CallShowCards(cards, choice);
            }
            else
            {
                var card = _manageView.DisplayCardPrompt(cards, "Choose a card to delete::");
                _cardService.CardDelete(card.CardId);
                ReturnToCardList(choice, "Card deleted successfully =)");
            }
        }

        private List<StackModel> GetStackList()
        {
            var stacks = _stackService.StacksRead();
            return stacks;
        }
         
        private List<CardDto> GetCardList(StackModel choice)
        {
            var cards = _cardService.CardsRead(choice);
            return cards;
        }

        private string CallGetStackName(string msg)
        {
            var stacks = GetStackList();
            string stackName = _manageView.GetStackName(msg);

            while (stacks.Where(x => x.Name == stackName).Count() > 0)
            {
                stackName = _manageView.GetStackName(msg, exists: true);
            }
            return stackName;
        }

        private void ReturnToManageMenu(string msg)
        {
            Console.Clear();    
            AnsiConsole.MarkupLine($"[lime]{msg}[/]");

            AnsiConsole.Status()
                .Spinner(Spinner.Known.Point)
                .SpinnerStyle("white")
                .Start("Press any key to return to menu", x =>
                {
                    Console.ReadKey();
                });
            HandleManageMenu();
        }

        private void ReturnToCardList(StackModel choice, string msg)
        {
            Console.Clear();
            AnsiConsole.MarkupLine($"[lime]{msg}[/]");

            AnsiConsole.Status()
                .Spinner(Spinner.Known.Point)
                .SpinnerStyle("white")
                .Start("Press any key to return", x =>
                {
                    Console.ReadKey();
                });
            CallShowCards(GetCardList(choice), choice);
        }
    }
}
