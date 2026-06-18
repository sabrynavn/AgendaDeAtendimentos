# Agenda de Atendimentos

Sistema desktop em C# (Windows Forms) para gerenciar clientes, serviços e agendamentos, com banco de dados local em SQLite e autenticação com controle de acesso por papéis (RBAC).

Feito para pequenos negócios que trabalham com horário marcado, como barbearias, salões ou consultórios. Permite cadastrar clientes, definir os serviços oferecidos e organizar os agendamentos do dia a dia.

## Funcionalidades

- Login com senha criptografada (hash via BCrypt)
- Controle de acesso por papéis: Admin, Operador e Visualizador
- Cadastro de clientes (nome, telefone e e-mail)
- Cadastro de serviços (nome e valor)
- Agendamentos vinculando cliente, serviço e data/hora, com status (Agendado, Confirmado, Cancelado, Concluido)
- Banco de dados criado automaticamente na primeira execução, já com tabelas e usuários padrão

## Tecnologias

- C# / .NET 10 (net10.0-windows)
- Windows Forms
- SQLite (Microsoft.Data.Sqlite)
- BCrypt.Net-Next

## Arquitetura

O projeto segue uma divisão em camadas. A tela nunca acessa o banco direto, sempre passa pelo Service e pelo Repository:

Form (tela)
  -> Service (regras de negócio)
    -> Repository (acesso a dados / SQL)
      -> SQLite (sistema.db)

Models: Cliente, Servico, Agendamento, Usuario, Papel
Repositories: ClienteRepository, ServicoRepository, AgendamentoRepository, UsuarioRepositorio
Services: ClienteService, ServicoService, AgendamentoService, AuthService, UsuarioService
Forms: FormLogin, FormPrincipal

## Estrutura de pastas

AgendaDeAtendimentos/
  Data/
    DatabaseConfig.cs
  Models/
    Agendamento.cs
    Papel.cs
    Usuario.cs
    cliente.cs
    servico.cs
  Repositories/
    AgendamentoRepository.cs
    ClienteRepository.cs
    ServicoRepository.cs
    UsuarioRepositorio.cs
    IUsuarioRepository.cs
  Services/
    AgendamentoService.cs
    AuthService.cs
    ClienteService.cs
    ServicoService.cs
    UsuarioService.cs
  FormLogin.cs
  FormPrincipal.cs
  Estilo.cs
  Program.cs

## Pré-requisitos

- .NET SDK 10 ou superior
- Windows (o projeto usa Windows Forms)
- Visual Studio 2022 ou superior (opcional)

## Como executar

git clone https://github.com/sabrynavn/AgendaDeAtendimentos.git
cd AgendaDeAtendimentos
dotnet restore
dotnet run --project AgendaDeAtendimentos

Ou abra o arquivo AgendaDeAtendimentos.slnx no Visual Studio e rode com F5.

Na primeira execução o arquivo sistema.db é criado automaticamente, já com os usuários padrão abaixo.

## Usuários padrão

Admin: login admin, senha admin123
Operador: login operador, senha operador123
Visualizador: login visual, senha visual123

Essas credenciais servem apenas para teste e desenvolvimento. Em produção, troque as senhas e evite versionar o arquivo sistema.db.

## Próximos passos

- Implementar o UsuarioService, que hoje está vazio
- Criar uma tela para gerenciar usuários (hoje só é possível pelo banco)
- Adicionar validações de e-mail e telefone nos formulários
- Permitir editar cliente, serviço e data de um agendamento já criado, hoje só é possível alterar o status

## Licença

Projeto ainda sem licença definida.
