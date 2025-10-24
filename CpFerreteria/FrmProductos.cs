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
        public FrmFerreteria()
        {
            InitializeComponent();
        }

        public object FerreteriaCln { get; private set; }

        private void listar()
        {
            var lista = FerreteriaCln.listarPa(txtBuscar.Text.Trim());
            dgvLista.DataSource = lista;
            dgvLista.Columns["IdProducto"].Visible = false;
            dgvLista.Columns["Nombre"].Visible = false;
            dgvLista.Columns["Precio"].Visible = false;
            dgvLista.Columns["Stock"].Visible = false;
            dgvLista.Columns["estado"].Visible = false;

            if (lista.Count > 0) dgvLista.CurrentCell = dgvLista.Rows[0].Cells["Nombre"];
            btnEditar.Enabled = lista.Count > 0;
            btnEliminar.Enabled = lista.Count > 0;
        }

        private void FrmProductos_Load(object sender, EventArgs e)
        {
            Size = new Size(800, 600);
            listar();
        }
    }
