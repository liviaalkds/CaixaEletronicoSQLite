namespace CaixaEletronico.Models;
public class Conta
{
    // criei variaveis q são privadas pois se tratam de dados sensíveis
    private int _numero;
    private string _titular;
    private decimal _saldo;

    //construtor p new
    public Conta(int numero, string titular)
    {
        _numero = numero;
        _titular = titular;
        _saldo = 0.0m; // zerado e m conforme regra
    }
    
    public int Numero => _numero;
    public string Titular => _titular;
    public decimal Saldo => _saldo; //para q outras classses possam ler mas sem modificar
}