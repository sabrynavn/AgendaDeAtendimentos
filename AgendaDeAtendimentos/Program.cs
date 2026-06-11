// ========================================
// Program.cs — Ponto de entrada do sistema
// ========================================

// "namespace" é como uma pasta virtual que organiza as classes
namespace AgendaDeAtendimentos
{
    // "static" significa que essa classe não precisa ser instanciada com "new"
    // "internal" quer dizer que só pode ser usada dentro deste projeto
    internal static class Program
    {
        /// <summary>
        /// Este é o ponto de entrada da aplicação.
        /// Tudo começa aqui quando o programa é executado.
        /// </summary>
        
        // [STAThread] é uma regra obrigatória para programas Windows Forms
        // Ela diz ao Windows como lidar com a interface gráfica
        [STAThread]
        static void Main()
        {
            // ApplicationConfiguration.Initialize() prepara o visual do app
            // (coisas como fonte, DPI, etc.) baseado nas configurações do Windows
            ApplicationConfiguration.Initialize();

            // Application.Run() inicia a janela principal do programa
            // Tudo que estiver dentro do FormPrincipal vai aparecer na tela
            // Enquanto essa janela estiver aberta, o programa continua rodando
            Application.Run(new FormPrincipal());
        }
    }
}
