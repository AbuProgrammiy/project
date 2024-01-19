//Assalomualeykum ustoz, uy ishini tekshirgani kirib ushbu habarni oqivotgan boses, 
//sizdan iltimos uy ishimmi ertaga ertalabdan tekshirin, soat 3 00 AM gacham tayor qib qoyaman!🙏
//hozircha chala...


using HomeOne;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using System.Threading;
using System.IO.Compression;
namespace Zipper_Pipper
{
    internal class Ziplavor
    {
        public static bool FindAndZip(string FileOrFolder, ref List<string> ziplanganlar)
        {
            void FaylniQidir(List<string> Topilmalar, string Path, string FileOrFolder)
            {
                //faylarni tekshirib korish
                string[] Fayllar = Directory.GetFiles(Path);
                string[] Folders = Directory.GetDirectories(Path);

                foreach (string f in Fayllar)
                {
                    string[] GumondorFayl = f.Split('\\');
                    if (GumondorFayl[GumondorFayl.Length - 1] == FileOrFolder)
                        Topilmalar.Add(f);
                }
                foreach (string f in Folders)
                {
                    string[] GumondorFolder = f.Split("\\");
                    if (GumondorFolder[GumondorFolder.Length - 1] == FileOrFolder)
                        Topilmalar.Add(f);
                }

                //folderlarni ichiga kirish

                foreach (string f in Folders)
                {
                    //bir xil folderlarga murojat qilib bolmaydi shuning uchun exception qaytaradi
                    try
                    {
                        FaylniQidir(Topilmalar, f, FileOrFolder);
                    }
                    catch { }
                }

            }


            //Rootlarni aniqlash
            List<string> Topilmalar = new List<string>();
            DriveInfo[] Disklar = DriveInfo.GetDrives();
            for (int i = 0; i < Disklar.Length; i++)
            {
                FaylniQidir(Topilmalar, Convert.ToString(Disklar[i])!, FileOrFolder);
            }

            if (Topilmalar.Count == 0)
                return false;
            for (int i = 0; i < Topilmalar.Count; i++)
            {
                if (System.IO.File.Exists(Topilmalar[i]))
                {
                    string[] source = Topilmalar[i].Split("\\");
                    Topilmalar[i] = "";
                    for (int j = 0; j < source.Length - 1; j++)
                        Topilmalar[i] += source[j];

                    if (!System.IO.File.Exists(Topilmalar[i] + ".zip"))
                    {
                        ZipFile.CreateFromDirectory(Topilmalar[i], Topilmalar[i] + ".zip");
                        ziplanganlar.Add(Topilmalar[i] + ".zip");
                    }

                    else { ziplanganlar.Add(Topilmalar[i] + ".zip"); }
                }
                else
                {
                    if (!System.IO.File.Exists(Topilmalar[i] + ".zip"))
                    {
                        ZipFile.CreateFromDirectory(Topilmalar[i], Topilmalar[i] + ".zip");
                        ziplanganlar.Add(Topilmalar[i] + ".zip");
                    }

                    else { ziplanganlar.Add(Topilmalar[i] + ".zip"); }
                }
            }
            return true;

            //Abu Programmiy

        }


    }
    class Program
    {
        static async Task Main()
        {
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

            // using Telegram.Bot.Types.ReplyMarkups;
//kereli narsala


            var me = await botClient.GetMeAsync();

            Console.WriteLine($"Start listening for @{me.Username}");
            Console.ReadLine();

            cts.Cancel();

            async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
            {
                ReplyKeyboardMarkup MenuButtons = new(new[]
                {
                    new KeyboardButton[] { "File yoki folder qidirsh", "Zip file yoki folder jonatish","ChatID berish" },
                })
                {
                    ResizeKeyboard = true
                };


                if (update.Message is not { } message)
                    return;
                ChatId chatId = message.Chat.Id;

                if (message.Type == MessageType.Text&&message.Text== "File yoki folder qidirsh")
                {
                    await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Iltimos file yoki folder nomini kiritng\nmen uni kompyuteringizdan qidirp, topilsa ziplap jonataman!",
                        cancellationToken: cancellationToken);
                }
                else if (message.Type==MessageType.Text)
                {
                    await botClient.SendTextMessageAsync(
                        chatId:chatId,
                        text: "Biroz kuting, kompyuteringizni biroz qiynaymiz (10-20sec)",
                        cancellationToken: cancellationToken);

                    List<string> ziplanganlar=new List<string>();
                    bool holat=Ziplavor.FindAndZip(message.Text!,ref ziplanganlar);

                    if (holat)
                    {

                        foreach (string z in ziplanganlar)
                        {
                            string[] nom = z.Split('\\');
                            if (nom[nom.Length-1] == message.Text+".zip")
                            {
                                await using Stream stream = System.IO.File.OpenRead(z);
                                await botClient.SendDocumentAsync(
                                    chatId: chatId,
                                    document: InputFile.FromStream(stream: stream, fileName: nom[nom.Length - 1]),
                                    caption: $"{message.Text} Fayli yoki Folderi Muvafaqiyatli ziplandi!",
                                    cancellationToken: cancellationToken);
                            }
                        }
                    }
                    else
                    {
                        await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: $"{message.Text} Fayli yoki Folderi topilmadiku oka😨",
                            cancellationToken: cancellationToken);
                    }
                }
                else
                {
                    await botClient.SendTextMessageAsync(
                        chatId:chatId,
                        text: "Xush kelibsiz😀\npastdagi tugmachalardan birini tanlang👇👇👇",
                        replyMarkup: MenuButtons,
                        cancellationToken: cancellationToken);
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

        }
    }
}
