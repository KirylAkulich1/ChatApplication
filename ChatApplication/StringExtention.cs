using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using ChatApplication.Messages;

namespace ChatApplication
{
    public static  class StringExtention
    {
        public static string MD5Hash(this string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

        public static byte[] ToByteArray(this string self)
        {
            return Encoding.Unicode.GetBytes(self);
        }

        public static Message ToMessage(this string self)
        {
            return JsonSerializer.Deserialize<Message>(self);
        }
    }
}