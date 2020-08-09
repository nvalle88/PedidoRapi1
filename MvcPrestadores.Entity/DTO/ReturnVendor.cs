using System.ComponentModel.DataAnnotations;

namespace MvcPedidos.Entity.DTO
{

    public class ReturnVendor
    {
        public string VedorId { get; set; }
        public string VendorName { get; set; }

        [DisplayFormat(DataFormatString = "{0:#.####}")]
        public string DeliveryCost { get; set; }
    }

}
