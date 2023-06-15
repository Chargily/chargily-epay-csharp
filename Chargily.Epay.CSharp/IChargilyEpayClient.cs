using System.Threading.Tasks;

namespace Chargily.Epay.CSharp
{
    public interface IChargilyEpayClient<TResponse, TRequest>
    {
        Task<TResponse> CreatePayment(TRequest request);
    }
}