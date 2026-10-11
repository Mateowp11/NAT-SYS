namespace NatSys.UI
{
    partial class frmReportes
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblTitulo = new System.Windows.Forms.Label();
            this.btnViabilidad = new System.Windows.Forms.Button();
            this.btnEvolucion = new System.Windows.Forms.Button();
            this.btnVolver = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // lblTitulo
            //
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(30, 20);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(100, 25);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Reportes";
            //
            // btnViabilidad
            //
            this.btnViabilidad.Location = new System.Drawing.Point(30, 65);
            this.btnViabilidad.Name = "btnViabilidad";
            this.btnViabilidad.Size = new System.Drawing.Size(300, 45);
            this.btnViabilidad.TabIndex = 1;
            this.btnViabilidad.Text = "Viabilidad de marca";
            this.btnViabilidad.UseVisualStyleBackColor = true;
            this.btnViabilidad.Click += new System.EventHandler(this.btnViabilidad_Click);
            //
            // btnEvolucion
            //
            this.btnEvolucion.Location = new System.Drawing.Point(30, 120);
            this.btnEvolucion.Name = "btnEvolucion";
            this.btnEvolucion.Size = new System.Drawing.Size(300, 45);
            this.btnEvolucion.TabIndex = 2;
            this.btnEvolucion.Text = "Evolución por estilo (gráfico)";
            this.btnEvolucion.UseVisualStyleBackColor = true;
            this.btnEvolucion.Click += new System.EventHandler(this.btnEvolucion_Click);
            //
            // btnVolver
            //
            this.btnVolver.Location = new System.Drawing.Point(220, 180);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(110, 32);
            this.btnVolver.TabIndex = 3;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            //
            // frmReportes
            //
            this.CancelButton = this.btnVolver;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 230);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.btnEvolucion);
            this.Controls.Add(this.btnViabilidad);
            this.Controls.Add(this.lblTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmReportes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "NAT-SYS - Reportes";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Button btnViabilidad;
        private System.Windows.Forms.Button btnEvolucion;
        private System.Windows.Forms.Button btnVolver;
    }
}
