using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ApiServer.Config
{
    public class AuthOptions
    {
        public const string ISSUER = "DanisApiServer"; // издатель токена
        public const string AUDIENCE = "http://localhost/"; // потребитель токена
        const string KEY = "mxl3.kpoY2H4T5nDIdXI5IKTprui2escsCMNiUm5%}m(Uoki^/JqGd@0)<-,kx";   // ключ для шифрации
        public const int LIFETIME = 10; // время жизни токена - 10 минута
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
