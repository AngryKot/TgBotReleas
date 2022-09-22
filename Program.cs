using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;
using Microsoft.VisualBasic;
using TgBot00;
using Telegram.Bot.Types.ReplyMarkups;
using Microsoft.Data.Sqlite;
using Telegram.Bot.Types.Enums;
using Newtonsoft.Json;
using System.Collections.Generic;
using NLog;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.Entity;
using Telegram.Bot.Types.ReplyMarkups;
using static System.Net.Mime.MediaTypeNames;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;


namespace TelegramBotExperiments
{



     class Program
    {
        //изменения
       
        static string file = System.IO.File.ReadAllText(@"Token.txt");
        static ITelegramBotClient bot = new TelegramBotClient(file.ToString());

        private static Logger logger = LogManager.GetCurrentClassLogger();

        private static ReplyKeyboardMarkup lastkeyboard = new ReplyKeyboardMarkup(new KeyboardButton(""));
        private static ReplyKeyboardMarkup replyKeyboard = new ReplyKeyboardMarkup(new KeyboardButton(""));

        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {

            logger.Debug("log {0}", "/start /bb /help");
            // Некоторые действия
            Console.WriteLine(JsonConvert.SerializeObject(update));
            if (update.Type == UpdateType.Message)
            {
                var message = update.Message;
                InsertData(message);
                //System.IO.File.AppendAllText("text.txt", $"Message:{message.Text}, message_id:{message.MessageId}, FROMid:{message.From.Id}, FROMisBot:{message.From.IsBot}, date:{message.Date}, FROMusername:{message.From.Username}, FROMLastname:{message.From.LastName}\n");

                logger.Debug("log {0}", "Кнопка Start"); //лог
     
                if (message.Text.ToLower() == "/start")
                {
                    await botClient.SendTextMessageAsync(message.Chat, "Приветствую 🎶 ");
                    await botClient.SendTextMessageAsync(message.Chat, "Какой плейлист желаете послушать сегодня? 🔉 \n" +
                        "/menu");
                    return;
                    
                }
                if (message.Text.ToLower() == "/bb")
                {
                    await botClient.SendTextMessageAsync(message.Chat, "До свидания");
                    return;
                }
                if (message.Text.ToLower() == "/help")
                {
                    await botClient.SendTextMessageAsync(message.Chat, "Данный бот явлется продуктом учебной практики студента \n" +
                        "В нём храниться несколько плейлистов, выводимых по совокумности нажатых клавиш \n" +
                        "Достумные команды:\n" +
                        "/start\n" +
                        "/help \n" +
                        "/bb "
                        );
                    return;
                }
                if (message.Text.ToLower() == "/menu")
                {
                    var replyKeyboard = new ReplyKeyboardMarkup(new[]
                    {
                    new KeyboardButton("Выбрать плейлист"),
                    //new KeyboardButton("Рандомный плейлист")
                    }
                    );
                    await bot.SendTextMessageAsync(message.From.Id, "Выберите опцию", replyMarkup: replyKeyboard);
                    return;
                }

               
                if (message.Text.ToLower() == "выбрать плейлист")
                {
                    //изменения
                    lastkeyboard = replyKeyboard;
                    replyKeyboard = new ReplyKeyboardMarkup(new[]
                    {
                    new KeyboardButton("Спокойное"),
                    new KeyboardButton("Грустное"),
                    new KeyboardButton("Весёлое"),
                    new KeyboardButton("Агрессивное"),
                    //new KeyboardButton("Назад")
                    }
                    );
                    await bot.SendTextMessageAsync(message.From.Id, "Ориентируйтесь на ваш вкус", replyMarkup: replyKeyboard);
                    return;
                }
              
                if (message.Text.ToLower() == "спокойное")
                {
                    {
                        var hyperLinkKeyboard = new Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup(Telegram.Bot.Types.ReplyMarkups.InlineKeyboardButton.WithUrl("Без слов", "https://www.youtube.com/watch?v=c1BJ4U9cJFc"));
                        await bot.SendTextMessageAsync(message.Chat, "Плейлист: спокойное", replyMarkup: hyperLinkKeyboard);

                    }
                    {
                        var hyperLinkKeyboard = new Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup(Telegram.Bot.Types.ReplyMarkups.InlineKeyboardButton.WithUrl("На русском", "https://www.youtube.com/watch?v=ji86lelGVKA"));
                        await bot.SendTextMessageAsync(message.Chat, "Плейлист: спокойное", replyMarkup: hyperLinkKeyboard);
                    }
                    {
                        var hyperLinkKeyboard = new Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup(Telegram.Bot.Types.ReplyMarkups.InlineKeyboardButton.WithUrl("Иностранное", "https://www.youtube.com/watch?v=Hc10febKlX8"));
                        await bot.SendTextMessageAsync(message.Chat, "Плейлист: спокойное", replyMarkup: hyperLinkKeyboard);
                        return;
                    }
                }
      
                if (message.Text.ToLower() == "грустное")
                {
                    {
                        var hyperLinkKeyboard = new Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup(Telegram.Bot.Types.ReplyMarkups.InlineKeyboardButton.WithUrl("На русском", "https://ru.stackoverflow.com/"));
                        await bot.SendTextMessageAsync(message.Chat, "Плейлист: грустное", replyMarkup: hyperLinkKeyboard);
                    }
                    {
                        var hyperLinkKeyboard = new Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup(Telegram.Bot.Types.ReplyMarkups.InlineKeyboardButton.WithUrl("Иностранное", "https://www.youtube.com/watch?v=az0yiBFXrvg"));
                        await bot.SendTextMessageAsync(message.Chat, "Плейлист: грустное", replyMarkup: hyperLinkKeyboard);
                        return;
                    }
                }
                
                if (message.Text.ToLower() == "весёлое")
                {
                    {
                        var hyperLinkKeyboard = new Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup(Telegram.Bot.Types.ReplyMarkups.InlineKeyboardButton.WithUrl("На русском", "https://www.youtube.com/watch?v=5XOLxSgsIWs"));
                        await bot.SendTextMessageAsync(message.Chat, "Плейлист: весёлое", replyMarkup: hyperLinkKeyboard);
                    }
                    {
                        var hyperLinkKeyboard = new Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup(Telegram.Bot.Types.ReplyMarkups.InlineKeyboardButton.WithUrl("Иностранное", "https://www.youtube.com/watch?v=KKw6CC47ap4"));
                        await bot.SendTextMessageAsync(message.Chat, "Плейлист: весёлое", replyMarkup: hyperLinkKeyboard);
                        return;
                    }
                }
                if (message.Text.ToLower() == "агрессивное")
                {
                    {
                        var hyperLinkKeyboard = new Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup(Telegram.Bot.Types.ReplyMarkups.InlineKeyboardButton.WithUrl("На русском", "https://www.youtube.com/watch?v=FpZJZgaJeDU"));
                        await bot.SendTextMessageAsync(message.Chat, "Плейлист: агрессивное", replyMarkup: hyperLinkKeyboard);
                    }
                    {
                        var hyperLinkKeyboard = new Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup(Telegram.Bot.Types.ReplyMarkups.InlineKeyboardButton.WithUrl("Иностранное", "https://www.youtube.com/watch?v=NHj7YqsmBsM"));
                        await bot.SendTextMessageAsync(message.Chat, "Плейлист: агрессивное", replyMarkup: hyperLinkKeyboard);
                        return;
                    }
                }
            }
        }

        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            // Некоторые действия
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }


