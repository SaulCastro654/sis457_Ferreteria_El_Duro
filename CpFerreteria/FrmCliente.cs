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
    public partial class FrmCliente : Form
    {
        private bool esNuevo = false;
        public FrmCliente()
        {
            InitializeComponent();
        }
        public void listar()
        {
            var lista = ClienteCln.listarPa(txtParametro.Text.Trim());
            dgvLista.DataSource = lista;
            dgvLista.Columns["IdCliente"].Visible = false;
            dgvLista.Columns["estado"].Visible = false;
            dgvLista.Columns["Nombre"].HeaderText = "Nombre";
            dgvLista.Columns["Telefono"].HeaderText = "Telefono";
            dgvLista.Columns["Direccion"].HeaderText = "Direccion";
            dgvLista.Columns["Fecha"].HeaderText = "Fecha";
            dgvLista.Columns["usuarioRegistro"].HeaderText = "Usuario Registro";
            dgvLista.Columns["fechaRegistro"].HeaderText = "Fecha Registro";

            if (lista.Count > 0) dgvLista.CurrentCell = dgvLista.Rows[0].Cells["Nombre"];
            btnEditar.Enabled = lista.Count > 0;
            btnEliminar.Enabled = lista.Count > 0;
        }
        private void FrmCliente_Load(object sender, EventArgs e)
        {
            Size = new Size(621, 314);
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

            int id = (int)dgvLista.CurrentRow.Cells["IdCliente"].Value;
            var cliente = ClienteCln.obtenerUno(id);
            txtNombre.Text = cliente.Nombre;
            txtTelefono.Text = cliente.Telefono;
            txtDireccion.Text = cliente.Direccion;
            if (cliente.Fecha.HasValue)
                dtpFecha.Value = cliente.Fecha.Value;
            else
                dtpFecha.Value = DateTime.Now;

            txtNombre.Focus();
        }
        private void limpiar()
        {
            txtNombre.Clear();
            txtTelefono.Clear();
            txtDireccion.Clear();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Size = new Size(621, 314);
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
            erpTelefono.Clear();
            erpDireccion.Clear();

            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                erpNombre.SetError(txtNombre, "El Nombre es obligatorio");
                esValido = false;
            }
            if (string.IsNullOrEmpty(txtTelefono.Text))
            {
                erpTelefono.SetError(txtTelefono, "El Telefono es obligatorio");
                esValido = false;
            }
            if (string.IsNullOrEmpty(txtDireccion.Text))
            {
                erpDireccion.SetError(txtDireccion, "La Direccion es obligatoria");
                esValido = false;
            }
            return esValido;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                var cliente = new Cliente();
                cliente.Nombre = txtNombre.Text.Trim();
                cliente.Telefono = txtTelefono.Text.Trim();
                cliente.Direccion = txtDireccion.Text.Trim();
                cliente.Fecha = dtpFecha.Value;
                cliente.usuarioRegistro = Util.usuario.Nombre;

                if (esNuevo)
                {
                    cliente.fechaRegistro = DateTime.Now;
                    cliente.estado = 1;
                    ClienteCln.insertar(cliente);
                }
                else
                {
                    cliente.IdCliente = (int)dgvLista.CurrentRow.Cells["IdCliente"].Value;
                    ClienteCln.actualizar(cliente);
                }
                listar();
                btnCancelar.PerformClick();
                MessageBox.Show("Cliente guardado correctamente", "::: Mensaje - Ferreteria :::",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int id = (int)dgvLista.CurrentRow.Cells["IdCliente"].Value;
            string nombre = dgvLista.CurrentRow.Cells["Nombre"].Value.ToString();
            DialogResult dialog = MessageBox.Show($"¿Está seguro de eliminar el cliente {nombre}?",
                "::: Mensaje - Ferreteria :::", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                ClienteCln.eliminar(id, Util.usuario.Nombre);
                listar();
                MessageBox.Show("Producto dado de baja correctamente", "::: Mensaje - Ferreteria :::",
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
