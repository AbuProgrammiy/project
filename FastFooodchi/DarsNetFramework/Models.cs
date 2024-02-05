namespace Models
{
    public class UsersInfo
    {
        public string firstName {  get; set; }
        public string lastName { get; set; }
        public long chatId  { get; set; }
        public string phoneNumber {get; set; }
    }
    public class Categoies
    {
        public string categoryName { get; set; }
    }
    public class Products
    {
        public string name { get; set; }
        public string description { get; set; }
        public double price { get; set; }
        public string category { get; set; }
    }
    public class PayType
    {
        public string payType { get; set; }
    }
    public class OrderStatus
    {
        public string orderStatus { get; set; }
    }
}