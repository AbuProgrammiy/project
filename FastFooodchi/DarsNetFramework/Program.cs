using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

// 1 Tonna kodni oqishga tayyormisiz?
//hazillashdim, hammasi kereli kodla)


namespace DarsNetFramework
{
    public class FastFoodManagment
    {
    //Constructor
        public FastFoodManagment()
        {
             Methods.DirectoryAndFileCreate();
             Action().GetAwaiter().GetResult();
        }
    //declarations
        public static TelegramBotClient botClient = new TelegramBotClient("6526535333:AAHXVFBwruT-4zsnhbI_h2cVTRJXRr9cA7g");
        public long Admin= 1268306947;
        public string MainPath = "C:/FastFoodchi/";
        public Methods Methods = new Methods();
        public AdminMessageMethods AdminMessageMethods=new AdminMessageMethods(botClient);
        public UserMessageMethods UserMessageMethods=new UserMessageMethods(botClient);
        public bool adminParolCheck=false;
        public bool userParolCheck=false;
        public bool CrudPost=false;
        public int[] CRUD = new int[2];
        public string parol = "dovay";
        string firstName = "";
        string lastName = "";
        string phoneNumber = "";
        

    //Action
        public async Task Action()
        {

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

            Console.WriteLine($"Start listening for @{me.Username} {me.Id}");
            Console.ReadLine();
            cts.Cancel();

    //Eng kereli joyi
            async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
            {
                if (update.Message is not { } message)
                    return;
                long chatId = message.Chat.Id;

                bool Adminmi=false;
                if (chatId == Admin)
                    Adminmi = true;
            //chatid soralsa
                if (message.Text == "/chatid")
                {
                    await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: $"Sizning ChatIdingiz: {chatId}",
                        cancellationToken: cancellationToken);
                    return;
                }
//Adminaka------
                if(Adminmi)
                {
                    //CRUD ammalari 
                    if (CrudPost)
                    {
                        if (CRUD[1] == 1)
                            if (Methods.CreateJson(CRUD, message.Text))
                                await botClient.SendTextMessageAsync(
                                    chatId: chatId,
                                    text: "😇: Muvaffaqaiyatli create qilindi!",
                                    cancellationToken: cancellationToken);
                            else
                                await botClient.SendTextMessageAsync(
                                    chatId: chatId,
                                    text: "😇: Notori Formatda kiritngiz",
                                    cancellationToken: cancellationToken);
                        CrudPost = false;
                    }
                    //Parol termoqchi bosa
                    else if (adminParolCheck)
                        if(message.Text==parol)
                        {
                        //registratsiya qilish
                            Methods.UserWrite(firstName, lastName, phoneNumber, chatId);
                            adminParolCheck = false;

                            await botClient.SendTextMessageAsync(
                                chatId: chatId,
                                text: "Tog'ri parol!\nMuvaffaqiyatli registratsiya qilindingiz! 😇",
                                cancellationToken: cancellationToken);
                            
                            Thread.Sleep(1500);
                            AdminMessageMethods.MainMenu(chatId, "Endi Szga quydagi Imkoniyatlar ochildi! 👮🏿‍♀\n👇👇👇").GetAwaiter().GetResult(); 
                        }
                        else
                        {
                            await botClient.SendTextMessageAsync(
                                    chatId: chatId,
                                    text: "Notori parol, aldashga urunmang! 🥸",
                                    cancellationToken: cancellationToken);
                        }
                   //start
                    else if (message.Text == "/start")
                    {
                        if (Methods.UsersRead(chatId))
                        {
                            await botClient.SendTextMessageAsync(
                                chatId: chatId,
                                text: "Botga Hush kelibsiz\nHurmatli Admin 🫡",
                                cancellationToken: cancellationToken);
                            Thread.Sleep(1500);
                            AdminMessageMethods.MainMenu(chatId, "Endi Szga quydagi Imkoniyatlar ochildi! 👮🏿‍♀\n👇👇👇").GetAwaiter().GetResult();
                        }
                        else
                        {
                            ReplyKeyboardMarkup contactButton = new(new[]
                            {
                                KeyboardButton.WithRequestContact("Share Contact"),
                            })
                            { ResizeKeyboard=true};
                            
                            await botClient.SendTextMessageAsync(
                                chatId: chatId,
                                text: "Contact jo'natib\nRegistratsiyadan otishiz kere Hurmatli Admin 🥸\n   *birmatta faqat",
                                replyMarkup: contactButton,
                                cancellationToken: cancellationToken);
                         }
                    }
                    else if (message.Text == "/chatid")
                        await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: $"Sizning chatIdingiz: {chatId} 🥷🏿",
                            cancellationToken: cancellationToken);
                //Contact Jo'natilganda uni json filega yozadi
                    else if (message.Contact is not null)
                    {
                        firstName=message.Contact.FirstName;
                        lastName=message.Contact.LastName;
                        phoneNumber=message.Contact.PhoneNumber;

                        await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: $"AdminAka\nUshbu raqamga: {message.Contact.PhoneNumber} kod jonatildi\n shuni yuboring manga! 🥸",
                            cancellationToken: cancellationToken);

                        Methods.SendMessage(message.Contact.PhoneNumber);
                        adminParolCheck = true;
                    }
            //Partal
                //Menu 1
                    else if (new string[] {"CRUD", "Buyurtmalar ro'yhati (excel)", "Mijozlar ro'yhati (pdf)" }.Contains(message.Text))
                    {
                        if(message.Text=="CRUD")
                            AdminMessageMethods.CrudMenu(chatId, "Nmani CRUD qimoqchisiz? 🥸\nho'jayn").GetAwaiter().GetResult();
                        else
                            AdminMessageMethods.Razrabotka(chatId).GetAwaiter().GetResult();
                    }
                //Menu 2
                    else if (new string[] { "Go Back", "Category", "Product", "Pay Type", "Order Status" }.Contains(message.Text))
                    {
                        if (message.Text == "Go Back")
                            AdminMessageMethods.MainMenu(chatId,"Orqaga Qaytildi!").GetAwaiter().GetResult();
                        else if (message.Text== "Category")
                        {
                            AdminMessageMethods.CrudAction(chatId).GetAwaiter().GetResult();
                            CRUD[0] = 1;
                        }
                        else if (message.Text == "Product")
                        {
                            AdminMessageMethods.CrudAction(chatId).GetAwaiter().GetResult();
                            CRUD[0] = 2;
                        }
                        else if (message.Text == "Pay Type")
                        {
                            AdminMessageMethods.CrudAction(chatId).GetAwaiter().GetResult();
                            CRUD[0] = 3;
                        }
                        else if (message.Text == "Order Status")
                        {
                            AdminMessageMethods.CrudAction(chatId).GetAwaiter().GetResult();
                            CRUD[0] = 4;
                        }
                        else if (message.Text == "Buyurtma Status")
                        {
                            AdminMessageMethods.CrudAction(chatId).GetAwaiter().GetResult();
                            CRUD[0] = 5;
                        }
                    }
                //Menu 3
                    else if (new string[] { "Gо Back", "Create", "Read", "Update", "Delete" }.Contains(message.Text))
                    {
                        if (message.Text == "Gо Back")
                        {
                            AdminMessageMethods.CrudMenu(chatId, "Orqaga qaytildi!").GetAwaiter().GetResult();
                        }
                        else if (message.Text == "Create")
                        {
                            CRUD[1] = 1;
                            AdminMessageMethods.CreateDialog(chatId, CRUD).GetAwaiter().GetResult();
                            CrudPost = true;
                        }
                        else if (message.Text == "Read")
                        {
                            CRUD[1] = 2;
                            AdminMessageMethods.ReadDialog(chatId,CRUD).GetAwaiter().GetResult();
                        }
                        else if (message.Text == "Update")
                        {
                            CRUD[1] = 3;
                            AdminMessageMethods.Razrabotka(chatId).GetAwaiter().GetResult();
                        }
                        else if (message.Text == "Delete")
                        {
                            CRUD[1] = 4;
                            AdminMessageMethods.Razrabotka(chatId).GetAwaiter().GetResult();
                        }
                    }
                    else
                                await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Notori kamanda kirtingiz shef 🥸",
                            cancellationToken: cancellationToken);
                }
