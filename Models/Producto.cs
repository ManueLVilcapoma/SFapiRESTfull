using System;
using System.Collections.Generic;

namespace SFapiRESTfull.Models;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string Codigo { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public int Cantidad { get; set; }

    public int CantidadMinima { get; set; }

    public DateTime? FechaDeVencimiento { get; set; }

    public bool? Estado { get; set; }

    public int CategoriaId { get; set; }

    public virtual Categoria Categoria { get; set; } = null!;

    public virtual ICollection<DetalleDeCompra> DetalleDeCompras { get; set; } = new List<DetalleDeCompra>();

    public virtual ICollection<Kardex> Kardices { get; set; } = new List<Kardex>();
}
