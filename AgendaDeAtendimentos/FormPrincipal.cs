// ============================================================
// FormPrincipal.cs - A TELA PRINCIPAL DO SISTEMA
// ============================================================
//
// O QUE FAZ?
// Essa é a janela principal do programa. Ela tem 3 abas:
// 1. Clientes - cadastrar e listar clientes
// 2. Serviços - cadastrar e listar serviços
// 3. Agendamentos - agendar horários e mudar status
//
// COMO ELA SE CONECTA COM O BANCO?
// ANTES: usava Repositorio.Clientes (lista em memória - perdia tudo ao fechar)
// AGORA: usa os Services (ClienteService, ServicoService, AgendamentoService)
//         que chamam os Repositories que salvam no SQLite
//
// ARQUITETURA (CAMINHO DOS DADOS):
// FormPrincipal → Services → Repositories → SQLite (sistema.db)

using System;
using System.Collections.Generic;  // Para usar List<T>
using System.Windows.Forms;        // Para usar os controles (Button, TextBox, etc.)
using AgendaDeAtendimentos.Models;   // Para usar Cliente, Servico, Agendamento
using AgendaDeAtendimentos.Services; // Para usar ClienteService, etc.

namespace AgendaDeAtendimentos
{
    // "public partial class FormPrincipal" - partial significa que a classe
    // está dividida em DOIS arquivos:
    // - FormPrincipal.cs (a lógica - esse aqui)
    // - FormPrincipal.Designer.cs (os botões, textos, listas - criado pelo Visual Studio)
    // ": Form" = essa classe HERDA de Form (vira uma janela do Windows)
    public partial class FormPrincipal : Form
    {
        // --- VARIÁVEIS PRIVADAS (OS SERVICES) ---

        // Essas são as "pontes" para a camada de dados.
        // "private" = só essa classe pode usar.
        // "readonly" = depois de criado, não pode trocar.
        // "_clienteService" (underline) = convenção para variável privada.

        // Service responsável por lidar com clientes.
        private readonly ClienteService _clienteService;
        // Service responsável por lidar com serviços.
        private readonly ServicoService _servicoService;
        // Service responsável por lidar com agendamentos.
        private readonly AgendamentoService _agendamentoService;


        // --- CONSTRUTOR ---
        // Roda quando a tela principal é criada.
        public FormPrincipal()
        {
            // InitializeComponent() é chamado do FormPrincipal.Designer.cs.
            // Ele cria todos os botões, caixas de texto, listas, etc. na tela.
            InitializeComponent();

            // Cria os services que vão gerenciar os dados.
            // Cada Service internamente cria seu próprio Repository.
            _clienteService = new ClienteService();
            _servicoService = new ServicoService();
            _agendamentoService = new AgendamentoService();
        }


        // --- EVENTO: QUANDO A TELA CARREGA ---
        // Esse método roda AUTOMATICAMENTE quando a janela abre.
        // (quem chama ele é o "FormPrincipal_Load" registrado no designer)
        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            // Carrega os dados do banco e mostra nas listas da tela.
            CarregarClientes();
            CarregarServicos();
            CarregarAgendamentos();

            // Aplica as permissões do usuário (RBAC) - desabilita botões se necessário.
            AplicarRegrasSegurancaRBAC();
        }


        // --- BOTÃO: CADASTRAR CLIENTE ---
        // Roda quando o usuário clica em "Cadastrar" na aba Clientes.
        private void btnCadastrarCliente_Click(object sender, EventArgs e)
        {
            // Se o campo nome está vazio, não faz nada (volta).
            // Isso evita cadastrar cliente sem nome.
            if (string.IsNullOrWhiteSpace(txtNomeCliente.Text))
                return;

            // Cria um novo objeto Cliente com os dados da tela.
            // Os valores vêm das caixas de texto (txtNomeCliente, txtTelefone, txtEmail).
            var cliente = new Cliente(
                txtNomeCliente.Text,   // Nome digitado
                txtTelefone.Text,      // Telefone digitado
                txtEmail.Text          // Email digitado
            );

            // Salva o cliente no BANCO (via Service → Repository → SQLite).
            // Como o cliente.Id é 0 (acabou de ser criado), o Service vai chamar Inserir().
            _clienteService.Salvar(cliente);

            // Limpa os campos da tela para o próximo cadastro.
            txtNomeCliente.Clear();
            txtTelefone.Clear();
            txtEmail.Clear();

            // Atualiza a lista de clientes na tela (busca do banco de novo).
            CarregarClientes();
        }


