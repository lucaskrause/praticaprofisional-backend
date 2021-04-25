using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RUPsystem.Entities
{
    public abstract class AbstractEntity
    {
        public AbstractEntity()
        {
            this.codigo = 0;
            this.status = "Ativo";
        }

        [Key]
        [Column("id")]
        public int codigo { get; set; }

        [Column("dtCadastro")]
        [DataType(DataType.DateTime)]
        public DateTime? dtCadastro { get; set; }

        [Column("dtAlteracao")]
        [DataType(DataType.DateTime)]
        public DateTime? dtAlteracao { get; set; }

        [Column("status")]
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
    }
}
