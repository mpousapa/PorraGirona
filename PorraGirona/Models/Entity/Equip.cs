using System;
using System.Collections.Generic;

#nullable disable

namespace PorraGirona.Models.Entity
{
    public partial class Equip
    {
        public Equip()
        {
            Jugadors = new HashSet<Jugador>();
            PartitIdequiplocalNavigations = new HashSet<Partit>();
            PartitIdequipvisitantNavigations = new HashSet<Partit>();
        }

        public int Idequip { get; set; }
        public string Nom { get; set; }
        public byte[] Imatge { get; set; }

        public virtual ICollection<Jugador> Jugadors { get; set; }
        public virtual ICollection<Partit> PartitIdequiplocalNavigations { get; set; }
        public virtual ICollection<Partit> PartitIdequipvisitantNavigations { get; set; }
    }
}
