// Quando o formulário abre, carregamos os ComboBoxes com os dados já cadastrados
private void FormAgendamentos_Load(object sender, EventArgs e)
{
    // DataSource do ComboBox funciona igual ao da ListBox
    // Vai mostrar os clientes usando o ToString() de cada um
    cmbCliente.DataSource = new List<Cliente>(Repositorio.Clientes);
    cmbServico.DataSource = new List<Servico>(Repositorio.Servicos);
}

private void btnAgendar_Click(object sender, EventArgs e)
{
    // Verifica se o usuário selecionou algum cliente e algum serviço
    // SelectedItem é o item atualmente selecionado no ComboBox
    if (cmbCliente.SelectedItem == null || cmbServico.SelectedItem == null) return;

    // Cria o agendamento passando os objetos selecionados nos ComboBoxes
    // "(Cliente)" é um cast — estamos dizendo ao C# que o SelectedItem é um Cliente
    // dtpDataHora.Value = o DateTime selecionado no DateTimePicker
    var ag = new Agendamento(
        (Cliente)cmbCliente.SelectedItem,
        (Servico)cmbServico.SelectedItem,
        dtpDataHora.Value
    );

    Repositorio.Agendamentos.Add(ag);

    // Mostra uma mensagem de confirmação para o usuário
    MessageBox.Show("Agendamento criado!");
}

private void btnFiltrar_Click(object sender, EventArgs e)
{
    // Pega só a DATA do DateTimePicker, sem a hora (ex: 08/06/2026 00:00:00)
    // ".Date" zera a parte da hora, facilitando a comparação
    var data = dtpFiltro.Value.Date;

    // LINQ — é uma forma de consultar listas como se fosse um banco de dados
    // .Where() filtra: só agendamentos cuja data (sem hora) seja igual ao filtro
    // a => é uma "lambda" — "a" representa cada agendamento da lista
    // .OrderBy() ordena do mais cedo para o mais tarde
    // .ToList() converte o resultado filtrado de volta para uma Lista
    var resultado = Repositorio.Agendamentos
        .Where(a => a.DataHora.Date == data)
        .OrderBy(a => a.DataHora)
        .ToList();

    listAgendamentos.DataSource = null;
    listAgendamentos.DataSource = resultado;
}