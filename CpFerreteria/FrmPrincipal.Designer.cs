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
            this.btnCaProductos = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCaProductos
            // 
            this.btnCaProductos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCaProductos.Location = new System.Drawing.Point(21, 92);
            this.btnCaProductos.Name = "btnCaProductos";
            this.btnCaProductos.Size = new System.Drawing.Size(117, 48);
            this.btnCaProductos.TabIndex = 0;
            this.btnCaProductos.Text = "Productos";
            this.btnCaProductos.UseVisualStyleBackColor = true;
            this.btnCaProductos.Click += new System.EventHandler(this.btnCaProductos_Click);
            // 
            // FrmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(793, 509);
            this.Controls.Add(this.btnCaProductos);
            this.Name = "FrmPrincipal";
            this.Text = "FrmPrincipal";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCaProductos;
    }
}