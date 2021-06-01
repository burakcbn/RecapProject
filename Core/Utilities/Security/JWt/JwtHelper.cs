using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utilities.Security.Encriyption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Core.Utilities.Security.JWt
{
    public class JwtHelper : ITokenHelper
    {
        public IConfiguration Configuration { get; }
        TokenOptions _tokenOptions;
        DateTime _accessTokenExpiration;

        public JwtHelper(IConfiguration configuration)
        {
           /* Configuration = configuration;
            _tokenOptions.Audience = Configuration["Audience"];
            _tokenOptions.Issuer = Configuration["Issuer"];
            _tokenOptions.AccessTokenExpiration = Convert.ToInt32(Configuration["AccessTokenExpiration"]);
            _tokenOptions.SecurityKey = Configuration["SecurityKey"];*/
        }

        public AccessToken CreateToken(User user, List<OperationClaim> operationClaims)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var signingCredantials = SigningCredantialsHelper.CreateSigningCredantialKey(securityKey);
            var jwt = CreateSecurityToken(user, operationClaims, _tokenOptions, signingCredantials);
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new AccessToken()
            {
                Expiration = _accessTokenExpiration,
                Token = token
            };
        }

        private JwtSecurityToken CreateSecurityToken(User user, List<OperationClaim> operationClaims,
            TokenOptions _tokenOptions, SigningCredentials SigningCredantials)
        {
            var jwt = new JwtSecurityToken(
                issuer: _tokenOptions.Issuer,
                audience: _tokenOptions.Audience,
                claims: SetClaims(user, operationClaims),
                notBefore: DateTime.Now,
                expires: _accessTokenExpiration,
                signingCredentials: SigningCredantials);
            return jwt;
        }

        private IEnumerable<Claim> SetClaims(User user, List<OperationClaim> operationClaims)
        {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddEmail(user.Email);
            claims.AddName($"{user.FirstName} {user.LastName}");
            claims.AddRoles(operationClaims.Select(c => c.Name).ToArray());

            return claims;
        }
    }
}
