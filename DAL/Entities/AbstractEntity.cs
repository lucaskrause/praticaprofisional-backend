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
            this.Codigo = 0;
            this.Status = "Ativo";
        }

        [Key]
        [Column("id")]
        public int Codigo { get; set; }

        [Column("dtCadastro")]
        [DataType(DataType.DateTime)]
        [JsonIgnore]
        public DateTime? DtCadastro { get; set; }

        [Column("dtAlteracao")]
        [DataType(DataType.DateTime)]
        [JsonIgnore]
        public DateTime? DtAlteracao { get; set; }

        [Column("status")]
        public string Status { get; set; }

        public virtual void PrepareSave()
        {
            this.DtCadastro = this.DtCadastro == null ? DateTime.Now : this.DtCadastro;
            this.DtAlteracao = (Codigo > 0 ? DateTime.Now : this.DtCadastro);
        }

        public virtual void Ativar()
        {
            this.Status = "Ativo";
        }

        public virtual void Inativar()
        {
            this.Status = "Inativo";
        }
    }
}
