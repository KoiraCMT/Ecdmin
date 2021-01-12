using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using Furion;
using Furion.Authorization;
using Furion.DataEncryption;

namespace Ecdmin.Application.Utils
{
    public class JwtUtil
    {
        /// <summary> 
        /// 生成jwt token
        /// </summary> 
        /// <param name="userId">用户id</param> 
        /// <param name="username">用户名</param> 
        /// <returns>成功返回jwt token字符串</returns> 
        public static string Generate(string userId, string username)
        {
            var jwtSettings = App.GetOptions<JWTSettingsOptions>();
            var dateTimeOffset = DateTimeOffset.UtcNow;
            return JWTEncryption.Encrypt(jwtSettings.IssuerSigningKey, new Dictionary<string, object>()
            {
                { "user_id", DESUtil.DESEncrypt(userId) },
                { "username", username },
                { JwtRegisteredClaimNames.Iat, dateTimeOffset.ToUnixTimeSeconds() },
                { JwtRegisteredClaimNames.Nbf, dateTimeOffset.ToUnixTimeSeconds() },
                { JwtRegisteredClaimNames.Exp, DateTimeOffset.UtcNow.AddSeconds(jwtSettings.ExpiredTime.Value * 60).ToUnixTimeSeconds() },
                { JwtRegisteredClaimNames.Iss, jwtSettings.ValidIssuer},
                { JwtRegisteredClaimNames.Aud, jwtSettings.ValidAudience }
            });
        }
    }
}