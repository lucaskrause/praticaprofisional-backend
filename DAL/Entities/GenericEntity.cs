using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RUPsystem.Entities
{
    public abstract class GenericEntity
    {
        public GenericEntity()
        {
            this.Id = 0;
            this.Status = "Ativo";
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("creation_date")]
        [DataType(DataType.DateTime)]
        [JsonIgnore]
        public DateTime? CreationDate { get; set; }

        [Column("last_modified")]
        [DataType(DataType.DateTime)]
        [JsonIgnore]
        public DateTime? LastModified { get; set; }

        [Column("status")]
        public virtual string Status { get; set; }

        public virtual void PrepareSave()
        {
            this.CreationDate = CreationDate ?? DateTime.Now;
            this.LastModified = (Id > 0 ? DateTime.Now : this.CreationDate);
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
