using System.Drawing;
using System.Windows.Forms;

namespace AgendaDeAtendimentos
{
    public static class Estilo
    {
        // Suas cores da imagem_d58236.png
        public static readonly Color Cream = Color.FromArgb(246, 238, 236);
        public static readonly Color LipstickRose = Color.FromArgb(166, 58, 79);

        // O método que aplica o "CSS" em lote
        public static void Aplicar(Form formulario)
        {
            formulario.BackColor = Cream;

            // Varre todos os controles do formulário recursivamente
            EstilizarControles(formulario.Controls);
        }

        private static void EstilizarControles(Control.ControlCollection controles)
        {
            foreach (Control c in controles)
            {
                // Se for um botão, aplica o Lipstick Rose
                if (c is Button btn)
                {
                    btn.BackColor = LipstickRose;
                    btn.ForeColor = Color.White;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                }
                // Se for uma caixa de texto ou combo, deixa com borda simples
                else if (c is TextBox || c is ComboBox || c is ListBox)
                {
                    if (c is TextBox txt) txt.BorderStyle = BorderStyle.FixedSingle;
                    if (c is ListBox lb) lb.BorderStyle = BorderStyle.FixedSingle;
                }

                // Se o controle tiver sub-controles (ex: painéis, abas), estiliza eles também
                if (c.Controls.Count > 0)
                {
                    EstilizarControles(c.Controls);
                }
            }
        }
    }
}
