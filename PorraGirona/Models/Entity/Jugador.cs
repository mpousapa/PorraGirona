using System;
using System.Collections.Generic;

#nullable disable

namespace PorraGirona.Models.Entity
{
    public partial class Jugador
    {
        public int Idjugador { get; set; }
        public string Nom { get; set; }
        public int? Idequip { get; set; }
        public string Temporada { get; set; }

        public virtual Equip IdequipNavigation { get; set; }
    }
}
