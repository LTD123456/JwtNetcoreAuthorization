using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace testJWT.Models
{
    public class JWTContainerModel : IAuthContainerModel
    {
        public string SecretKey { get; set; } = "adakldjksaldjksajdkaljdksadjksahdjsahdjksahdjk";
        public string SecurityAlgorithm { get; set; } = SecurityAlgorithms.HmacSha256Signature;
        public int ExpireMinutes { get; set; } = 10880;
        public Claim[] Claims { get; set; }
    }
}
