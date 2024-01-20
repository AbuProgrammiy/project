using Newtonsoft.Json.Linq;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

var botClient = new TelegramBotClient("6912030821:AAH4IJkK9nCFZ94YAYOf_iIfiagquHprE4Y");

using CancellationTokenSource cts = new();

ReceiverOptions receiverOptions = new()
{
    AllowedUpdates = Array.Empty<UpdateType>() 
};

botClient.StartReceiving(
    updateHandler: HandleUpdateAsync,
    pollingErrorHandler: HandlePollingErrorAsync,
    receiverOptions: receiverOptions,
    cancellationToken: cts.Token
);

var me = await botClient.GetMeAsync();

Console.WriteLine($"Start listening for @{me.Username}");
Console.ReadLine();

cts.Cancel();

async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{
    if (update.Message is not { } message)
        return;
    var chatId = message.Chat.Id;

    if (message.Text == "/start")
        await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: "Link yuboring",
            cancellationToken: cancellationToken);
    else
    {
        try
        {

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://instagram-downloader-download-instagram-videos-stories1.p.rapidapi.com/?url=https%3A%2F%2F" + message.Text),
                Headers =
            {
                { "X-RapidAPI-Key", "f55f0b2ffemsh829a09013ea2105p1317b3jsnfc0bd37ae303" },
                { "X-RapidAPI-Host", "instagram-downloader-download-instagram-videos-stories1.p.rapidapi.com" },
            },
            };
            string data = "";
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                data = body;
                //Console.WriteLine(body);
            }
            JArray jArray = JArray.Parse(data);
            string url = "";
            string description = "";
            foreach (JObject j in jArray)
            {
                url = (string)j["url"]!;
                description = (string)j["title"]!;
            }


            Console.WriteLine($"Received a '{message.Text}' message in chat {chatId}.");

            await botClient.SendVideoAsync(
                chatId: chatId,
                video: InputFile.FromUri(url),
                caption: description,
                cancellationToken: cancellationToken);
        }
        catch
        {
            await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Notogri link",
                cancellationToken: cancellationToken);
        }
    }
    
}

Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
{
    var ErrorMessage = exception switch
    {
        ApiRequestException apiRequestException
            => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
        _ => exception.ToString()
    };

    Console.WriteLine(ErrorMessage);
    return Task.CompletedTask;
}

