using System;
using System.Collections.Generic;

namespace SFapiRESTfull.Models;

public partial class UsuarioPermiso
{
    public int IdUsuarioPermiso { get; set; }

    public int UsuarioId { get; set; }

    public int PermisoId { get; set; }

    public virtual Permiso Permiso { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
