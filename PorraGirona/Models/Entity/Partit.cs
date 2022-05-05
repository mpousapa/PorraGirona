using System;
using System.Collections.Generic;

#nullable disable

namespace PorraGirona.Models.Entity
{
    public partial class Partit
    {
        public Partit()
        {
            Porres = new HashSet<Porre>();
        }

        public int Idpartit { get; set; }
        public int? Idequiplocal { get; set; }
        public int? Idequipvisitant { get; set; }
        public DateTime? Datainici { get; set; }
        public int? Jornada { get; set; }
        public int? Golslocal { get; set; }
        public int? Golsvisitant { get; set; }
        public string Finalitzat { get; set; }
        public string Temporada { get; set; }
        public string Idsjugadorslocal { get; set; }
        public string Idsjugadorsvisitant { get; set; }

        public virtual Equip IdequiplocalNavigation { get; set; }
        public virtual Equip IdequipvisitantNavigation { get; set; }
        public virtual ICollection<Porre> Porres { get; set; }
    }
}
