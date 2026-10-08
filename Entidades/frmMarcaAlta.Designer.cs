namespace NatSys.UI
{
    partial class frmMarcaAlta
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
            this.lblAtleta = new System.Windows.Forms.Label();
            this.cmbAtleta = new System.Windows.Forms.ComboBox();
            this.lblTipo = new System.Windows.Forms.Label();
            this.cmbTipo = new System.Windows.Forms.ComboBox();
            this.lblPrueba = new System.Windows.Forms.Label();
            this.cmbPrueba = new System.Windows.Forms.ComboBox();
            this.lblLargoPileta = new System.Windows.Forms.Label();
            this.cmbLargoPileta = new System.Windows.Forms.ComboBox();
            this.lblTiempo = new System.Windows.Forms.Label();
            this.txtTiempo = new System.Windows.Forms.TextBox();
            this.lblFormatoTiempo = new System.Windows.Forms.Label();
            this.lblTorneo = new System.Windows.Forms.Label();
            this.cmbTorneo = new System.Windows.Forms.ComboBox();
            this.lblFecha = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.lblRecordPersonal = new System.Windows.Forms.Label();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // lblAtleta
            //
            this.lblAtleta.AutoSize = true;
            this.lblAtleta.Location = new System.Drawing.Point(20, 23);
            this.lblAtleta.Name = "lblAtleta";
            this.lblAtleta.Size = new System.Drawing.Size(44, 15);
            this.lblAtleta.TabIndex = 0;
            this.lblAtleta.Text = "Atleta:";
            //
            // cmbAtleta
            //
            this.cmbAtleta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAtleta.Location = new System.Drawing.Point(130, 20);
            this.cmbAtleta.Name = "cmbAtleta";
            this.cmbAtleta.Size = new System.Drawing.Size(220, 23);
            this.cmbAtleta.TabIndex = 1;
            //
            // lblTipo
            //
            this.lblTipo.AutoSize = true;
            this.lblTipo.Location = new System.Drawing.Point(20, 58);
            this.lblTipo.Name = "lblTipo";
            this.lblTipo.Size = new System.Drawing.Size(34, 15);
            this.lblTipo.TabIndex = 2;
            this.lblTipo.Text = "Tipo:";
            //
            // cmbTipo
            //
            this.cmbTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipo.Items.AddRange(new object[] { "oficial", "test" });
            this.cmbTipo.Location = new System.Drawing.Point(130, 55);
            this.cmbTipo.Name = "cmbTipo";
            this.cmbTipo.Size = new System.Drawing.Size(220, 23);
            this.cmbTipo.TabIndex = 3;
            this.cmbTipo.SelectedIndexChanged += new System.EventHandler(this.cmbTipo_SelectedIndexChanged);
            //
            // lblPrueba
            //
            this.lblPrueba.AutoSize = true;
            this.lblPrueba.Location = new System.Drawing.Point(20, 93);
            this.lblPrueba.Name = "lblPrueba";
            this.lblPrueba.Size = new System.Drawing.Size(49, 15);
            this.lblPrueba.TabIndex = 4;
            this.lblPrueba.Text = "Prueba:";
            //
            // cmbPrueba
            //
            this.cmbPrueba.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPrueba.Location = new System.Drawing.Point(130, 90);
            this.cmbPrueba.Name = "cmbPrueba";
            this.cmbPrueba.Size = new System.Drawing.Size(220, 23);
            this.cmbPrueba.TabIndex = 5;
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
            this.cmbLargoPileta.Location = new System.Drawing.Point(130, 125);
            this.cmbLargoPileta.Name = "cmbLargoPileta";
            this.cmbLargoPileta.Size = new System.Drawing.Size(220, 23);
            this.cmbLargoPileta.TabIndex = 7;
            //
            // lblTiempo
            //
            this.lblTiempo.AutoSize = true;
            this.lblTiempo.Location = new System.Drawing.Point(20, 163);
            this.lblTiempo.Name = "lblTiempo";
            this.lblTiempo.Size = new System.Drawing.Size(50, 15);
            this.lblTiempo.TabIndex = 8;
            this.lblTiempo.Text = "Tiempo:";
            //
            // txtTiempo
            //
            this.txtTiempo.Location = new System.Drawing.Point(130, 160);
            this.txtTiempo.Name = "txtTiempo";
            this.txtTiempo.Size = new System.Drawing.Size(110, 23);
            this.txtTiempo.TabIndex = 9;
            //
            // lblFormatoTiempo (ayuda para el usuario)
            //
            this.lblFormatoTiempo.AutoSize = true;
            this.lblFormatoTiempo.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblFormatoTiempo.Location = new System.Drawing.Point(250, 163);
            this.lblFormatoTiempo.Name = "lblFormatoTiempo";
            this.lblFormatoTiempo.Size = new System.Drawing.Size(100, 15);
            this.lblFormatoTiempo.TabIndex = 10;
            this.lblFormatoTiempo.Text = "ss.cc o mm:ss.cc";
            //
            // lblTorneo
            //
            this.lblTorneo.AutoSize = true;
            this.lblTorneo.Location = new System.Drawing.Point(20, 198);
            this.lblTorneo.Name = "lblTorneo";
            this.lblTorneo.Size = new System.Drawing.Size(48, 15);
            this.lblTorneo.TabIndex = 11;
            this.lblTorneo.Text = "Torneo:";
            //
            // cmbTorneo
            //
            this.cmbTorneo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTorneo.Location = new System.Drawing.Point(130, 195);
            this.cmbTorneo.Name = "cmbTorneo";
            this.cmbTorneo.Size = new System.Drawing.Size(220, 23);
            this.cmbTorneo.TabIndex = 12;
            this.cmbTorneo.SelectedIndexChanged += new System.EventHandler(this.cmbTorneo_SelectedIndexChanged);
            //
            // lblFecha
            //
            this.lblFecha.AutoSize = true;
            this.lblFecha.Location = new System.Drawing.Point(20, 233);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(41, 15);
            this.lblFecha.TabIndex = 13;
            this.lblFecha.Text = "Fecha:";
            //
            // dtpFecha
            //
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecha.Location = new System.Drawing.Point(130, 230);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(220, 23);
            this.dtpFecha.TabIndex = 14;
            //
            // lblRecordPersonal (solo se ve al modificar o consultar)
            //
            this.lblRecordPersonal.AutoSize = true;
            this.lblRecordPersonal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblRecordPersonal.Location = new System.Drawing.Point(130, 265);
            this.lblRecordPersonal.Name = "lblRecordPersonal";
            this.lblRecordPersonal.Size = new System.Drawing.Size(120, 15);
            this.lblRecordPersonal.TabIndex = 15;
            this.lblRecordPersonal.Text = "Récord personal:";
            //
            // btnGuardar
            //
            this.btnGuardar.Location = new System.Drawing.Point(70, 295);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(110, 32);
            this.btnGuardar.TabIndex = 16;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            //
            // btnCancelar
            //
            this.btnCancelar.Location = new System.Drawing.Point(200, 295);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(110, 32);
            this.btnCancelar.TabIndex = 17;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            //
            // frmMarcaAlta
            //
            this.AcceptButton = this.btnGuardar;
            this.CancelButton = this.btnCancelar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 345);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.lblRecordPersonal);
            this.Controls.Add(this.dtpFecha);
            this.Controls.Add(this.lblFecha);
            this.Controls.Add(this.cmbTorneo);
            this.Controls.Add(this.lblTorneo);
            this.Controls.Add(this.lblFormatoTiempo);
            this.Controls.Add(this.txtTiempo);
            this.Controls.Add(this.lblTiempo);
            this.Controls.Add(this.cmbLargoPileta);
            this.Controls.Add(this.lblLargoPileta);
            this.Controls.Add(this.cmbPrueba);
            this.Controls.Add(this.lblPrueba);
            this.Controls.Add(this.cmbTipo);
            this.Controls.Add(this.lblTipo);
            this.Controls.Add(this.cmbAtleta);
            this.Controls.Add(this.lblAtleta);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMarcaAlta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Nueva marca";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblAtleta;
        private System.Windows.Forms.ComboBox cmbAtleta;
        private System.Windows.Forms.Label lblTipo;
        private System.Windows.Forms.ComboBox cmbTipo;
        private System.Windows.Forms.Label lblPrueba;
        private System.Windows.Forms.ComboBox cmbPrueba;
        private System.Windows.Forms.Label lblLargoPileta;
        private System.Windows.Forms.ComboBox cmbLargoPileta;
        private System.Windows.Forms.Label lblTiempo;
        private System.Windows.Forms.TextBox txtTiempo;
        private System.Windows.Forms.Label lblFormatoTiempo;
        private System.Windows.Forms.Label lblTorneo;
        private System.Windows.Forms.ComboBox cmbTorneo;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Label lblRecordPersonal;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
    }
}