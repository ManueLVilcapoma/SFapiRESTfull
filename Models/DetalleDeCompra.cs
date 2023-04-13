using System;
using System.Collections.Generic;

namespace SFapiRESTfull.Models;

public partial class DetalleDeCompra
{
    public int IdDetalleDeCompra { get; set; }

    public int Cantidad { get; set; }

    public int ProductoId { get; set; }

    public int CompraId { get; set; }

    public virtual Compra Compra { get; set; } = null!;

    public virtual Producto Producto { get; set; } = null!;
}
