using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoletoNetCore.Validator
{
    public class ValidaRenderBoleto : AbstractValidator<Model.Boleto>
    {

        public ValidaRenderBoleto()
        {
            RuleFor(x => x.ContaEmissao)
                .NotNull().WithMessage("É necessário ter uma ContaEmissao");

            When(x => x.ContaEmissao != null, () =>
            {
                RuleFor(x => x.ContaEmissao.Agencia)
                .NotEmpty()
                .WithMessage("É necessário ter um número de Agencia para ContaEmissao.");

                //RuleFor(x => x.ContaEmissao.DigAgencia)
                //    .NotEmpty()
                //    .WithMessage("É necessário ter um número de DigAgencia para ContaEmissao.");

                RuleFor(x => x.ContaEmissao.Conta)
                    .NotEmpty()
                    .WithMessage("É necessário ter um número de conta para ContaEmissao");

                //RuleFor(x => x.ContaEmissao.DigConta)
                //    .NotEmpty()
                //    .WithMessage("É necessário ter um DigConta para ContaEmissao");

                RuleFor(x => x.ContaEmissao.Carteira)
                    .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage("É necessário ter uma Carteira para a ContaEmissao.");
            });

            RuleFor(x => x.ClienteCedente)
                .NotNull().WithMessage("É necessário ter um ClienteCedente ");

            When(x => x.ClienteCedente != null, () =>
            {
                RuleFor(x => x.ClienteCedente.CpfCnpj)
                    .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage("É necessário ter um CpfCnpj para o ClienteCedente.");

                RuleFor(x => x.ClienteCedente.NomeRazao)
                    .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage("É necessário ter um NomeRazao para o ClienteCedente.");
            });

            RuleFor(x => x.DocumentoSacado)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage("É necessário ter um DocumentoSacado.");

            RuleFor(x => x.NossoNumero)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage("É necessário ter um NossoNumero.");

            RuleFor(x => x.NomeSacado)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage("É necessário ter um NomeSacado.");

            RuleFor(x => x.EnderecoLogradouro)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage("É necessário ter um EnderecoLogradouro.");

            RuleFor(x => x.NumeroLogradouro)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage("É necessário ter um NumeroLogradouro.");

            //RuleFor(x => x.ComplementoLogradouro)
            //    .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage("É necessário ter um ComplementoLogradouro.");

            RuleFor(x => x.BairroLogradouro)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage("É necessário ter um BairroLogradouro.");

            RuleFor(x => x.CidadeLogradouro)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage("É necessário ter uma CidadeLogradouro.");

            RuleFor(x => x.EstadoLogradouro)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage("É necessário ter um EstadoLogradouro.");

            RuleFor(x => x.CepLogradouro)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage("É necessário ter um CepLogradouro.");

            RuleFor(x => x.DataCadastro)
                .NotNull().NotEmpty().WithMessage("É necessário ter uma DataCadastro.");

            RuleFor(x => x.DataVencimento)
                .NotNull().NotEmpty().WithMessage("É necessário ter uma DataVencimento.");

            RuleFor(x => x.ValorBoleto)
                .NotNull().NotEmpty().WithMessage("Valor do Boleto deve ser maior do que zero.");

            RuleFor(x => x.NumeroDocumento)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage("É necessário ter um NumeroDocumento.");
        }
    }
}
