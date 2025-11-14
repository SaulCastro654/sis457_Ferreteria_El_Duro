using CadFerreteria;
using ClnFerreteria;
using CpFerreteria;
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
        public void listar()
        {
            var lista = ProductoCln.listarPa(txtParametro.Text.Trim());
            dgvLista.DataSource = lista;
            dgvLista.Columns["IdProducto"].Visible = false;
            dgvLista.Columns["estado"].Visible = false;
            dgvLista.Columns["Nombre"].HeaderText = "Nombre";
            dgvLista.Columns["Stock"].HeaderText = "Stock";
            dgvLista.Columns["Precio"].HeaderText = "Precio";
            dgvLista.Columns["Marca"].HeaderText = "Marca";
            dgvLista.Columns["usuarioRegistro"].HeaderText = "Usuario Registro";
            dgvLista.Columns["fechaRegistro"].HeaderText = "Fecha Registro";

            if (lista.Count > 0) dgvLista.CurrentCell = dgvLista.Rows[0].Cells["Nombre"];
            btnEditar.Enabled = lista.Count > 0;
            btnEliminar.Enabled = lista.Count > 0;
        }
        private void cargarMarcas()
        {
            var marcas = new List<string> { "Tramontina", "Pretul", "Stanley", "Bosch", "Otra" };

            cbmMarca.DataSource = marcas;
        }
        private void FrmProductos_Load(object sender, EventArgs e)
        {
            Size = new Size(621, 322);
            cargarMarcas();
            listar();
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            esNuevo = true;
            pnlAcciones.Enabled = false;
            Size = new Size(621, 473);
            txtNombre.Focus();
        }
        private void btnEditar_Click(object sender, EventArgs e)
        {
            esNuevo = false;
            pnlAcciones.Enabled = false;
            Size = new Size(621, 473);

            int id = (int)dgvLista.CurrentRow.Cells["IdProducto"].Value;
            var producto = ProductoCln.obtenerUno(id);
            txtNombre.Text = producto.Nombre;
            nudStock.Value = producto.Stock;
            nudPrecioVenta.Value = producto.Precio;
            cbmMarca.SelectedItem = producto.Marca;

            txtNombre.Focus();
        }
        private void limpiar()
        {
            txtNombre.Clear();
            nudStock.Value = 0;
            nudPrecioVenta.Value = 0;
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Size = new Size(621, 322);
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
            erpNombre.Clear();
            erpStock.Clear();
            erpPrecio.Clear();
            erpMarca.Clear();

            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                erpNombre.SetError(txtNombre, "El Nombre es obligatorio");
                esValido = false;
            }
            if (string.IsNullOrEmpty(nudStock.Value.ToString()))
            {
                erpStock.SetError(nudStock, "El Stock es obligatorio");
                esValido = false;
            }
            if (nudPrecioVenta.Value == 0)
            {
                erpPrecio.SetError(nudPrecioVenta, "El Precio es obligatorio");
                esValido = false;
            }
            if (string.IsNullOrEmpty(cbmMarca.Text))
            {
                erpMarca.SetError(cbmMarca, "La Marca es obligatoria");
                esValido = false;
            }
            return esValido;
        }
        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            if (validar())
            {
                var producto = new Producto();
                producto.Nombre = txtNombre.Text.Trim();
                producto.Stock = (int)nudStock.Value;
                producto.Precio = nudPrecioVenta.Value;
                producto.Marca = (string)cbmMarca.SelectedItem;
                producto.usuarioRegistro = Util.usuario.Nombre;

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
                btnCancelar.PerformClick();
                MessageBox.Show("Producto guardado correctamente", "::: Mensaje - Ferreteria :::",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int id = (int)dgvLista.CurrentRow.Cells["IdProducto"].Value;
            string nombre = dgvLista.CurrentRow.Cells["Nombre"].Value.ToString();
            DialogResult dialog = MessageBox.Show($"¿Está seguro de eliminar el producto {nombre}?",
                "::: Mensaje - Minerva :::", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                ProductoCln.eliminar(id, Util.usuario.Nombre);
                listar();
                MessageBox.Show("Producto dado de baja correctamente", "::: Mensaje - Minerva :::",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtParametro_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
