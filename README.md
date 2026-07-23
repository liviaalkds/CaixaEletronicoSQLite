# Caixa Eletrônico (Console App com SQLite)

Aplicação de console desenvolvida em C# (.NET 8) que simula as principais operações de um caixa eletrônico, utilizando SQLite para persistência local dos dados.

---

## Tecnologias Utilizadas

- C#
- SQLite
- Microsoft.Data.Sqlite (ADO.NET)

---

## Pré-requisitos

Antes de executar o projeto, certifique-se de ter instalado:

- .NET 8 SDK
- Uma IDE compatível (JetBrains Rider, Visual Studio ou Visual Studio Code)

---

## Como Executar

### 1. Clone o repositório

```bash
git clone https://github.com/liviaalkds/CaixaEletronicoSQLite.git
```

Ou navegue até a pasta do projeto.

### 2. Restaure as dependências

```bash
dotnet restore
```

### 3. Execute a aplicação

```bash
dotnet run
```

Na primeira execução, será criado automaticamente o banco de dados **caixaEletronico.db** na pasta do projeto.

---

## Estrutura do Projeto

```text
CaixaEletronico/
│
├── Data/
│   └── Database.cs
│
├── Models/
│   ├── Conta.cs
│   └── Transacao.cs
│
├── Program.cs
│
└── caixaEletronico.db
```

### Models

Contém as classes de domínio responsáveis por representar as entidades do sistema.

#### Conta

- Cadastro de contas
- Consulta de saldo
- Depósitos
- Saques
- Transferências

#### Transacao

- Registro do histórico de movimentações
- Consulta das transações realizadas

Toda a comunicação com o banco é feita utilizando ADO.NET através do pacote `Microsoft.Data.Sqlite`.

### Data

Contém a classe `Database`, responsável por:

- Criar a conexão com o SQLite;
- Inicializar o banco de dados;
- Criar automaticamente as tabelas caso ainda não existam.

### Program

É o ponto de entrada da aplicação.

Responsável por:

- Exibir o menu principal;
- Receber as entradas do usuário;
- Chamar os métodos responsáveis pelas operações bancárias.

---

## Funcionalidades

### Criar Conta

- Gera automaticamente um número de conta;
- Define o titular;
- Cria a conta com saldo inicial igual a R$ 0,00.

### Depositar

- Localiza a conta;
- Soma o valor informado ao saldo;
- Registra a movimentação no histórico.

### Sacar

- Verifica se existe saldo suficiente;
- Efetua o saque;
- Atualiza o saldo;
- Registra a operação.

### Transferir

- Valida a existência das contas;
- Verifica saldo disponível;
- Realiza a transferência;
- Registra a saída da conta de origem;
- Registra a entrada na conta de destino.

### Consultar Saldo

Permite visualizar o saldo atualizado de qualquer conta cadastrada.

### Consultar Histórico

Exibe todas as movimentações financeiras da conta selecionada, ordenadas da mais recente para a mais antiga.

Cada registro informa:

- Tipo da operação;
- Valor movimentado;
- Data e horário da transação.

---

## Banco de Dados

O sistema utiliza um banco de dados SQLite local.

Na primeira execução são criadas automaticamente as tabelas necessárias para o funcionamento da aplicação.

Principais entidades:

- Contas
- Transações

---

## Objetivo do Projeto

Este projeto foi desenvolvido com fins acadêmicos para praticar conceitos de:

- Programação Orientada a Objetos (POO)
- Persistência de dados
- SQLite
- ADO.NET
- Estruturação em camadas
- Aplicações Console com .NET 8

---

## Autora

**Lívia Alkimim dos Santos**

Desenvolvido utilizando C# + .NET 8 + SQLite.
