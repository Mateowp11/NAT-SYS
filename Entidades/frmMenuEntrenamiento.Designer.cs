namespace NatSys.UI
{
    partial class frmMenuEntrenamiento
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
            this.btnGrupos = new System.Windows.Forms.Button();
            this.btnTrabajos = new System.Windows.Forms.Button();
            this.btnVolver = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // lblTitulo
            //
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(20, 15);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(140, 25);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Entrenamiento";
            //
            // btnGrupos
            //
            this.btnGrupos.Location = new System.Drawing.Point(20, 60);
            this.btnGrupos.Name = "btnGrupos";
            this.btnGrupos.Size = new System.Drawing.Size(150, 70);
            this.btnGrupos.TabIndex = 1;
            this.btnGrupos.Text = "Grupos de entrenamiento";
            this.btnGrupos.UseVisualStyleBackColor = true;
            this.btnGrupos.Click += new System.EventHandler(this.btnGrupos_Click);
            //
            // btnTrabajos
            //
            this.btnTrabajos.Location = new System.Drawing.Point(190, 60);
            this.btnTrabajos.Name = "btnTrabajos";
            this.btnTrabajos.Size = new System.Drawing.Size(150, 70);
            this.btnTrabajos.TabIndex = 2;
            this.btnTrabajos.Text = "Cargar trabajo";
            this.btnTrabajos.UseVisualStyleBackColor = true;
            this.btnTrabajos.Click += new System.EventHandler(this.btnTrabajos_Click);
            //
            // btnVolver
            //
            this.btnVolver.Location = new System.Drawing.Point(240, 150);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(100, 35);
            this.btnVolver.TabIndex = 3;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            //
            // frmMenuEntrenamiento
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 205);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.btnTrabajos);
            this.Controls.Add(this.btnGrupos);
            this.Controls.Add(this.lblTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMenuEntrenamiento";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "NAT-SYS - Entrenamiento";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Button btnGrupos;
        private System.Windows.Forms.Button btnTrabajos;
        private System.Windows.Forms.Button btnVolver;
    }
}
