namespace PoC_Demo.Model
{
    public class Property
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string SearchCode { get; set; }
        public int SellerId { get; set; }
        public string Place { get; set; }
        public string State { get; set; }
        public string Image { get; set; }
        public string Country { get; set; }
        public string PropertyType { get; set; }
        public decimal Area { get; set; }
        public int Bedrooms { get; set; }
        public string Facilities { get; set; }
        public decimal Amount { get; set; }
        public DateTime DatePosted { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
