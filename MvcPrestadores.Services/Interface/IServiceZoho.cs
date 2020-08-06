using MvcPedidos.Entity.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MvcPedidos.Services.Interface
{
    public interface IServiceZoho
    {
        Task<List<ReturnBizTypes>> GetBizTypes(int tryCallService = 1);
        Task<List<ReturnSpeciality>> GetSpecialities(int idBizType, int tryCallService = 1);
        Task<List<ReturnVendor>> GetVendorsBySpecialityAndCustomer(string specialityName,string customerId, int tryCallService = 1);
        Task<List<ReturnItem>> GetItemsByVendor(string vendorId, int tryCallService = 1);
    }
}
