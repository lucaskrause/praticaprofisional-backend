using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    class AbstractEntity
    {
        [Required]
        public string Pais { get; set; }

        [Required]
        public string Sigla { get; set; }

        [Required]
        public string DDI { get; set; }
    }
}
