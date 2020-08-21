using testJWT.Models;
using System;
using System.Security.Claims;
using testJWT.Managers;
using System.Collections.Generic;
using System.Linq;

namespace testJWT
{
    class Program
    {
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
        static void Main(string[] args) 
        {
            IAuthContainerModel model = GetJWTContainerModel("abc","lethienduc123@gmail.com");
            IAuthService authService = new JWTService(model.SecretKey);

            string token = authService.GenerateToken(model);

            if(!authService.IsTokenValid(token))
            {
                throw new UnauthorizedAccessException();
            }
            else
            {
                List<Claim> claims = authService.GetTokenClaims(token).ToList();

                Console.WriteLine(claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Name)).Value);
                Console.WriteLine(claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Email)).Value);
            }
        }
    }
}
