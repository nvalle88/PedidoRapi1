using MvcPedidos.Entity.DTO;
using MvcPrestadores.Entity.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MvcPedidos.Services.Interface
{
    public interface IServiceZoho
    {
        Task<List<ReturnBizTypes>> GetBizTypes(int tryCallService);
        Task<List<ReturnSpeciality>> GetSpecialities(int idBizType, int tryCallService);
        Task<List<ReturnVendor>> GetVendorsBySpecialityAndCustomer(string specialityName,string customerId, int tryCallService);
        Task<List<ReturnItem>> GetItemsByVendor(string vendorId, int tryCallService);
        Task<bool> ProcessOrder(Order order, int tryCallService);
    }
}
