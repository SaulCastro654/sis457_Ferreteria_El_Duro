namespace CpFerreteria
{
    partial class FrmDetalles
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDetalles));
            this.pnlAcciones = new System.Windows.Forms.Panel();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.gbxListado = new System.Windows.Forms.GroupBox();
            this.dgvLista = new System.Windows.Forms.DataGridView();
            this.txtParametro = new System.Windows.Forms.TextBox();
            this.lblBuscar = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gbxDatos = new System.Windows.Forms.GroupBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.lblSubtotalR = new System.Windows.Forms.Label();
            this.lblSubtotal = new System.Windows.Forms.Label();
            this.nudCantidad = new System.Windows.Forms.NumericUpDown();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.cbmClientes = new System.Windows.Forms.ComboBox();
            this.lblVenta = new System.Windows.Forms.Label();
            this.cbmProducto = new System.Windows.Forms.ComboBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.lblProducto = new System.Windows.Forms.Label();
            this.erpProducto = new System.Windows.Forms.ErrorProvider(this.components);
            this.erpVenta = new System.Windows.Forms.ErrorProvider(this.components);
            this.erpCantidad = new System.Windows.Forms.ErrorProvider(this.components);
            this.erpSubtotal = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnBuscar = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pnlAcciones.SuspendLayout();
            this.gbxListado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLista)).BeginInit();
            this.gbxDatos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpProducto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpVenta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpCantidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpSubtotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlAcciones
            // 
            this.pnlAcciones.BackColor = System.Drawing.Color.Transparent;
            this.pnlAcciones.Controls.Add(this.btnNuevo);
            this.pnlAcciones.Controls.Add(this.btnEditar);
            this.pnlAcciones.Controls.Add(this.btnEliminar);
            this.pnlAcciones.Location = new System.Drawing.Point(640, 138);
            this.pnlAcciones.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlAcciones.Name = "pnlAcciones";
            this.pnlAcciones.Size = new System.Drawing.Size(717, 85);
            this.pnlAcciones.TabIndex = 9;
            // 
            // btnNuevo
            // 
            this.btnNuevo.BackColor = System.Drawing.Color.Gold;
            this.btnNuevo.Font = new System.Drawing.Font("Noto Sans", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.btnNuevo.Image = global::CpFerreteria.Properties.Resources.nuevo31;
            this.btnNuevo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNuevo.Location = new System.Drawing.Point(1, 4);
            this.btnNuevo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(224, 78);
            this.btnNuevo.TabIndex = 25;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNuevo.UseVisualStyleBackColor = false;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnEditar.Font = new System.Drawing.Font("Noto Sans", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.btnEditar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnEditar.Image = global::CpFerreteria.Properties.Resources.editar4;
            this.btnEditar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEditar.Location = new System.Drawing.Point(243, 4);
            this.btnEditar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(224, 78);
            this.btnEditar.TabIndex = 9;
            this.btnEditar.Text = "Editar";
            this.btnEditar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEditar.UseVisualStyleBackColor = false;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.Color.Gold;
            this.btnEliminar.Font = new System.Drawing.Font("Noto Sans", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.btnEliminar.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnEliminar.Image = global::CpFerreteria.Properties.Resources.eliminar4;
            this.btnEliminar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEliminar.Location = new System.Drawing.Point(475, 4);
            this.btnEliminar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(232, 78);
            this.btnEliminar.TabIndex = 9;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnCerrar.Font = new System.Drawing.Font("Montserrat Subrayada", 12F, System.Drawing.FontStyle.Bold);
            this.btnCerrar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnCerrar.Image = global::CpFerreteria.Properties.Resources.cerrar3;
            this.btnCerrar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCerrar.Location = new System.Drawing.Point(48, 812);
            this.btnCerrar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(185, 78);
            this.btnCerrar.TabIndex = 10;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // gbxListado
            // 
            this.gbxListado.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.gbxListado.Controls.Add(this.dgvLista);
            this.gbxListado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxListado.Location = new System.Drawing.Point(640, 230);
            this.gbxListado.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbxListado.Name = "gbxListado";
            this.gbxListado.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbxListado.Size = new System.Drawing.Size(939, 607);
            this.gbxListado.TabIndex = 10;
            this.gbxListado.TabStop = false;
            this.gbxListado.Text = "Lista";
            // 
            // dgvLista
            // 
            this.dgvLista.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLista.Location = new System.Drawing.Point(9, 15);
            this.dgvLista.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvLista.Name = "dgvLista";
            this.dgvLista.RowHeadersWidth = 51;
            this.dgvLista.Size = new System.Drawing.Size(915, 566);
            this.dgvLista.TabIndex = 0;
            // 
            // txtParametro
            // 
            this.txtParametro.Font = new System.Drawing.Font("NSimSun", 14.25F);
            this.txtParametro.Location = new System.Drawing.Point(769, 86);
            this.txtParametro.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtParametro.Name = "txtParametro";
            this.txtParametro.Size = new System.Drawing.Size(459, 35);
            this.txtParametro.TabIndex = 13;
            // 
            // lblBuscar
            // 
            this.lblBuscar.AutoSize = true;
            this.lblBuscar.BackColor = System.Drawing.Color.Gold;
            this.lblBuscar.Font = new System.Drawing.Font("Noto Sans", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.lblBuscar.Location = new System.Drawing.Point(488, 86);
            this.lblBuscar.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(262, 33);
            this.lblBuscar.TabIndex = 12;
            this.lblBuscar.Text = "Buscar por Nombre:";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.Gold;
            this.label1.Font = new System.Drawing.Font("Montserrat Subrayada", 26.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(487, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(336, 50);
            this.label1.TabIndex = 11;
            this.label1.Text = "VENTAS";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbxDatos
            // 
            this.gbxDatos.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.gbxDatos.Controls.Add(this.btnGuardar);
            this.gbxDatos.Controls.Add(this.lblSubtotalR);
            this.gbxDatos.Controls.Add(this.lblSubtotal);
            this.gbxDatos.Controls.Add(this.nudCantidad);
            this.gbxDatos.Controls.Add(this.lblCantidad);
            this.gbxDatos.Controls.Add(this.cbmClientes);
            this.gbxDatos.Controls.Add(this.lblVenta);
            this.gbxDatos.Controls.Add(this.cbmProducto);
            this.gbxDatos.Controls.Add(this.btnCancelar);
            this.gbxDatos.Controls.Add(this.lblProducto);
            this.gbxDatos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxDatos.Location = new System.Drawing.Point(48, 444);
            this.gbxDatos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbxDatos.Name = "gbxDatos";
            this.gbxDatos.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbxDatos.Size = new System.Drawing.Size(559, 271);
            this.gbxDatos.TabIndex = 15;
            this.gbxDatos.TabStop = false;
            this.gbxDatos.Text = "Datos";
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.Gold;
            this.btnGuardar.Font = new System.Drawing.Font("Noto Sans", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.btnGuardar.Image = global::CpFerreteria.Properties.Resources.guardar3;
            this.btnGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGuardar.Location = new System.Drawing.Point(68, 196);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(200, 68);
            this.btnGuardar.TabIndex = 14;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // lblSubtotalR
            // 
            this.lblSubtotalR.AutoSize = true;
            this.lblSubtotalR.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.lblSubtotalR.Font = new System.Drawing.Font("Noto Sans", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtotalR.ForeColor = System.Drawing.Color.Gold;
            this.lblSubtotalR.Location = new System.Drawing.Point(441, 119);
            this.lblSubtotalR.Name = "lblSubtotalR";
            this.lblSubtotalR.Size = new System.Drawing.Size(99, 41);
            this.lblSubtotalR.TabIndex = 23;
            this.lblSubtotalR.Text = "00,00";
            // 
            // lblSubtotal
            // 
            this.lblSubtotal.AutoSize = true;
            this.lblSubtotal.Font = new System.Drawing.Font("Noto Sans", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.lblSubtotal.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblSubtotal.Location = new System.Drawing.Point(307, 127);
            this.lblSubtotal.Name = "lblSubtotal";
            this.lblSubtotal.Size = new System.Drawing.Size(125, 33);
            this.lblSubtotal.TabIndex = 21;
            this.lblSubtotal.Text = "Subtotal:";
            // 
            // nudCantidad
            // 
            this.nudCantidad.Font = new System.Drawing.Font("NSimSun", 14.25F);
            this.nudCantidad.Location = new System.Drawing.Point(145, 129);
            this.nudCantidad.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.nudCantidad.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudCantidad.Name = "nudCantidad";
            this.nudCantidad.Size = new System.Drawing.Size(136, 35);
            this.nudCantidad.TabIndex = 20;
            this.nudCantidad.ValueChanged += new System.EventHandler(this.nudCantidad_ValueChanged);
            // 
            // lblCantidad
            // 
            this.lblCantidad.AutoSize = true;
            this.lblCantidad.Font = new System.Drawing.Font("Noto Sans", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.lblCantidad.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblCantidad.Location = new System.Drawing.Point(15, 128);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Size = new System.Drawing.Size(131, 33);
            this.lblCantidad.TabIndex = 19;
            this.lblCantidad.Text = "Cantidad:";
            // 
            // cbmClientes
            // 
            this.cbmClientes.Font = new System.Drawing.Font("NSimSun", 14.25F);
            this.cbmClientes.FormattingEnabled = true;
            this.cbmClientes.Location = new System.Drawing.Point(140, 74);
            this.cbmClientes.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbmClientes.Name = "cbmClientes";
            this.cbmClientes.Size = new System.Drawing.Size(404, 32);
            this.cbmClientes.TabIndex = 18;
            // 
            // lblVenta
            // 
            this.lblVenta.AutoSize = true;
            this.lblVenta.Font = new System.Drawing.Font("Noto Sans", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.lblVenta.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblVenta.Location = new System.Drawing.Point(27, 74);
            this.lblVenta.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblVenta.Name = "lblVenta";
            this.lblVenta.Size = new System.Drawing.Size(108, 33);
            this.lblVenta.TabIndex = 17;
            this.lblVenta.Text = "Cliente:";
            // 
            // cbmProducto
            // 
            this.cbmProducto.Font = new System.Drawing.Font("NSimSun", 14.25F);
            this.cbmProducto.FormattingEnabled = true;
            this.cbmProducto.Location = new System.Drawing.Point(140, 25);
            this.cbmProducto.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbmProducto.Name = "cbmProducto";
            this.cbmProducto.Size = new System.Drawing.Size(404, 32);
            this.cbmProducto.TabIndex = 16;
            this.cbmProducto.SelectedIndexChanged += new System.EventHandler(this.cbmProducto_SelectedIndexChanged);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnCancelar.Font = new System.Drawing.Font("Noto Sans", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.btnCancelar.Image = global::CpFerreteria.Properties.Resources.cancelar31;
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelar.Location = new System.Drawing.Point(292, 196);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(200, 68);
            this.btnCancelar.TabIndex = 15;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // lblProducto
            // 
            this.lblProducto.AutoSize = true;
            this.lblProducto.Font = new System.Drawing.Font("Noto Sans", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.lblProducto.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblProducto.Location = new System.Drawing.Point(8, 25);
            this.lblProducto.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblProducto.Name = "lblProducto";
            this.lblProducto.Size = new System.Drawing.Size(136, 33);
            this.lblProducto.TabIndex = 5;
            this.lblProducto.Text = "Producto:";
            // 
            // erpProducto
            // 
            this.erpProducto.ContainerControl = this;
            // 
            // erpVenta
            // 
            this.erpVenta.ContainerControl = this;
            // 
            // erpCantidad
            // 
            this.erpCantidad.ContainerControl = this;
            // 
            // erpSubtotal
            // 
            this.erpSubtotal.ContainerControl = this;
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.Gold;
            this.btnBuscar.Font = new System.Drawing.Font("Noto Sans", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.btnBuscar.Image = global::CpFerreteria.Properties.Resources.buscar3;
            this.btnBuscar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuscar.Location = new System.Drawing.Point(1237, 52);
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(175, 70);
            this.btnBuscar.TabIndex = 14;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::CpFerreteria.Properties.Resources.detalles3;
            this.pictureBox1.Location = new System.Drawing.Point(217, 215);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(241, 222);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 18;
            this.pictureBox1.TabStop = false;
            // 
            // FrmDetalles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BackgroundImage = global::CpFerreteria.Properties.Resources.Whisk_9f8c7f3e68e6336aa42482a9b12e13acdr;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1611, 902);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.gbxDatos);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.txtParametro);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.lblBuscar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gbxListado);
            this.Controls.Add(this.pnlAcciones);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FrmDetalles";
            this.Text = "VENTAS";
            this.Load += new System.EventHandler(this.FrmDetalles_Load);
            this.pnlAcciones.ResumeLayout(false);
            this.gbxListado.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLista)).EndInit();
            this.gbxDatos.ResumeLayout(false);
            this.gbxDatos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpProducto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpVenta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpCantidad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpSubtotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlAcciones;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.GroupBox gbxListado;
        private System.Windows.Forms.DataGridView dgvLista;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtParametro;
        private System.Windows.Forms.Label lblBuscar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbxDatos;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Label lblProducto;
        private System.Windows.Forms.ComboBox cbmProducto;
        private System.Windows.Forms.ComboBox cbmClientes;
        private System.Windows.Forms.Label lblVenta;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.ErrorProvider erpProducto;
        private System.Windows.Forms.ErrorProvider erpVenta;
        private System.Windows.Forms.ErrorProvider erpCantidad;
        private System.Windows.Forms.ErrorProvider erpSubtotal;
        private System.Windows.Forms.NumericUpDown nudCantidad;
        private System.Windows.Forms.Label lblSubtotal;
        private System.Windows.Forms.Label lblSubtotalR;
        private System.Windows.Forms.Button btnNuevo;
        protected internal System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}