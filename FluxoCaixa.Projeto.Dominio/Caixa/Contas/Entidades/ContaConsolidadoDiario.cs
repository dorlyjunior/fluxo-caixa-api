using FluxoCaixa.Projeto.Biblioteca.Especificacoes;
using FluxoCaixa.Projeto.Biblioteca.Excecoes;
using FluxoCaixa.Projeto.Biblioteca.NHibernate;
using FluxoCaixa.Projeto.Dominio.Caixa.Contas.Enumeradores;
using System;

namespace FluxoCaixa.Projeto.Dominio.Caixa.Contas.Entidades
{
    public class ContaConsolidadoDiario : IEntity
    {
        public virtual int Id { get; set; }
        public virtual string DiaSemana { get; set; }
        public virtual DateTime DataDia { get; set; }
        public virtual int Dia { get; set; }
        public virtual int Mes { get; set; }
        public virtual int Ano { get; set; }
        public virtual decimal TotalCredito { get; set; }
        public virtual decimal TotalDebito { get; set; }
        public virtual decimal Saldo { get; set; }
        public virtual ContaConsolidadoDiarioStatusEnum Status { get; set; }
        public virtual DateTime DataConsolidacao { get; set; }
        public virtual Conta Conta { set; get; }

        public ContaConsolidadoDiario() { }

        public ContaConsolidadoDiario(Conta conta)
        {
            SetConta(conta);
            SetDataDia();
            SetDiaSemana(DataDia);
            SetDia(DataDia);
            SetMes(DataDia);
            SetAno(DataDia);
            SetTotalCredito(0);
            SetTotalDebito(0);
            SetSaldo(conta.Saldo);
            SetStatus(ContaConsolidadoDiarioStatusEnum.EmAberto);
        }

        public virtual void SetDataDia()
        {
            DataDia = DateTime.Now.Date;
        }

        public virtual void SetDataConsolidacao(DateTime data)
        {
            if (data == null) throw new CampoObrigatorioExcecao("Data de Consolidação");

            DataConsolidacao = data;
        }

        public virtual void SetDiaSemana(DateTime data)
        {
            DiaSemana = DiaDaSemanaEspecificacao.RecuperarDiaDaSemana(data);
        }
        
        public virtual void SetDia(DateTime data)
        {
            Dia = data.Day;
        }
        
        public virtual void SetMes(DateTime data)
        {
            Mes = data.Month;
        }
        
        public virtual void SetAno(DateTime data)
        {
            Ano = data.Year;
        }

        public virtual void SetSaldo(decimal saldo)
        {
            if (saldo < 0) throw new RegraDeNegocioExcecao("O saldo é inválido. Certifique-se que seja um número maior do que zero.");

            Saldo = saldo;
        }
        
        public virtual void SetTotalCredito(decimal total)
        {
            TotalCredito = total;
        }
        
        public virtual void IncrementarTotalCredito(decimal valor)
        {
            TotalCredito += valor;
        }
        
        public virtual void SetTotalDebito(decimal total)
        {
            TotalDebito = total;
        }

        public virtual void IncrementarTotalDebito(decimal valor)
        {
            if(valor >= Conta.Saldo) { throw new RegraDeNegocioExcecao("Não há saldo em conta suficiente para debitar."); }
            
            TotalDebito += valor;
        }
        
        public virtual void AtualizarSaldo()
        { 
            Saldo = TotalCredito - TotalDebito;
        }

        public virtual void SetConta(Conta conta)
        {
            Conta = conta ?? throw new CampoObrigatorioExcecao("Conta");
        }

        public virtual void Consolidar()
        {
            SetStatus(ContaConsolidadoDiarioStatusEnum.Consolidado);
        }

        public virtual void SetStatus(ContaConsolidadoDiarioStatusEnum status)
        {
            Status = status;
        }

        public virtual void SetStatus(int status)
        {
            if (status <= 0 || status > 2) throw new RegraDeNegocioExcecao("O status informado é inválido.");

            Status = (ContaConsolidadoDiarioStatusEnum)status;
        }

        public virtual void VerificaSeODiaEstaConsolidado()
        {
            if (Status == ContaConsolidadoDiarioStatusEnum.Consolidado)
            {
                throw new RegraDeNegocioExcecao("Operação inválida! O dia encontra-se consolidado e não é mais possível fazer lançamentos.");
            }
        }
    }
}
