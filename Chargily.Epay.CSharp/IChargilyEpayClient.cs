using System.Threading.Tasks;

namespace Chargily.Epay
{
    public interface IChargilyEpayClient<TResponse, TRequest>
    {
        Task<TResponse> CreatePayment(TRequest request);
    }
}