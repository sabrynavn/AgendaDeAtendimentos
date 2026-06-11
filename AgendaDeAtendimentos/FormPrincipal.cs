
//  contém os eventos e métodos que fazem
// o programa funcionar: cadastrar cliente, cadastrar serviço,
// agendar horários, e atualizar status.

// "using" importa funcionalidades que vamos usar
using System;
using System.Collections.Generic;  // Para usar List<>
using System.Windows.Forms;       // Para usar MessageBox, Button, etc.

namespace AgendaDeAtendimentos
{
    // "public partial class" significa que esta classe está dividida em dois arquivos:
    //   1. FormPrincipal.cs   (a lógica)  ← ESTAMOS AQUI
    //   2. FormPrincipal.Designer.cs (os botões, labels, etc. que eu arrastei)
    //
    // ": Form" significa que essa classe HERDA tudo de um formulário do Windows
    public partial class FormPrincipal : Form
    {
       
        //Construtor
        // Roda automaticamente quando o formulário é criado
        public FormPrincipal()
        {
            // InitializeComponent() é um método que está no arquivo Designer.cs
            // Ele cria todos os botões, caixas de texto, etc. que eu coloquei na tela
            InitializeComponent();
        }

        // Evento que quando o formulário CARREGA (abre)

        // "sender" = quem disparou o evento (o próprio formulário)
        // "e" = informações extras sobre o evento
        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            // Quando o programa abre, já carrega as listas
            // para os ComboBox e ListBox aparecerem preenchidos
            CarregarClientes();
            CarregarServicos();
            CarregarAgendamentos();
        }

        // Evento que clicou em "Cadastrar" (Cliente) 

        private void btnCadastrarCliente_Click(object sender, EventArgs e)
        {
            // Se o campo nome estiver vazio, sai do método sem fazer nada
            // IsNullOrWhiteSpace = é nulo, vazio ou só espaços?
            if (string.IsNullOrWhiteSpace(txtNomeCliente.Text)) return;

            // Cria um NOVO cliente com os dados que o usuário digitou
            var cliente = new Cliente(txtNomeCliente.Text, txtTelefone.Text);

            // Adiciona esse cliente na lista do repositório (banco temporário)
            Repositorio.Clientes.Add(cliente);

            // Limpa os campos de texto para o próximo cadastro
            txtNomeCliente.Clear();
            txtTelefone.Clear();

            // Atualiza a lista na tela para mostrar o novo cliente
            CarregarClientes();
        }

        // Evento clicou em "Cadastrar" (Serviço) 

        private void btnCadastrarServico_Click(object sender, EventArgs e)
        {
            // Tenta converter o texto do preço para decimal
            // "TryParse" devolve "true" se deu certo, "false" se não
            // "out decimal preco" = se deu certo, guarda o valor em "preco"
            // Se não conseguir converter (ex: usuário digitou letras), sai do método
            if (!decimal.TryParse(txtPreco.Text, out decimal preco)) return;

            // Cria um NOVO serviço com nome e preço
            var servico = new Servico(txtNomeServico.Text, preco);

            // Adiciona no repositório
            Repositorio.Servicos.Add(servico);

            // Limpa os campos
            txtNomeServico.Clear();
            txtPreco.Clear();

            // Atualiza a lista na tela
            CarregarServicos();
        }

        // Evento clicou em "Agendar"

        private void btnAgendar_Click(object sender, EventArgs e)
        {
            // Verifica se o usuário selecionou um cliente E um serviço
            // SelectedItem é o item que está selecionado no ComboBox
            // Se qualquer um for null, mostra aviso e sai
            if (cmbCliente.SelectedItem == null || cmbServico.SelectedItem == null)
            {
                MessageBox.Show("Selecione um cliente e um serviço.");
                return;
            }

            // Junta a data escolhida com a hora escolhida
            // .Date pega só a data (00:00), .TimeOfDay pega só o horário
            DateTime dataHora = dtpData.Value.Date + dtpHora.Value.TimeOfDay;

            // Cria um novo agendamento
            // (Cliente) e (Servico) são "casts" convertemos o item selecionado
            // de volta para o tipo original (porque SelectedItem é "object")
            var ag = new Agendamento(
                (Cliente)cmbCliente.SelectedItem,
                (Servico)cmbServico.SelectedItem,
                dataHora
            );

            // Adiciona na lista de agendamentos
            Repositorio.Agendamentos.Add(ag);

            // Atualiza a lista na tela
            CarregarAgendamentos();

            MessageBox.Show("Agendamento criado!");
        }

        // Evento: selecionou um item na lista de agendamentos 

        private void listAgendamentos_SelectedIndexChanged(object sender, EventArgs e)
        {
            // "is" verifica se o item selecionado é do tipo Agendamento
            // Se for, já guarda na variável "ag" — isso se chama "pattern matching"
            // Funciona como: if (listAgendamentos.SelectedItem != null && é Agendamento)
            if (listAgendamentos.SelectedItem is Agendamento ag)
            {
                // Quando o usuário clica em um agendamento,
                // o ComboBox de status mostra o status ATUAL desse agendamento
                cmbStatus.SelectedItem = ag.Status;
            }
        }

        //  Evento clicou em "Atualizar Status" 

        private void btnAtualizarStatus_Click(object sender, EventArgs e)
        {
            // "is not" = se NÃO for um Agendamento, mostra aviso
            if (listAgendamentos.SelectedItem is not Agendamento ag)
            {
                MessageBox.Show("Selecione um agendamento na lista.");
                return;
            }

            // Verifica se tem um status selecionado no ComboBox
            // "is not string" = se não for texto (ex: estiver vazio), sai
            if (cmbStatus.SelectedItem is not string novoStatus) return;

            // Chama o método AlterarStatus do agendamento selecionado
            // Esse método só aceita: "Agendado", "Confirmado", "Cancelado", "Concluido"
            ag.AlterarStatus(novoStatus);

            // Atualiza a lista para mostrar a mudança
            CarregarAgendamentos();
        }

        
        // Métodos auxiliares que dá pra reutilizar
        // Atualiza as listas de clientes (no ComboBox e no ListBox)
        private void CarregarClientes()
        {
            // "null" primeiro para limpar e depois "new List(...)" para recarregar
            // Isso é um truque para forçar o ComboBox/ListBox a se atualizar
            cmbCliente.DataSource = null;
            cmbCliente.DataSource = new List<Cliente>(Repositorio.Clientes);

            listClientes.DataSource = null;
            listClientes.DataSource = new List<Cliente>(Repositorio.Clientes);
        }

        // Atualiza as listas de serviços
        private void CarregarServicos()
        {
            cmbServico.DataSource = null;
            cmbServico.DataSource = new List<Servico>(Repositorio.Servicos);

            listServicos.DataSource = null;
            listServicos.DataSource = new List<Servico>(Repositorio.Servicos);
        }

        // Atualiza a lista de agendamentos
        private void CarregarAgendamentos()
        {
            listAgendamentos.DataSource = null;
            listAgendamentos.DataSource = new List<Agendamento>(Repositorio.Agendamentos);
        }
    }
}
