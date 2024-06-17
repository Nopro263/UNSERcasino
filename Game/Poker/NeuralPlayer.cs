using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNSERcasino.Game.Poker.NN;

namespace UNSERcasino.Game.Poker
{
    internal class NeuralPlayer : PokerPlayer
    {
        private NetworkTrainer _trainer;
        public NeuralPlayer(NetworkTrainer trainer, Poker poker, string name, Card[] hand) : base(poker, name, hand)
        {
            _trainer = trainer;
        }

        public override void Next()
        {
            base.Next();

            int result = _trainer.Predict(Hand, _poker.DealerHand, _poker.CurrentBet, _poker.Pot);

            switch(result)
            {
                case 0:
                    {
                        Fold();
                        break;
                    }
                case 1:
                    {
                        Check();
                        break;
                    }
                case 2:
                    {
                        Raise(5);
                        break;
                    }
                case 3:
                    {
                        Raise(15);
                        break;
                    }
                case 4:
                    {
                        Raise(50);
                        break;
                    }
            }
        }
    }
}
