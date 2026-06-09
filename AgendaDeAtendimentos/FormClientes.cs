// Load = evento que roda automaticamente quando o formulário abre
// Usamos para já carregar a lista atualizada na tela
private void FormClientes_Load(object sender, EventArgs e)
{
    AtualizarLista();
}
private void btnCadastrar_Click(object sender, EventArgs e)
{
    // Verifica se o campo Nome está vazio ou só com espaços
    // Se estiver, sai do método sem fazer nada (evita cadastrar cliente sem nome)
    if (string.IsNullOrWhiteSpace(txtNome.Text)) return;

    // Cria um novo objeto Cliente com os valores digitados nos TextBoxes
    // txtNome.Text = o texto que está dentro da caixa de texto Nome
    var cliente = new Cliente(txtNome.Text, txtTelefone.Text);

    // Adiciona o cliente criado dentro da lista do Repositorio
    Repositorio.Clientes.Add(cliente);

    // Limpa os campos de texto para o usuário poder digitar outro
    txtNome.Clear();
    txtTelefone.Clear();

    // Atualiza a ListBox para mostrar o cliente recém-cadastrado
    AtualizarLista();
}

private void AtualizarLista()
{
    // Precisamos zerar o DataSource antes de reatribuir
    // Se não fizer isso, o C# pode não perceber que a lista mudou
    listClientes.DataSource = null;

    // DataSource = de onde a ListBox vai buscar os dados para exibir
    // Criamos uma cópia da lista com "new List<>" para forçar a atualização visual
    // A ListBox vai chamar ToString() de cada Cliente para exibir o texto
    listClientes.DataSource = new List<Cliente>(Repositorio.Clientes);
}