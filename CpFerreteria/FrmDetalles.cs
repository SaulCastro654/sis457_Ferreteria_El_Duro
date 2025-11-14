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
        public FrmDetalles()
        {
            InitializeComponent();
        }
        private void listar()
        {
            var lista = DetallesCln.listarPa(txtParametro.Text.Trim());
            dgvLista.DataSource = lista;
            dgvLista.Columns["IdDetalle"].Visible = false;
            dgvLista.Columns["Cliente"].Visible = true;
            dgvLista.Columns["Producto"].Visible = true;
            dgvLista.Columns["estado"].Visible = false;
            dgvLista.Columns["Cantidad"].HeaderText = "Cantidad";
            dgvLista.Columns["Subtotal"].HeaderText = "Subtotal";
            dgvLista.Columns["usuarioRegistro"].Visible = false;
            dgvLista.Columns["fechaRegistro"].HeaderText = "fechaRegistro";

            if (lista.Count > 0) dgvLista.CurrentCell = dgvLista.Rows[0].Cells["Cantidad"];
            btnEditar.Enabled = lista.Count > 0;
            btnEliminar.Enabled = lista.Count > 0;
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
            Size = new Size(621, 321);
            cargarProductos();
            cargarClientes();
            listar();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            esNuevo = true;
            pnlAcciones.Enabled = false;
            Size = new Size(621, 469);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            esNuevo = false;
            pnlAcciones.Enabled = false;
            Size = new Size(621, 469);

            int id = (int)dgvLista.CurrentRow.Cells["IdDetalle"].Value;
            var detalle = DetallesCln.obtenerUno(id);
            cbmProducto.SelectedValue = detalle.IdProducto;
            cbmClientes.SelectedValue = detalle.IdCliente;
            nudCantidad.Value = (int)detalle.Cantidad;
            nudSubtotal.Value = (decimal)detalle.Subtotal;
        }
        private void limpiar()
        {
            cbmProducto.SelectedIndex = -1;
            cbmClientes.SelectedIndex = -1;
            nudCantidad.Value = 0;
            nudSubtotal.Value = 0;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Size = new Size(621, 321);
            pnlAcciones.Enabled = true;
            limpiar();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            listar();
        }
        private void txtParametro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) listar();
        }
        private bool validar()
        {
            bool esValido = true;
            erpProducto.Clear();
            erpVenta.Clear();
            erpCantidad.Clear();
            erpSubtotal.Clear();

            if (string.IsNullOrEmpty(cbmProducto.Text))
            {
                erpProducto.SetError(cbmProducto, "El Producto es obligatorio");
                esValido = false;
            }
            if (string.IsNullOrEmpty(cbmClientes.Text))
            {
                erpVenta.SetError(cbmClientes, "La Venta es obligatoria");
                esValido = false;
            }
            if (string.IsNullOrEmpty(nudCantidad.Value.ToString()))
            {
                erpCantidad.SetError(nudCantidad, "La Cantidad es obligatoria");
                esValido = false;
            }
            if (nudSubtotal.Value == 0)
            {
                erpSubtotal.SetError(nudSubtotal, "El Subtotal debe ser mayor a cero");
                esValido = false;
            }

            return esValido;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                var detalle = new DetalleVenta();
                detalle.IdProducto = cbmProducto.SelectedValue != null
                      ? Convert.ToInt32(cbmProducto.SelectedValue)
                      : (int?)null;

                detalle.IdCliente = cbmClientes.SelectedValue != null
                                  ? Convert.ToInt32(cbmClientes.SelectedValue)
                                  : (int?)null;

                detalle.Cantidad = (int)nudCantidad.Value;
                detalle.Subtotal = (decimal)nudSubtotal.Value;
                detalle.usuarioRegistro = Util.usuario.Nombre;

                if (esNuevo)
                {
                    detalle.fechaRegistro = DateTime.Now;
                    detalle.estado = 1;
                    DetallesCln.insertar(detalle);
                }
                else
                {
                    detalle.IdDetalle = (int)dgvLista.CurrentRow.Cells["IdDetalle"].Value;
                    DetallesCln.actualizar(detalle);
                }
                listar();
                btnCancelar.PerformClick();
                MessageBox.Show("Detalle de venta guardado correctamente", "::: Mensaje - Ferreteria :::",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int id = (int)dgvLista.CurrentRow.Cells["IdDetalle"].Value;
            string cliente = dgvLista.CurrentRow.Cells["Cliente"].Value.ToString();
            DialogResult dialog = MessageBox.Show($"¿Está seguro de eliminar Detalle {cliente}?",
                "::: Mensaje - Ferreteria :::", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                DetallesCln.eliminar(id, Util.usuario.Nombre);
                listar();
                MessageBox.Show("Detalle dado de baja correctamente", "::: Mensaje - Ferreteria :::",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
