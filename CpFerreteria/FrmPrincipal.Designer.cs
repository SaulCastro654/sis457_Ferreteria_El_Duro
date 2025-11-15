namespace CpFerreteria
{
    partial class FrmPrincipal
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
            this.btnDetalles = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnCaProductos = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnDetalles
            // 
            this.btnDetalles.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnDetalles.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDetalles.Image = global::CpFerreteria.Properties.Resources.detalles1;
            this.btnDetalles.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnDetalles.Location = new System.Drawing.Point(318, 102);
            this.btnDetalles.Margin = new System.Windows.Forms.Padding(2);
            this.btnDetalles.Name = "btnDetalles";
            this.btnDetalles.Size = new System.Drawing.Size(124, 117);
            this.btnDetalles.TabIndex = 2;
            this.btnDetalles.Text = "Detalles";
            this.btnDetalles.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnDetalles.UseVisualStyleBackColor = false;
            this.btnDetalles.Click += new System.EventHandler(this.btnDetalles_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Image = global::CpFerreteria.Properties.Resources.clientes1;
            this.button1.Location = new System.Drawing.Point(175, 102);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(115, 117);
            this.button1.TabIndex = 1;
            this.button1.Text = "Clientes";
            this.button1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnCaProductos
            // 
            this.btnCaProductos.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnCaProductos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCaProductos.Image = global::CpFerreteria.Properties.Resources.Productos1;
            this.btnCaProductos.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCaProductos.Location = new System.Drawing.Point(22, 102);
            this.btnCaProductos.Margin = new System.Windows.Forms.Padding(2);
            this.btnCaProductos.Name = "btnCaProductos";
            this.btnCaProductos.Size = new System.Drawing.Size(114, 117);
            this.btnCaProductos.TabIndex = 0;
            this.btnCaProductos.Text = "Productos";
            this.btnCaProductos.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCaProductos.UseVisualStyleBackColor = false;
            this.btnCaProductos.Click += new System.EventHandler(this.btnCaProductos_Click);
            // 
            // FrmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BackgroundImage = global::CpFerreteria.Properties.Resources.principal;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(471, 271);
            this.Controls.Add(this.btnDetalles);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnCaProductos);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FrmPrincipal";
            this.Text = "FrmPrincipal";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCaProductos;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnDetalles;
    }
}