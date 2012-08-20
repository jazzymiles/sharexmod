using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelpersLib.Cryptography
{
    public static class CryptHelper
    {
        public static string Encrypt(string text)
        {
            return new CryptKeys().Encrypt(text);
        }

        public static string Decrypt(string text)
        {
            return new CryptKeys().Decrypt(text);
        }
    }
}