        static void Main(string[] args)
        {

            Console.WriteLine("Запущен бот " + bot.GetMeAsync().Result.FirstName);

            logger.Debug("log {0}", "Event handler");

            CreateTable();
          
            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }, // receive all update types
            };
            bot.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );
            Console.ReadLine();
             
            using (var connection = new SqliteConnection("Data Source=usersdata.db"))
            {
                connection.Open();
            }
            Console.Read();
            string sqlExpression = "SELECT * FROM BGY";
            using (var connection = new SqliteConnection("Data Source=Music.db"))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read())   // построчно считываем данные
                        {
                            var id = reader.GetValue(0);
                            var Mood = reader.GetValue(1);
                            var Language = reader.GetValue(2);
                            var Link = reader.GetValue(3);


                            Playlists bgu = new Playlists(id, Mood, Language, Link);
                        }


                    }
                }
            }
            Console.Read();
        }
        //Работа с БД для аналитики пользователей
        //Соединение с БД
        static SQLiteConnection CreateConnection()
        {
            SQLiteConnection sqlite_conn;
            // Create a new database connection:
            sqlite_conn = new SQLiteConnection("Data Source= users.db; Version = 3; New = True; Compress = True; ");
            // Open the connection:
            try
            {
                sqlite_conn.Open();
            }
            catch (Exception ex)
            {

            }
            return sqlite_conn;
        }
        //Создание таблиц
        static void CreateTable()
        {
            SQLiteConnection conn = CreateConnection();
            SQLiteCommand sqlite_cmd;
            string Createsql = "CREATE TABLE IF NOT EXISTS Users (Text text, ID INT, FromID INT, Bot boolean, Date string(40), Username string(30), Firstname string(25), Lastname string(25))";
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = Createsql;
            sqlite_cmd.ExecuteNonQuery();
        }

        //Вставка данных в таблицу
        static void InsertData(Message message)
        {
            SQLiteConnection conn = CreateConnection();
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = $"INSERT INTO Users (Text, ID, FromID, Bot, Date, Username, Firstname, Lastname) " +
                $"VALUES( '{message.Text}', {message.MessageId}, {message.From.Id}, {message.From.IsBot}, '{message.Date}', '{message.From.Username}', '{message.From.FirstName}', '{message.From.LastName}' ); ";
            sqlite_cmd.ExecuteNonQuery();
        }   
    }
    }

