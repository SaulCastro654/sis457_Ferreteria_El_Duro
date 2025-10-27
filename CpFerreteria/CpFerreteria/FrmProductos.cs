using CadFerreteria;
using ClnFerreteria;
using CpMinerva;
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
    public partial class FrmProductos : Form
    {
        private bool esNuevo = false;
        public FrmProductos()
        {
            InitializeComponent();
        }
        private void listar()
        {
            var lista = ProductoCln.listarPa(txtParametro.Text.Trim());
            dgvLista.DataSource = lista;
            dgvLista.Columns["IdProducto"].Visible = false;
            dgvLista.Columns["Nombre"].HeaderText = "Nombre";
            dgvLista.Columns["Precio"].HeaderText = "Precio";
            dgvLista.Columns["Stock"].HeaderText = "Stock";

            if (lista.Count > 0) dgvLista.CurrentCell = dgvLista.Rows[0].Cells["Nombre"];
            btnEditar.Enabled = lista.Count > 0;
            btnEliminar.Enabled = lista.Count > 0;
        }

        private void FrmProductos_Load(object sender, EventArgs e)
        {
            Size = new Size(777, 338);
            listar();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            esNuevo = true;
            pnlAccion.Visible = true;
            Size = new Size(777, 466);
            txtNombre.Focus();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            esNuevo = false;
            pnlAccion.Visible = false;
            Size = new Size(777, 466);

            int id = (int)dgvLista.CurrentRow.Cells["IdProducto"].Value;
            var producto = ProductoCln.obtenerUno(id);
            txtNombre.Text = producto.Nombre;
            nudStock.Value = producto.Stock;
            nudPrecioVenta.Value = producto.Precio;

            txtNombre.Focus();
        }

        private void limpiar()
        {
            txtNombre.Clear();
            nudPrecioVenta.Value = 0;
            nudStock.Value = 0;
        }

        private void btnCerrar1_Click(object sender, EventArgs e)
        {
            Size = new Size(818, 391);
            pnlAccion.Visible = true;
            limpiar();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            listar();
        }

        private void txtParametro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) listar();
        }

        private bool validar()
        {
            bool esValido = true;
            erpNombre.Clear();
            erpStock.Clear();
            erpPrecio.Clear();

            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                erpNombre.SetError(txtNombre, "El nombre es obligatorio.");
                esValido = false;
            }
            if (string.IsNullOrEmpty(nudStock.Value.ToString()))
            {
                erpStock.SetError(nudStock, "El nombre es obligatorio.");
                esValido = false;
            }
            if (string.IsNullOrEmpty(nudPrecioVenta.Value.ToString()))
            {
                erpPrecio.SetError(nudPrecioVenta, "El nombre es obligatorio.");
                esValido = false;
            }

            return esValido;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                var producto = new Producto();
                producto.Nombre = txtNombre.Text.Trim();
                producto.Stock = (int)nudStock.Value;
                producto.Precio = nudPrecioVenta.Value;

                if (esNuevo)
                {
                    producto.fechaRegistro = DateTime.Now;
                    producto.estado = 1;
                    ProductoCln.insertar(producto);
                }
                else
                {
                    producto.IdProducto = (int)dgvLista.CurrentRow.Cells["IdProducto"].Value;
                    ProductoCln.actualizar(producto);
                }
                listar();
                btnCerrar1.PerformClick();
                MessageBox.Show("Los datos se guardaron correctamente.", "Sistema de Ferretería",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int id = (int)dgvLista.CurrentRow.Cells["IdProducto"].Value;
            string nombre = dgvLista.CurrentRow.Cells["Nombre"].Value.ToString();
            DialogResult dialog = MessageBox.Show($"¿Está seguro de eliminar el producto {nombre}?", "Sistema de Ferretería",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                ProductoCln.eliminar(id, nombre);
                listar();
                MessageBox.Show("Producto Borrado", "Sistema de Ferretería",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
