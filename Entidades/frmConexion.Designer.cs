namespace NatSys.UI
{
    partial class frmConexion
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
            this.lblEstado = new System.Windows.Forms.Label();
            this.lblDetalle = new System.Windows.Forms.Label();
            this.pbEspera = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            //
            // lblEstado
            //
            this.lblEstado.AutoSize = true;
            this.lblEstado.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblEstado.Location = new System.Drawing.Point(20, 20);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(260, 20);
            this.lblEstado.TabIndex = 0;
            this.lblEstado.Text = "Conectando con la base de datos...";
            //
            // lblDetalle
            //
            this.lblDetalle.AutoSize = true;
            this.lblDetalle.ForeColor = System.Drawing.Color.DimGray;
            this.lblDetalle.Location = new System.Drawing.Point(20, 50);
            this.lblDetalle.Name = "lblDetalle";
            this.lblDetalle.Size = new System.Drawing.Size(330, 15);
            this.lblDetalle.TabIndex = 1;
            this.lblDetalle.Text = "Si hace rato que nadie usa el sistema, puede tardar hasta un minuto.";
            //
            // pbEspera
            //
            this.pbEspera.Location = new System.Drawing.Point(20, 80);
            this.pbEspera.MarqueeAnimationSpeed = 30;
            this.pbEspera.Name = "pbEspera";
            this.pbEspera.Size = new System.Drawing.Size(400, 18);
            this.pbEspera.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pbEspera.TabIndex = 2;
            //
            // frmConexion
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 120);
            this.ControlBox = false;
            this.Controls.Add(this.pbEspera);
            this.Controls.Add(this.lblDetalle);
            this.Controls.Add(this.lblEstado);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmConexion";
            this.ShowInTaskbar = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NAT-SYS";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.Label lblDetalle;
        private System.Windows.Forms.ProgressBar pbEspera;
    }
}