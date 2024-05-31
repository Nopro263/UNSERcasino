using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNSERcasino.Game.Poker.Eval
{
    internal class RoyalFlushResult : Result
    {
        private Card[] _cards;

        private RoyalFlushResult(StraightFlushResult sfr)
        {
            _cards = new Card[5];
            sfr.GetCards().CopyTo(_cards, 0);
        }

        public static List<RoyalFlushResult> GetRoyalFlush(List<Result> cards)
        {
            List<RoyalFlushResult> rfr = new List<RoyalFlushResult>();
            foreach(Result r in cards)
            {
                if(r is StraightFlushResult sfr)
                {
                    rfr.Add(new RoyalFlushResult(sfr));
                }
            }

            return rfr;
        }
        public override Card[] GetCards()
        {
            return _cards;
        }

        protected override int GetRanking()
        {
            return 1;
        }
    }
}
