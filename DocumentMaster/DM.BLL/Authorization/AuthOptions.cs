using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.BLL.Authorization
{
    public class AuthOptions
    {
        public const string ISSUER = "SmirnovAuthServer"; // издатель токена
        public const string AUDIENCE = "AnyClient"; // потребитель токена
        const string KEY = "any_phrase_to_create_encryption_johnson";   // ключ для шифрации
        public const int LIFETIME = 240; // время жизни токена - 240 минут
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
