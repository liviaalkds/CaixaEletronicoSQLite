using Microsoft.Data.Sqlite;

namespace CaixaEletronico.Data;

public class Database
{
    // name
    private const string ConnectionString = "Data Source=caixaEletronico.db";

    public static void InicializarBanco() //metodo pra criar tabela se nao existir 
    {
        using (var connection = new SqliteConnection(ConnectionString))
        {
            connection.Open();

            string sql = @"
                CREATE TABLE IF NOT EXISTS Contas (
                    Numero INTEGER PRIMARY KEY,
                    Titular TEXT NOT NULL,
                    Saldo REAL NOT NULL
                );

                CREATE TABLE IF NOT EXISTS Transacoes (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    NumeroConta INTEGER,
                    Tipo TEXT NOT NULL,
                    Valor REAL NOT NULL,
                    Data TEXT NOT NULL,
                    ContaDestino INTEGER
                );
            ";

            using (var command = new SqliteCommand(sql, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}