        // --- BOTÃO: CADASTRAR SERVIÇO ---
        // Roda quando o usuário clica em "Cadastrar" na aba Serviços.
        private void btnCadastrarServico_Click(object sender, EventArgs e)
        {
            // Tenta converter o texto do preço para número decimal.
            // "decimal.TryParse" tenta converter. Se conseguir, guarda em "valor".
            // Se não conseguir (ex: usuário digitou letras), sai do método.
            // "out decimal valor" = o resultado sai por essa variável.
            if (!decimal.TryParse(txtPreco.Text, out decimal valor))
                return;

            // Cria um novo objeto Servico com nome e valor.
            var servico = new Servico(
                txtNomeServico.Text,  // Nome do serviço
                valor                 // Preço (já convertido para decimal)
            );

            // Salva o serviço no banco.
            _servicoService.Salvar(servico);

            // Limpa os campos.
            txtNomeServico.Clear();
            txtPreco.Clear();

            // Atualiza a lista de serviços na tela.
            CarregarServicos();
        }


        // --- BOTÃO: AGENDAR ---
        // Roda quando o usuário clica em "Agendar" na aba Agendamentos.
        private void btnAgendar_Click(object sender, EventArgs e)
        {
            // Verifica se o usuário selecionou um cliente E um serviço.
            // Se não selecionou, mostra um aviso e para.
            if (cmbCliente.SelectedItem == null ||
                cmbServico.SelectedItem == null)
            {
                MessageBox.Show(
                    "Selecione um cliente e um serviço."
                );
                return;
            }

            // Junta a data escolhida (dtpData) com a hora escolhida (dtpHora).
            // dtpData.Value.Date = só a data (sem hora)
            // dtpHora.Value.TimeOfDay = só a hora (sem data)
            // Somando os dois, temos data + hora completos.
            DateTime dataHora =
                dtpData.Value.Date +
                dtpHora.Value.TimeOfDay;

            // Cria um novo agendamento.
            // Os itens selecionados nos ComboBoxs são convertidos para Cliente e Servico.
            // (Cliente) = conversão forçada (CAST) porque SelectedItem é Object.
            var ag = new Agendamento(
                (Cliente)cmbCliente.SelectedItem,
                (Servico)cmbServico.SelectedItem,
                dataHora
            );

            // Salva o agendamento no banco.
            _agendamentoService.Salvar(ag);

            // Atualiza a lista de agendamentos na tela.
            CarregarAgendamentos();

            // Mostra uma mensagem de confirmação.
            MessageBox.Show("Agendamento criado!");
        }


