
//Os controles visuais do formulário
// Esse arquivo é criado pelo Windows Forms Designer (a parte gráfica).
// Eu não preciso digitar esse código manualmente — eu arrasto os botões
// e caixas de texto na tela, e o Visual Studio gera isso aqui.
//
// Mas é importante entender o que cada parte faz!
//
// "partial class" significa que este arquivo é METADE da classe FormPrincipal
// A outra metade está em FormPrincipal.cs (a lógica)

namespace AgendaDeAtendimentos
{
    partial class FormPrincipal
    {
        // "IContainer" guarda referências de todos os componentes
        // para poder liberar memória quando o formulário fechar
        private System.ComponentModel.IContainer components = null;

        // Dispose é chamado quando o formulário é fechado
        // Aqui liberamos a memória dos componentes para não vazar
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

       
        // InitializeComponent — O coração do Designer
   
        // Esse método é chamado pelo construtor do FormPrincipal
        // Ele CRIA e CONFIGURA todos os botões, labels, listas, etc.
        //
        // Cada "new Button()" cria um botão novo na memória
        // Depois configuramos onde ele fica, qual texto aparece, etc.
        // No final, adicionamos ele na lista "Controls" do formulário

        private void InitializeComponent()
        {
            // Criação de todos os componentes 
            // (Criei tudo antes de configurar)

            tabControl = new TabControl();         // O controle de abas
            tabClientes = new TabPage();            // Aba "Clientes"
            listClientes = new ListBox();           // Lista de clientes
            txtTelefone = new TextBox();            // Campo de telefone
            txtNomeCliente = new TextBox();         // Campo de nome do cliente
            btnCadastrarCliente = new Button();     // Botão "Cadastrar" da aba Clientes
            label3 = new Label();                   // Rótulo "Clientes:"
            label2 = new Label();                   // Rótulo "Telefone:"
            label1 = new Label();                   // Rótulo "Nome:"

            tabServicos = new TabPage();            // Aba "Serviços"
            listServicos = new ListBox();           // Lista de serviços
            txtPreco = new TextBox();               // Campo de preço
            txtNomeServico = new TextBox();         // Campo de nome do serviço
            btnCadastrarServico = new Button();     // Botão "Cadastrar" da aba Serviços
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();

            tabAgendamentos = new TabPage();        // Aba "Agendamentos"
            listAgendamentos = new ListBox();       // Lista de agendamentos
            cmbStatus = new ComboBox();             // ComboBox de status
            cmbServico = new ComboBox();            // ComboBox de serviço (para escolher)
            cmbCliente = new ComboBox();            // ComboBox de cliente (para escolher)
            dtpHora = new DateTimePicker();         // Seletor de hora
            dtpData = new DateTimePicker();         // Seletor de data
            btnAgendar = new Button();              // Botão "Agendar"
            btnAtualizarStatus = new Button();      // Botão "Atualizar Status"
            label11 = new Label();
            label10 = new Label();
            label9 = new Label();
            label8 = new Label();
            label7 = new Label();

            // SuspendLayout() = "segura" as alterações visuais
            // Enquanto está suspenso, o formulário não fica piscando
            // enquanto adicionamos vários controles
            tabControl.SuspendLayout();
            tabClientes.SuspendLayout();
            tabServicos.SuspendLayout();
            tabAgendamentos.SuspendLayout();
            SuspendLayout();

            // tabControl — O controle que tem as 3 abas
            
            // Controls.Add() adiciona cada aba dentro do tabControl
            tabControl.Controls.Add(tabClientes);
            tabControl.Controls.Add(tabServicos);
            tabControl.Controls.Add(tabAgendamentos);

            // Dock = DockStyle.Fill faz o tabControl ocupar a TELA INTEIRA
            tabControl.Dock = DockStyle.Fill;
            tabControl.Location = new Point(0, 0);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;    // Começa na primeira aba (Clientes)
            tabControl.Size = new Size(800, 450);
            tabControl.TabIndex = 0;

            
            // tabClientes — Aba de Clientes
           

            // AddRange adiciona VÁRIOS controles de uma vez
            tabClientes.Controls.Add(listClientes);
            tabClientes.Controls.Add(txtTelefone);
            tabClientes.Controls.Add(txtNomeCliente);
            tabClientes.Controls.Add(btnCadastrarCliente);
            tabClientes.Controls.Add(label3);
            tabClientes.Controls.Add(label2);
            tabClientes.Controls.Add(label1);
            tabClientes.Location = new Point(4, 29);
            tabClientes.Name = "tabClientes";
            tabClientes.Padding = new Padding(3);
            tabClientes.Size = new Size(792, 417);
            tabClientes.TabIndex = 0;
            tabClientes.Text = "Clientes";
            tabClientes.UseVisualStyleBackColor = true;

            //  listClientes (ListBox) 
            // Mostra todos os clientes cadastrados
            listClientes.FormattingEnabled = true;
            listClientes.Location = new Point(8, 190);  // Posição X, Y
            listClientes.Name = "listClientes";
            listClientes.Size = new Size(200, 104);     // Largura, Altura
            listClientes.TabIndex = 6;

            //  txtTelefone (TextBox) 
            // Onde o usuário digita o telefone
            txtTelefone.Location = new Point(8, 131);
            txtTelefone.Name = "txtTelefone";
            txtTelefone.Size = new Size(250, 27);
            txtTelefone.TabIndex = 5;

            //  txtNomeCliente (TextBox) 
            // Onde o usuário digita o nome do cliente
            txtNomeCliente.Location = new Point(8, 61);
            txtNomeCliente.Name = "txtNomeCliente";
            txtNomeCliente.Size = new Size(768, 27);
            txtNomeCliente.TabIndex = 4;

            // btnCadastrarCliente (Button)
            // Botão que chama btnCadastrarCliente_Click
            btnCadastrarCliente.Location = new Point(8, 326);
            btnCadastrarCliente.Name = "btnCadastrarCliente";
            btnCadastrarCliente.Size = new Size(94, 29);
            btnCadastrarCliente.TabIndex = 3;
            btnCadastrarCliente.Text = "Cadastrar";
            btnCadastrarCliente.UseVisualStyleBackColor = true;

            // += é como conectar um fio: quando o botão for clicado,
            // o método btnCadastrarCliente_Click (em FormPrincipal.cs) será executado
            btnCadastrarCliente.Click += btnCadastrarCliente_Click;

            // --- Labels (rótulos de texto) ---
            label3.AutoSize = true;
            label3.Location = new Point(8, 167);
            label3.Name = "label3";
            label3.Size = new Size(60, 20);
            label3.TabIndex = 2;
            label3.Text = "Clientes:";

            label2.AutoSize = true;
            label2.Location = new Point(8, 108);
            label2.Name = "label2";
            label2.Size = new Size(66, 20);
            label2.TabIndex = 1;
            label2.Text = "Telefone:";

            label1.AutoSize = true;
            label1.Location = new Point(8, 38);
            label1.Name = "label1";
            label1.Size = new Size(53, 20);
            label1.TabIndex = 0;
            label1.Text = "Nome:";

          
            // tabServicos — Aba de Serviços
            

            tabServicos.Controls.Add(listServicos);
            tabServicos.Controls.Add(txtPreco);
            tabServicos.Controls.Add(txtNomeServico);
            tabServicos.Controls.Add(btnCadastrarServico);
            tabServicos.Controls.Add(label6);
            tabServicos.Controls.Add(label5);
            tabServicos.Controls.Add(label4);
            tabServicos.Location = new Point(4, 29);
            tabServicos.Name = "tabServicos";
            tabServicos.Padding = new Padding(3);
            tabServicos.Size = new Size(792, 417);
            tabServicos.TabIndex = 1;
            tabServicos.Text = "Serviços";
            tabServicos.UseVisualStyleBackColor = true;

            listServicos.FormattingEnabled = true;
            listServicos.Location = new Point(8, 190);
            listServicos.Name = "listServicos";
            listServicos.Size = new Size(200, 104);
            listServicos.TabIndex = 6;

            txtPreco.Location = new Point(8, 131);
            txtPreco.Name = "txtPreco";
            txtPreco.Size = new Size(150, 27);
            txtPreco.TabIndex = 5;

            txtNomeServico.Location = new Point(8, 61);
            txtNomeServico.Name = "txtNomeServico";
            txtNomeServico.Size = new Size(768, 27);
            txtNomeServico.TabIndex = 4;

            btnCadastrarServico.Location = new Point(8, 326);
            btnCadastrarServico.Name = "btnCadastrarServico";
            btnCadastrarServico.Size = new Size(94, 29);
            btnCadastrarServico.TabIndex = 3;
            btnCadastrarServico.Text = "Cadastrar";
            btnCadastrarServico.UseVisualStyleBackColor = true;
            btnCadastrarServico.Click += btnCadastrarServico_Click;

            label6.AutoSize = true;
            label6.Location = new Point(8, 167);
            label6.Name = "label6";
            label6.Size = new Size(66, 20);
            label6.TabIndex = 2;
            label6.Text = "Serviços:";

            label5.AutoSize = true;
            label5.Location = new Point(8, 108);
            label5.Name = "label5";
            label5.Size = new Size(49, 20);
            label5.TabIndex = 1;
            label5.Text = "Preço:";

            label4.AutoSize = true;
            label4.Location = new Point(8, 38);
            label4.Name = "label4";
            label4.Size = new Size(53, 20);
            label4.TabIndex = 0;
            label4.Text = "Nome:";

           
            // tabAgendamentos — Aba de Agendamentos
           

            tabAgendamentos.Controls.Add(listAgendamentos);
            tabAgendamentos.Controls.Add(cmbStatus);
            tabAgendamentos.Controls.Add(cmbServico);
            tabAgendamentos.Controls.Add(cmbCliente);
            tabAgendamentos.Controls.Add(dtpHora);
            tabAgendamentos.Controls.Add(dtpData);
            tabAgendamentos.Controls.Add(btnAtualizarStatus);
            tabAgendamentos.Controls.Add(btnAgendar);
            tabAgendamentos.Controls.Add(label11);
            tabAgendamentos.Controls.Add(label10);
            tabAgendamentos.Controls.Add(label9);
            tabAgendamentos.Controls.Add(label8);
            tabAgendamentos.Controls.Add(label7);
            tabAgendamentos.Location = new Point(4, 29);
            tabAgendamentos.Name = "tabAgendamentos";
            tabAgendamentos.Padding = new Padding(3);
            tabAgendamentos.Size = new Size(792, 417);
            tabAgendamentos.TabIndex = 2;
            tabAgendamentos.Text = "Agendamentos";
            tabAgendamentos.UseVisualStyleBackColor = true;

            // listAgendamentos 
            listAgendamentos.FormattingEnabled = true;
            listAgendamentos.Location = new Point(8, 258);
            listAgendamentos.Name = "listAgendamentos";
            listAgendamentos.Size = new Size(776, 104);
            listAgendamentos.TabIndex = 11;
            // Quando o usuário clicar em um item da lista, executa esse método
            listAgendamentos.SelectedIndexChanged += listAgendamentos_SelectedIndexChanged;

            // cmbStatus (ComboBox)
            // Dropdown com os status possíveis
            cmbStatus.FormattingEnabled = true;
            // Items.AddRange já adiciona todos os status de uma vez
            cmbStatus.Items.AddRange(new object[] { "Agendado", "Confirmado", "Cancelado", "Concluido" });
            cmbStatus.Location = new Point(542, 88);
            cmbStatus.Name = "cmbStatus";
            cmbStatus.Size = new Size(150, 28);
            cmbStatus.TabIndex = 10;

            // cmbServico (ComboBox) 
            // Dropdown para escolher o serviço na hora de agendar
            cmbServico.FormattingEnabled = true;
            cmbServico.Location = new Point(8, 157);
            cmbServico.Name = "cmbServico";
            cmbServico.Size = new Size(776, 28);
            cmbServico.TabIndex = 9;

            // --- cmbCliente (ComboBox) ---
            // Dropdown para escolher o cliente na hora de agendar
            cmbCliente.FormattingEnabled = true;
            cmbCliente.Location = new Point(8, 48);
            cmbCliente.Name = "cmbCliente";
            cmbCliente.Size = new Size(776, 28);
            cmbCliente.TabIndex = 8;

            // --- dtpHora (DateTimePicker) ---
            // Seletor de hora (mostra só o horário, não a data)
            dtpHora.Format = DateTimePickerFormat.Time;
            dtpHora.Location = new Point(270, 88);
            dtpHora.Name = "dtpHora";
            dtpHora.Size = new Size(250, 27);
            dtpHora.TabIndex = 7;

            // --- dtpData (DateTimePicker) ---
            // Seletor de data (calendário)
            dtpData.Location = new Point(8, 88);
            dtpData.Name = "dtpData";
            dtpData.Size = new Size(250, 27);
            dtpData.TabIndex = 6;

            // --- btnAgendar ---
            btnAgendar.Location = new Point(8, 382);
            btnAgendar.Name = "btnAgendar";
            btnAgendar.Size = new Size(94, 29);
            btnAgendar.TabIndex = 5;
            btnAgendar.Text = "Agendar";
            btnAgendar.UseVisualStyleBackColor = true;
            btnAgendar.Click += btnAgendar_Click;

            // --- btnAtualizarStatus ---
            btnAtualizarStatus.Location = new Point(118, 382);
            btnAtualizarStatus.Name = "btnAtualizarStatus";
            btnAtualizarStatus.Size = new Size(140, 29);
            btnAtualizarStatus.TabIndex = 12;
            btnAtualizarStatus.Text = "Atualizar Status";
            btnAtualizarStatus.UseVisualStyleBackColor = true;
            btnAtualizarStatus.Click += btnAtualizarStatus_Click;

            // --- Labels da aba Agendamentos ---
            label11.AutoSize = true;
            label11.Location = new Point(542, 65);
            label11.Name = "label11";
            label11.Size = new Size(52, 20);
            label11.TabIndex = 4;
            label11.Text = "Status:";

            label10.AutoSize = true;
            label10.Location = new Point(270, 65);
            label10.Name = "label10";
            label10.Size = new Size(45, 20);
            label10.TabIndex = 3;
            label10.Text = "Hora:";

            label9.AutoSize = true;
            label9.Location = new Point(8, 65);
            label9.Name = "label9";
            label9.Size = new Size(44, 20);
            label9.TabIndex = 2;
            label9.Text = "Data:";

            label8.AutoSize = true;
            label8.Location = new Point(8, 134);
            label8.Name = "label8";
            label8.Size = new Size(58, 20);
            label8.TabIndex = 1;
            label8.Text = "Serviço:";

            label7.AutoSize = true;
            label7.Location = new Point(8, 25);
            label7.Name = "label7";
            label7.Size = new Size(58, 20);
            label7.TabIndex = 0;
            label7.Text = "Cliente:";

            
            // FormPrincipal — O formulário em si
            

            AutoScaleDimensions = new SizeF(8F, 20F);  // Escala automática
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);           // Tamanho da janela

