using System;

namespace RUPsystem.Entities
{
    public abstract class AbstractEntity
    {
        public AbstractEntity()
        {
            this.codigo = 0;
            this.Status = "Ativo";
        }

        public int codigo { get; set; }

        public DateTime dtCadastro { get; set; }

        public DateTime dtAlteracao { get; set; }

        public string Status { get; set; }

        public virtual void PrepareSave()
        {
            this.dtCadastro = this.dtCadastro == null ? DateTime.Now : this.dtCadastro;
            this.dtAlteracao = (codigo > 0 ? DateTime.Now : this.dtAlteracao);
        }

        public virtual void Ativar()
        {
            this.Status = "Ativo";
        }

        public virtual void Desativar()
        {
            this.Status = "Inativo";
        }
    }
}
