namespace NatSys.UI
{
    partial class frmCalcularPasajes
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
            this.lblPrueba = new System.Windows.Forms.Label();
            this.cmbPrueba = new System.Windows.Forms.ComboBox();
            this.lblLargoPileta = new System.Windows.Forms.Label();
            this.cmbLargoPileta = new System.Windows.Forms.ComboBox();
            this.lblMarcaObjetivo = new System.Windows.Forms.Label();
            this.txtMarcaObjetivo = new System.Windows.Forms.TextBox();
            this.lblFormatoTiempo = new System.Windows.Forms.Label();
            this.btnCalcular = new System.Windows.Forms.Button();
            this.lblPlanVigente = new System.Windows.Forms.Label();
            this.lblRecordPersonal = new System.Windows.Forms.Label();
            this.lblViabilidad = new System.Windows.Forms.Label();
            this.dgvPasajes = new System.Windows.Forms.DataGridView();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnDescartar = new System.Windows.Forms.Button();
            this.btnVolver = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPasajes)).BeginInit();
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
            this.cmbAtleta.Location = new System.Drawing.Point(150, 20);
            this.cmbAtleta.Name = "cmbAtleta";
            this.cmbAtleta.Size = new System.Drawing.Size(220, 23);
            this.cmbAtleta.TabIndex = 1;
            this.cmbAtleta.SelectedIndexChanged += new System.EventHandler(this.cmbAtleta_SelectedIndexChanged);
            //
            // lblPrueba
            //
            this.lblPrueba.AutoSize = true;
            this.lblPrueba.Location = new System.Drawing.Point(20, 58);
            this.lblPrueba.Name = "lblPrueba";
            this.lblPrueba.Size = new System.Drawing.Size(49, 15);
            this.lblPrueba.TabIndex = 2;
            this.lblPrueba.Text = "Prueba:";
            //
            // cmbPrueba
            //
            this.cmbPrueba.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPrueba.Location = new System.Drawing.Point(150, 55);
            this.cmbPrueba.Name = "cmbPrueba";
            this.cmbPrueba.Size = new System.Drawing.Size(220, 23);
            this.cmbPrueba.TabIndex = 3;
            this.cmbPrueba.SelectedIndexChanged += new System.EventHandler(this.cmbPrueba_SelectedIndexChanged);
            //
            // lblLargoPileta
            //
            this.lblLargoPileta.AutoSize = true;
            this.lblLargoPileta.Location = new System.Drawing.Point(20, 93);
            this.lblLargoPileta.Name = "lblLargoPileta";
            this.lblLargoPileta.Size = new System.Drawing.Size(94, 15);
            this.lblLargoPileta.TabIndex = 4;
            this.lblLargoPileta.Text = "Largo de pileta:";
            //
            // cmbLargoPileta
            //
            this.cmbLargoPileta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLargoPileta.Items.AddRange(new object[] { "25", "50" });
            this.cmbLargoPileta.Location = new System.Drawing.Point(150, 90);
            this.cmbLargoPileta.Name = "cmbLargoPileta";
            this.cmbLargoPileta.Size = new System.Drawing.Size(80, 23);
            this.cmbLargoPileta.TabIndex = 5;
            this.cmbLargoPileta.SelectedIndexChanged += new System.EventHandler(this.cmbLargoPileta_SelectedIndexChanged);
            //
            // lblMarcaObjetivo
            //
            this.lblMarcaObjetivo.AutoSize = true;
            this.lblMarcaObjetivo.Location = new System.Drawing.Point(20, 128);
            this.lblMarcaObjetivo.Name = "lblMarcaObjetivo";
            this.lblMarcaObjetivo.Size = new System.Drawing.Size(92, 15);
            this.lblMarcaObjetivo.TabIndex = 6;
            this.lblMarcaObjetivo.Text = "Marca objetivo:";
            //
            // txtMarcaObjetivo
            //
            this.txtMarcaObjetivo.Location = new System.Drawing.Point(150, 125);
            this.txtMarcaObjetivo.Name = "txtMarcaObjetivo";
            this.txtMarcaObjetivo.Size = new System.Drawing.Size(80, 23);
            this.txtMarcaObjetivo.TabIndex = 7;
            this.txtMarcaObjetivo.TextChanged += new System.EventHandler(this.txtMarcaObjetivo_TextChanged);
            //
            // lblFormatoTiempo
            //
            this.lblFormatoTiempo.AutoSize = true;
            this.lblFormatoTiempo.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblFormatoTiempo.Location = new System.Drawing.Point(240, 128);
            this.lblFormatoTiempo.Name = "lblFormatoTiempo";
            this.lblFormatoTiempo.Size = new System.Drawing.Size(100, 15);
            this.lblFormatoTiempo.TabIndex = 8;
            this.lblFormatoTiempo.Text = "ss.cc o mm:ss.cc";
            //
            // btnCalcular
            //
            this.btnCalcular.Location = new System.Drawing.Point(150, 160);
            this.btnCalcular.Name = "btnCalcular";
            this.btnCalcular.Size = new System.Drawing.Size(110, 30);
            this.btnCalcular.TabIndex = 9;
            this.btnCalcular.Text = "Calcular";
            this.btnCalcular.UseVisualStyleBackColor = true;
            this.btnCalcular.Click += new System.EventHandler(this.btnCalcular_Click);
            //
            // lblPlanVigente
            //
            this.lblPlanVigente.AutoSize = true;
            this.lblPlanVigente.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblPlanVigente.Location = new System.Drawing.Point(20, 205);
            this.lblPlanVigente.Name = "lblPlanVigente";
            this.lblPlanVigente.Size = new System.Drawing.Size(0, 15);
            this.lblPlanVigente.TabIndex = 10;
            this.lblPlanVigente.Text = "";
            //
            // lblRecordPersonal
            //
            this.lblRecordPersonal.AutoSize = true;
            this.lblRecordPersonal.Location = new System.Drawing.Point(20, 230);
            this.lblRecordPersonal.Name = "lblRecordPersonal";
            this.lblRecordPersonal.Size = new System.Drawing.Size(0, 15);
            this.lblRecordPersonal.TabIndex = 11;
            this.lblRecordPersonal.Text = "";
            //
            // lblViabilidad
            //
            this.lblViabilidad.AutoSize = true;
            this.lblViabilidad.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblViabilidad.Location = new System.Drawing.Point(20, 255);
            this.lblViabilidad.Name = "lblViabilidad";
            this.lblViabilidad.Size = new System.Drawing.Size(0, 15);
            this.lblViabilidad.TabIndex = 12;
            this.lblViabilidad.Text = "";
            //
            // dgvPasajes
            //
            this.dgvPasajes.AllowUserToAddRows = false;
            this.dgvPasajes.AllowUserToDeleteRows = false;
            this.dgvPasajes.ReadOnly = true;
            this.dgvPasajes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPasajes.MultiSelect = false;
            this.dgvPasajes.AutoGenerateColumns = true;
            this.dgvPasajes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPasajes.Location = new System.Drawing.Point(20, 285);
            this.dgvPasajes.Name = "dgvPasajes";
            this.dgvPasajes.RowHeadersVisible = false;
            this.dgvPasajes.Size = new System.Drawing.Size(420, 205);
            this.dgvPasajes.TabIndex = 13;
            //
            // btnGuardar
            //
            this.btnGuardar.Location = new System.Drawing.Point(20, 505);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(110, 35);
            this.btnGuardar.TabIndex = 14;
            this.btnGuardar.Text = "Guardar plan";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            //
            // btnDescartar
            //
            this.btnDescartar.Location = new System.Drawing.Point(140, 505);
            this.btnDescartar.Name = "btnDescartar";
            this.btnDescartar.Size = new System.Drawing.Size(110, 35);
            this.btnDescartar.TabIndex = 15;
            this.btnDescartar.Text = "Descartar";
            this.btnDescartar.UseVisualStyleBackColor = true;
            this.btnDescartar.Click += new System.EventHandler(this.btnDescartar_Click);
            //
            // btnVolver
            //
            this.btnVolver.Location = new System.Drawing.Point(330, 505);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(110, 35);
            this.btnVolver.TabIndex = 16;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            //
            // frmCalcularPasajes
            //
            this.AcceptButton = this.btnCalcular;
            this.CancelButton = this.btnVolver;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 560);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.btnDescartar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.dgvPasajes);
            this.Controls.Add(this.lblViabilidad);
            this.Controls.Add(this.lblRecordPersonal);
            this.Controls.Add(this.lblPlanVigente);
            this.Controls.Add(this.btnCalcular);
            this.Controls.Add(this.lblFormatoTiempo);
            this.Controls.Add(this.txtMarcaObjetivo);
            this.Controls.Add(this.lblMarcaObjetivo);
            this.Controls.Add(this.cmbLargoPileta);
            this.Controls.Add(this.lblLargoPileta);
            this.Controls.Add(this.cmbPrueba);
            this.Controls.Add(this.lblPrueba);
            this.Controls.Add(this.cmbAtleta);
            this.Controls.Add(this.lblAtleta);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCalcularPasajes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "NAT-SYS - Calcular pasajes";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPasajes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblAtleta;
        private System.Windows.Forms.ComboBox cmbAtleta;
        private System.Windows.Forms.Label lblPrueba;
        private System.Windows.Forms.ComboBox cmbPrueba;
        private System.Windows.Forms.Label lblLargoPileta;
        private System.Windows.Forms.ComboBox cmbLargoPileta;
        private System.Windows.Forms.Label lblMarcaObjetivo;
        private System.Windows.Forms.TextBox txtMarcaObjetivo;
        private System.Windows.Forms.Label lblFormatoTiempo;
        private System.Windows.Forms.Button btnCalcular;
        private System.Windows.Forms.Label lblPlanVigente;
        private System.Windows.Forms.Label lblRecordPersonal;
        private System.Windows.Forms.Label lblViabilidad;
        private System.Windows.Forms.DataGridView dgvPasajes;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnDescartar;
        private System.Windows.Forms.Button btnVolver;
    }
}