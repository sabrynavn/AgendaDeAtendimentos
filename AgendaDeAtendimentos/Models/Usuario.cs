using System;
using System.Collections.Generic;
using System.Text;

namespace AgendaDeAtendimentos.Models
{
    // A classe Usuario representa a tabela 'usuarios' do nosso banco de dados.
    // Ela serve para transportar os dados do utilizador logado por todo o sistema
    public class Usuario
    {
        // ID único do utilizador (será a Chave Primária no SQLite)
        public int Id { get; set; }

        // Nome completo do utilizador (ex: "Maria Silva")
        public string Nome { get; set; }

        // O apelido/username que ele digita para entrar (ex: "admin")
        public string Login { get; set; }

        // Nunca guardamos a senha real. Guardamos o HASH (a senha criptografada), o BCrypt vai transformar "admin123" num texto gigante e seguro (ex: "$2b$10$...")
        public string SenhaHash { get; set; }

        // Chave Estrangeira: liga o utilizador a um ID da tabela de Papéis
        public int PapelId { get; set; }

        // Propriedade auxiliar para facilitar: guarda o texto do papel ("Admin", "Operador")
        // Evita que tenhamos de fazer consultas complexas apenas para saber o nome do nível de acesso.
        public string PapelNome { get; set; }
    }
}
