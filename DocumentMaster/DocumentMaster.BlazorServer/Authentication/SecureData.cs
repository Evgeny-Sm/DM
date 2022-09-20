using System.Security.Cryptography;
using System.Text;

namespace DocumentMaster.BlazorServer.Authentication
{
    public class SecureData
    {
        public const int DataSize= 32;
        public static string GetHashData(string value)
        {
            var data=Encoding.UTF8.GetBytes(value);
            using (SHA512 shaM = new SHA512Managed())
            {
                var hash = shaM.ComputeHash(data);

                var hashedInputStringBuilder = new System.Text.StringBuilder(128);

                foreach (var b in hash)

                    hashedInputStringBuilder.Append(b.ToString("X2"));

                return hashedInputStringBuilder.ToString();
            }


        }

    }
}
