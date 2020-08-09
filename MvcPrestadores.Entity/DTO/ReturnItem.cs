using System.ComponentModel.DataAnnotations;

namespace MvcPedidos.Entity.DTO
{
    public class ReturnItem
    {
        public string itemId { get; set; }
        public string vendorId { get; set; }
        public string itemDescription { get; set; }
        public string sku { get; set; }
        public object vendorSku { get; set; }
        public string category { get; set; }
        public bool isActive { get; set; }
        public string price { get; set; }
        public string taxPercent { get; set; }
        public object commentsForAgent { get; set; }

    }
}
