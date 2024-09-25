using LinkedIn.Models.Users;
using LinkedIn.Repository.IRepository;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LinkedIn.JWTFeatures
{
    public class JWTHandler
    {
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _jwtSettings;

        public JWTHandler(
            IConfiguration configuration)
        {
            _configuration = configuration;
            _jwtSettings = _configuration.GetSection("JWT");
        }

        public List<Claim> GetClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("Email", user.Email),
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName),
            };

            return claims;
        }

        public SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_jwtSettings.GetSection("Secret").Value);
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        public JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims, bool refresh = false)
        {
            var tokenOptions = new JwtSecurityToken(
                issuer: _jwtSettings["ValidIssuer"],
                audience: _jwtSettings["ValidAudience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(!refresh ? Convert.ToDouble(_jwtSettings["ExpiryInMinutes"]) : 525600),
                signingCredentials: signingCredentials);

            return tokenOptions;
        }

        public string GenerateJWTToken(User user)
        {
            var signingCredentials = GetSigningCredentials();
            var claims = GetClaims(user);
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return token;
        }

        public string GenerateJWTRefreshToken()
        {
            var signingCredentials = GetSigningCredentials();
            var refreshTokenOptions = GenerateTokenOptions(signingCredentials, new List<Claim>());
            var refreshToken = new  JwtSecurityTokenHandler().WriteToken(refreshTokenOptions);

            return refreshToken;
        }

        public ClaimsPrincipal DecodeJwtToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.GetSection("Secret").Value);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false, // Check if the token has expired
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtSettings["ValidIssuer"],
                ValidAudience = _jwtSettings["ValidAudience"],
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ClockSkew = TimeSpan.Zero // Optional: reduce clock skew to minimize delay in token expiration validation
            };

            try
            {
                // Validate the token and get the claims principal
                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

                // Additional validation if necessary
                if (validatedToken is JwtSecurityToken jwtToken && jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    return principal; // The token is valid, return the claims principal
                }
                else
                {
                    throw new SecurityTokenException("Invalid token");
                }
            }
            catch (Exception ex)
            {
                // Handle any validation failure
                throw new SecurityTokenException("Token validation failed", ex);
            }
        }
    }

    
}
