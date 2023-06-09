using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace WebApi.Token;

public class TokenJWTBuilder
{
    private SecurityKey securityKey = null;
    private string subject = "";
    private string issuer = ""; 
    private string audience = "";
    private Dictionary<string, string> claims = new Dictionary<string, string>();
    private int expiryInMinutes = 5;

    public TokenJWTBuilder AddSecurityKey(SecurityKey securityKey)
    {
        this.securityKey = securityKey;
        return this;
    }

    public TokenJWTBuilder AddSubject(string subject)
    {
        this.subject = subject;
        return this;
    }

    public TokenJWTBuilder AddIssuer(string issuer)
    {
        this.issuer = issuer;
        return this;
    }

    public TokenJWTBuilder AddAudience(string audience)
    {
        this.audience = audience;
        return this;
    }

    public TokenJWTBuilder AddClaim(string type, string value)
    {
        this.claims.Add(type, value);
        return this;
    }

    public TokenJWTBuilder AddExpiry(int expiryInMinutes)
    {
        this.expiryInMinutes = expiryInMinutes;
        return this;
    }


    // Validação do Token
    private void EnsureArguments()
    {
        if (this.securityKey == null)
            throw new ArgumentNullException("SecurityKee");

        if (string.IsNullOrEmpty(this.subject))
            throw new ArgumentNullException("Subject");

        if (string.IsNullOrEmpty(this.issuer))
            throw new ArgumentNullException("Issuer");

        if (string.IsNullOrEmpty(this.audience))
            throw new ArgumentNullException("Audience");        
    }

    
    // Criação do Token de autenticação
    public TokenJWT Builder()
    {
        // Chama a validação
        EnsureArguments();

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, this.subject),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        }.Union(this.claims.Select(item => new Claim(item.Key, item.Value)));

        var token = new JwtSecurityToken(
            issuer: this.issuer,
            audience: this.audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expiryInMinutes),
            signingCredentials: new SigningCredentials(
                                             this.securityKey,
                                             SecurityAlgorithms.HmacSha256)
            );

        return new TokenJWT(token);
    }
}
