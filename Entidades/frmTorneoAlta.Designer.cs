namespace NatSys.UI
{
    partial class frmTorneoAlta
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
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblFecha = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.lblSede = new System.Windows.Forms.Label();
            this.txtSede = new System.Windows.Forms.TextBox();
            this.lblLargoPileta = new System.Windows.Forms.Label();
            this.cmbLargoPileta = new System.Windows.Forms.ComboBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // lblNombre
            //
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(20, 23);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(90, 15);
            this.lblNombre.TabIndex = 0;
            this.lblNombre.Text = "Nombre torneo:";
            //
            // txtNombre
            //
            this.txtNombre.Location = new System.Drawing.Point(150, 20);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(180, 23);
            this.txtNombre.TabIndex = 1;
            //
            // lblFecha
            //
            this.lblFecha.AutoSize = true;
            this.lblFecha.Location = new System.Drawing.Point(20, 58);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(45, 15);
            this.lblFecha.TabIndex = 2;
            this.lblFecha.Text = "Fecha:";
            //
            // dtpFecha
            //
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecha.Location = new System.Drawing.Point(150, 55);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(180, 23);
            this.dtpFecha.TabIndex = 3;
            //
            // lblSede
            //
            this.lblSede.AutoSize = true;
            this.lblSede.Location = new System.Drawing.Point(20, 93);
            this.lblSede.Name = "lblSede";
            this.lblSede.Size = new System.Drawing.Size(38, 15);
            this.lblSede.TabIndex = 4;
            this.lblSede.Text = "Sede:";
            //
            // txtSede
            //
            this.txtSede.Location = new System.Drawing.Point(150, 90);
            this.txtSede.Name = "txtSede";
            this.txtSede.Size = new System.Drawing.Size(180, 23);
            this.txtSede.TabIndex = 5;
            //
            // lblLargoPileta
            //
            this.lblLargoPileta.AutoSize = true;
            this.lblLargoPileta.Location = new System.Drawing.Point(20, 128);
            this.lblLargoPileta.Name = "lblLargoPileta";
            this.lblLargoPileta.Size = new System.Drawing.Size(94, 15);
            this.lblLargoPileta.TabIndex = 6;
            this.lblLargoPileta.Text = "Largo de pileta:";
            //
            // cmbLargoPileta
            //
            this.cmbLargoPileta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLargoPileta.Items.AddRange(new object[] { "25", "50" });
            this.cmbLargoPileta.Location = new System.Drawing.Point(150, 125);
            this.cmbLargoPileta.Name = "cmbLargoPileta";
            this.cmbLargoPileta.Size = new System.Drawing.Size(180, 23);
            this.cmbLargoPileta.TabIndex = 7;
            //
            // btnGuardar
            //
            this.btnGuardar.Location = new System.Drawing.Point(60, 175);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(110, 32);
            this.btnGuardar.TabIndex = 8;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            //
            // btnCancelar
            //
            this.btnCancelar.Location = new System.Drawing.Point(180, 175);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(110, 32);
            this.btnCancelar.TabIndex = 9;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            //
            // frmTorneoAlta
            //
            this.AcceptButton = this.btnGuardar;
            this.CancelButton = this.btnCancelar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 230);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.cmbLargoPileta);
            this.Controls.Add(this.lblLargoPileta);
            this.Controls.Add(this.txtSede);
            this.Controls.Add(this.lblSede);
            this.Controls.Add(this.dtpFecha);
            this.Controls.Add(this.lblFecha);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.lblNombre);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTorneoAlta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Nuevo torneo";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Label lblSede;
        private System.Windows.Forms.TextBox txtSede;
        private System.Windows.Forms.Label lblLargoPileta;
        private System.Windows.Forms.ComboBox cmbLargoPileta;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
    }
}