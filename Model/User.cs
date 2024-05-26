namespace PoC_Demo.Model
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
    public class Favorite
    {
        public int FavoriteId { get; set; }
        public int BuyerId { get; set; }
        public int SellerId { get; set; }
        public int PropertyId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
    public class Interest
    {
        public int InterestId { get; set; }
        public int BuyerId { get; set; }
        public int SellerId { get; set; }
        public int PropertyId { get; set; }
        public DateTime InterestDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
