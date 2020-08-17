namespace MvcPrestadores.Entity.DTO
{
    public class OrderDetails
    {
        public string itemId { get; set; }
        public string vendorId { get; set; }
        public int count { get; set; }
        public string description { get; set; }
    }
}
