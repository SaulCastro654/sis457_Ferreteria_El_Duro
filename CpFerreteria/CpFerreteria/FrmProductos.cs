using CadFerreteria;
using ClnFerreteria;
using System;
using System.Data;
using System.Drawing;
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

            if (lista.Count > 0)
                dgvLista.CurrentCell = dgvLista.Rows[0].Cells["Nombre"];

            btnEditar.Enabled = lista.Count > 0;
            btnEliminar.Enabled = lista.Count > 0;
        }

        private void FrmProductos_Load(object sender, EventArgs e)
        {
            Size = new Size(611, 318);
            listar();

            HabilitarBotones(true);
            HabilitarCampos(false);
        }

        private void HabilitarBotones(bool estado)
        {
            btnNuevo.Enabled = estado;
            btnEditar.Enabled = estado;
            btnEliminar.Enabled = estado;
            btnBuscar.Enabled = estado;

            // Botones del panel de edición
            btnGuardar.Enabled = !estado;
            btnCerrar1.Enabled = !estado;
        }

        private void HabilitarCampos(bool estado)
        {
            txtNombre.Enabled = estado;
            nudPrecioVenta.Enabled = estado;
            nudStock.Enabled = estado;
        }

        private void limpiar()
        {
            txtNombre.Clear();
            nudPrecioVenta.Value = 0;
            nudStock.Value = 0;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            esNuevo = true;
            HabilitarBotones(false);
            HabilitarCampos(true);

            Size = new Size(610, 471);
            limpiar();
            txtNombre.Focus();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            esNuevo = false;
            HabilitarBotones(false);
            HabilitarCampos(true);

            Size = new Size(610, 471);

            int id = (int)dgvLista.CurrentRow.Cells["IdProducto"].Value;
            var producto = ProductoCln.obtenerUno(id);

            txtNombre.Text = producto.Nombre;
            nudStock.Value = producto.Stock;
            nudPrecioVenta.Value = producto.Precio;

            txtNombre.Focus();
        }

        private void btnCerrar1_Click(object sender, EventArgs e)
        {
            HabilitarBotones(true);
            HabilitarCampos(false);

            Size = new Size(611, 318);
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

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                erpNombre.SetError(txtNombre, "El nombre es obligatorio.");
                esValido = false;
            }

            if (nudStock.Value <= 0)
            {
                erpStock.SetError(nudStock, "El stock debe ser mayor a 0.");
                esValido = false;
            }

            if (nudPrecioVenta.Value <= 0)
            {
                erpPrecio.SetError(nudPrecioVenta, "El precio debe ser mayor a 0.");
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

                MessageBox.Show("Producto guardado correctamente",
                    "Sistema Ferretería",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int id = (int)dgvLista.CurrentRow.Cells["IdProducto"].Value;
            string nombre = dgvLista.CurrentRow.Cells["Nombre"].Value.ToString();

            var dialog = MessageBox.Show(
                $"¿Está seguro de eliminar el producto '{nombre}'?",
                "Sistema Ferretería", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialog == DialogResult.Yes)
            {
                ProductoCln.eliminar(id, nombre);
                listar();

                MessageBox.Show("Producto eliminado correctamente",
                    "Sistema Ferretería",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        

        private void btnGuardar_Click_1(object sender, EventArgs e)
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

        listar();            // Refresca el DataGrid
        btnCerrar1.PerformClick();  // Cierra el panel de edición y habilita botones

        MessageBox.Show("Producto guardado correctamente",
            "Sistema Ferretería",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);
    }
}

        private void nudStock_ValueChanged(object sender, EventArgs e)
        {
            if (nudStock.Value > 10000)
            {
                nudStock.Value = 10000;  // Limita al máximo
            }
        }

        private void nudPrecioVenta_ValueChanged(object sender, EventArgs e)
        {
            if (nudPrecioVenta.Value > 9999)
            {
                nudPrecioVenta.Value = 9999;   // Limita al máximo
            }
        }
    }
}
