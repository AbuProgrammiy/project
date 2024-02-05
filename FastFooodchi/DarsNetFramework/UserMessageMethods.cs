using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace DarsNetFramework
{
    /*Progrm.cs da message jonatish ammalari kopayip ketgani uchun 
     * ishlatish/oqish /tushunishni/tartiblash/ osonlastirish uchun shu yerga kochirdim ) */
    public class UserMessageMethods
    {
        public UserMessageMethods(TelegramBotClient botClient)
        {
            this.botClient = botClient;
        }
        public TelegramBotClient botClient { get; set; }

        public async Task MainMenu(long chatId, string message)
        {
            using CancellationTokenSource cts = new();

            ReplyKeyboardMarkup mainMenuButtons =
                new(new[]
                {
                    new KeyboardButton[] { "Mahsulotlani ko'rish" },
                    new KeyboardButton[] { "Savatcha" },
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
        public async Task ShowCategory(long chatId, string message)
        {
            using CancellationTokenSource cts = new();

            ReplyKeyboardMarkup mainMenuButtons =
                new(new[]
                {
                    new KeyboardButton[] { "Go Back" },
                    new KeyboardButton[] { "Burgerlar" },
                    new KeyboardButton[] { "Hot-Doglar" },
                    new KeyboardButton[] { "Lavashlar" },
                    new KeyboardButton[] { "Ichimliklar" },
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
