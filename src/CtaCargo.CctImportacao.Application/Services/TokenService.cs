using CtaCargo.CctImportacao.Application.Services.Contracts;
using CtaCargo.CctImportacao.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CtaCargo.CctImportacao.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(Usuario user)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var jwtSettingsSection = _configuration.GetSection("TokenJwtSettings");

            int intDias = Convert.ToInt32(jwtSettingsSection.GetSection("ExpiracaoEmHoras").Value);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtSettingsSection.GetSection("Secret").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Nome),
                    new Claim(ClaimTypes.Email, user.EMail),
                    new Claim("CompanyId", user.EmpresaId.ToString()),
                    new Claim("UserId", user.Id.ToString()),
                    new Claim("Environment", environment)
                }),
                Expires = DateTime.UtcNow.AddHours(intDias),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            if (user.AcessaCiasAereas)
                tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, "AdminCiaAerea"));
            if (user.AcessaClientes)
                tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, "AdminClientes"));
            if (user.AcessaUsuarios)
                tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, "AdminUsuarios"));
            if (user.AlteraCia)
                tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, "AlteraCia"));

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
