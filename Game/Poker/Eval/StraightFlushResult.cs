using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNSERcasino.Game.Poker.Eval
{
    internal class StraightFlushResult : Result
    {
        private Card[] _cards;
        private StraightFlushResult(StraightResult sr)
        {
            _cards = new Card[5];
            sr.GetCards().CopyTo(_cards, 0);
        }
        public override Card[] GetCards()
        {
            return _cards;
        }

        public static List<StraightFlushResult> GetStraightFlush(List<Result> cards)
        {
            List<StraightFlushResult> sfr = new List<StraightFlushResult>();

            foreach (Result r in cards)
            {
                if(r is StraightResult s)
                {
                    foreach(Result r2 in cards)
                    {
                        if(r2 is FlushResult f)
                        {
                            if (s.EqualsIgnoreType(f))
                            {
                                sfr.Add(new StraightFlushResult(s));
                            }
                        }
                        
                    }
                }
            }

            return sfr;
        }
        protected override int GetRanking()
        {
            return 2;
        }

        public override string ToString()
        {
            return "straight flush";
        }
    }
}
