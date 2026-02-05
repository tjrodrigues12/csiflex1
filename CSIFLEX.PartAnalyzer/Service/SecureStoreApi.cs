using System;
using System.Text;
using System.Security.Cryptography;

namespace CSIFLEX.PartAnalyzer.Service
{
    public static class SecureStoreApi
    {
        private static readonly byte[] entropy = { 3, 5, 6, 2, 5, 6, 2, 4 };

        public static string TryEncrypt(string text)
        {
            if (text.HasValue())
            {
                var originalText = Encoding.Unicode.GetBytes(text);

                var encryptedText = ProtectedData.Protect(originalText, entropy, DataProtectionScope.CurrentUser);
                return Convert.ToBase64String(encryptedText);
            }
            return string.Empty;
        }

        public static string TryDecrypt(string text)
        {
            if (text.HasValue())
            {
                var encryptedText = Convert.FromBase64String(text);
                var originalText = ProtectedData.Unprotect(encryptedText, entropy, DataProtectionScope.CurrentUser);
                return Encoding.Unicode.GetString(originalText);
            }
            return string.Empty;
        }
    }
}
