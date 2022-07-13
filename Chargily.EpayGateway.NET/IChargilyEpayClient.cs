using System.Threading.Tasks;

namespace Chargily.EpayGateway.NET
{
    public interface IChargilyEpayClient<TResponse, TRequest>
    {
        Task<TResponse> CreatePayment(TRequest request);
    }
}