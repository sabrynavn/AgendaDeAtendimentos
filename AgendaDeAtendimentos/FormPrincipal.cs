// Chamado quando o usuário clica no botão "Clientes"
using System.Drawing.Text;

private void btnClientes_Click(object sender, EventArgs e)
{
    // "new FormClientes()" cria uma nova instância do formulário de clientes
    // ".ShowDialog()" abre o formulário de forma modal —
    // ou seja, o usuário precisa fechar ele antes de voltar ao menu principal
    new FormClientes().ShowDialog();
}
private void btnServicos_Click(object sender, EventArgs e)
{
    new FormServicos().ShowDialog();
}
private void btnAgendamentos_Click(object sender, EventArgs e)
{
    new FormAgendamentos().ShowDialog();
}
private void btnAgendamentos_Click(Object sender, EventArgs e)
{
    new FormAgendamentos().ShowDiaLog();
}