            // Adiciona o tabControl (com tudo dentro) ao formulário
            Controls.Add(tabControl);

            Name = "FormPrincipal";
            Text = "Barbearia Arroche";                // Título da janela

            // Load é o evento que dispara quando o formulário ABRE
            Load += FormPrincipal_Load;

            // ResumeLayout() = "volta ao normal" depois de SuspendLayout()
            // PerformLayout() = força o layout a se atualizar
            tabControl.ResumeLayout(false);
            tabClientes.ResumeLayout(false);
            tabClientes.PerformLayout();
            tabServicos.ResumeLayout(false);
            tabServicos.PerformLayout();
            tabAgendamentos.ResumeLayout(false);
            tabAgendamentos.PerformLayout();
            ResumeLayout(false);
        }

       
        // Declaração dos controles (variáveis)
        // Aqui declaramos todas as variáveis que representam os
        // botões, listas, textos, etc. que criamos lá em cima.
        //
        // "private" = só pode ser usado dentro da classe FormPrincipal
        // Elas combinam com os "new Button()", "new ListBox()", etc.

        private TabControl tabControl;
        private TabPage tabClientes;
        private TabPage tabServicos;
        private TabPage tabAgendamentos;

        // Controles da aba Clientes
        private ListBox listClientes;
        private TextBox txtTelefone;
        private TextBox txtNomeCliente;
        private Button btnCadastrarCliente;
        private Label label3;
        private Label label2;
        private Label label1;

        // Controles da aba Serviços
        private ListBox listServicos;
        private TextBox txtPreco;
        private TextBox txtNomeServico;
        private Button btnCadastrarServico;
        private Label label6;
        private Label label5;
        private Label label4;

        // Controles da aba Agendamentos
        private ListBox listAgendamentos;
        private ComboBox cmbStatus;
        private ComboBox cmbServico;
        private ComboBox cmbCliente;
        private DateTimePicker dtpHora;
        private DateTimePicker dtpData;
        private Button btnAgendar;
        private Button btnAtualizarStatus;
        private Label label11;
        private Label label10;
        private Label label9;
        private Label label8;
        private Label label7;
    }
}
