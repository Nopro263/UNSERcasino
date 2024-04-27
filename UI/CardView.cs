namespace UNSERcasino.UI
{
    internal class CardView : IView
    {
        
        public Card Card { get; set; }

        public CardView(Card card) {
            Card = card;
        }

        public int getXSize()
        {
            return 15;
        }

        public int getYSize()
        {
            return 13;
        }

        public void printToCanvas(Canvas canvas, int x, int y)
        {
            if(Card.Hidden)
            {
                printHidden(canvas, x, y);
                return;
            }
            string space = "";
            string number = Card.CardValue.ToString();

            if(number.Length == 1) {
                space = " ";
            }
            string color = ((char)Card.CardType).ToString();

            canvas.print(x, y, " ------------- ");
            canvas.print(x, y+1, "|" + number + color + space + "          |");
            canvas.print(x, y+2, "|   -------   |");
            canvas.print(x, y+3, "|  |       |  |");
            canvas.print(x, y+4, "|  |       |  |");
            canvas.print(x, y+5, "|  |       |  |");
            canvas.print(x, y+6, "|  |       |  |");
            canvas.print(x, y+7, "|  |       |  |");
            canvas.print(x, y+8, "|  |       |  |");
            canvas.print(x, y+9, "|  |       |  |");
            canvas.print(x, y+10, "|   -------   |");
            canvas.print(x, y+11, "|          " + space + color + number + "|");
            canvas.print(x, y+12, " ------------- ");

            printSpecial(canvas, x, y, (char)Card.CardType);
        }

        private void printHidden(Canvas canvas, int x, int y) {
            canvas.print(x, y, " ------------- ");
            canvas.print(x, y+1, "| * * * * * * |");
            canvas.print(x, y+2, "|* * * * * * *|");
            canvas.print(x, y+3, "| * * * * * * |");
            canvas.print(x, y+4, "|* * * * * * *|");
            canvas.print(x, y+5, "| * * * * * * |");
            canvas.print(x, y+6, "|* * * * * * *|");
            canvas.print(x, y+7, "| * * * * * * |");
            canvas.print(x, y+8, "|* * * * * * *|");
            canvas.print(x, y+9, "| * * * * * * |");
            canvas.print(x, y+10, "|* * * * * * *|");
            canvas.print(x, y+11, "| * * * * * * |");
            canvas.print(x, y+12, " ------------- ");
        }

        private void printSpecial(Canvas canvas, int x, int y, char color)
        {
            if(Card.CardValue == CardValue.ZWEI)
            {
                canvas.print(x + 7, y + 4, color);
                canvas.print(x + 7, y + 8, color);
            } 
            else if (Card.CardValue == CardValue.DREI)
            {
                canvas.print(x + 7, y + 4, color);
                canvas.print(x + 7, y + 6, color);
                canvas.print(x + 7, y + 8, color);
            }
            else if(Card.CardValue == CardValue.VIER)
            {
                canvas.print(x + 4, y + 3, color);
                canvas.print(x + 10, y + 3, color);
                canvas.print(x + 10, y + 9, color);
                canvas.print(x + 4, y + 9, color);
            } 
            else if (Card.CardValue == CardValue.FUENF)
            {
                canvas.print(x + 4, y + 3, color);
                canvas.print(x + 10, y + 3, color);
                canvas.print(x + 10, y + 9, color);
                canvas.print(x + 4, y + 9, color);
                canvas.print(x + 7, y + 6, color);
            }
            else if (Card.CardValue == CardValue.SECHS)
            {
                canvas.print(x + 4, y + 3, color);
                canvas.print(x + 10, y + 3, color);
                canvas.print(x + 4, y + 6, color);
                canvas.print(x + 10, y + 6, color);
                canvas.print(x + 10, y + 9, color);
                canvas.print(x + 4, y + 9, color);
            }
            else if (Card.CardValue == CardValue.SIEBEN)
            {
                canvas.print(x + 4, y + 3, color);
                canvas.print(x + 10, y + 3, color);
                canvas.print(x + 4, y + 6, color);
                canvas.print(x + 10, y + 6, color);
                canvas.print(x + 10, y + 9, color);
                canvas.print(x + 4, y + 9, color);

                canvas.print(x + 7, y + 4, color);
            } 
            else if (Card.CardValue == CardValue.ACHT)
            {
                canvas.print(x + 4, y + 3, color);
                canvas.print(x + 10, y + 3, color);
                canvas.print(x + 4, y + 6, color);
                canvas.print(x + 10, y + 6, color);
                canvas.print(x + 10, y + 9, color);
                canvas.print(x + 4, y + 9, color);

                canvas.print(x + 7, y + 4, color);
                canvas.print(x + 7, y + 8, color);
            }
            else if (Card.CardValue == CardValue.NEUN)
            {
                canvas.print(x + 4, y + 3, color);
                canvas.print(x + 10, y + 3, color);
                canvas.print(x + 4, y + 5, color);
                canvas.print(x + 10, y + 5, color);
                canvas.print(x + 10, y + 7, color);
                canvas.print(x + 4, y + 7, color);
                canvas.print(x + 10, y + 9, color);
                canvas.print(x + 4, y + 9, color);


                canvas.print(x + 7, y + 6, color);
            }
            else if (Card.CardValue == CardValue.ZEHN)
            {
                canvas.print(x + 4, y + 3, color);
                canvas.print(x + 10, y + 3, color);
                canvas.print(x + 4, y + 5, color);
                canvas.print(x + 10, y + 5, color);
                canvas.print(x + 10, y + 7, color);
                canvas.print(x + 4, y + 7, color);
                canvas.print(x + 10, y + 9, color);
                canvas.print(x + 4, y + 9, color);


                canvas.print(x + 7, y + 4, color);
                canvas.print(x + 7, y + 8, color);
            }
            else if (Card.CardValue == CardValue.BUB)
            {
                canvas.print(x + 4, y + 3, color);
                canvas.print(x + 7, y + 6, 'B');
                canvas.print(x + 10, y + 9, color);
            } 
            else if (Card.CardValue == CardValue.DAME)
            {
                canvas.print(x + 4, y + 3, color);
                canvas.print(x + 7, y + 6, 'Q');
                canvas.print(x + 10, y + 9, color);
            }
            else if (Card.CardValue == CardValue.KOENIG)
            {
                canvas.print(x + 4, y + 3, color);
                canvas.print(x + 7, y + 6, 'K');
                canvas.print(x + 10, y + 9, color);
            }
            else if (Card.CardValue == CardValue.ASS)
            {
                canvas.print(x + 7, y + 6, color);
            }
        }
    }
}
