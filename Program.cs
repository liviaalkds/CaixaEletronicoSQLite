using CaixaEletronico.Data;
using CaixaEletronico.Models;
using CaixaEletronico;

namespace CaixaEletronico;

class Program
{
    static void Main(string[] args)
    {
        Database.InicializarBanco();
        int opcao = -1;

        do
        {
            Console.Clear();
            Console.WriteLine("==== Caixa Eletrônico ====");
            Console.WriteLine("1 - Criar Conta");
            Console.WriteLine("2 - Depositar");
            Console.WriteLine("3 - Sacar");
            Console.WriteLine("4 - Transferir");
            Console.WriteLine("5 - Consultar Saldo");
            Console.WriteLine("6 - Consultar Histórico");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha uma opção: ");

            if (int.TryParse(Console.ReadLine(), out opcao))
            {
                switch (opcao)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("--- CRIAR CONTA ---");
                        Console.Write("Digite o nome do titular: ");
                        string titular = Console.ReadLine();
                        Conta novaConta = Transacao.CriaConta(titular);
                        Console.WriteLine($"\nConta criada com sucesso!");
                        Console.WriteLine($"Número da Conta: {novaConta.Numero}");
                        Console.WriteLine($"Titular: {novaConta.Titular}");
                        break;

                    case 2:
                        Console.Clear();
                        Console.WriteLine("--- DEPOSITAR ---");
                        Console.Write("Número da conta: ");
                        if (int.TryParse(Console.ReadLine(), out int contaDep))
                        {
                            Console.Write("Valor do depósito: R$ ");
                            if (decimal.TryParse(Console.ReadLine(), out decimal valDep) && valDep > 0)
                            {
                                Transacao.Depositar(contaDep, valDep);
                                Console.WriteLine("\nDepósito realizado com sucesso!");
                            }
                            else { Console.WriteLine("\nValor inválido."); }
                        }
                        break;

                    case 3:
                        Console.Clear();
                        Console.WriteLine("--- SACAR ---");
                        Console.Write("Número da conta: ");
                        if (int.TryParse(Console.ReadLine(), out int contaSaq))
                        {
                            Console.Write("Valor do saque: R$ ");
                            if (decimal.TryParse(Console.ReadLine(), out decimal valSaq) && valSaq > 0)
                            {
                                bool sucesso = Transacao.Sacar(contaSaq, valSaq);
                                if (sucesso) Console.WriteLine("\nSaque realizado com sucesso!");
                                else Console.WriteLine("\nSaldo insuficiente ou conta inválida!");
                            }
                            else { Console.WriteLine("\nValor inválido."); }
                        }
                        break;

                    case 4:
                        Console.Clear();
                        Console.WriteLine("--- TRANSFERIR ---");
                        Console.Write("Número da conta de ORIGEM: ");
                        if (int.TryParse(Console.ReadLine(), out int origem))
                        {
                            Console.Write("Número da conta de DESTINO: ");
                            if (int.TryParse(Console.ReadLine(), out int destino))
                            {
                                Console.Write("Valor da transferência: R$ ");
                                if (decimal.TryParse(Console.ReadLine(), out decimal valTrans) && valTrans > 0)
                                {
                                    bool sucesso = Transacao.Transferir(origem, destino, valTrans);
                                    if (sucesso) Console.WriteLine("\nTransferência realizada com sucesso!");
                                    else Console.WriteLine("\nSaldo insuficiente na origem ou contas inválidas!");
                                }
                                else { Console.WriteLine("\nValor inválido."); }
                            }
                        }
                        break;

                    case 5:
                        Console.Clear();
                        Console.WriteLine("--- CONSULTAR SALDO ---");
                        Console.Write("Número da conta: ");
                        if (int.TryParse(Console.ReadLine(), out int contaSaldo))
                        {
                            decimal saldo = Transacao.ConsultarSaldo(contaSaldo);
                            if (saldo >= 0) Console.WriteLine($"\nSaldo atual: R$ {saldo:F2}");
                            else Console.WriteLine("\nConta não encontrada.");
                        }
                        break;

                    case 6:
                        Console.Clear();
                        Console.WriteLine("--- CONSULTAR HISTÓRICO ---");
                        Console.Write("Número da conta: ");
                        if (int.TryParse(Console.ReadLine(), out int contaHist))
                        {
                            Transacao.ConsultarHistorico(contaHist);
                        }
                        break;

                    case 0:
                        Console.WriteLine("\nSaindo do sistema... Até logo!");
                        break;

                    default:
                        Console.WriteLine("\nOpção inválida!");
                        break;
                }

                if (opcao != 0)
                {
                    Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("\nDigite um número válido. Pressione qualquer tecla...");
                Console.ReadKey();
            }

        } while (opcao != 0);
    }
}