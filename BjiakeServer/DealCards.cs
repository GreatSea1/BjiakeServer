using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace SocketTcpServer  
{
    class DealCards : Deck
    {
        public Hand playerHandEvaluate;
        public Hand playerTwoHandEvaluate;

        public HandEvaluator playerHandEvaluator;
        public HandEvaluator playerTwoHandEvaluator;

        public Card[] playerHand;
        public Card[] playerTwoHand;
        public Card[] dealerCards;

        public Card[] deckCards;
        public Card[] deckCardsPlayerTwo;

        private Card[] sortedPlayerTwoHand;
        private Card[] sortedPlayerHand;
        private Card[] sortedDealerCards;

        private Card[] sortedDeckCards;
        private Card[] sortedDeckCardsPlayerTwo;

        

        public DealCards()
        {
            playerHand = new Card[2];
            playerTwoHand = new Card[2];
            dealerCards = new Card[5];

            

            sortedPlayerHand = new Card[2];
            sortedPlayerTwoHand = new Card[2];
            sortedDealerCards = new Card[5];

            deckCards = new Card[7];
            sortedDeckCards = new Card[7];

            deckCardsPlayerTwo = new Card[7];
            sortedDeckCardsPlayerTwo = new Card[7];
        }
        
        public void Deal()
        {
            SetUpDeck();//Создание колоды карт
            GetHand();//Тасование в руки
            SortCards();//Сортировка для сравнивания
            //DisplayPlayersCard();//Показать карты игрока
            //Motion();//Сделать ход(bet, fold, raise, check, call)
            //DisplayFlope();//Флоп
            ////Сделать ход(bet, fold, raise, check, call)
            //DisplayTern();//Терн
            ////Сделать ход(bet, fold, raise, check, call)
            //DisplayRiver();//Ривер
            ////Сделать ход(bet, fold, raise, check, call)
            ////EvalueateHands();//Подсчет очков
        }
        public void EvaluateHands()
        {
            //create player's computer's evaluation objects (passing SORTED hand to constructor)

            playerHandEvaluator = new(sortedPlayerHand, sortedDealerCards, sortedDeckCards);
            playerTwoHandEvaluator = new(sortedPlayerTwoHand, sortedDealerCards, sortedDeckCardsPlayerTwo);

            //get the player;s and computer's hand
            playerHandEvaluate = playerHandEvaluator.EvaluateHand();
            playerTwoHandEvaluate = playerTwoHandEvaluator.EvaluateHand();

            //display each hand

            Console.WriteLine("\nPlayer Hand: " + playerHandEvaluate);
            Console.WriteLine("\nPlayer Two Hand: " + playerTwoHandEvaluate);

            //evaluate hands
            if (playerHandEvaluate > playerTwoHandEvaluate)
            {
                Console.WriteLine("Player №1 WINS! Его выигрыш:" + Program.bank);
                Program.playerMoney += Program.bank;
            }
            else if (playerHandEvaluate < playerTwoHandEvaluate)
            {
                Console.WriteLine("Player №2 WINS! Его выигрыш:" + Program.bank);
                Program.playerTwoMoney += Program.bank;
            }
            else //if the hands are the same, evaluate the values
            {
                //first evaluate who has higher value of poker hand
                if (playerHandEvaluator.HandValues.Total > playerTwoHandEvaluator.HandValues.Total)
                {
                    Console.WriteLine("Player №1 WINS! Его выигрыш:" + Program.bank);
                    Program.playerMoney += Program.bank;
                }
                else if (playerHandEvaluator.HandValues.Total < playerTwoHandEvaluator.HandValues.Total)
                {
                    Console.WriteLine("Player №2 WINS! Его выигрыш:" + Program.bank);
                    Program.playerTwoMoney += Program.bank;
                }

                //i# both hanve the same poker hand (for example, both have a pair of queens),
                //than the player with the next higher card wins L
                else if (playerHandEvaluator.HandValues.HighCard > playerTwoHandEvaluator.HandValues.HighCard)
                {
                    Console.WriteLine("Player №1 WINS! Его выигрыш:" + Program.bank);
                    Program.playerMoney += Program.bank;
                }
                else if (playerHandEvaluator.HandValues.HighCard < playerTwoHandEvaluator.HandValues.HighCard)
                {
                    Console.WriteLine("Player №2 WINS! Его выигрыш:" + Program.bank);
                    Program.playerTwoMoney += Program.bank;
                }
                else
                {
                    Console.WriteLine("No one wins!");
                    Program.bank /= 2;
                    Program.playerMoney += Program.bank;
                    Program.playerTwoMoney += Program.bank;
                    Console.WriteLine("Каждый получил:" + Program.bank);
                }
            }
        }

        public void SortCards()//Сортировка карт для удобного сравнивания
        {
            var QueryPlayer = from hand in playerHand
                              orderby hand.MyValue
                              select hand;

            var QueryPlayerTwo = from hand in playerTwoHand
                                 orderby hand.MyValue
                                 select hand;

            var QueryDealer = from hand in dealerCards
                              orderby hand.MyValue
                              select hand;

            var QueryDeckCards = from hand in deckCards
                                 orderby hand.MyValue
                                 select hand;
            var QueryDeckCardsPlayerTwo = from hand in deckCardsPlayerTwo
                                          orderby hand.MyValue
                                          select hand;

            var index = 0;
            foreach (var element in QueryPlayer.ToList())
            {
                sortedPlayerHand[index] = element;
                index++;
            }

            index = 0;
            foreach (var element in QueryPlayerTwo.ToList())
            {
                sortedPlayerTwoHand[index] = element;
                index++;
            }

            index = 0;
            foreach (var element in QueryDealer.ToList())
            {
                sortedDealerCards[index] = element;
                index++;
            }

            index = 0;
            foreach (var element in QueryDeckCards.ToList())
            {
                sortedDeckCards[index] = element;
                index++;
            }
            index = 0;
            foreach (var element in QueryDeckCardsPlayerTwo.ToList())
            {
                sortedDeckCardsPlayerTwo[index] = element;
                index++;
            }
            
        }
        public void GetHand()//Раздача карт
        {
            //5 карт дилера
            for (int i = 0; i < 5; i++)
            {
                dealerCards[i] = getDeck[i];
            }
            //2 карты 1 игроку
            for (int i = 5; i < 7; i++)
            {
                playerHand[i - 5] = getDeck[i];
            }
            //
            for (int i = 7; i < 9; i++)
            {
                playerTwoHand[i - 7] = getDeck[i];
            }
            for (int i = 0; i < 7; i++)
            {
                deckCards[i] = getDeck[i];
            }
            for (int i = 0; i < 7; i++)
            {
                if (i < 5)
                {
                    deckCardsPlayerTwo[i] = getDeck[i];
                }
                else if (i >= 5)
                {
                    deckCardsPlayerTwo[i] = getDeck[i + 2];
                }
            }
        }

        
        public void DisplayPlayerCard()
        {
            //Отображение карт игрока
            int y = 14;//Перемещение в место для карт игрока
            int x = 0;
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Карты 1 Игрока");
            y = 15;
            Console.SetCursorPosition(x, y);
            for (int i = 5; i < 7; i++)
            {
                DrawCards.DrawCardOutLine(x, y);
                DrawCards.DrawCardSuitValue(sortedPlayerHand[i - 5], x, y);
                //DrawCards.DrawCardSuitValue(playerHand[i - 5], x, y);
                x++;
            }
        }
        public void DisplayPlayerTwoCard()
        {
            //Отображение карт игрока
            int y = 14;//Перемещение в место для карт игрока
            int x = 3;
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.SetCursorPosition(x * 12, y);
            Console.WriteLine("Карты 2 Игрока");
            y = 15;
            Console.SetCursorPosition(x, y);
            for (int i = 7; i < 9; i++)
            {
                DrawCards.DrawCardOutLine(x, y);
                DrawCards.DrawCardSuitValue(sortedPlayerTwoHand[i - 7], x, y);
                //DrawCards.DrawCardSuitValue(playerTwoHand[i - 7], x, y);
                x++;
            }
        }

        public void DisplayFlope()//Отображение флопа
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
                DrawCards.DrawCardSuitValue(sortedDealerCards[i], x, y);
                //DrawCards.DrawCardSuitValue(dealerCards[i], x, y);
                x++;
            }
        }
        public void DisplayTurn()//Отображение терна
        {
            int x = 3;//Счет карты
            int y = 2;//Курсор(вверх вниз)

            Console.SetCursorPosition(x, y);

            DrawCards.DrawCardOutLine(x, y);
            DrawCards.DrawCardSuitValue(sortedDealerCards[x], x, y);
            //DrawCards.DrawCardSuitValue(dealerCards[x], x, y);
        }

        public void DisplayRiver()//Отображение ривера
        {
            int x = 4;//Счет карты
            int y = 2;//Курсор(вверх вниз)

            Console.SetCursorPosition(x, y);

            DrawCards.DrawCardOutLine(x, y);
            DrawCards.DrawCardSuitValue(sortedDealerCards[x], x, y);
            //DrawCards.DrawCardSuitValue(dealerCards[x], x, y);
        }
    }


}
