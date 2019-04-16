using BoletoNetCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            IBanco _banco;
            var nossoNumero = "00000012345";
            var aceite = "N";

            var contaBancaria = new ContaBancaria
            {
                Agencia = "3392",
                DigitoAgencia = "0",
                Conta = "0000340",
                DigitoConta = "9",
                CarteiraPadrao = "09",
                TipoCarteiraPadrao = TipoCarteira.CarteiraCobrancaSimples,
                TipoFormaCadastramento = TipoFormaCadastramento.ComRegistro,
                TipoImpressaoBoleto = TipoImpressaoBoleto.Empresa
            };

            var cedente = new Cedente
            {
                CPFCNPJ = "23.322.675/0001-52",
                Nome = "INTERMEIO SOLUCOES EM PAGAMENTOS",
                Codigo = "1213141",
                CodigoDV = "",
                Endereco = new Endereco
                {
                    LogradouroEndereco = "Rua Teste do Cedente",
                    LogradouroNumero = "789",
                    LogradouroComplemento = "Cj 333",
                    Bairro = "Bairro",
                    Cidade = "Cidade",
                    UF = "SP",
                    CEP = "65432987"
                },
                ContaBancaria = contaBancaria
            };

            var sacado = new Sacado
            {
                CPFCNPJ = "23.322.675/0001-52",
                Nome = "Bruno Intermeio Burgues",
                Observacoes = "OBSERVAÇÃO",
                Endereco = new Endereco
                {
                    LogradouroEndereco = "Rua Juréia",
                    LogradouroNumero = "834",
                    Bairro = "Chácara Inglesa",
                    Cidade = "São Paulo",
                    UF = "SP",
                    CEP = "04140110"
                }
            };

            _banco = Banco.Instancia(Bancos.Bradesco);
            _banco.Cedente = cedente;
            _banco.FormataCedente();

            var boleto = new Boleto(_banco)
            {
                Sacado = sacado, //GerarSacado(),
                DataEmissao = DateTime.Now,
                DataProcessamento = DateTime.Now,
                DataVencimento = new DateTime(2019, 03, 23),
                ValorTitulo = (decimal)90.00,
                NossoNumero = nossoNumero == "" ? "" : nossoNumero,
                NumeroDocumento = "BB" + 2.ToString("D6") + (char)(64 + 2),
                EspecieDocumento = TipoEspecieDocumento.DM,
                Aceite = aceite,
                CodigoInstrucao1 = "11",
                CodigoInstrucao2 = "22",
                DataDesconto = DateTime.Now.AddMonths(2),
                ValorDesconto = (decimal)(100 * 2 * 0.10),
                DataMulta = new DateTime(2019, 03, 20),
                PercentualMulta = (decimal)2.00,
                ValorMulta = (decimal)03.13,
                DataJuros = DateTime.Now.AddMonths(2),
                PercentualJurosDia = (decimal)0.2,
                ValorJurosDia = (decimal)(100 * 2 * (0.2 / 100)),
                MensagemArquivoRemessa = "Mensagem para o arquivo remessa",
                NumeroControleParticipante = "CHAVEPRIMARIA=" + 2,
                ValorAbatimento = (decimal)03.00,
                ValorOutrasDespesas = (decimal)08.00,
                ValorOutrosCreditos = (decimal)06.00,
                CarteiraImpressaoBoleto = "009",
            };

            //Utils.RenderizaBoletos(_banco, TipoArquivo.CNAB400, "BancoBradescoCarteira09", 1, "N", "00000012345");

            //var html = Utils2.RenderizaBoletos(boleto, TipoArquivo.CNAB400, "BancoBradescoCarteira09");

            var html = BoletoNetCore.Util.BoletoHtml.RenderizaBoletos(boleto, TipoArquivo.CNAB400, "BancoBradescoCarteira09");
        }
    }
}
