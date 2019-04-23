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
            Model.Boleto boletoModel = new Model.Boleto();

            boletoModel.ClienteCedente = new Model.Cliente
            {
                CpfCnpj = "361.908.418-13",
                NomeRazao = "Cliente Cedente"
            };
            boletoModel.NossoNumero = "00000012345";
            boletoModel.EnderecoLogradouro = "Rua monlevade";
            boletoModel.NumeroLogradouro = "128";
            boletoModel.ComeplementoLogradouro = "Casa";
            boletoModel.BairroLogradouro = "Vila Romano";
            boletoModel.CidadeLogradouro = "São Paulo";
            boletoModel.EstadoLogradouro = "SP";
            boletoModel.CepLogradouro = "04679-345";
            boletoModel.DocumentoSacado = "361.908.418-13";
            boletoModel.NomeSacado = "Caio Eduardo Ragazzi Gemignani";
            boletoModel.DataCadastro = new DateTime(2019, 4, 22);
            boletoModel.DataProcessamento = new DateTime(2019, 4, 22);
            boletoModel.DataVencimento = new DateTime(2019, 4, 22).AddMonths(1);
            boletoModel.ValorBoleto = 35.40M;
            boletoModel.NumeroDocumento = "99999999";
            boletoModel.Instrucao1 = "Instrução 1";
            boletoModel.Instrucao2 = "Instrução 2";
            boletoModel.Instrucao3 = "Instrução 3";
            boletoModel.Instrucao4 = "Instrução 4";
            boletoModel.PercentualMulta = 10M;
            boletoModel.ValorMulta = 20M;
            boletoModel.PercentualJuros = 30M;
            boletoModel.ValorJuros = 40M;

            var stringHtml = BoletoNetCore.Util.BoletoHtml.GeraBoleto(boletoModel);
        }
    }
}
