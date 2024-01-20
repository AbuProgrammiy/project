//umuman bugi yoq (agar tori ishlatses)
//hamma funksiyalari joyida togri ishlavotti
//bot linki https://t.me/BotMennn_bot

using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using System.Threading;
using System.IO.Compression;
using System.Net;
namespace Zipper_Pipper
{
    #region Ziplash hududi
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
    #endregion
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
                bool kirur = true;
                if(message.Type==MessageType.Text)
                {
                    foreach(char c in message.Text)
                        if(!char.IsDigit(c))
                        {
                            kirur = false;
                            break;
                        }
                }
                bool yoq = true;
                if(message.Type==MessageType.Text&&message.Text=="/start")
                {

                    if(System.IO.File.Exists("C:/CHATID.txt"))
                    {
                        string dataId = System.IO.File.ReadAllText("C:/CHATID.txt");
                        string[] dataArray = dataId.Split("\n");
                        for(int i = 0; i < dataArray.Length; i++)
                        {
                            try
                            {
                                if (dataArray[i].Split(":")[0] == chatId)
                                {
                                    if (System.IO.File.Exists($"C:/{dataArray[i].Split(":")[1]}.txt"))
                                    {
                                        await botClient.SendTextMessageAsync(
                                            chatId: chatId,
                                            text: "Sizga jonatmalar bor ekan🙌🙌🙌",
                                            cancellationToken: cancellationToken);
                                        string[] filelar = System.IO.File.ReadAllText($"C:/{dataArray[i].Split(":")[1]}.txt").Split("\n");
                                        for (int j = 0; j < filelar.Length; j++)
                                        {
                                            await using Stream stream = System.IO.File.OpenRead(filelar[j]);

                                            await botClient.SendDocumentAsync(
                                                chatId: chatId,
                                                document: InputFile.FromStream(stream: stream, fileName: filelar[j].Split("\\")[filelar[j].Split("\\").Length - 1]),
                                                caption: $"sizga jonatma!",
                                                cancellationToken: cancellationToken);
                                        }
                                        yoq = false;
                                        break;
                                    }

                                }
                            }
                            catch { }
                        }
                        if (yoq)
                        {
                            await botClient.SendTextMessageAsync(
                                        chatId: chatId,
                                        text: "Sizga jonatmalar yoq",
                                        cancellationToken: cancellationToken);
                        }
                    }
                }
                else if (message.Type == MessageType.Text&&message.Text== "File yoki folder qidirsh")
                {
                    await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Iltimos file yoki folder nomini kiritng\nmen uni kompyuteringizdan qidirp, topilsa ziplap jonataman!",
                        cancellationToken: cancellationToken);
                }
                else if (message.Type == MessageType.Text && message.Text == "Zip file yoki folder jonatish")
                {
                    await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Menga biron bir ziplangan file jonating",
                        cancellationToken: cancellationToken);

                }
                else if(message.Type==MessageType.Document)
                {
                    await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Yahshi qabul qildm👍",
                        cancellationToken: cancellationToken);
                    try
                    {

                        string fileUrl = $"https://api.telegram.org/file/bot<6912030821:AAH4IJkK9nCFZ94YAYOf_iIfiagquHprE4Y>/<{message.Document.FileId}>";
                        using (WebClient client = new WebClient())
                        {
                            client.DownloadFile(fileUrl, $"C:/{message.Document.FileName}");
                        }
                    }
                    catch { }
                }
                else if(message.Type==MessageType.Text&&message.Text== "ChatID berish")
                {
                    await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Biron bir insonni chat idisni yuborin\nva sizning ziplangan file yoki folderlaringiz u insonga boradi🫣",
                        cancellationToken:cancellationToken);

                }
                else if(message.Type==MessageType.Text&&kirur)
                {
                    await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Yahshi endi ushbu odam ushbu botga /start ni bosishi bilan siz qidirgan, jonatgan zip file yoki folderlaringiz jonatiladi",
                        cancellationToken: cancellationToken);
                    if (!System.IO.File.Exists("C:/CHATID.txt"))
                        using (System.IO.File.Create("C:/CHATID.txt")) { }
                    await System.IO.File.AppendAllTextAsync("C:/CHATID.txt",$"{message.Text}:{chatId}\n");
                }
                else if (message.Type==MessageType.Text)
                {
                    await botClient.SendTextMessageAsync(
                        chatId:chatId,
                        text: "Biroz kuting, kompyuteringizni biroz qiynaymiz (10-20sec)\nohirgacha poylen",
                        cancellationToken: cancellationToken);

                    List<string> ziplanganlar=new List<string>();
                    bool holat=Ziplavor.FindAndZip(message.Text!,ref ziplanganlar);

                    if (holat)
                    {
                        if (!System.IO.File.Exists("C:/" + message.Chat.Id))
                            using (System.IO.File.Create("C:/" + message.Chat.Id + ".txt")) { }
                        foreach (string z in ziplanganlar)
                        {
                            string[] nom = z.Split('\\');
                            if (nom[nom.Length-1] == message.Text+".zip")
                            {
                                await System.IO.File.AppendAllTextAsync($"C:/{message.Chat.Id}.txt", z + "\n");
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
