using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DataTransferObjects
{
    public class LocacoesDTO
    {
        public DateTime dtLocacao { get; set; }

        public List<AreasLocacao> areasLocacao { get; set; }
    }
}
