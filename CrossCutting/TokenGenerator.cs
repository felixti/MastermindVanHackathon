using System;

namespace MastermindVanHackathon.CrossCutting
{
    public static class TokenGenerator
    {
        public static string GenerateToken()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        }
    }
}