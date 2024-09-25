using System;
using System.Collections.Generic;

namespace kinder_care.Models;

public partial class RelPadresNino
{
    public int IdPadre { get; set; }

    public int IdNino { get; set; }

    public string Relacion { get; set; } = null!;

    public virtual Nino IdNinoNavigation { get; set; } = null!;

    public virtual Padre IdPadreNavigation { get; set; } = null!;
}
