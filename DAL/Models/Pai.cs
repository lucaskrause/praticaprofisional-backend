using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public abstract class Pai
    {
        public Pai()
        {
            this.codigo = 0;
            this.status = "Ativo";
        }

        public int codigo { get; set; }

        public DateTime? dtCadastro { get; set; }

        public DateTime? dtAlteracao { get; set; }

        public string status { get; set; }

        public virtual void PrepareSave()
        {
            this.dtCadastro = this.dtCadastro == null ? DateTime.Now : this.dtCadastro;
            this.dtAlteracao = (codigo > 0 ? DateTime.Now : this.dtCadastro);
        }

        public virtual void Ativar()
        {
            this.status = "Ativo";
        }

        public virtual void Inativar()
        {
            this.status = "Inativo";
        }

        public virtual void Cancelar()
        {
            this.status = "Cancelado";
        }

        public abstract string Validation();
    }
}
