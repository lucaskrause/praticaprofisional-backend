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
            else if (this.sigla == null || this.sigla == "")
            {
                return "Sigla obrigatória";
            }
            else if (this.ddi == null || this.ddi == "")
            {
                return "DDI obrigatório";
            }
            else
            {
                return null;
            }
        }
    }
}
