using System.IO;

namespace chargily.epay.csharp
{
    public interface IWebHookValidator
    {
        bool Validate(string signature, string bodyJson);
        bool Validate(string signature, Stream body);
    }
}