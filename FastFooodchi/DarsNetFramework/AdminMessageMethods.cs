using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace DarsNetFramework
{
/*Progrm.cs da message jonatish ammalari kopayip ketgani uchun 
 * ishlatish/oqish /tushunishni/tartiblash/ osonlastirish uchun shu yerga kochirdim ) */
    
    public class AdminMessageMethods
    {
        public AdminMessageMethods(TelegramBotClient botClient)
        {
            this.botClient = botClient;
        }
        public TelegramBotClient botClient { get; set; }
        public Methods Method = new Methods();
        
        public async Task MainMenu(long chatId,string message)
        {
            using CancellationTokenSource cts = new();

            ReplyKeyboardMarkup mainMenuButtons = 
                new(new[]
                {
                    new KeyboardButton[] { "CRUD" },
                    new KeyboardButton[] { "Buyurtmalar ro'yhati (excel)" },
                    new KeyboardButton[] { "Mijozlar ro'yhati (pdf)" },
                })
                {
                    ResizeKeyboard = true
                };
            await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: message,
                replyMarkup: mainMenuButtons,
                cancellationToken: cts.Token);
        }
        public async Task CrudMenu(long chatId,string message)
        {
            using CancellationTokenSource cts = new();

            ReplyKeyboardMarkup mainMenuButtons = 
                new(new[]
                {

                    new KeyboardButton[] { "Go Back","Category" },
                    new KeyboardButton[] { "Order Status","Product" },
                    new KeyboardButton[] { "Pay Type", " " },
                })
                {
                    ResizeKeyboard = true
                };
            await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: message,
                replyMarkup: mainMenuButtons,
                cancellationToken: cts.Token);
        }
        public async Task CrudAction(long chatId)
        {
            using CancellationTokenSource cts = new();

            ReplyKeyboardMarkup mainMenuButtons =
                new(new[]
                {

                    new KeyboardButton[] { "Gо Back","Create" },
                    new KeyboardButton[] { " ","Read" },
                    new KeyboardButton[] { " ","Update" },
                    new KeyboardButton[] { " ","Delete"}
                })
                {
                    ResizeKeyboard = true
                };
            await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Nma qmoqchisiz endi 🥸\nshef",
                replyMarkup: mainMenuButtons,
                cancellationToken: cts.Token);
        }

        public async Task CreateDialog(long chatId, int[] CRUD)
        {
            using CancellationTokenSource cts = new();

            if (CRUD[0] == 1)
                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "🥸: Yangi kategoriya nomini yuboring: ",
                    cancellationToken: cts.Token);
            else if (CRUD[0] == 2)
                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "🥸: Product qoshish uchun quydagicha formatda malumot yuboring:\n\n" +
                    "Nomi|Tasnif|narx|mavjutCategoriya",
                    cancellationToken: cts.Token);
            else if (CRUD[0] == 3)
                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "🥸: Yangi to'lov turi nomini yuboring: ",
                    cancellationToken: cts.Token);
            else if (CRUD[0] == 4)
                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "🥸: Yangi Status nomini kiritng: ",
                    cancellationToken: cts.Token);

        }
        public async Task ReadDialog(long chatId, int[] CRUD)
        {
            using CancellationTokenSource cts = new();
            string data = "";
            
            if (CRUD[0] == 1)
            {
                data = Method.ReadJson(CRUD);
                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: data,
                    cancellationToken: cts.Token);
            }
            else if (CRUD[0] == 2)
            {
                data = Method.ReadJson(CRUD);
                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: data,
                    cancellationToken: cts.Token);
            }
            else if (CRUD[0] == 3)
            {
                data = Method.ReadJson(CRUD);
                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: data,
                    cancellationToken: cts.Token);
            }
                
            else if (CRUD[0] == 4)
            {
                data = Method.ReadJson(CRUD);
                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: data,
                    cancellationToken: cts.Token);
            }
                
        }

        public async Task Razrabotka(long chatId)
        {
            using CancellationTokenSource cts = new();

            await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Hali uyogiga ulgurmadimuu 🥹",
                cancellationToken: cts.Token);
        }
    }
}
