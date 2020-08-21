using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SampleNetCoreAPI.Models
{
    public class JWTContainerModel : IAuthContainerModel
    {
        public static IConfiguration Intance;
        public string SecretKey
        {
            get
            {
                var appsettings = Intance.GetSection("Appsettings");
                return appsettings["SecretKey"];
            }
        }
        public string SecurityAlgorithm { get; set; } = SecurityAlgorithms.HmacSha256Signature;
        public int ExpireMinutes { get; set; } = 10880;
        public Claim[] Claims { get; set; }

        private static JWTContainerModel GetJWTContainerModel(string name, string email)
        {
            return new JWTContainerModel()
            {
                Claims = new Claim[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Name, name)
                }
            };
        }
    }
}
