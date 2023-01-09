using System;
using System.Collections.Generic;

namespace NeatBurguer.Models
{
    public partial class Clasificacion
    {
        public Clasificacion()
        {
            Menus = new HashSet<Menu>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Menu> Menus { get; set; }
    }
}
