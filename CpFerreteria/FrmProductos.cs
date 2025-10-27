using CpFerreteria;
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
    public partial class FrmFerreteria : Form
    {
        private bool esNuevo = false;
        public FrmFerreteria()
        {
            InitializeComponent();
        }
        private void listar()
        {
            var lista = ProductoCln.listarPa(txtParametro.Text.Trim());
            dgvLista.DataSource = lista;
            dgvLista.Columns["IdProducto"].Visible = false;
            dgvLista.Columns["Nombre"].HeaderText = "Producto";
            dgvLista.Columns["Precio"].HeaderText = "Precio";
            dgvLista.Columns["Stock"].HeaderText = "Stock";

            if (lista.Count > 0) dgvLista.CurrentCell = dgvLista.Rows[0].Cells[1];
            btnEditar.Enabled = lista.Count > 0;
            btnEliminar.Enabled = lista.Count > 0;
        }

        private void FrmProductos_Load(object sender, EventArgs e)
        {
            Size = new Size(820, 385);
            listar();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            esNuevo = true;
            pnlAccion.Enabled = false;
            Size = new Size(800, 555);
            txtNombre.Focus();
        }

        private void txtDescripcion_TextChanged(object sender, EventArgs e)
        {

        }

        private void nudPrecioVenta_ValueChanged(object sender, EventArgs e)
        {

        }

        private void FrmFerreteria_Load(object sender, EventArgs e)
        {

        }
    }
}
