
using NoahCardOutput;

namespace UNSERcasino.Menu
{
    internal class Poker : BaseMenu
    {
        public override string[] update(TimeSpan s, string? i)
        {
            string[] result = new string[0];
            string[] community_cards = printCards(new PrintableCard[] { new PrintableCard(PrintableCardType.Kreuz, PrintableCardValue.KOENIG, true), 
                                                                        new PrintableCard(PrintableCardType.Kreuz, PrintableCardValue.KOENIG, true),
                                                                        new PrintableCard(PrintableCardType.Kreuz, PrintableCardValue.KOENIG, true),
                                                                        new PrintableCard(PrintableCardType.Kreuz, PrintableCardValue.KOENIG, true),
                                                                        new PrintableCard(PrintableCardType.Kreuz, PrintableCardValue.KOENIG, true)});

            string[] my_cards = printCards(new PrintableCard[] { PrintableCard.FromKarte("2 Pik"), PrintableCard.FromKarte("3 Kreuz")});

            string[] buffer = new string[] {"", "", ""};

            result = result.Concat(community_cards).Concat(buffer).Concat(my_cards).ToArray();

            MenuManager.Instance.requestInput();
            return result;
        }

        private string[] printCards(PrintableCard[] cards)
        {
            string[] res = new string[13];

            for(int i = 0; i < 13; i++)
            {
                res[i] = "";
                for (int j = 0; j < cards.Length; j++)
                {
                    res[i] += cards[j].getStrings()[i];
                    res[i] += " ";
                }
                res[i] = res[i];
            }

            return res;
        }
    }
}
