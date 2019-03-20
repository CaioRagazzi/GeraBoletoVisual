using BoletoNetCore;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            IBanco _banco;

            var contaBancaria = new ContaBancaria
            {
                Agencia = "3392",
                DigitoAgencia = "0",
                Conta = "0000340",
                DigitoConta = "0",
                CarteiraPadrao = "26",
                TipoCarteiraPadrao = TipoCarteira.CarteiraCobrancaSimples,
                TipoFormaCadastramento = TipoFormaCadastramento.ComRegistro,
                TipoImpressaoBoleto = TipoImpressaoBoleto.Empresa
            };
            _banco = Banco.Instancia(Bancos.Bradesco);
            _banco.Cedente = Utils.GerarCedente("1213141", "", "", contaBancaria);
            _banco.FormataCedente();

            List<StringBuilder> htmls = Utils.TestarHomologacao(_banco, TipoArquivo.CNAB400, "BancoBradescoCarteira09", 5, true, "?", 223344);
        }
    }
}
