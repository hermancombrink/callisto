using System;

namespace Callisto.SharedKernel
{
    /// <summary>
    /// Defines the <see cref="Crypto" />
    /// </summary>
    public static class Crypto
    {
        /// <summary>
        /// The GetStringSha256Hash
        /// </summary>
        /// <param name="text">The <see cref="string"/></param>
        /// <returns>The <see cref="string"/></returns>
        public static string GetStringSha256Hash(string text, string salt = "")
        {
            if (String.IsNullOrEmpty(text))
                return String.Empty;

            using (var sha = new System.Security.Cryptography.SHA256Managed())
            {
                byte[] textData = System.Text.Encoding.UTF8.GetBytes($"{text}{salt}");
                byte[] hash = sha.ComputeHash(textData);
                return BitConverter.ToString(hash).Replace("-", String.Empty);
            }
        }
    }
}
