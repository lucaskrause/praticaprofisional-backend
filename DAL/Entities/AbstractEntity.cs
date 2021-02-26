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
            this.Status = "Ativo";
        }

        [Key]
        [Column("id")]
        public int codigo { get; set; }

        [Column("dtCadastro")]
        [DataType(DataType.DateTime)]
        [JsonIgnore]
        public DateTime? dtCadastro { get; set; }

        [Column("dtAlteracao")]
        [DataType(DataType.DateTime)]
        [JsonIgnore]
        public DateTime? dtAlteracao { get; set; }

        [Column("status")]
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
