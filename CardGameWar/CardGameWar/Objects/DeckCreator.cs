using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGameWar.Objects
{
    public static class DeckCreator
    { 
        public static Queue<Card> CreateCards()
        {
            Queue<Card> cards = new Queue<Card>();
            for(int i = 2; i <= 14; i++)
            {
                foreach(Suit suit in Enum.GetValues(typeof(Suit)))
                {
                    cards.Enqueue(new Card()
                    {
                        Suit = suit,
                        Value = i,
                        DisplayName = GetShortName(i, suit)
                    });
                }
            }
            return Shuffle(cards);
        }

        private static Queue<Card> Shuffle(Queue<Card> cards)
        {
            //Shuffle the existing cards using Fisher-Yates Modern
            List<Card> transformedCards = cards.ToList();
            Random r = new Random(DateTime.Now.Millisecond);
            for (int n = transformedCards.Count - 1; n > 0; --n)
            {
                //Step 2: Randomly pick a card which has not been shuffled
                int k = r.Next(n + 1);

                //Step 3: Swap the selected item with the last "unselected" card in the collection
                Card temp = transformedCards[n];
                transformedCards[n] = transformedCards[k];
                transformedCards[k] = temp;
            }

            Queue<Card> shuffledCards = new Queue<Card>();
            foreach(var card in transformedCards)
            {
                shuffledCards.Enqueue(card);
            }

            return shuffledCards;
        }

        private static string GetShortName(int value, Suit suit)
        {
            string valueDisplay = "";
            if (value >= 2 && value < 10)
            {
                valueDisplay = "|      " + value.ToString() + "    |";
            }
            else if (value == 10)
            {
                valueDisplay = "|     " + value.ToString() + "    |";
            }
            else if (value == 11)
            {
                valueDisplay = "|    Jack   |";
            }
            else if (value == 12)
            {
                valueDisplay = "|    Queen  |";
            }
            else if (value == 13)
            {
                valueDisplay = "|    King   |";
            }
            else if (value == 14)
            {
                valueDisplay = "|     Ace   |";
            }

            string suitDisplay = Enum.GetName(typeof(Suit), suit);
            if (suitDisplay.Equals("Diamonds"))
            {
                suitDisplay = "| ♦Diamonds♦|";
            }
            else if (suitDisplay.Equals("Spades"))
            {
                suitDisplay = "|  ♠Spades♠ |";
            }
            else if (suitDisplay.Equals("Clubs"))
            {
                suitDisplay = "|  ♣Clubs♣  |";
            }
            else if (suitDisplay.Equals("Hearts"))
            {
                suitDisplay = "|  ♥Hearts♥ |";
            }

            //return valueDisplay + Enum.GetName(typeof(Suit), suit)[0];
            return "-------------" + "\n" +
                   "|           |" + "\n" +
                   "|           |" + "\n" + //♥♦♣♠
                valueDisplay + "\n" +
                   "|     of    |" + "\n" +
                suitDisplay+ "\n" +
                   "|           |" + "\n" +
                   "|           |" + "\n" +
                   "-------------";


        }
    }
}
