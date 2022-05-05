using System;
using System.Collections.Generic;

#nullable disable

namespace PorraGirona.Models.Entity
{
    public partial class Porre
    {
        public int Idporra { get; set; }
        public int? Golslocal { get; set; }
        public int? Golsvisitant { get; set; }
        public DateTime? Data { get; set; }
        public string Idsgolejadorslocal { get; set; }
        public string Idsgolejadorsvisitant { get; set; }
        public int? Idpenyista { get; set; }
        public int? Idpartit { get; set; }

        public virtual Partit IdpartitNavigation { get; set; }
        public virtual Penyiste IdpenyistaNavigation { get; set; }
    }
}
