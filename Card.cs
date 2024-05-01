using UNSERcasino.UI;

namespace UNSERcasino
{
    internal class Card : IComparable<Card>
    {
        public CardValue CardValue { get; set; }
        public CardType CardType { get; set; }
        public bool Hidden { get; set; }

        public Card(CardValue cardValue, CardType cardType, bool hidden)
        {
            CardValue = cardValue;
            CardType = cardType;
            Hidden = hidden;
        }

        public static Card[] GetCards()
        {
            Card[] cards = new Card[52];
            CardType[] type = GetTypes();
            CardValue[] values = CardValue.GetValues();

            int i = 0;
            foreach (CardType cardType in type)
            {
                foreach(CardValue cardValue in values)
                {
                    cards[i++] = new Card(cardValue, cardType, false);
                }
            }

            return cards;
        }

        private static CardType[] GetTypes()
        {
            return new CardType[] {CardType.Pik, CardType.Kreuz, CardType.Herz, CardType.Karo};
        }

        public int CompareTo(Card? other)
        {
            return CardValue.Rating - other.CardValue.Rating;
        }
    }
}
