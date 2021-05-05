using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class FormasPagamento : AbstractEntity
    {
        [Required]
        public string descricao { get; set; } 
    }
}
