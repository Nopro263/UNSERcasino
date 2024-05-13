namespace UNSERcasino.Game.Poker.Eval
{
    internal class Evaluator
    {
        public static void Eval(Card[] dealer, Card[] hand)
        {
            foreach(Result res in InternalEval(dealer, hand))
            {
                foreach(Card card in res.GetCards()) 
                {
                    //Console.Write(card.CardValue);
                }

                //Console.WriteLine();
            }
        }

        private static List<Result> InternalEval(Card[] dealer, Card[] hand)
        {
            Card[] cards = new Card[dealer.Length + hand.Length];
            dealer.CopyTo(cards, 0);
            hand.CopyTo(cards, dealer.Length);


            List<Result> result = new List<Result>();

            result.AddRange(PairResult.GetPairs(cards));
            result.AddRange(TripletResult.GetTriplets(cards));

            return result;
        }
    }
}
