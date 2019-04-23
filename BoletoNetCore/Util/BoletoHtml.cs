using BoletoNetCore;
using System;
using System.Text;

namespace BoletoNetCore.Util
{
    public static class BoletoHtml
    {
        private static string RenderizaBoletos(Boleto boleto)
        {
            var boletoFormatado = FormataBoleto(boleto);

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
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.ToString());
                return null;
            }
        }

        private static Boleto FormataBoleto(Boleto boleto)
        {
            var aceite = boleto.Aceite;

            if (aceite == "?")
                boleto.Aceite = "N";

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
            // Avalista
            //if (_contador % 3 == 0)
            //{
            //    boleto.Avalista = boleto.Sacado;
            //    boleto.Avalista.Nome = boleto.Avalista.Nome.Replace("Sacado", "Avalista");
            //}
            // Grupo Demonstrativo do Boleto
            var grupoDemonstrativo = new GrupoDemonstrativo { Descricao = "GRUPO 1" };
            grupoDemonstrativo.Itens.Add(new ItemDemonstrativo { Descricao = "Grupo 1, Item 1", Referencia = boleto.DataEmissao.AddMonths(-1).Month + "/" + boleto.DataEmissao.AddMonths(-1).Year, Valor = boleto.ValorTitulo * (decimal)0.15 });
            grupoDemonstrativo.Itens.Add(new ItemDemonstrativo { Descricao = "Grupo 1, Item 2", Referencia = boleto.DataEmissao.AddMonths(-1).Month + "/" + boleto.DataEmissao.AddMonths(-1).Year, Valor = boleto.ValorTitulo * (decimal)0.05 });
            boleto.Demonstrativos.Add(grupoDemonstrativo);
            grupoDemonstrativo = new GrupoDemonstrativo { Descricao = "GRUPO 2" };
            grupoDemonstrativo.Itens.Add(new ItemDemonstrativo { Descricao = "Grupo 2, Item 1", Referencia = boleto.DataEmissao.Month + "/" + boleto.DataEmissao.Year, Valor = boleto.ValorTitulo * (decimal)0.20 });
            boleto.Demonstrativos.Add(grupoDemonstrativo);
            grupoDemonstrativo = new GrupoDemonstrativo { Descricao = "GRUPO 3" };
            grupoDemonstrativo.Itens.Add(new ItemDemonstrativo { Descricao = "Grupo 3, Item 1", Referencia = boleto.DataEmissao.AddMonths(-1).Month + "/" + boleto.DataEmissao.AddMonths(-1).Year, Valor = boleto.ValorTitulo * (decimal)0.37 });
            grupoDemonstrativo.Itens.Add(new ItemDemonstrativo { Descricao = "Grupo 3, Item 2", Referencia = boleto.DataEmissao.Month + "/" + boleto.DataEmissao.Year, Valor = boleto.ValorTitulo * (decimal)0.03 });
            grupoDemonstrativo.Itens.Add(new ItemDemonstrativo { Descricao = "Grupo 3, Item 3", Referencia = boleto.DataEmissao.Month + "/" + boleto.DataEmissao.Year, Valor = boleto.ValorTitulo * (decimal)0.12 });
            grupoDemonstrativo.Itens.Add(new ItemDemonstrativo { Descricao = "Grupo 3, Item 4", Referencia = boleto.DataEmissao.AddMonths(+1).Month + "/" + boleto.DataEmissao.AddMonths(+1).Year, Valor = boleto.ValorTitulo * (decimal)0.08 });
            boleto.Demonstrativos.Add(grupoDemonstrativo);

            boleto.ValidarDados();
            return boleto;
        }

        public static string GeraBoleto(Model.Boleto boletoModel)
        {
            IBanco _banco;
            var nossoNumero = boletoModel.NossoNumero;
            var aceite = "?";

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
                //deixar fixo valores intermeio
                CPFCNPJ = boletoModel.ClienteCedente.CpfCnpj,
                Nome = boletoModel.ClienteCedente.NomeRazao,
                Codigo = "Codigo Cedente", //ajustar
                CodigoDV = "Codigo DV Cedente", //ajustar
                Endereco = new Endereco
                {
                    LogradouroEndereco = boletoModel.EnderecoLogradouro,
                    LogradouroNumero = boletoModel.NumeroLogradouro,
                    LogradouroComplemento = boletoModel.ComeplementoLogradouro,
                    Bairro = boletoModel.BairroLogradouro,
                    Cidade = boletoModel.CidadeLogradouro,
                    UF = boletoModel.EstadoLogradouro,
                    CEP = boletoModel.CepLogradouro
                },
                ContaBancaria = contaBancaria
            };

            var sacado = new Sacado
            {
                CPFCNPJ = boletoModel.DocumentoSacado,
                Nome = boletoModel.NomeSacado,
                Observacoes = "",
                Endereco = new Endereco
                {
                    LogradouroEndereco = boletoModel.EnderecoLogradouro,
                    LogradouroNumero = boletoModel.NumeroLogradouro,
                    LogradouroComplemento = boletoModel.ComeplementoLogradouro,
                    Bairro = boletoModel.BairroLogradouro,
                    Cidade = boletoModel.CidadeLogradouro,
                    UF = boletoModel.EstadoLogradouro,
                    CEP = boletoModel.CepLogradouro
                }
            };

            _banco = Banco.Instancia(Bancos.Bradesco);
            _banco.Cedente = cedente;
            _banco.FormataCedente();

            var boleto = new Boleto(_banco)
            {
                Sacado = sacado,
                DataEmissao = boletoModel.DataCadastro,
                DataProcessamento = Convert.ToDateTime(boletoModel.DataProcessamento),
                DataVencimento = boletoModel.DataVencimento,
                ValorTitulo = boletoModel.ValorBoleto,
                NossoNumero = nossoNumero == "" ? "" : nossoNumero,
                NumeroDocumento = boletoModel.NumeroDocumento,
                Aceite = aceite,
                ComplementoInstrucao1 = boletoModel.Instrucao1,
                ComplementoInstrucao2 = boletoModel.Instrucao2,
                ComplementoInstrucao3 = boletoModel.Instrucao3,
                ComplementoInstrucao4 = boletoModel.Instrucao4,
                PercentualMulta = boletoModel.PercentualMulta,
                ValorMulta = boletoModel.ValorMulta,
                PercentualJurosDia = boletoModel.PercentualJuros,
                ValorJurosDia = boletoModel.ValorJuros,
                EspecieDocumento = TipoEspecieDocumento.DM,
                CarteiraImpressaoBoleto = "009", //ajustar
                //DataDesconto = DateTime.Now.AddMonths(2), //ajustar
                //ValorDesconto = (decimal)(100 * 2 * 0.10), //ajustar
                //DataJuros = DateTime.Now.AddMonths(2), //ajustar
                //DataMulta = new DateTime(2019, 03, 20), //ajustar
                //MensagemArquivoRemessa = "Mensagem para o arquivo remessa", //ajustar
                //NumeroControleParticipante = "CHAVEPRIMARIA=" + 2, //ajustar
                //ValorAbatimento = (decimal)03.00, //ajustar
                //ValorOutrasDespesas = (decimal)08.00, //ajustar
                //ValorOutrosCreditos = (decimal)06.00, //ajustar
            };

            return RenderizaBoletos(boleto);
        }
    }
}
