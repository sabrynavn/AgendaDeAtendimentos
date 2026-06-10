using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AgendaAtendimentos
{
    public partial class FormServicos : Form
    {
        public FormServicos()
        {
            InitializeComponent();
        }

        private void FormServicos_Load(object sender, EventArgs e)
        {
            AtualizarLista();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            // TryParse tenta converter o texto em decimal
            // Se o usuário digitou letras ou deixou vazio, sai do método
            if (!decimal.TryParse(txtPreco.Text, out decimal preco)) return;

            var servico = new Servico(txtNome.Text, preco);
            Repositorio.Servicos.Add(servico);
            AtualizarLista();
        }

        private void AtualizarLista()
        {
            listServicos.DataSource = null;
            listServicos.DataSource = new List<Servico>(Repositorio.Servicos);
        }
    }
}