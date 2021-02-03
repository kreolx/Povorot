using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Povorot.Api.Options
{
    public class AuthOptions
    {
        public string Issuer { get; set; }
        public string Key { get; set; }
        public string Audience { get; set; }
        public int Expires { get; set; }

        public SymmetricSecurityKey GetKey() => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
    }
}