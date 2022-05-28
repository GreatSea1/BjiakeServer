using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketTcpClient 
{
    class DisplayOnConsole
    {
        public void PlayerCard()
        {
            //Отображение карт игрока
            int y = 14;//Перемещение в место для карт игрока
            int x = 0;
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Ваши карты");
            y = 15;
            Console.SetCursorPosition(x, y);
            for (int i = 0; i < 2; i++)
            {
                DrawCards.DrawCardOutLine(x, y);
                DrawCards.DrawCardSuitValue(x, y, Program.arraySuitOfCards[i], Program.arrayValueOfCards[i]);
                x++;
            }
        }

        public void Flope()//Отображение флопа
        {
            int x = 0;//Счет карты
            int y = 1;//Курсор(вверх вниз)//ЛСП карусель

            //Отображение карт дилера

            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Карты Дилера");
            y = 2;
            Console.SetCursorPosition(x, y);

            for (int i = 0; i < 3; i++)
            {
                DrawCards.DrawCardOutLine(x, y);
                DrawCards.DrawCardSuitValue(x, y, Program.arraySuitOfCards[i], Program.arrayValueOfCards[i]);
                x++;
            }
        }

        public void Turn()
        {
            int x = 3;//Счет карты
            int y = 2;//Курсор(вверх вниз)

            Console.SetCursorPosition(x, y);

            DrawCards.DrawCardOutLine(x, y);
            DrawCards.DrawCardSuitValue(x, y, Program.arraySuitOfCards[Program.stage], Program.arrayValueOfCards[Program.stage]);
        }
        public void River()//Отображение ривера
        {
            int x = 4;//Счет карты
            int y = 2;//Курсор(вверх вниз)

            Console.SetCursorPosition(x, y);

            DrawCards.DrawCardOutLine(x, y);
            DrawCards.DrawCardSuitValue(x, y, Program.arraySuitOfCards[Program.stage], Program.arrayValueOfCards[Program.stage]);
        }
    }


}
