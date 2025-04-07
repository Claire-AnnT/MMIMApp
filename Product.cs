namespace MiniMartApp.Models
{
    public class Product
    {
        public string Name { get; set; }
        public string UID { get; set; }
        public int Units { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public int MinUnits { get; set; }
        public string Category { get; set; }
    }
}
