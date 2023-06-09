using System.IdentityModel.Tokens.Jwt;

namespace WebApi.Token;

public class TokenJWT
{
    // Propriedade privada
    private JwtSecurityToken token;

    // Acesso somente através do construtor da classe
    internal TokenJWT(JwtSecurityToken token)
    {
        this.token = token;
    }

    // Propriedade de tempo de validação, tempo que o Token vai permanecer válido
    public DateTime ValidTo => token.ValidTo;

    public string value => new JwtSecurityTokenHandler().WriteToken(this.token);
}
