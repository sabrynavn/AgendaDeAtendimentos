using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AgendaDeAtendimentos.Models;
using AgendaDeAtendimentos.Repositories; //Da acesso à classe usuário que esta na pasta Models 

namespace AgendaDeAtendimentos
{
    public partial class FormLogin : Form
    {
        // 'static' significa que esta variável fica guardada na memória global do programa. Qualquer outro ecrã (como o Form1) poderá consultar 'FormLogin.UsuarioLogado'
        // para saber quem está a mexer no sistema e qual é o seu papel.

        // Instancia o repositório para podermos fazer consultas ao banco de dados
        private readonly UsuarioRepository _usuarioRepository = new UsuarioRepository();

        public static Usuario? UsuarioLogado { get; set; }
        public FormLogin()
        {
            InitializeComponent();
        }

        // Instancia o repositório para podermos fazer consultas ao banco de dados
        private readonly UsuarioRepository _usuarioRepository = new UsuarioRepository();

       

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            // Pega o texto digitado nos campos de texto
            string loginDigitado = txtLogin.Text;
            string senhaDigitada = txtSenha.Text;

            if (loginDigitado == "admin" && senhaDigitada == "admin123")
            {// Se for admin, criamos o objeto com a permissão máxima ("Admin")
                UsuarioLogado = new Usuario { Nome = "Administrador", Login = "admin", PapelNome = "Admin" };
            }
            else if (loginDigitado == "operador" && senhaDigitada == "123") // Se for operador, terá nível intermédio (faz CRUD, mas não mexe em acessos)
            {
                UsuarioLogado = new Usuario { Nome = "Operador", Login = "operador", PapelNome = "Operador" };
            }
            else if (loginDigitado == "visualizador" && senhaDigitada == "123") // Se for visualizador, o nível é de apenas leitura (bloqueia gravação)
            {
                UsuarioLogado = new Usuario { Nome = "Apenas Leitura", Login = "visualizador", PapelNome = "Visualizador" };
            }
            else // Se não acertar nenhuma combinação, exibe erro e interrompe o método com o 'return'
            {
                MessageBox.Show("Usuário ou Senha incorretos", "erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Se chegou aqui, significa que o login foi aceito
            // Define o DialogResult como OK para avisar o Program.cs que o ecrã principal pode abrir
            this.DialogResult = DialogResult.OK;

            this.Close(); //Fecha o ecrã Login de forma segura


        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