//User--------
                else
                {
                //User parolini tekshirish kere bosa
                    if (userParolCheck)
                        if (message.Text == parol)
                        {
                        //registratsiya qilish
                            Methods.UserWrite(firstName, lastName, phoneNumber, chatId);
                            userParolCheck = false;
                            await botClient.SendTextMessageAsync(
                                chatId: chatId,
                                text: "Tog'ri parol!\nMuvaffaqiyatli registratsiya qilindingiz! 😇",
                                cancellationToken: cancellationToken);
                            Thread.Sleep(1500);
                            UserMessageMethods.MainMenu(chatId, "Marhamat Tanlen! 😇").GetAwaiter().GetResult();
                        }
                        else
                        {
                            await botClient.SendTextMessageAsync(
                                chatId: chatId,
                                text: "Notori parol, aldashga urunmang! 🥸",
                                cancellationToken: cancellationToken);
                        }
                //User start bossa
                    else if (message.Text == "/start")
                    {
                        if (Methods.UsersRead(chatId))
                        {
                            await botClient.SendTextMessageAsync(
                                chatId: chatId,
                                text: "Botga Hush kelibsiz\nHurmatli Foydalanuvchu 😇",
                                cancellationToken: cancellationToken);
                            Thread.Sleep(1500);
                            UserMessageMethods.MainMenu(chatId, "Marhamat Tanlen! 😇").GetAwaiter().GetResult();
                        }
                        else
                        {
                            ReplyKeyboardMarkup contactButton = new(new[]
                            {
                                KeyboardButton.WithRequestContact("Share Contact"),
                            })
                            { ResizeKeyboard=true};
                            await botClient.SendTextMessageAsync(
                                chatId: chatId,
                                text: "Contact jo'natib\nRegistratsiyadan otishiz kere Hurmatli Foydalanuvchi 🥸\n   *birmatta faqat",
                                replyMarkup: contactButton,
                                cancellationToken: cancellationToken);
                        }
                    }
                //User Contact jonatsa
                    else if (message.Contact is not null)
                    {
                        firstName = message.Contact.FirstName;
                        lastName = message.Contact.LastName;
                        phoneNumber = message.Contact.PhoneNumber;

                        await botClient.SendTextMessageAsync(
                           chatId: chatId,
                           text: $"Hurmatli Foydalanuvchi\nUshbu raqamga: {message.Contact.PhoneNumber} kod jonatildi\n shuni yuboring manga! 🥸",
                           replyMarkup: new ReplyKeyboardRemove(),
                           cancellationToken: cancellationToken);

                        Methods.SendMessage(message.Contact.PhoneNumber);
                        userParolCheck = true;
                    }
            //Partal
                //Menu 1
                    else if (new string[] { "Mahsulotlani ko'rish", "Savatcha"}.Contains(message.Text))
                    {
                        if (message.Text == "Mahsulotlani ko'rish")
                            UserMessageMethods.ShowCategory(chatId, "Categoriyani tanlang").GetAwaiter().GetResult();
                        else
                            UserMessageMethods.Razrabotka(chatId).GetAwaiter().GetResult();
                    }
                //Menu 2
                    else if (new string[] {"Go Back", "Burgerlar", "Hot-Doglar", "Lavashlar", "Ichimliklar" }.Contains(message.Text))
                    {
                        if (message.Text == "Go Back")
                            UserMessageMethods.MainMenu(chatId, "Orqaga qaytildi").GetAwaiter().GetResult();
                        else
                            UserMessageMethods.Razrabotka(chatId).GetAwaiter().GetResult();
                    }
                    else
                                await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Notori kamanda, brat! 🥸",
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
    internal class Program
    {
        static void Main(string[] args)
        {
            FastFoodManagment start= new FastFoodManagment();   
        }
    }
}

//Hulosa 
//1 Sleeplar chiroyli va realni chiqishi uchun ishlatildi(user bitta messagini oqib olgunuga qadar ikkinchisi kutip turadi)
//2  bir hil SendTextMessagelar overload bolgan, sababi user va admin uchun alohida gaplar yozilgan
//3 Messagelari matniga alohida urg'u berilgan)
//4 if elslar performence efficencyni oshirish uchun foydalandm 
//5 Lyuboy kompyuterda run qisa ishlivuradigan qildm (malumotlar filesini tashlab bershim shart emas)

//codlardagi Commentlarga etibor bering )


 /* 😀: Telegram botim strukturasi juda ham murakkab va betartib bolishi mumkin
 *  Lekin iloji boricha CleenCode va TelegramBot/Json methodlaridan chiroyli foydalanishga harakat qildim )
 */