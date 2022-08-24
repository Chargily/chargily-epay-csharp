﻿using System.IO;

namespace Chargily.Epay
{
    public interface IWebHookValidator
    {
        bool Validate(string signature, string bodyJson);
        bool Validate(string signature, Stream body);
    }
}