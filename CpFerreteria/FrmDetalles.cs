using CadFerreteria;
using ClnFerreteria;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CpFerreteria
{
    public partial class FrmDetalles : Form
    {
        private bool esNuevo = false;
        private List<DetalleVenta> carrito = new List<DetalleVenta>();

        public FrmDetalles()
        {
            InitializeComponent();
        }
        private void listar()
        {
            var lista = DetallesCln.listarPa(txtParametro.Text.Trim());
            using (var context = new LabFerreteriaEntities())
            {
                foreach (var item in lista)
                {
                    var producto = context.Producto.Find(item.IdProducto);
                    if (producto != null)
                        item.Precio = producto.Precio;
                }

                dgvLista.DataSource = lista;

                dgvLista.Columns["IdDetalle"].Visible = false;
                dgvLista.Columns["Cliente"].HeaderText = "Cliente";
                dgvLista.Columns["Producto"].HeaderText = "Producto";
                dgvLista.Columns["Precio"].Visible = false;
                dgvLista.Columns["Cantidad"].HeaderText = "Cantidad";
                dgvLista.Columns["Subtotal"].HeaderText = "Subtotal";
                dgvLista.Columns["Total"].HeaderText = "Total";
                dgvLista.Columns["Entrega"].HeaderText = "Entrega";
                dgvLista.Columns["Entrega"].HeaderText = "Entrega";
                dgvLista.Columns["estado"].Visible = false;
                dgvLista.Columns["usuarioRegistro"].Visible = false;
                dgvLista.Columns["fechaRegistro"].Visible = false;
            }
        }
        private void actualizarSubtotal()
        {
            if (cbmProducto.SelectedValue == null)
            {
                lblSubtotalR.Text = "0.00";
                return;
            }
            int idProducto;
            if (!int.TryParse(cbmProducto.SelectedValue.ToString(), out idProducto))
            {
                lblSubtotalR.Text = "0.00";
                return;
            }

            using (var context = new LabFerreteriaEntities())
            {
                var producto = context.Producto.Find(idProducto);
                if (producto == null)
                {
                    lblSubtotalR.Text = "0.00";
                    return;
                }

                decimal subtotal = producto.Precio * nudCantidad.Value;
                lblSubtotalR.Text = subtotal.ToString("0.00");
            }
        }

        private void cargarClientes()
        {
            var clientes = ClienteCln.listarPa("");
            cbmClientes.DataSource = clientes;
            cbmClientes.DisplayMember = "Nombre";
            cbmClientes.ValueMember = "IdCliente";
            cbmClientes.SelectedIndex = -1;
        }

        private void cargarProductos()
        {
            var productos = ProductoCln.listarPa("");
            cbmProducto.DataSource = productos;
            cbmProducto.DisplayMember = "Nombre";
            cbmProducto.ValueMember = "IdProducto";
            cbmProducto.SelectedIndex = -1;
        }
        private void FrmDetalles_Load(object sender, EventArgs e)
        {
            Size = new Size(715, 537);
            cargarProductos();
            cargarClientes();
            gbxDatos.Enabled = false;
            listar();
        }

        private void cbmProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            actualizarSubtotal();
        }

        private void nudCantidad_ValueChanged(object sender, EventArgs e)
        {
            actualizarSubtotal();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            esNuevo = true;
            pnlAcciones.Enabled = false;
            gbxDatos.Enabled = true;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            esNuevo = false;
            pnlAcciones.Enabled = false;
            gbxDatos.Enabled = true;

            int id = (int)dgvLista.CurrentRow.Cells["IdDetalle"].Value;
            var detalle = DetallesCln.obtenerUno(id);
            cbmProducto.SelectedValue = detalle.IdProducto;
            cbmClientes.SelectedValue = detalle.IdCliente;
            nudCantidad.Value = (int)detalle.Cantidad;

            actualizarSubtotal();
        }
        private void limpiar()
        {
            cbmProducto.SelectedIndex = -1;
            cbmClientes.SelectedIndex = -1;
            nudCantidad.Value = 0;
            lblSubtotalR.Text = "00,00";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            pnlAcciones.Enabled = true;
            limpiar();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (cbmProducto.SelectedValue == null || cbmClientes.SelectedValue == null || nudCantidad.Value <= 0)
            {
                MessageBox.Show("Completa todos los campos correctamente.");
                return;
            }
            int idCliente = (int)cbmClientes.SelectedValue;
            Venta ventaActual;

            using (var context = new LabFerreteriaEntities())
            {
                ventaActual = context.Venta.FirstOrDefault(v => v.IdCliente == idCliente && v.estado != -1);
                if (ventaActual == null)
                {
                    ventaActual = new Venta
                    {
                        IdCliente = idCliente,
                        Total = 0,
                        usuarioRegistro = Util.usuario.Nombre,
                        fechaRegistro = DateTime.Now,
                        estado = 1
                    };
                    context.Venta.Add(ventaActual);
                    context.SaveChanges();
                }

                DetalleVenta detalle;
                if (!esNuevo && dgvLista.CurrentRow != null)
                {
                    int idDetalle = Convert.ToInt32(dgvLista.CurrentRow.Cells["IdDetalle"].Value);
                    detalle = context.DetalleVenta.Find(idDetalle);
                    if (detalle == null) return;

                    detalle.IdProducto = (int)cbmProducto.SelectedValue;
                    detalle.Cantidad = (int)nudCantidad.Value;
                    detalle.Subtotal = detalle.Cantidad * context.Producto.Find(detalle.IdProducto).Precio;
                    detalle.usuarioRegistro = Util.usuario.Nombre;
                }
                else
                {
                    detalle = new DetalleVenta
                    {
                        IdVenta = ventaActual.IdVenta,
                        IdCliente = idCliente,
                        IdProducto = (int)cbmProducto.SelectedValue,
                        Cantidad = (int)nudCantidad.Value,
                        Subtotal = (int)nudCantidad.Value * context.Producto.Find((int)cbmProducto.SelectedValue).Precio,
                        usuarioRegistro = Util.usuario.Nombre,
                        fechaRegistro = DateTime.Now,
                        estado = 1
                    };
                    context.DetalleVenta.Add(detalle);
                }

                context.SaveChanges();
                ventaActual.Total = context.DetalleVenta
                    .Where(d => d.IdVenta == ventaActual.IdVenta && d.estado != -1)
                    .Sum(d => d.Subtotal);
                context.SaveChanges();
            }
            var lista = DetallesCln.listarPa("");
            dgvLista.DataSource = lista;
            cbmProducto.SelectedIndex = -1;
            nudCantidad.Value = 1;
            lblSubtotalR.Text = "0.00";
            gbxDatos.Enabled = true;
            esNuevo = true;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int id = (int)dgvLista.CurrentRow.Cells["IdDetalle"].Value;
            DetallesCln.eliminar(id, Util.usuario.Nombre);
            listar();
            MessageBox.Show("Detalle eliminado correctamente");
        }
        private void txtParametro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) listar();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            listar();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void cbmProducto_TextChanged(object sender, EventArgs e)
        {
            var productos = ProductoCln.listarPa(cbmProducto.Text);
            cbmProducto.DataSource = productos;
            cbmProducto.DisplayMember = "Nombre";
            cbmProducto.ValueMember = "IdProducto";
            cbmProducto.DroppedDown = true;
            cbmProducto.SelectionStart = cbmProducto.Text.Length;
            cbmProducto.Cursor = Cursors.Default;
        }

        private void cbmClientes_TextChanged(object sender, EventArgs e)
        {
            var clientes = ClienteCln.listarPa(cbmClientes.Text);
            cbmClientes.DataSource = clientes;
            cbmClientes.DisplayMember = "Nombre";
            cbmClientes.ValueMember = "IdCliente";
            cbmClientes.DroppedDown = true;
            cbmClientes.SelectionStart = cbmClientes.Text.Length;
            cbmClientes.Cursor = Cursors.Default;
        }
    }
}
