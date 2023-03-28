using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Notificacoes;

public class Notifica
{
    // Construtor da List, toda vez que chamar, ele cria uma list 
    public Notifica()
    {
        notificacoes = new List<Notifica>();    
    }

    [NotMapped]
    public string NomePropriedade { get; set; }

    [NotMapped]
    public string mensagem { get; set; }

    [NotMapped]
    public List<Notifica> notificacoes;

    // Metodo para verificar se foi digitado 
    public bool ValidarPropriedadeString(string valor, string nomePropriedade)
    {
        if (string.IsNullOrWhiteSpace(valor) || string.IsNullOrWhiteSpace(nomePropriedade))
        {
            notificacoes.Add(new Notifica
            {
                mensagem = "Campo Obrigatório",
                NomePropriedade = nomePropriedade
            });

            return false;
        }

        return true;   
    }

    // Metodo que valida inteiros
    public bool ValidarPropriedadeInt(int valor, string nomePropriedade)
    {
        if (valor < 1 || string.IsNullOrWhiteSpace(nomePropriedade))
        {
            notificacoes.Add(new Notifica
            {
                mensagem = "Campo Obrigatório",
                NomePropriedade = nomePropriedade
            }); 

            return false;
        }

        return true;
    }
}