        // --- EVENTO: SELECIONAR AGENDAMENTO NA LISTA ---
        // Roda quando o usuário clica em um agendamento na lista.
        private void listAgendamentos_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            // Verifica se o item selecionado é realmente um Agendamento.
            // "is Agendamento ag" = tenta converter e já guarda em "ag".
            if (listAgendamentos.SelectedItem is Agendamento ag)
            {
                // Se conseguiu, seleciona o status atual no ComboBox de status.
                // Isso permite que o usuário veja o status e mude se quiser.
                cmbStatus.SelectedItem = ag.Status;
            }
        }


        // --- BOTÃO: ATUALIZAR STATUS ---
        // Roda quando o usuário clica em "Atualizar Status".
        private void btnAtualizarStatus_Click(
            object sender,
            EventArgs e)
        {
            // Verifica se tem um agendamento selecionado na lista.
            // "is not Agendamento ag" = se NÃO for um Agendamento, mostra erro.
            if (listAgendamentos.SelectedItem is not Agendamento ag)
            {
                MessageBox.Show(
                    "Selecione um agendamento na lista."
                );
                return;
            }

            // Verifica se tem um status selecionado no ComboBox.
            if (cmbStatus.SelectedItem is not string novoStatus)
                return;

            // Muda o status no objeto em memória.
            ag.AlterarStatus(novoStatus);

            // Salva a mudança no banco (via Service → Repository).
            _agendamentoService.AtualizarStatus(ag.Id, ag.Status);

            // Atualiza a lista na tela.
            CarregarAgendamentos();
        }


        // --- CARREGAR CLIENTES ---
        // Busca todos os clientes do BANCO e mostra na tela.
        // Isso roda toda vez que a lista precisa ser atualizada.
        private void CarregarClientes()
        {
            // Chama o service que busca no banco.
            var clientes = _clienteService.ListarTodos();

            // Atualiza o ComboBox de clientes (usado na aba Agendamentos).
            cmbCliente.DataSource = null;                   // Limpa
            cmbCliente.DataSource = new List<Cliente>(clientes); // Recarrega

            // Atualiza a lista visual de clientes (aba Clientes).
            listClientes.DataSource = null;
            listClientes.DataSource = new List<Cliente>(clientes);
        }


        // --- CARREGAR SERVIÇOS ---
        // Busca todos os serviços do BANCO e mostra na tela.
        private void CarregarServicos()
        {
            var servicos = _servicoService.ListarTodos();

            // Atualiza o ComboBox de serviços.
            cmbServico.DataSource = null;
            cmbServico.DataSource = new List<Servico>(servicos);

            // Atualiza a lista visual de serviços.
            listServicos.DataSource = null;
            listServicos.DataSource = new List<Servico>(servicos);
        }


        // --- CARREGAR AGENDAMENTOS ---
        // Busca todos os agendamentos do BANCO e mostra na tela.
        private void CarregarAgendamentos()
        {
            var agendamentos = _agendamentoService.ListarTodos();

            listAgendamentos.DataSource = null;
            listAgendamentos.DataSource = new List<Agendamento>(agendamentos);
        }


        // --- RBAC - CONTROLE DE ACESSO ---
        // RBAC = Role-Based Access Control
        // Isso define o que cada usuário pode fazer baseado no papel dele.
        //
        // 3 PAPÉIS:
        // - Visualizador: só olha, não mexe em nada
        // - Operador: pode cadastrar e alterar
        // - Admin: pode tudo
        private void AplicarRegrasSegurancaRBAC()
        {
            // Pega o usuário que está logado no momento.
            // AuthService.UsuarioAtual é definido quando o login é feito.
            var usuario = AuthService.UsuarioAtual;

            // Se não tem usuário logado ou não tem papel, assume "Visualizador".
            // "??" = operador de coalescência: se for nulo, usa o valor da direita.
            string papel =
                usuario?.Papel?.Nome ?? "Visualizador";

            // Mostra no título da janela quem está logado e qual o papel.
            this.Text +=
                $" | Usuário: {usuario?.Login} ({papel})";

            // --- SE FOR VISUALIZADOR ---
            // Só pode VER os dados, não pode criar ou alterar nada.
            if (papel == "Visualizador")
            {
                // Desabilita (fica cinza) todos os botões de ação.
                btnCadastrarCliente.Enabled = false;
                btnCadastrarServico.Enabled = false;
                btnAgendar.Enabled = false;
                btnAtualizarStatus.Enabled = false;

                // Remove os eventos de clique - segurança extra.
                // Mesmo se alguém tentar clicar, não vai funcionar.
                btnCadastrarCliente.Click -= btnCadastrarCliente_Click;
                btnCadastrarServico.Click -= btnCadastrarServico_Click;

                // Avisa o usuário que está em modo só leitura.
                MessageBox.Show(
                    "Modo somente leitura.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }

            // --- SE FOR OPERADOR ---
            // Pode cadastrar e alterar (permissão total no dia a dia).
            else if (papel == "Operador")
            {
                btnCadastrarCliente.Enabled = true;
                btnCadastrarServico.Enabled = true;
                btnAgendar.Enabled = true;
                btnAtualizarStatus.Enabled = true;
            }

            // --- SE FOR ADMIN ---
            // Não precisa mexer em nada porque todos os botões já vêm habilitados
            // por padrão. O admin tem controle total.
        }
    }
}
