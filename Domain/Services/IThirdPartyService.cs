using System;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IThirdPartyService
    {
        Task<string> SendRequestToThirdParty(Guid orderId);
    }
}
