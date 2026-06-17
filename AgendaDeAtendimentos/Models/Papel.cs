using System;
using System.Collections.Generic;
using System.Text;

namespace AgendaDeAtendimentos.Models
{
    public class Papel
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty; //Admin, Operador, Visualizador
    }

        internal static class Program
        {
            [STAThread]
            static void Main()
            {
                // Configurações padrão do Windows Forms para renderização de fontes e estilos visuais
                ApplicationConfiguration.Initialize();

                // 1. Instanciamos o formulário de login na memória
                FormLogin telaLogin = new FormLogin();

                // 2. Abrimos a tela de login usando 'ShowDialog()'.
                // O ShowDialog faz a janela abrir em modo de bloqueio: o utilizador não consegue clicar em mais nada
                // até resolver essa tela. O código do Main fica "pausado" aqui a aguardar uma resposta.
                if (telaLogin.ShowDialog() == DialogResult.OK)
                {
                    // 3. Se a tela de login fechou devolvendo 'DialogResult.OK', significa que as credenciais estão certas!
                    // Só agora damos o comando para arrancar o formulário principal (Form1)
                    Application.Run(new FormPrincipal());
                }

                // Se o utilizador fechar a tela de login no "X" sem autenticar, o ShowDialog NÃO retorna OK,
                // o 'if' é ignorado e o programa encerra-se aqui silenciosamente, protegendo o sistema.
            }
        }
    }
