﻿using BoletoNetCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Model.Boleto boletoModel = new Model.Boleto
            //{
            //    ClienteCedente = new Model.Cliente
            //    {
            //        CpfCnpj = "361.908.418-13",
            //        NomeRazao = "Cliente Cedente"
            //    },
            //    NossoNumero = "00000012345",
            //    EnderecoLogradouro = "Rua monlevade",
            //    NumeroLogradouro = "128",
            //    ComeplementoLogradouro = "Casa",
            //    BairroLogradouro = "Vila Romano",
            //    CidadeLogradouro = "São Paulo",
            //    EstadoLogradouro = "SP",
            //    CepLogradouro = "04679-345",
            //    DocumentoSacado = "361.908.418-13",
            //    NomeSacado = "Caio Eduardo Ragazzi Gemignani",
            //    DataCadastro = new DateTime(2019, 4, 22),
            //    DataProcessamento = new DateTime(2019, 4, 22),
            //    DataVencimento = new DateTime(2019, 4, 22).AddMonths(1),
            //    ValorBoleto = 35.40M,
            //    NumeroDocumento = "99999999",
            //    Instrucao1 = "Instrução 1",
            //    Instrucao2 = "Instrução 2",
            //    Instrucao3 = "Instrução 3",
            //    Instrucao4 = "Instrução 4",
            //    PercentualMulta = 00M,
            //    ValorMulta = 00M,
            //    PercentualJuros = 00M,
            //    ValorJuros = 00M
            //};

            //Model.Boleto boletoModel2 = new Model.Boleto
            //{

            //    Id = new Guid("3EC118ED-B348-4B21-81B7-00D0497C6E9B"),
            //    DataCadastro = new DateTime(2019, 04, 17, 14, 30, 18),
            //    Status = false,
            //    NomeSacado = null,
            //    DocumentoSacado = null,
            //    Logradouro = null,
            //    EnderecoLogradouro = null,
            //    ComplementoLogradouro = null,
            //    BairroLogradouro = null,
            //    CepLogradouro = "",
            //    NumeroLogradouro = null,
            //    CidadeLogradouro = null,
            //    EstadoLogradouro = null,
            //    EmailSacado = null,
            //    TelefoneSacado = null,
            //    NossoNumero = null,
            //    NumeroDocumento = null,
            //    DataVencimento = new DateTime(2019, 04, 27, 00, 00, 00),
            //    ValorBoleto = -100M,
            //    Instrucao1 = null,
            //    Instrucao2 = null,
            //    Instrucao3 = null,
            //    Instrucao4 = null,
            //    SoftDescript = null,
            //    DataPagamento = null,
            //    Valorpago = 0.00M,
            //    StatusBoleto = 0,
            //    DataDaBaixa = null,
            //    ConfirmacaoCliente = false,
            //    RegistradoNoBancoCentral = true,
            //    PercentualJuros = 0M,
            //    ValorJuros = 0.00M,
            //    QntDiasJuros = 3,
            //    PercentualMulta = 0M,
            //    ValorMulta = 0.00M,
            //    QntDiasMultas = 0,
            //    LinhaDigitavel = null,
            //    CodigoDoCodigoDeBarras = null,
            //    UsuarioId = new Guid("387274D9-FCE0-46B4-B028-67BF65133EA8"),
            //    ClienteCedenteId = new Guid("73878520-3D04-4ABF-BA8A-68D438F94080"),
            //    Celular = null,
            //    BloqueadoAte = null,
            //    Compensado = false,
            //    DataCompensacao = null,
            //    Processado = false,
            //    DataProcessamento = null,
            //    BaixadoPorDecursoDePrazo = false,
            //    TaxaCobrada = 0.00M,
            //    ParametroId = new Guid("6243667D-E4EA-4237-8503-C4F52516CB5D"),
            //    SmsEnviados = 0,
            //    EmailsEnviados = 0,
            //    Tipo = null,
            //    ControleBoletoCliente = null,
            //    ContaEmissaoId = null,
            //    LoteId = null,
            //    SplitId = null,
            //    ClienteCedente = new Model.Cliente
            //    {
            //        CpfCnpj = null,
            //        NomeRazao = null
            //    },
            //    ContaEmissao = new Model.ContaEmissao
            //    {
            //        Agencia = 0,
            //        DigAgencia = 0,
            //        Conta = 0,
            //        DigConta = 0,
            //        Carteira = null
            //    }
            //};

            Model.Boleto boletoModel3 = new Model.Boleto
            {
                Id = new Guid("3EC118ED-B348-4B21-81B7-00D0497C6E9B"),
                DataCadastro = new DateTime(2019, 04, 22),
                Status = false,
                NomeSacado = "CAIOCAIOCAIOCAIOCAIOCAIOCAIOCAIOCAIOCAIOCAIOCAIOCAIOCAIOCAIOCAIOCAIOCAIOCAIOCAIOCAIOCAIO",
                DocumentoSacado = "44572133816",
                Logradouro = "RUA",
                EnderecoLogradouro = "ONDE OS GATOS PERDEU AS BOTAS",
                ComplementoLogradouro = "Casa",
                BairroLogradouro = "CENTRO",
                CepLogradouro = "04045004",
                NumeroLogradouro = "10",
                CidadeLogradouro = "SAO PAULO",
                EstadoLogradouro = "SP",
                EmailSacado = "josedascouves@gmail.com",
                TelefoneSacado = "111111111",
                NossoNumero = "90670000025",
                NumeroDocumento = "33445566",
                DataVencimento = new DateTime(2019, 05, 09),
                ValorBoleto = 500000000000000000.00M,
                Instrucao1 = "MOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOM",
                Instrucao2 = "MOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOM",
                Instrucao3 = "MOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOMOM",
                SoftDescript = "         a ",
                DataPagamento = null,
                Valorpago = 0.00M,
                StatusBoleto = 0,
                DataDaBaixa = null,
                ConfirmacaoCliente = false,
                RegistradoNoBancoCentral = true,
                PercentualJuros = 0.00M,
                ValorJuros = 5000000000.00M,
                QntDiasJuros = 0,
                PercentualMulta = 0.00M,
                ValorMulta = 50000000000.00M,
                QntDiasMultas = 0,
                LinhaDigitavel = null,
                CodigoDoCodigoDeBarras = null,
                UsuarioId = new Guid("387274D9-FCE0-46B4-B028-67BF65133EA8"),
                ClienteCedenteId = new Guid("73878520-3D04-4ABF-BA8A-68D438F94080"),
                Celular = null,
                BloqueadoAte = null,
                Compensado = false,
                DataCompensacao = null,
                Processado = false,
                DataProcessamento = null,
                BaixadoPorDecursoDePrazo = false,
                TaxaCobrada = 0.00M,
                ParametroId = new Guid("6243667D-E4EA-4237-8503-C4F52516CB5D"),
                SmsEnviados = 0,
                EmailsEnviados = 0,
                Tipo = null,
                ControleBoletoCliente = null,
                ContaEmissaoId = null,
                LoteId = null,
                SplitId = null,
                ClienteCedente = new Model.Cliente
                {
                    CpfCnpj = "23.322.675/0001-52",
                    NomeRazao = "INTERMEIO SOLUÇÕES DE PAGAMENTO LTDA"
                },
                ContaEmissao = new Model.ContaEmissao
                {
                    Agencia = 3392,
                    DigAgencia = 0,
                    Conta = 340,
                    DigConta = 9,
                    Carteira = "26",
                    Cnpj = "11.111.111/1111-11",
                    NomeCedente = "Intermeio"                    
                },
            };

            var stringHtml = BoletoNetCore.Util.BoletoHtml.GeraBoleto(boletoModel3);
        }
    }
}
