using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace DRInformationSystem.Auth;

public class AuthOptions
{
	public const string AggregatorRole = "aggregator";
	public const string ConsumerRole = "consumer";
	public const string ISSUER = "DRSystemServer"; // издатель токена
	public const string AUDIENCE = "DRSystemClient"; // потребитель токена
	const string KEY = "system_!2022key()CrypT";   // ключ для шифрации
	public const int LIFETIME = 24*60; // время жизни токена - 1 минута
	public static SymmetricSecurityKey GetSymmetricSecurityKey()
	{
		return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
	}
}