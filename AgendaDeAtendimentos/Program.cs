namespace AgendaDeAtendimentos
{
    internal static class Program
    {
        // Indica que a aplicação utilizará o modelo Single Thread Apartment (STA)
        // Necessário para diversos componentes do Windows Forms funcionarem corretamente
        [STAThread]
        static void Main()
        {
            // Inicializa o banco de dados antes de abrir qualquer tela do sistema
            // Caso o arquivo do banco não exista, ele será criado
            // Também cria as tabelas e insere os dados iniciais necessários
            AgendaDeAtendimentos.Data.DatabaseConfig.InicializarBanco();

            // Configurações padrão da aplicação Windows Forms
            // Responsável por inicializar recursos visuais e configurações da aplicação
            ApplicationConfiguration.Initialize();

            // Cria uma instância da tela de login
            // Essa será a primeira tela exibida ao usuário
            var login = new FormLogin();

            // Exibe a tela de login como uma janela modal
            // O código fica aguardando até que o usuário feche a tela
            if (login.ShowDialog() == DialogResult.OK)
            {
                // Se o login for realizado com sucesso,
                // abre a tela principal do sistema
                Application.Run(new FormPrincipal());
            }

            // Caso o login seja cancelado ou inválido,
            // a aplicação será encerrada sem abrir o sistema
        }
    }
}