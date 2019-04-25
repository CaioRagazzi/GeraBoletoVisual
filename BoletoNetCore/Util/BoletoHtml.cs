using BoletoNetCore;
using BoletoNetCore.Enums;
using System;
using System.Text;

namespace BoletoNetCore.Util
{
    public static class BoletoHtml
    {
        public static DefaultOfPagination GeraBoleto(Model.Boleto boletoModel)
        {
            try
            {
                var retorno = new Validator.ValidaRenderBoleto().Validate(boletoModel);

                if (retorno.Errors.Count > 0)
                {
                    return new DefaultOfPagination()
                    {
                        Status =false,
                        Resultado =  retorno.Errors
                    };
                }

                IBanco _banco;

                ContaBancaria contaBancaria = new ContaBancaria
                {
                    Agencia = boletoModel.ContaEmissao.Agencia.ToString(),
                    DigitoAgencia = boletoModel.ContaEmissao.DigAgencia.ToString(),
                    Conta = boletoModel.ContaEmissao.Conta.ToString(),
                    DigitoConta = boletoModel.ContaEmissao.DigConta.ToString(),
                    CarteiraPadrao = boletoModel.ContaEmissao.Carteira,
                    TipoCarteiraPadrao = TipoCarteira.CarteiraCobrancaSimples,
                    TipoFormaCadastramento = TipoFormaCadastramento.ComRegistro,
                    TipoImpressaoBoleto = TipoImpressaoBoleto.Empresa
                };

                Cedente cedente = new Cedente
                {
                    CPFCNPJ = boletoModel.ContaEmissao.Cnpj,
                    Nome = string.IsNullOrWhiteSpace(boletoModel.SoftDescript) ? boletoModel.ContaEmissao.NomeCedente + " / " + boletoModel.ClienteCedente.NomeRazao : boletoModel.SoftDescript,
                    Codigo = "Codigo Cedente",
                    CodigoDV = "Codigo DV Cedente",
                    ContaBancaria = contaBancaria
                };

                Sacado sacado = new Sacado
                {
                    CPFCNPJ = boletoModel.DocumentoSacado,
                    Nome = boletoModel.NomeSacado,
                    Observacoes = "",
                    Endereco = new Endereco
                    {
                        LogradouroEndereco = boletoModel.EnderecoLogradouro,
                        LogradouroNumero = boletoModel.NumeroLogradouro,
                        LogradouroComplemento = boletoModel.ComplementoLogradouro,
                        Bairro = boletoModel.BairroLogradouro,
                        Cidade = boletoModel.CidadeLogradouro,
                        UF = boletoModel.EstadoLogradouro,
                        CEP = boletoModel.CepLogradouro
                    }
                };

                _banco = Banco.Instancia(Bancos.Bradesco);
                _banco.Cedente = cedente;
                _banco.FormataCedente();

                Boleto boleto = new Boleto(_banco)
                {
                    Sacado = sacado,
                    DataEmissao = boletoModel.DataCadastro,
                    DataProcessamento = boletoModel.DataCadastro,
                    DataVencimento = boletoModel.DataVencimento,
                    ValorTitulo = boletoModel.ValorBoleto,
                    NossoNumero = boletoModel.NossoNumero,
                    NumeroDocumento = boletoModel.NumeroDocumento,
                    Aceite = "N",
                    ComplementoInstrucao1 = boletoModel.Instrucao1,
                    ComplementoInstrucao2 = boletoModel.Instrucao2,
                    ComplementoInstrucao3 = boletoModel.Instrucao3,
                    ComplementoInstrucao4 = boletoModel.Instrucao4,
                    PercentualMulta = boletoModel.PercentualMulta,
                    ValorMulta = boletoModel.ValorMulta,
                    PercentualJurosDia = boletoModel.PercentualJuros,
                    ValorJurosDia = boletoModel.ValorJuros,
                    EspecieDocumento = TipoEspecieDocumento.DM,
                    CarteiraImpressaoBoleto = boletoModel.ContaEmissao.Carteira,
                };

                return new DefaultOfPagination()
                {
                    Status = true,
                    Resultado = RenderizaBoletos(boleto)
                };

            }
            catch (Exception ex)
            {
                return new DefaultOfPagination()
                {
                    Status = false,
                    Resultado = $"Message {ex.Message} ==> Trace {ex.StackTrace} "
            };
            }
        }

        private static string RenderizaBoletos(Boleto boleto)
        {
            boleto.FormataDados();
            var boletoFormatado = FormataInstrucao(boleto);
            
            try
            {
                var html = new StringBuilder();

                using (var boletoParaImpressao = new BoletoBancario
                {
                    Boleto = boletoFormatado,
                    OcultarInstrucoes = true,
                    MostrarComprovanteEntrega = false,
                    MostrarEnderecoCedente = false,
                    ExibirDemonstrativo = false,
                    OcultarEnderecoSacado = false,
                    MostrarCodigoCarteira = true
                })
                {

                    html.Append("<div style=\"page-break-after: always;\">");
                    html.Append(boletoParaImpressao.MontaHtmlEmbedded());
                    html.Append("</div>");
                }

                return Convert.ToString(html);

            }
            catch (Exception ex)
            {
                return $"Message {ex.Message} ==> Trace {ex.StackTrace} ";
            }
        }

        private static Boleto FormataInstrucao(Boleto boleto)
        {
           
            // Mensagem - Instruções do Caixa
            StringBuilder msgCaixa = new StringBuilder();
            if (boleto.ValorDesconto > 0)
                msgCaixa.AppendLine($"Conceder desconto de {boleto.ValorDesconto.ToString("R$ ##,##0.00")} até {boleto.DataDesconto.ToString("dd/MM/yyyy")}. ");
            if (boleto.ValorMulta > 0)
                msgCaixa.AppendLine($"Cobrar multa de {boleto.ValorMulta.ToString("R$ ##,##0.00")} após o vencimento. ");
            if (boleto.ValorJurosDia > 0)
                msgCaixa.AppendLine($"Cobrar juros de {boleto.ValorJurosDia.ToString("R$ ##,##0.00")} por dia de atraso. ");
            if (!String.IsNullOrEmpty(boleto.ComplementoInstrucao1))
                msgCaixa.AppendLine("<br/>" + boleto.ComplementoInstrucao1);
            if (!String.IsNullOrEmpty(boleto.ComplementoInstrucao2))
                msgCaixa.AppendLine("<br/>" + boleto.ComplementoInstrucao2);
            if (!String.IsNullOrEmpty(boleto.ComplementoInstrucao3))
                msgCaixa.AppendLine("<br/>" + boleto.ComplementoInstrucao3);
            if (!String.IsNullOrEmpty(boleto.ComplementoInstrucao4))
                msgCaixa.AppendLine("<br/>" + boleto.ComplementoInstrucao4);
            boleto.MensagemInstrucoesCaixa = msgCaixa.ToString();
           
            return boleto;

        }
    }
}
