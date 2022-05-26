using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#nullable disable

namespace PorraGirona.Models.Entity
{
    public partial class PronPorres
    {
        public int Id { get; set; }
        
        public int? Jornada { get; set; }

        public DateTime Datainici { get; set; }

        public string Local { get; set; }

        public string Visitant { get; set; }

        public int? Predlocal { get; set; }

        public int? Predvisitant { get; set; }

        public int? Golslocal { get; set; }

        public int? Golsvisitant { get; set; }

        public byte[] Escutlocal { get; set; }
        public byte[] Escutvisitant { get; set; }
    }
}
