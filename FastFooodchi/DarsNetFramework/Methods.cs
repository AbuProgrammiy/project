using Models;
using System.Text.Json;
using Telegram.Bot.Types;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using static System.Runtime.InteropServices.JavaScript.JSType;



namespace DarsNetFramework
{
    public class Methods
    {
        public string MainPath = "C:/FastFoodchi/";

        //user chatid si fileda bor yoqlikga tekshiradi
        public bool UsersRead(long chatId)
        {
            string data = System.IO.File.ReadAllText(MainPath + "UsersInfo.json");
            try
            {
                List<UsersInfo> Users = JsonSerializer.Deserialize<List<UsersInfo>>(data);
                foreach (UsersInfo i in Users)
                    if (i.chatId == chatId)
                        return true;
                return false;
            }
            catch { return false; }
        }
        public void UserWrite(string FirstName, string LastName, string phoneNumber, long chatId)
        {
            UsersInfo UserInfoModel = new UsersInfo()
            {
                firstName = FirstName,
                lastName = LastName,
                chatId = chatId,
                phoneNumber = phoneNumber
            };
            string allData = System.IO.File.ReadAllText(MainPath + "UsersInfo.json");
            List<UsersInfo> allModels = new List<UsersInfo>();
            try
            {
                allModels = JsonSerializer.Deserialize<List<UsersInfo>>(allData)!;
            }
            catch { }
            allModels.Add(UserInfoModel);
            string data = JsonSerializer.Serialize(allModels);
            System.IO.File.WriteAllText(MainPath + "UsersInfo.json", data);
        }

        public void SendMessage(string phoneNumber)
        {
            string accountSid = "AC430ad330c541d9a94f3facbcecfca34c";
            string authToken = "cda7a7ef6a78ef23988e26448901c218";

            TwilioClient.Init(accountSid, authToken);
            var message = MessageResource.Create(
                body: ">>>>>>>>>>\n\n Parol: dovay",
                from: new Twilio.Types.PhoneNumber("+16562185537"),
                to: new Twilio.Types.PhoneNumber("+998935382210")
            );
        }

        //bot datalari saqlanadigan joyni tartblash uchun directory yasash
        //birinchi marta ishga tushurilganda file ochilib unga default qiymatlar beriladi
        public void DirectoryAndFileCreate()
        {
            if (!Directory.Exists(MainPath))
            {
                Directory.CreateDirectory(MainPath);
                using (System.IO.File.Create(MainPath + "UsersInfo.json")) { }
                using (StreamWriter file = new StreamWriter(MainPath + "Categories.json"))
                {
                    List<Categoies> CategoriesModel = new List<Categoies>()
                    {
                        new Categoies()
                        { categoryName="Burgerlar" },
                        new Categoies()
                        { categoryName="Hot-Doglar" },
                        new Categoies()
                        { categoryName="Lavashlar" },
                        new Categoies()
                        { categoryName="Ichimliklar" },
                    };
                    string data = JsonSerializer.Serialize(CategoriesModel);
                    file.Write(data);
                }
                using (StreamWriter file = new StreamWriter(MainPath + "Products.json"))
                {
                    List<Products> CategoriesModel = new List<Products>()
                    {
                        new Products()
                        {
                            name="BBQ Burger",
                            description="juda ham mazali",
                            price =12323.121,
                            category="Burgerlar"
                        },
                        new Products()
                        {
                            name="Turkey Burger",
                            description="Antiqa turkcha burger",
                            price =20500,
                            category="Burgerlar"
                        },
                        new Products()
                        {
                            name="Veggie Dog",
                            description="Toyimli va achiq",
                            price =18500.345,
                            category="Hot-Doglar"
                        },
                        new Products()
                        {
                            name="Hawaiian Dog",
                            description="Amerikani taniqli hotdogi",
                            price =25000.345,
                            category="Hot-Doglar"
                        },
                        new Products()
                        {
                            name="Chicken Lavash Wrap",
                            description="Tovu goshli lavash",
                            price =16000,
                            category="Lavashlar"
                        },
                        new Products()
                        {
                            name="Salad Lavash Wrap ",
                            description="Goshtsiz lavash",
                            price =8000.50,
                            category="Lavashlar"
                        },
                        new Products()
                        {
                            name="Mirinda",
                            description="Yengisi",
                            price =500.50,
                            category="Ichimliklar"
                        },
                        new Products()
                        {
                            name="HydroLife",
                            description="Tabiiy",
                            price =1000,
                            category="Ichimliklar"
                        },
                    };
                    string data = JsonSerializer.Serialize(CategoriesModel);
                    file.Write(data);
                }
                using (StreamWriter file = new StreamWriter(MainPath + "PayTypes.json"))
                {
                    List<PayType> payTypesModel = new List<PayType>()
                    {
                        new PayType()
                        { payType="Naqd" },
                        new PayType()
                        { payType="Click" },
                        new PayType()
                        { payType="PayMe" }
                    };
                    string data = JsonSerializer.Serialize(payTypesModel);
                    file.Write(data);
                }
                using (StreamWriter file = new StreamWriter(MainPath + "OrderStatus.json"))
                {
                    List<OrderStatus> OrderStatusModel = new List<OrderStatus>()
                    {
                        new OrderStatus()
                        { orderStatus="NEW" },
                        new OrderStatus()
                        { orderStatus="Completed" },
                        new OrderStatus()
                        { orderStatus="Delivered" }
                    };
                    string data = JsonSerializer.Serialize(OrderStatusModel);
                    file.Write(data);
                }
            }
        }

