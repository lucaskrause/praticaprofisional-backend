using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Paises : AbstractEntity
    {
        public string pais { get; set; }

        public string sigla { get; set; }

        public string ddi { get; set; }

        public override string Validation()
        {
            if (this.pais == null || this.pais == "")
            {
                return "País obrigatório";
            }
            else if (this.pais.Length > 50)
            {
                return "País deve ter no máximo 50 caracteres";
            }
            else if (this.sigla == null || this.sigla == "")
            {
                return "Sigla obrigatória";
            }
            else if (this.sigla.Length == 1 || this.sigla.Length > 3)
            {
                return "Sigla deve ter entre 2 e 3 caracteres";
            }
            else if (this.ddi == null || this.ddi == "")
            {
                return "DDI obrigatório";
            }
            else if (this.ddi.Length == 1 || this.ddi.Length > 4)
            {
                return "DDI deve ter entre 2 e 4 caracteres";
            }
            else
            {
                return null;
            }
        }
    }
}
