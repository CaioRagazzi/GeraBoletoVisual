using BoletoNetCore;
using System;
using System.Text;

namespace ConsoleApp1
{
    class Utils2
    {
        private static int _contador = 1;

        private static int _proximoNossoNumero = 1;

        internal static string RenderizaBoletos(Boleto boleto, TipoArquivo tipoArquivo, string nomeCarteira)
        {
            var boletoParaImPressao = GerarBoleto(boleto);

            try
            {
                var html = new StringBuilder();

                using (var boletoParaImpressao = new BoletoBancario
                {
                    Boleto = boleto,
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

        internal static Boleto GerarBoleto(Boleto boleto)
        {
            var aceite = boleto.Aceite;

            if (aceite == "?")
                aceite = _contador % 2 == 0 ? "N" : "A";

            // Mensagem - Instruções do Caixa
            StringBuilder msgCaixa = new StringBuilder();
            if (boleto.ValorDesconto > 0)
                msgCaixa.AppendLine($"Conceder desconto de {boleto.ValorDesconto.ToString("R$ ##,##0.00")} até {boleto.DataDesconto.ToString("dd/MM/yyyy")}. ");
            if (boleto.ValorMulta > 0)
                msgCaixa.AppendLine($"Cobrar multa de {boleto.ValorMulta.ToString("R$ ##,##0.00")} após o vencimento. ");
            if (boleto.ValorJurosDia > 0)
                msgCaixa.AppendLine($"Cobrar juros de {boleto.ValorJurosDia.ToString("R$ ##,##0.00")} por dia de atraso. ");
            boleto.MensagemInstrucoesCaixa = msgCaixa.ToString();
            // Avalista
            if (_contador % 3 == 0)
            {
                boleto.Avalista = boleto.Sacado;
                boleto.Avalista.Nome = boleto.Avalista.Nome.Replace("Sacado", "Avalista");
            }
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
            _contador++;
            _proximoNossoNumero++;
            return boleto;
        }
    }
}
