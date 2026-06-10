using System.ComponentModel.Design;
using System.Drawing.Text;

private void btnCadastrar_click(object sender, EventArgs e)
{
    //TryParse vai tentar converter o texto digitado transformando ele em decimal 
    //Se conseguir, guardar o resultado em preco e retornar true
    //Se o usuário digitar letras ou deixar em branco ele retorna false e sai do método 
    if (!decimal.TryParse(txtPreco.Text, out decimal preco)) return;

    var servico = new Servico(txtNome.Text, preco);
    Repositorio.Servicos.Add(servico);
    AtualizarLista();
}

private void AtualizarLista()
{
    listServicos.DataSource = null;
    listServicos.DataSource = new list<Servico>(Repositorio.Servicos);
}

