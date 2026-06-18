using System;
using System.Windows.Forms;
using AgendaDeAtendimentos.Services;

namespace AgendaDeAtendimentos
{
    public partial class FormLogin : Form
    {
        // Instância do serviço de autenticação
        // Será responsável por validar login e senha no banco de dados
        private readonly AuthService _auth;

        public FormLogin()
        {
            InitializeComponent();

            // Cria uma instância do serviço de autenticação
            _auth = new AuthService();
            Estilo.Aplicar(this);
        }


        // Evento executado quando o botão Entrar é clicado
        private void btnEntrar_Click(object sender, EventArgs e)
        {
            Autenticar();
        }

        // Evento executado quando uma tecla é pressionada no campo senha
        private void txtSenha_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Se o usuário pressionar Enter, tenta realizar o login
            if (e.KeyChar == (char)Keys.Enter)
                Autenticar();
        }

        // Método responsável por validar o acesso do usuário
        private void Autenticar()
        {
            // Verifica se os campos de login e senha foram preenchidos
            if (string.IsNullOrWhiteSpace(txtLogin.Text) ||
                string.IsNullOrWhiteSpace(txtSenha.Text))
            {
                MessageBox.Show(
                    "Preencha login e senha.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // Desabilita o botão para evitar que o usuário clique várias vezes
            // enquanto a autenticação está sendo processada
            btnEntrar.Enabled = false;

            try
            {
                // Chama o serviço de autenticação para validar as credenciais
                bool sucesso = _auth.Login(
                    txtLogin.Text.Trim(),
                    txtSenha.Text);

                // Se o login for válido
                if (sucesso)
                {
                    // Define que o formulário foi finalizado com sucesso
                    this.DialogResult = DialogResult.OK;

                    // Fecha a tela de login
                    this.Close();
                }
                else
                {
                    // Exibe uma mensagem genérica por segurança
                    // Assim não informamos se o erro foi no login ou na senha
                    MessageBox.Show(
                        "Usuário ou senha incorretos.",
                        "Acesso Negado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    // Limpa apenas o campo senha
                    txtSenha.Clear();

                    // Retorna o foco para o campo senha
                    txtSenha.Focus();
                }
            }
            catch (Exception ex)
            {
                // Captura qualquer erro inesperado durante a autenticação
                MessageBox.Show(
                    $"Erro ao realizar login: {ex.Message}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                // Este bloco sempre será executado
                // independente de sucesso ou erro

                // Reabilita o botão Entrar
                btnEntrar.Enabled = true;
            }
        }

        // Evento criado automaticamente pelo Visual Studio
        // Atualmente não possui implementação
        private void label2_Click(object sender, EventArgs e)
        {
        }

        // Evento executado quando o formulário é carregado
        // Pode ser utilizado futuramente para inicializações da tela
        private void FormLogin_Load(object sender, EventArgs e)
        {
        }


    }

}