private void FormAgendamentos_Load(object sender, EventArgs e)
{
    cmbCliente.DataSource = new List<Cliente>(Repositorio.Clientes);
    cmbServico.DataSource = new List<Servico>(Repositorio.Servicos);

    // Carrega os 4 status possíveis no ComboBox de alteração
    cmbStatus.Items.AddRange(new string[]
    {
        "Agendado",
        "Confirmado",
        "Cancelado",
        "Concluído"
    });

    // Deixa o primeiro item já selecionado por padrão
    cmbStatus.SelectedIndex = 0;
}

private void btnAlterarStatus_Click(object sender, EventArgs e)
{
    // Verifica se o usuário selecionou algum agendamento na lista
    if (listAgendamentos.SelectedItem == null)
    {
        MessageBox.Show("Selecione um agendamento na lista.");
        return;
    }

    // Pega o agendamento selecionado na ListBox
    // O cast "(Agendamento)" transforma o SelectedItem no tipo correto
    var agendamento = (Agendamento)listAgendamentos.SelectedItem;

    // Pega o texto selecionado no ComboBox de status
    var novoStatus = cmbStatus.SelectedItem.ToString();

    // Chama o método da classe — ele mesmo valida se o status é permitido
    agendamento.AlterarStatus(novoStatus);

    // Atualiza a lista na tela para refletir a mudança
    btnFiltrar_Click(sender, e);

    MessageBox.Show($"Status alterado para: {novoStatus}");
}