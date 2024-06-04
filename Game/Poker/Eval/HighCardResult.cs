using System.Collections.Generic;

namespace UNSERcasino.Game.Poker.Eval
{
    internal class HighCardResult : Result
    {
        private Card _card;
        private HighCardResult(Card c)
        {
            _card = c;
        }
        public override Card[] GetCards()
        {
            return new Card[] { _card };
        }

        protected override int GetRanking()
        {
            return 10 + (14 - _card.CardValue.Rating);
        }

        public static List<HighCardResult> GetHighCards(Card[] cards)
        {
            List<HighCardResult> res = new List<HighCardResult>();

            foreach (Card card in cards)
            {
                res.Add(new HighCardResult(card));
            }

            return res;
        }

        public override string ToString()
        {
            return "high card";
        }
    }
}
