Caixa Eletrônico (Console App com SQLite)
Aplicação de console em C# (.NET 8) que simula as operações básicas de um caixa eletrônico, utilizando persistência de dados local com SQLite.

Pré-requisitos
.NET 8 SDK instalado na máquina.

Uma IDE compatível (como JetBrains Rider ou Visual Studio Code) ou terminal com suporte à CLI do .NET.

Como Rodar
Clone o repositório ou navegue até a pasta raiz do projeto pelo terminal.

Restaure as dependências do projeto executando:

Bash
dotnet restore
Compile e execute a aplicação com o comando:

Bash
dotnet run
Na primeira execução, o sistema criará automaticamente o arquivo de banco de dados (caixaEletronico.db) na pasta de execução.

Como Funciona
O projeto é dividido em camadas estruturais para separar as responsabilidades:

Models: Contém as classes de domínio, como Conta e Transacao, que representam as entidades do sistema e encapsulam a lógica de negócio e as operações de banco de dados via ADO.NET (Microsoft.Data.Sqlite).

Data: Contém a classe Database, responsável por inicializar o banco de dados e garantir a criação das tabelas necessárias caso elas não existam.

Program: Contém o ponto de entrada da aplicação (Main) e a interface de usuário baseada em console, exibindo o menu interativo e capturando as entradas do teclado.

Funcionalidades Disponíveis
Criar Conta: Gera um número de conta aleatório, define o titular e cadastra o registro no banco com saldo zerado.

Depositar: Atualiza o saldo somando o valor informado e registra a operação na tabela de histórico.

Sacar: Verifica se há saldo suficiente, desconta o valor correspondente e registra a transação.

Transferir: Valida o saldo da conta de origem, realiza a operação entre duas contas distintas e grava os registros de envio e recebimento no histórico.

Consultar Saldo: Retorna e exibe o saldo atual da conta informada.

Consultar Histórico: Lista todas as movimentações financeiras vinculadas a uma conta específica ordenadas da mais recente para a mais antiga.
