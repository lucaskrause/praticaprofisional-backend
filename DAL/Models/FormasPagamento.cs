using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class FormasPagamento : AbstractEntity
    {
        public string descricao { get; set; }

        public override string Validation()
        {
            if (this.descricao == null || this.descricao == "")
            {
                return "Forma de Pagamento obrigatória";
            }
            else
            {
                return null;
            }
        }
    }
}
