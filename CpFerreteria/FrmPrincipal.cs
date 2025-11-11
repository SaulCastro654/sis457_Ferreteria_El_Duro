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
    public partial class FrmPrincipal : Form
    {
        private FrmLogin frmLogin;
        public FrmPrincipal(FrmLogin frmLogin)
        {
            InitializeComponent();
            this.frmLogin = frmLogin;
        }
        private void FrmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            Util.usuario = null;
            frmLogin.Show();
        }
        private void btnCaProductos_Click(object sender, EventArgs e)
        {
            new FrmProductos().ShowDialog();
        }
    }
}
