using BoletoNetCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
//using NReco.PdfGenerator;

namespace ConsoleApp1
{
    internal sealed class Utils
    {
        private static int _contador = 1;

        private static int _proximoNossoNumero = 1;

        internal static Cedente GerarCedente(string codigoCedente, string digitoCodigoCedente, string codigoTransmissao, ContaBancaria contaBancaria)
        {
            return new Cedente
            {
                CPFCNPJ = "23.322.675/0001-52",
                Nome = "INTERMEIO SOLUCOES EM PAGAMENTOS",
                Codigo = codigoCedente,
                CodigoDV = digitoCodigoCedente,
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
        }

        internal static Sacado GerarSacado()
        {
            if (_contador % 2 != 0)
                return new Sacado
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
                        CEP = "04140-110"
                    }
                };
            return new Sacado
            {
                CPFCNPJ = "71.738.978/0001-01",
                Nome = "Sacado Teste PJ",
                Observacoes = "Matricula 123/4",
                Endereco = new Endereco
                {
                    LogradouroEndereco = "Avenida Testando",
                    LogradouroNumero = "123",
                    Bairro = "Bairro",
                    Cidade = "Cidade",
                    UF = "SP",
                    CEP = "12345678"
                }
            };
        }

        internal static Boletos GerarBoletos(IBanco banco, int quantidadeBoletos, string aceite, string NossoNumero)
        {
            var boletos = new Boletos
            {
                Banco = banco
            };
            for (var i = 1; i <= quantidadeBoletos; i++)
                boletos.Add(GerarBoleto(banco, i, aceite, NossoNumero));
            return boletos;
        }

        internal static Boleto GerarBoleto(IBanco banco, int i, string aceite, string NossoNumero)
        {
            if (aceite == "?")
                aceite = _contador % 2 == 0 ? "N" : "A";

            var boleto = new Boleto(banco)
            {
                Sacado = GerarSacado(),
                DataEmissao = DateTime.Now,
                DataProcessamento = DateTime.Now,
                DataVencimento = new DateTime(2019, 03, 23),
                ValorTitulo = (decimal)90.00,
                NossoNumero = NossoNumero == "" ? "" : NossoNumero,
                NumeroDocumento = "BB" + _proximoNossoNumero.ToString("D6") + (char)(64 + i),
                EspecieDocumento = TipoEspecieDocumento.DM,
                Aceite = aceite,
                CodigoInstrucao1 = "11",
                CodigoInstrucao2 = "22",
                DataDesconto = DateTime.Now.AddMonths(i),
                ValorDesconto = (decimal)(100 * i * 0.10),
                DataMulta = new DateTime(2019, 03, 20),
                PercentualMulta = (decimal)2.00,
                ValorMulta = (decimal)03.13,
                DataJuros = DateTime.Now.AddMonths(i),
                PercentualJurosDia = (decimal)0.2,
                ValorJurosDia = (decimal)(100 * i * (0.2 / 100)),
                MensagemArquivoRemessa = "Mensagem para o arquivo remessa",
                NumeroControleParticipante = "CHAVEPRIMARIA=" + _proximoNossoNumero
            };
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
                boleto.Avalista = GerarSacado();
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

        internal static void RenderizaBoletos(IBanco banco, TipoArquivo tipoArquivo, string nomeCarteira, int quantidadeBoletos, bool gerarPDF, string aceite, string NossoNumero)
        {
            var boletos = GerarBoletos(banco, quantidadeBoletos, aceite, NossoNumero);

            try
            {
                var html = new StringBuilder();
                var contador = 1;
                foreach (var boletoTmp in boletos)
                {
                    using (var boletoParaImpressao = new BoletoBancario
                    {
                        Boleto = boletoTmp,
                        OcultarInstrucoes = true,
                        MostrarComprovanteEntrega = false,
                        MostrarEnderecoCedente = false,
                        ExibirDemonstrativo = false,
                        OcultarEnderecoSacado = false
                    })
                    {
                        var nomeArquivo = Path.Combine(Path.GetTempPath(), "Boleto2Net", $"{nomeCarteira}_{tipoArquivo}_{contador}.html");

                        if (File.Exists(nomeArquivo))
                        {
                            File.Delete(nomeArquivo);
                            if (File.Exists(nomeArquivo))
                                Console.WriteLine("Arquivo Boletos (PDF) não foi excluído: " + nomeArquivo);
                        }


                        html.Append("<div style=\"page-break-after: always;\">");
                        html.Append(boletoParaImpressao.MontaHtml());
                        html.Append("</div>");

                        using (StreamWriter sw = new StreamWriter(nomeArquivo))
                        {
                            sw.Write(html);
                        }

                        html.Clear();
                        contador++;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.ToString());
            }
        }
    }
}