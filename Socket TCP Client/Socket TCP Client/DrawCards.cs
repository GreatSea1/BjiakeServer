using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketTcpClient
{
    class DrawCards
    {
        public static void DrawCardOutLine(int xCoordinate, int yCoordinate)
        {
            Console.ForegroundColor = ConsoleColor.White;

            int x = xCoordinate * 12;
            int y = yCoordinate;

            Console.SetCursorPosition(x, y);
            //Контур карт
            Console.Write(" __________\n");//Верхняя часть карт

            for (int i = 0; i < 10; ++i)//Отображение остальной части карт
            {
                Console.SetCursorPosition(x, y + 1 + i);

                if (i != 9)
                {
                    Console.WriteLine("|          |");//Середина карт
                }
                else
                {
                    Console.WriteLine("|__________|");//Нижняя часть карт
                }
            }
        }
        // Отображение масти и значения карты внутри контура
        public static void DrawCardSuitValue(int xCoordinate, int yCoordinate, int suitCard, int valueCard)
        {
            string CardSuit = " ";

            int x = xCoordinate * 12;
            int y = yCoordinate;

            //По коду CP437
            //Черви и бубны красные, трефы и черви черные(2665, 2666, 2660, 2663)

            if(suitCard == 0)
            {
                CardSuit = "\u2665";// Hearts
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else if (suitCard == 1)
            {
                CardSuit = "\u2660"; // Spades
                Console.ForegroundColor = ConsoleColor.Black;
            }
            else if (suitCard == 2)
            {
                CardSuit = "\u2666";// Diamonds
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else if (suitCard == 3)
            {
                CardSuit = "\u2663"; // Clubs
                Console.ForegroundColor = ConsoleColor.Black;
            }

            string CardValue = " ";
            
            if(valueCard == 12)
            {
                CardValue = "Two";
            }
            else if (valueCard == 13)
            {
                CardValue = "Three";
            }
            else if (valueCard == 14)
            {
                CardValue = "Four";
            }
            else if (valueCard == 15)
            {
                CardValue = "Five";
            }
            else if (valueCard == 16)
            {
                CardValue = "Six";
            }
            else if (valueCard == 17)
            {
                CardValue = "Seven";
            }
            else if (valueCard == 18)
            {
                CardValue = "Eight";
            }
            else if (valueCard == 19)
            {
                CardValue = "Nine";
            }
            else if (valueCard == 20)
            {
                CardValue = "Ten";
            }
            else if (valueCard == 21)
            {
                CardValue = "Jack";
            }
            else if (valueCard == 22)
            {
                CardValue = "Queen";
            }
            else if (valueCard == 23)
            {
                CardValue = "King";
            }
            else if (valueCard == 24)
            {
                CardValue = "Ace";
            }

            Console.SetCursorPosition(x + 5, y + 5);
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Write(CardSuit);
            Console.SetCursorPosition(x + 4, y + 7);
            Console.Write(CardValue);
        }
    }
}
