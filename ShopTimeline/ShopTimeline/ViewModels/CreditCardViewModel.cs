using ShopTimeline.Models;
using ShopTimeline.Services;
using System.Collections.ObjectModel;

namespace ShopTimeline.ViewModels
{
    public class CreditCardViewModel : BaseViewModel
    {
        public CreditCardViewModel()
        {

            FakeService fk = new FakeService();

            Cards = new ObservableCollection<Card>(fk.GetAllCards()); 

        }

        private ObservableCollection<Card> _cards;
        public ObservableCollection<Card> Cards
        {
            get { return _cards; }
            set { SetProperty(ref _cards, value); }
        }
    }
}
