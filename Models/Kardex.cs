using System;
using System.Collections.Generic;

namespace SFapiRESTfull.Models;

public partial class Kardex
{
    public int IdKardex { get; set; }

    public DateTime FechaMovimiento { get; set; }

    public string? Motivo { get; set; }

    public string TipoMovimiento { get; set; } = null!;

    public int Cantidad { get; set; }

    public int Habia { get; set; }

    public int Hay { get; set; }

    public int UsuarioId { get; set; }

    public int ProductoId { get; set; }

    public virtual Producto Producto { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
