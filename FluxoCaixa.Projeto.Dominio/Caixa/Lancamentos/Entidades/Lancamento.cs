using FluxoCaixa.Projeto.Biblioteca.Excecoes;
using FluxoCaixa.Projeto.Biblioteca.NHibernate;
using FluxoCaixa.Projeto.Dominio.Caixa.Contas.Entidades;
using FluxoCaixa.Projeto.Dominio.Caixa.Lancamentos.Enumeradores;
using System;

namespace FluxoCaixa.Projeto.Dominio.Caixa.Lancamentos.Entidades
{
    public class Lancamento : IEntity
    {
        public virtual int Id { get; set; }
        public virtual string IdTransacao { get; set; }
        public virtual LancamentoTipoEnum Tipo { get; set; }
        public virtual string Descricao { get; set; }
        public virtual decimal Valor { get; set; }
        public virtual DateTime DataLancamento { get; set; }
        public virtual LancamentoStatusEnum Status { get; set; }
        public virtual Conta Conta { get; set; }
        
        public Lancamento() { }

        public Lancamento(Conta conta, LancamentoTipoEnum tipo, string descricao, decimal valor) 
        {
            SetConta(conta);
            SetIdTransacao();
            SetTipo(tipo);
            SetDescricao(descricao);
            SetValor(valor);
            SetDataLancamento();
            SetStatus(LancamentoStatusEnum.Efetivado);
        }
        
        public virtual void SetIdTransacao()
        {
            if(!string.IsNullOrWhiteSpace(IdTransacao)) 
            {
                throw new RegraDeNegocioExcecao("Operação inválida. Não é possível alterar o ID da transação uma vez criado.");
            }
            
            IdTransacao = Guid.NewGuid().ToString();
        }

        public virtual void SetTipo(LancamentoTipoEnum tipo)
        {
            Tipo = tipo;
        }

        public virtual void SetTipo(int tipo)
        {
            if (tipo <= 0 || tipo > 2) throw new RegraDeNegocioExcecao("O tipo informado é inválido. Certifique-se que seja Crédito (1) ou Débito (2)");

            Tipo = (LancamentoTipoEnum)tipo;
        }

        public virtual void SetDataLancamento()
        {
            DataLancamento = DateTime.Now;
        }

        public virtual void SetDescricao(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao)) throw new CampoObrigatorioExcecao("Descrição");

            if (descricao.Length > 200) throw new TamanhoMaximoExcecao("Descrição", 200);

            Descricao = descricao;
        }
        
        public virtual void SetValor(decimal valor)
        {
            if (valor <= 0) throw new RegraDeNegocioExcecao("O valor do lançamento é inválido. Certifique-se que seja um número maior do que zero.");

            Valor = valor;
        }

        public virtual void SetStatus(LancamentoStatusEnum status)
        {
            Status = status;
        }

        public virtual void SetStatus(int status)
        {
            if (status <= 0 || status > 4) throw new RegraDeNegocioExcecao("O status informado é inválido");

            Status = (LancamentoStatusEnum)status;
        }

        public virtual void SetConta(Conta conta)
        {
            Conta = conta ?? throw new CampoObrigatorioExcecao("Conta");
        }
    }
}
