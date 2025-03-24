using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebApiUseSqlLiteItog
{
    public class AuthOptions //Настройки токена
    {
        public const string ISSUER = "MyAuthServer"; // издатель токена
        public const string AUDIENCE = "MyAuthClient"; // потребитель токена
        public const int LIFETIME = 1;
        const string KEY = "mysupersecret_secretsecretsecretkey!123";   // ключ для шифрации
        public static SymmetricSecurityKey GetSymmetricSecurityKey() => //возврашение ключа безопасности
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
