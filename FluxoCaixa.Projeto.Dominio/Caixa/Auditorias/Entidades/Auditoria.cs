using FluxoCaixa.Projeto.Biblioteca.Excecoes;
using FluxoCaixa.Projeto.Biblioteca.NHibernate;
using System;

namespace FluxoCaixa.Projeto.Dominio.Caixa.Auditorias.Entidades
{
    public class Auditoria : IEntity
    {
        public virtual int Id { get; set; }
        public virtual string Usuario { get; set; }
        public virtual string Acao { get; set; }
        public virtual DateTime DataAcao { get; set; }

        public Auditoria () { }

        public Auditoria(string usuario, string acao)
        {
            SetUsuario(usuario);
            SetAcao(acao);
            SetDataAcao(DateTime.Now);
        }

        public virtual void SetUsuario(string acao)
        {
            if (string.IsNullOrWhiteSpace(acao)) throw new CampoObrigatorioExcecao("Usuário");

            if (acao.Length > 200) throw new TamanhoMaximoExcecao("Usuário", 200);

            Usuario = acao;
        }

        public virtual void SetAcao(string acao)
        {
            if (string.IsNullOrWhiteSpace(acao)) throw new CampoObrigatorioExcecao("Ação");

            if (acao.Length > 350) throw new TamanhoMaximoExcecao("Ação", 350);

            Acao = acao;
        }

        public virtual void SetDataAcao(DateTime data)
        {
            DataAcao = data;
        }

    }
}
