using System;
using System.Collections.Generic;

#nullable disable

namespace PorraGirona.Models.Entity
{
    public partial class Penyiste
    {
        public Penyiste()
        {
            Porres = new HashSet<Porre>();
        }

        public int Idpenyista { get; set; }
        public string Nom { get; set; }
        public string Cognoms { get; set; }
        public string Nif { get; set; }
        public string Numsoci { get; set; }
        public string Rol { get; set; }
        public string Alias { get; set; }
        public string Password { get; set; }
        public DateTime? Dataalta { get; set; }
        public byte[] Imatge { get; set; }
        public int? Idpenya { get; set; }

        public virtual Penye IdpenyaNavigation { get; set; }
        public virtual ICollection<Porre> Porres { get; set; }
    }
}
