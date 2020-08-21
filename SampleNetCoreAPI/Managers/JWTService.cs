﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.IdentityModel.Tokens;
using SampleNetCoreAPI.Models;

namespace SampleNetCoreAPI.Managers
{
    public class JWTService : IAuthService
    {
        public JWTService(string secretKey)
        {
            this.secretKey = secretKey;
        }

        public string secretKey { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string GenerateToken(IAuthContainerModel model)
        {
            if (model == null || model.Claims == null || model.Claims.Length == 0)
            {
                throw new ArgumentException("Argument to create token are not valid.");
            }
            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(model.Claims),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(model.ExpireMinutes)),
                SigningCredentials = new SigningCredentials(GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
            };

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            string token = jwtSecurityTokenHandler.WriteToken(securityToken);

            return token;
        }

        public IEnumerable<Claim> GetTokenClaims(string token)
        {
            if (String.IsNullOrEmpty(token))
            {
                throw new ArgumentException("Given token null or empty.");
            }
            TokenValidationParameters tokenValidationParameters = GetTokenValidationParameters();

            JwtSecurityTokenHandler JSTH = new JwtSecurityTokenHandler();
            try
            {
                ClaimsPrincipal tokenValid = JSTH.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
                return tokenValid.Claims;
            }
            catch (Exception ex)
            {
                throw ex;
            };
        }

        public bool IsTokenValid(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentException("Given token is null or empty");
            }
            TokenValidationParameters tokenValidationParameters = GetTokenValidationParameters();
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            try
            {
                ClaimsPrincipal tokenValid = jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private SecurityKey GetSymmetricSecurityKey()
        {
            byte[] symmetricKey = Convert.FromBase64String(this.secretKey);
            return new SymmetricSecurityKey(symmetricKey);
        }
        private TokenValidationParameters GetTokenValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = GetSymmetricSecurityKey()
            };
        }
    }
}
