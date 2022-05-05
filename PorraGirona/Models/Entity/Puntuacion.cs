using System;
using System.Collections.Generic;

#nullable disable

namespace PorraGirona.Models.Entity
{
    public partial class Puntuacion
    {
        public int Idpuntuacio { get; set; }
        public int Idpenyista { get; set; }
        public string Alias { get; set; }
        public int? Puntuacio { get; set; }
        public string Temporada { get; set; }
    }
}
