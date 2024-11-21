using System.Text;
using Application.Configuration.Options;
using Microsoft.IdentityModel.Tokens;

namespace Api;

public static class TokenValidationParametersFactory
{
    public static TokenValidationParameters AccessValidationParameters(JwtOptions jwtOptions)
    {
        return new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true, // Crucial for lifetime validation
            ClockSkew = TimeSpan.Zero, // Crucial for lifetime validation
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtOptions.Issuer,
            ValidAudience = jwtOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret)),
        };
    }
}