using System.Threading.Tasks;

namespace chargily.epay.csharp
{
    public interface IChargilyEpayClient<TResponse, TRequest>
    {
        Task<TResponse> CreatePayment(TRequest request);
    }
}