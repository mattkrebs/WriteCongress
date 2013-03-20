using System;
using System.Security.Cryptography;

///This came from jspaur (he pulled this from his core libs he uses for a variety of projects including trypaper
namespace WriteCongress.Core
{
    public enum StringEncodingFormat
    {
        Base64 = 1,
        Hexadecimal = 2
    }
    public static class CryptoHelper
    {

        /// <summary>
        /// Generates a random string using the Randon Number Generator Crypto Service
        /// </summary>
        /// <param name="length">The lenght of string to generate (Default = 32)</param>
        /// <param name="format">The format of the output string Default = Base64)</param>
        /// <returns></returns>
        public static string GenerateRandomString(int length = 32, StringEncodingFormat format = StringEncodingFormat.Base64)
        {
            var data = new byte[length];
            var rng = RNGCryptoServiceProvider.Create();
            rng.GetNonZeroBytes(data);

            if (format == StringEncodingFormat.Base64) {
                return Convert.ToBase64String(data).Left(length);
            }
            else {
                return data.AsHexidecimal().Left(32);
            }
        }
        public static string HMACObject(object o, string key, StringEncodingFormat format = StringEncodingFormat.Base64)
        {
            return HMACObject(o.ToString(), key, format);
        }
        public static string HMACObject(string s, string key, StringEncodingFormat format = StringEncodingFormat.Base64)
        {
            var bytes = System.Text.Encoding.Default.GetBytes(s);
            var keyBytes = System.Text.Encoding.Default.GetBytes(key);
            var hmac = new HMACSHA1(keyBytes);
            if (format == StringEncodingFormat.Base64)
            {
                return Convert.ToBase64String(hmac.ComputeHash(bytes));
            }
            else
            {
                return hmac.ComputeHash(bytes).AsHexidecimal();
            }
        }
        public static string HashAndSalt(string value, string salt, int iterations = 1776, int returnLength = 64)
        {
            Rfc2898DeriveBytes db = new Rfc2898DeriveBytes(value, System.Text.Encoding.Default.GetBytes(salt), iterations);
            return System.Convert.ToBase64String(db.GetBytes(returnLength)).Left(returnLength);
        }
    }
}
