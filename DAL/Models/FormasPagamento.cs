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
            else if (this.descricao.Length > 50)
            {
                return "Forma de Pagamento deve ter no máximo 50 caracteres";
            }
            else
            {
                return null;
            }
        }
    }
}
