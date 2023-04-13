using System;
using System.Collections.Generic;

namespace SFapiRESTfull.Models;

public partial class Compra
{
    public int IdCompra { get; set; }

    public DateTime Fecha { get; set; }

    public bool? Estado { get; set; }

    public int ProveedorId { get; set; }

    public virtual ICollection<DetalleDeCompra> DetalleDeCompras { get; set; } = new List<DetalleDeCompra>();

    public virtual Proveedore Proveedor { get; set; } = null!;
}
