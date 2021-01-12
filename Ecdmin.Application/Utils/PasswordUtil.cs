using Ecdmin.Core.Entities.Admin;
using Microsoft.AspNetCore.Identity;

namespace Ecdmin.Application.Utils
{
    public static class PasswordUtil
    {
        public static string HashPassword<T>(T data, string password) where T : class
        {
            var passwordHasher = new PasswordHasher<T>();
            return passwordHasher.HashPassword(data, password);
        }

        public static bool VerifyHashedPassword<T>(T data, string hashedPassword, string providedPassword) where T : class
        {
            var passwordHasher = new PasswordHasher<T>();
            var passwordVerificationResult = passwordHasher.VerifyHashedPassword(data, hashedPassword, providedPassword);
            return passwordVerificationResult == PasswordVerificationResult.Success;
        }
    }
}