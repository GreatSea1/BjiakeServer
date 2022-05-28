using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace SocketTcpClient
{

    class Program : DisplayOnConsole
    {
        // адрес и порт сервера, к которому будем подключаться
        static int port = 2115; // порт сервера
        static string address = "127.0.0.1"; // адрес сервера 176.196.126.194
        // Локальный адресс 127.0.0.1
        public static int[] arraySuitOfCards = new int[5];
        public static int[] arrayValueOfCards = new int[5];
        public static int stage = 1;//stage 2 handCard, stage 3 flope and turn, stage 4 river
        public static DisplayOnConsole display = new();
        public static void RecieveTableCardsFromServer(Socket user)
        {
            //Остановился на получении данных клиента
            string CardSuit = "empty";//Данные для сообщения
            byte[] data = Encoding.Unicode.GetBytes(CardSuit);

            // получаем ответ
            data = new byte[256]; // буфер для ответа
            StringBuilder builder = new StringBuilder();
            int bytes = 0; // количество полученных байт

            do
            {
                bytes = user.Receive(data, data.Length, 0);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (user.Available > 0);

            //Console.WriteLine("ответ сервера: " + builder.ToString());
            data = null;
            bytes = 0;
            builder.Clear();
        }



        //Получить данные
        public static void RecieveServerData(Socket user)
        {
            //Console.Clear();
            string message = "empty";//Данные для сообщения
            byte[] data = Encoding.Unicode.GetBytes(message);

            // получаем ответ
            data = new byte[256]; // буфер для ответа
            StringBuilder builder = new StringBuilder();
            int bytes = 0; // количество полученных байт

            do
            {
                bytes = user.Receive(data, data.Length, 0);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (user.Available > 0);
            Console.WriteLine("ответ сервера: " + builder.ToString());
            data = null;
            bytes = 0;
            builder.Clear();
        }

        public static void RecieveCards(Socket user)
        {
            string Cards = "empty"; //Данные для сообщения
            int enumCard;
            byte[] dataSuit = Encoding.Unicode.GetBytes(Cards);

            // получаем ответ
            dataSuit = new byte[256]; // буфер для ответа
            StringBuilder builder = new StringBuilder();
            int bytes = 0; // количество полученных байт
            do
            {
                bytes = user.Receive(dataSuit, dataSuit.Length, 0);
                builder.Append(Encoding.Unicode.GetString(dataSuit, 0, bytes));
            }
            while (user.Available > 0);
            Cards = builder.ToString();

            enumCard = Int32.Parse(Cards);//Значение масти карты
            //
            if (stage == 1)
            {//311209
                //Console.WriteLine(Cards[0]);
                arraySuitOfCards[0] = enumCard / 100000;//3
                arraySuitOfCards[1] = enumCard / 10000 % 10;//1
                arrayValueOfCards[0] = enumCard / 100 % 100;//12
                arrayValueOfCards[1] = enumCard % 100;//09
            }
            else if (stage == 2)
            {//302 12 23 11
                arraySuitOfCards[0] = enumCard / 100000000;//3
                arraySuitOfCards[1] = enumCard / 10000000 % 10;//0
                arraySuitOfCards[2] = enumCard / 1000000 % 10;//2
                arrayValueOfCards[0] = enumCard / 10000 % 100;//12
                arrayValueOfCards[1] = enumCard / 100 % 100;//23
                arrayValueOfCards[2] = enumCard % 100;//11
                //ss
            }
            else if (stage == 3)
            {//212
                //Console.WriteLine(Cards);
                arraySuitOfCards[3] = enumCard / 100;//2
                arrayValueOfCards[3] = enumCard % 100;//12
            }
            else if (stage == 4)
            {//315
                //Console.WriteLine(Cards);
                arraySuitOfCards[4] = enumCard / 100;//3
                arrayValueOfCards[4] = enumCard % 100;//15
            }
            // Console.WriteLine(enumCard);

            enumCard = 0;
            Cards = null;
            dataSuit = null;
            bytes = 0;
            builder.Clear();
        }
        public static void sleep()
        {
            Thread.Sleep(500);
        }
        public static void CloseSocket(Socket user)
        {
            // закрываем сокет
            user.Shutdown(SocketShutdown.Both);
            user.Close();
        }

        //отправить данные
        public static void SendServerData(Socket user)
        {
            string message = "empty";//Данные для сообщения
            byte[] data = Encoding.Unicode.GetBytes(message);

            // получаем ответ
            data = new byte[256]; // буфер для ответа
            StringBuilder builder = new StringBuilder();
            int bytes = 0; // количество полученных байт

            Console.Write("Введите сообщение:");
            message = Console.ReadLine();
            data = Encoding.Unicode.GetBytes(message);
            user.Send(data);
            data = null;
            bytes = 0;
            builder.Clear();
        }

        public static void SendMotion(Socket user)
        {
            string message = "empty";
            byte[] data = Encoding.Unicode.GetBytes(message);
            user.Send(data);
        }

        static void Main(string[] args)
        {
            Console.Title = "Блэйк Джек";

            ConsoleColor[] colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Clear();

            Console.BufferWidth = 120;
            Console.WindowWidth = Console.BufferWidth;
            Console.WindowHeight = 40;
            Console.BufferHeight = Console.WindowHeight;

            try
            {
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);

                Socket user = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); // Someone said "Family"?
                // подключаемся к удаленному хосту
                user.Connect(ipPoint);

                //RecieveServerData(user);
                ////RecieveServerData(user);
                //SendServerData(user);
                Console.Clear();
                //stage 1
                RecieveCards(user);
                display.PlayerCard();
                stage++;//2

                RecieveCards(user);
                display.Flope();
                //stage = 2

                stage++;//3
                RecieveCards(user);
                display.Turn();

                stage++;//4
                RecieveCards(user);
                display.River();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.Read();
        }
    }
}