        public bool CreateJson(int[] CRUD,string message)
        {
            string data = "";
        //Category bosa
            if (CRUD[0]==1)
            {
                List<Categoies> AllData = new List<Categoies>();
                using(StreamReader file = new StreamReader(MainPath +"Categories.json"))
                {
                    data = file.ReadToEnd();
                }
                try
                {
                    AllData = JsonSerializer.Deserialize<List<Categoies>>(data)!;
                }catch { }
                Categoies Model = new Categoies()
                {
                    categoryName = message
                };
                AllData.Add(Model);
                data = JsonSerializer.Serialize(AllData);
                using (StreamWriter file =new StreamWriter(MainPath +"Categories.json"))
                {
                    file.Write(data);
                }
            }

        //Product bosa
            else if (CRUD[0]==2)
            {
                try
                {
                    List<Products> AllData = new List<Products>();
                    using (StreamReader file = new StreamReader(MainPath + "Products.json"))
                    {
                        data = file.ReadToEnd();
                    }
                    try
                    {
                        AllData = JsonSerializer.Deserialize<List<Products>>(data)!;
                    }
                    catch { }
                    string[] message1 = message.Split('|');
                    Products Model = new Products()
                    {
                        name = message1[0],
                        description = message1[1],
                        price = Convert.ToDouble(message1[2]),
                        category = message1[3],
                    };
                    AllData.Add(Model);
                    data = JsonSerializer.Serialize(AllData);
                    using (StreamWriter file = new StreamWriter(MainPath + "Products.json"))
                    {
                        file.Write(data);
                    }
                }
                catch { return false; }
            }
        //Pay Type bosa
            else if (CRUD[0]==3)
            {
                List<PayType> AllData = new List<PayType>();
                using (StreamReader file = new StreamReader(MainPath + "PayTypes.json"))
                {
                    data = file.ReadToEnd();
                }
                try
                {
                    AllData = JsonSerializer.Deserialize<List<PayType>>(data)!;
                }
                catch { }
                PayType Model = new PayType()
                {
                    payType = message,
                };
                AllData.Add(Model);
                data = JsonSerializer.Serialize(AllData);
                using (StreamWriter file = new StreamWriter(MainPath + "PayTypes.json"))
                {
                    file.Write(data);
                }
            }

        //OrerStaus bosa
            else if (CRUD[0]==4)
            {
                List<OrderStatus> AllData = new List<OrderStatus>();
                using (StreamReader file = new StreamReader(MainPath + "PayTypes.json"))
                {
                    data = file.ReadToEnd();
                }
                try
                {
                    AllData = JsonSerializer.Deserialize<List<OrderStatus>>(data)!;
                }
                catch { }
                OrderStatus Model = new OrderStatus()
                {
                    orderStatus = message,
                };
                AllData.Add(Model);
                data = JsonSerializer.Serialize(AllData);
                using (StreamWriter file = new StreamWriter(MainPath + "PayTypes.json"))
                {
                    file.Write(data);
                }
            }
            return true;
        }
        public string ReadJson(int[] CRUD)
        {
            //List<string> data= new List<string>();
            string export = "";
            string json = "";
            if (CRUD[0]==1)
            {
                List<Categoies> AllData = new List<Categoies>();
                using (StreamReader file = new StreamReader(MainPath + "Categories.json"))
                {
                    json = file.ReadToEnd();
                }
                try
                {
                    AllData = JsonSerializer.Deserialize<List<Categoies>>(json)!;
                }
                catch { }
                for (int i = 0;i<AllData.Count;i++)
                {
                    export+=$"{i+1}) "+AllData[i].categoryName+"\n";
                }
            }
            else if(CRUD[0]==2)
            {
                List<Products> AllData = new List<Products>();
                using (StreamReader file = new StreamReader(MainPath + "Products.json"))
                {
                    json = file.ReadToEnd();
                }
                try
                {
                    AllData = JsonSerializer.Deserialize<List<Products>>(json)!;
                }
                catch { }
                for(int i = 0; i<AllData.Count;i++)
                {
                    export += $"1) Nomi:         {AllData[i].name}\n";
                    export += $"   Tasnif:       {AllData[i].description}\n";
                    export += $"   Narx:         {AllData[i].price}\n";
                    export += $"   Categoriya:   {AllData[i].category}\n\n";
                }
            }
            else if (CRUD[0]==3)
            {
                List<PayType> AllData = new List<PayType>();
                using (StreamReader file = new StreamReader(MainPath + "PayTypes.json"))
                {
                    json = file.ReadToEnd();
                }
                try
                {
                    AllData = JsonSerializer.Deserialize<List<PayType>>(json)!;
                }
                catch { }
                for (int i =0; i<AllData.Count;)
                {
                    export += $"1) {AllData[i].payType}\n";
                }
            }
            return export;
        }

    }
}