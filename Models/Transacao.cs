using Microsoft.Data.Sqlite;
using CaixaEletronico.Models;

namespace CaixaEletronico;

public class Transacao
{
    private const string ConnectionString = "Data Source=caixaEletronico.db";

    public static Conta CriaConta(string titular)
    {
        Random random = new Random();
        int numeroConta = random.Next(1000, 99999); //gera um número aleatório p conta entre 1000 e 9999
        
        Conta novaConta = new Conta(numeroConta, titular);
        //CRIA O OBJETO COM COM SALDO ZERADO AUTOMATICAMENTE

        using (var connection = new SqliteConnection(ConnectionString))
        {
            connection.Open();
            string sql = "INSERT INTO Contas (Numero, Titular, Saldo) VALUES (@numero, @titular, @saldo)";

            using (var command = new SqliteCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@numero", novaConta.Numero);
                command.Parameters.AddWithValue("@titular", novaConta.Titular);
                command.Parameters.AddWithValue("@saldo", novaConta.Saldo); //passa os parâmetros
                command.ExecuteNonQuery(); //executa p salvar no banco
            }
        }

        return novaConta;
    }

    public static void Depositar(int numeroConta, decimal valor)
    {
        using (var connection = new SqliteConnection(ConnectionString))
        {
            connection.Open();

            // atualiza o saldo na tabela contas somando o valor novo
            string sqlUpdate = "UPDATE Contas SET Saldo = Saldo + @valor WHERE Numero = @numero";
            using (var cmdUpdate = new SqliteCommand(sqlUpdate, connection))
            {
                cmdUpdate.Parameters.AddWithValue("@valor", valor);
                cmdUpdate.Parameters.AddWithValue("@numero", numeroConta);
                cmdUpdate.ExecuteNonQuery(); //roda o update
            }

            // salva no historico q teve deposito
            RegistrarHistorico(connection, numeroConta, "Depósito", valor, null);
        }
    }

    public static bool Sacar(int numeroConta, decimal valor)
    {
        using (var connection = new SqliteConnection(ConnectionString))
        {
            connection.Open();

            // ve se tem grana suficiente antes de tirar
            decimal saldoAtual = ObterSaldo(connection, numeroConta);
            if (saldoAtual < valor) return false; //se for menor retorna falso n da p sacar

            // tira a grana do saldo atual
            string sqlUpdate = "UPDATE Contas SET Saldo = Saldo - @valor WHERE Numero = @numero";
            using (var cmdUpdate = new SqliteCommand(sqlUpdate, connection))
            {
                cmdUpdate.Parameters.AddWithValue("@valor", valor);
                cmdUpdate.Parameters.AddWithValue("@numero", numeroConta);
                cmdUpdate.ExecuteNonQuery();
            }

            // grava no historico q sacou
            RegistrarHistorico(connection, numeroConta, "Saque", valor, null);
            return true;
        }
    }

    public static bool Transferir(int contaOrigem, int contaDestino, decimal valor)
    {
        using (var connection = new SqliteConnection(ConnectionString))
        {
            connection.Open();

            // checa se quem ta mandando tem saldo
            decimal saldoOrigem = ObterSaldo(connection, contaOrigem);
            if (saldoOrigem < valor) return false;

            // tira da origem
            string sqlOut = "UPDATE Contas SET Saldo = Saldo - @valor WHERE Numero = @numero";
            using (var cmdOut = new SqliteCommand(sqlOut, connection))
            {
                cmdOut.Parameters.AddWithValue("@valor", valor);
                cmdOut.Parameters.AddWithValue("@numero", contaOrigem);
                cmdOut.ExecuteNonQuery();
            }

            // bota no destino
            string sqlIn = "UPDATE Contas SET Saldo = Saldo + @valor WHERE Numero = @numero";
            using (var cmdIn = new SqliteCommand(sqlIn, connection))
            {
                cmdIn.Parameters.AddWithValue("@valor", valor);
                cmdIn.Parameters.AddWithValue("@numero", contaDestino);
                cmdIn.ExecuteNonQuery();
            }

            // registra p os dois lados no historico
            RegistrarHistorico(connection, contaOrigem, "Transferência Enviada", valor, contaDestino);
            RegistrarHistorico(connection, contaDestino, "Transferência Recebida", valor, contaOrigem);
            return true;
        }
    }

    public static decimal ConsultarSaldo(int numeroConta)
    {
        using (var connection = new SqliteConnection(ConnectionString))
        {
            connection.Open();
            return ObterSaldo(connection, numeroConta); //chama o metodo aux p pegar o saldo
        }
    }

    public static void ConsultarHistorico(int numeroConta)
    {
        using (var connection = new SqliteConnection(ConnectionString))
        {
            connection.Open();
            string sql = "SELECT Data, Tipo, Valor, ContaDestino FROM Transacoes WHERE NumeroConta = @numero ORDER BY Id DESC";

            using (var command = new SqliteCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@numero", numeroConta);
                using (var reader = command.ExecuteReader())
                {
                    Console.WriteLine($"\n--- HISTÓRICO DA CONTA {numeroConta} ---");
                    bool temRegistros = false;
                    while (reader.Read()) //le linha por linha do banco
                    {
                        temRegistros = true;
                        string data = reader.GetString(0);
                        string tipo = reader.GetString(1);
                        decimal valor = reader.GetDecimal(2);
                        string destino = reader.IsDBNull(3) ? "" : $" (Destino/Origem: {reader.GetInt32(3)})";

                        Console.WriteLine($"[{data}] {tipo}: R$ {valor:F2}{destino}");
                    }
                    if (!temRegistros) Console.WriteLine("Nenhuma transação encontrada."); //se n tiver nada avisa
                }
            }
        }
    }

    private static decimal ObterSaldo(SqliteConnection connection, int numeroConta)
    {
        string sql = "SELECT Saldo FROM Contas WHERE Numero = @numero";
        using (var command = new SqliteCommand(sql, connection))
        {
            command.Parameters.AddWithValue("@numero", numeroConta);
            var result = command.ExecuteScalar(); //pega so um valor unico
            return result != null ? Convert.ToDecimal(result) : -1;
        }
    }

    private static void RegistrarHistorico(SqliteConnection connection, int numeroConta, string tipo, decimal valor, int? contaDestino)
    {
        string sql = "INSERT INTO Transacoes (NumeroConta, Tipo, Valor, Data, ContaDestino) VALUES (@numero, @tipo, @valor, @data, @destino)";
        using (var command = new SqliteCommand(sql, connection))
        {
            command.Parameters.AddWithValue("@numero", numeroConta);
            command.Parameters.AddWithValue("@tipo", tipo);
            command.Parameters.AddWithValue("@valor", valor);
            command.Parameters.AddWithValue("@data", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")); //pega data e hora atual
            command.Parameters.AddWithValue("@destino", contaDestino.HasValue ? (object)contaDestino.Value : DBNull.Value); //trata se for nulo
            command.ExecuteNonQuery();
        }
    }
}