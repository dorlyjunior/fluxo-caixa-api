using FluxoCaixa.Projeto.Biblioteca.Enumeradores;
using FluxoCaixa.Projeto.Biblioteca.Excecoes;
using FluxoCaixa.Projeto.Biblioteca.NHibernate;

namespace FluxoCaixa.Projeto.Dominio.Caixa.Contas.Entidades
{
    public class Conta : IEntity
    {
        public virtual int Id { get; set; }
        public virtual string Nome { get; set; }
        public virtual decimal Saldo { get; set; }
        public virtual AtivoInativoEnum Status { get; set; }

        public Conta() { }

        public Conta(string nome)
        {
            SetNome(nome);
            SetSaldo(0);
            SetStatus(AtivoInativoEnum.Ativo);
        }

        public virtual void SetNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome)) throw new CampoObrigatorioExcecao("Nome");

            if (nome.Length > 200) throw new TamanhoMaximoExcecao("Nome", 200);

            Nome = nome;
        }

        public virtual void SetSaldo(decimal saldo)
        {
            if (saldo < 0) throw new RegraDeNegocioExcecao("O saldo do lançamento é inválido. Certifique-se que seja um número maior do que zero.");

            Saldo = saldo;
        }
        
        public virtual void IncrementarSaldo(decimal valor)
        {
            Saldo += valor;
        }
        
        public virtual void DecrementarSaldo(decimal valor)
        {
            Saldo -= valor;
        }

        public virtual void SetStatus(AtivoInativoEnum status)
        {
            Status = status;
        }

        public virtual void SetStatus(int status)
        {
            if (status <= 0 || status > 3) throw new RegraDeNegocioExcecao("O status informado é inválido.");

            Status = (AtivoInativoEnum)status;
        }

        public virtual bool VerificaSeEstaInativa()
        {
            return Status == AtivoInativoEnum.Inativo ? true : false;
        }
    }
}
