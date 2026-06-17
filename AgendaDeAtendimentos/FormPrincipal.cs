using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AgendaDeAtendimentos.Services;

namespace AgendaDeAtendimentos
{
    public partial class FormPrincipal : Form
    {
        // Construtor da tela principal
        // É executado quando o formulário é criado
        public FormPrincipal()
        {
            InitializeComponent();
        }

        // Evento executado quando a tela é carregada
        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            // Carrega os dados existentes para os componentes da tela
            CarregarClientes();
            CarregarServicos();
            CarregarAgendamentos();

            // Aplica as permissões do usuário logado
            AplicarRegrasSegurancaRBAC();
        }

        // Evento executado ao clicar no botão de cadastrar cliente
        private void btnCadastrarCliente_Click(object sender, EventArgs e)
        {
            // Verifica se o nome foi preenchido
            if (string.IsNullOrWhiteSpace(txtNomeCliente.Text))
                return;

            // Cria um novo objeto Cliente utilizando os dados digitados
            var cliente = new Cliente(
                txtNomeCliente.Text,
                txtTelefone.Text
            );

            // Adiciona o cliente ao repositório em memória
            Repositorio.Clientes.Add(cliente);

            // Limpa os campos após o cadastro
            txtNomeCliente.Clear();
            txtTelefone.Clear();

            // Atualiza as listas da tela
            CarregarClientes();
        }

        // Evento executado ao clicar no botão de cadastrar serviço
        private void btnCadastrarServico_Click(object sender, EventArgs e)
        {
            // Tenta converter o texto digitado para decimal
            // Se não conseguir, interrompe a execução
            if (!decimal.TryParse(txtPreco.Text, out decimal preco))
                return;

            // Cria um novo serviço
            var servico = new Servico(
                txtNomeServico.Text,
                preco
            );

            // Adiciona o serviço à lista
            Repositorio.Servicos.Add(servico);

            // Limpa os campos
            txtNomeServico.Clear();
            txtPreco.Clear();

            // Atualiza as listas exibidas
            CarregarServicos();
        }

        // Evento executado ao clicar no botão Agendar
        private void btnAgendar_Click(object sender, EventArgs e)
        {
            // Verifica se cliente e serviço foram selecionados
            if (cmbCliente.SelectedItem == null ||
                cmbServico.SelectedItem == null)
            {
                MessageBox.Show(
                    "Selecione um cliente e um serviço."
                );
                return;
            }

            // Junta a data escolhida com o horário escolhido
            DateTime dataHora =
                dtpData.Value.Date +
                dtpHora.Value.TimeOfDay;

            // Cria um novo agendamento
            var ag = new Agendamento(
                (Cliente)cmbCliente.SelectedItem,
                (Servico)cmbServico.SelectedItem,
                dataHora
            );

            // Adiciona o agendamento ao repositório
            Repositorio.Agendamentos.Add(ag);

            // Atualiza a lista de agendamentos
            CarregarAgendamentos();

            MessageBox.Show("Agendamento criado!");
        }

        // Evento executado quando um item da lista é selecionado
        private void listAgendamentos_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            // Verifica se o item selecionado é um agendamento
            if (listAgendamentos.SelectedItem is Agendamento ag)
            {
                // Atualiza o ComboBox com o status atual do agendamento
                cmbStatus.SelectedItem = ag.Status;
            }
        }

        // Evento executado ao clicar no botão Atualizar Status
        private void btnAtualizarStatus_Click(
            object sender,
            EventArgs e)
        {
            // Verifica se existe um agendamento selecionado
            if (listAgendamentos.SelectedItem is not Agendamento ag)
            {
                MessageBox.Show(
                    "Selecione um agendamento na lista."
                );
                return;
            }

            // Obtém o novo status selecionado
            if (cmbStatus.SelectedItem is not string novoStatus)
                return;

            // Atualiza o status do agendamento
            ag.AlterarStatus(novoStatus);

            // Atualiza a lista na tela
            CarregarAgendamentos();
        }

        // Atualiza os componentes relacionados aos clientes
        private void CarregarClientes()
        {
            // Remove a referência anterior
            cmbCliente.DataSource = null;

            // Carrega novamente os clientes no ComboBox
            cmbCliente.DataSource =
                new List<Cliente>(Repositorio.Clientes);

            // Atualiza a lista visual de clientes
            listClientes.DataSource = null;
            listClientes.DataSource =
                new List<Cliente>(Repositorio.Clientes);
        }

        // Atualiza os componentes relacionados aos serviços
        private void CarregarServicos()
        {
            cmbServico.DataSource = null;

            cmbServico.DataSource =
                new List<Servico>(Repositorio.Servicos);

            listServicos.DataSource = null;

            listServicos.DataSource =
                new List<Servico>(Repositorio.Servicos);
        }

        // Atualiza a lista de agendamentos
        private void CarregarAgendamentos()
        {
            listAgendamentos.DataSource = null;

            listAgendamentos.DataSource =
                new List<Agendamento>(Repositorio.Agendamentos);
        }

        // Aplica as regras de permissão baseadas no papel do usuário
        // RBAC = Role Based Access Control
        // Controle de acesso baseado em funções/cargos
        private void AplicarRegrasSegurancaRBAC()
        {
            // Obtém o usuário autenticado
            var usuario = AuthService.UsuarioAtual;

            // Se não existir usuário ou papel, assume Visualizador
            string papel =
                usuario?.Papel?.Nome ?? "Visualizador";

            // Exibe o usuário logado no título da janela
            this.Text +=
                $" | Usuário: {usuario?.Login} ({papel})";

            // Usuário apenas para visualização
            if (papel == "Visualizador")
            {
                // Desabilita todos os botões de alteração
                btnCadastrarCliente.Enabled = false;
                btnCadastrarServico.Enabled = false;
                btnAgendar.Enabled = false;
                btnAtualizarStatus.Enabled = false;

                // Remove os eventos de clique por segurança
                btnCadastrarCliente.Click -= btnCadastrarCliente_Click;
                btnCadastrarServico.Click -= btnCadastrarServico_Click;

                MessageBox.Show(
                    "Modo somente leitura.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }

            // Usuário operador pode cadastrar e alterar informações
            else if (papel == "Operador")
            {
                btnCadastrarCliente.Enabled = true;
                btnCadastrarServico.Enabled = true;
                btnAgendar.Enabled = true;
                btnAtualizarStatus.Enabled = true;
            }

            // Usuário Admin possui acesso total ao sistema
            // Nenhuma restrição é aplicada
        }
    }
}