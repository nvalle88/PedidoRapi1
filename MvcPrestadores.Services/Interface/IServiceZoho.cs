using MvcPrestadores.Entity.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MvcPrestadores.Services.Interface
{
    public interface IServiceZoho
    {
        Task<List<ReturnBizTypes>> GetBizTypes(int tryCallService = 1);
        Task<List<ReturnSpeciality>> GetSpecialities(int idBizType, int tryCallService = 1);
    }
}
