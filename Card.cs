using UNSERcasino.UI;

namespace UNSERcasino
{
    internal class Card
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
    }
}
