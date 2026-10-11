namespace NatSys.UI
{
    partial class frmReporteViabilidad
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
            this.lblFechaObjetivo = new System.Windows.Forms.Label();
            this.dtpFechaObjetivo = new System.Windows.Forms.DateTimePicker();
            this.btnGenerar = new System.Windows.Forms.Button();
            this.lblRecordPersonal = new System.Windows.Forms.Label();
            this.lblTendencia = new System.Windows.Forms.Label();
            this.lblProyeccion = new System.Windows.Forms.Label();
            this.lblVeredicto = new System.Windows.Forms.Label();
            this.lblConclusion = new System.Windows.Forms.Label();
            this.lblProgresion = new System.Windows.Forms.Label();
            this.dgvProgresion = new System.Windows.Forms.DataGridView();
            this.btnExportarPdf = new System.Windows.Forms.Button();
            this.btnVolver = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProgresion)).BeginInit();
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
            this.cmbAtleta.SelectedIndexChanged += new System.EventHandler(this.Dato_Cambiado);
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
            this.cmbPrueba.SelectedIndexChanged += new System.EventHandler(this.Dato_Cambiado);
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
            this.cmbLargoPileta.SelectedIndexChanged += new System.EventHandler(this.Dato_Cambiado);
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
            this.txtMarcaObjetivo.TextChanged += new System.EventHandler(this.Dato_Cambiado);
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
            // lblFechaObjetivo
            //
            this.lblFechaObjetivo.AutoSize = true;
            this.lblFechaObjetivo.Location = new System.Drawing.Point(20, 163);
            this.lblFechaObjetivo.Name = "lblFechaObjetivo";
            this.lblFechaObjetivo.Size = new System.Drawing.Size(90, 15);
            this.lblFechaObjetivo.TabIndex = 9;
            this.lblFechaObjetivo.Text = "Fecha objetivo:";
            //
            // dtpFechaObjetivo
            //
            this.dtpFechaObjetivo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaObjetivo.Location = new System.Drawing.Point(150, 160);
            this.dtpFechaObjetivo.Name = "dtpFechaObjetivo";
            this.dtpFechaObjetivo.Size = new System.Drawing.Size(120, 23);
            this.dtpFechaObjetivo.TabIndex = 10;
            this.dtpFechaObjetivo.ValueChanged += new System.EventHandler(this.Dato_Cambiado);
            //
            // btnGenerar
            //
            this.btnGenerar.Location = new System.Drawing.Point(150, 195);
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.Size = new System.Drawing.Size(120, 30);
            this.btnGenerar.TabIndex = 11;
            this.btnGenerar.Text = "Generar";
            this.btnGenerar.UseVisualStyleBackColor = true;
            this.btnGenerar.Click += new System.EventHandler(this.btnGenerar_Click);
            //
            // lblRecordPersonal
            //
            this.lblRecordPersonal.AutoSize = true;
            this.lblRecordPersonal.Location = new System.Drawing.Point(20, 245);
            this.lblRecordPersonal.Name = "lblRecordPersonal";
            this.lblRecordPersonal.Size = new System.Drawing.Size(0, 15);
            this.lblRecordPersonal.TabIndex = 12;
            //
            // lblTendencia
            //
            this.lblTendencia.AutoSize = true;
            this.lblTendencia.Location = new System.Drawing.Point(20, 270);
            this.lblTendencia.Name = "lblTendencia";
            this.lblTendencia.Size = new System.Drawing.Size(0, 15);
            this.lblTendencia.TabIndex = 13;
            //
            // lblProyeccion
            //
            this.lblProyeccion.AutoSize = true;
            this.lblProyeccion.Location = new System.Drawing.Point(20, 295);
            this.lblProyeccion.Name = "lblProyeccion";
            this.lblProyeccion.Size = new System.Drawing.Size(0, 15);
            this.lblProyeccion.TabIndex = 14;
            //
            // lblVeredicto
            //
            this.lblVeredicto.AutoSize = true;
            this.lblVeredicto.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblVeredicto.Location = new System.Drawing.Point(20, 322);
            this.lblVeredicto.Name = "lblVeredicto";
            this.lblVeredicto.Size = new System.Drawing.Size(0, 20);
            this.lblVeredicto.TabIndex = 15;
            //
            // lblConclusion (AutoSize apagado para que el texto largo baje de renglon)
            //
            this.lblConclusion.AutoSize = false;
            this.lblConclusion.Location = new System.Drawing.Point(20, 350);
            this.lblConclusion.Name = "lblConclusion";
            this.lblConclusion.Size = new System.Drawing.Size(600, 65);
            this.lblConclusion.TabIndex = 16;
            //
            // lblProgresion
            //
            this.lblProgresion.AutoSize = true;
            this.lblProgresion.Location = new System.Drawing.Point(20, 420);
            this.lblProgresion.Name = "lblProgresion";
            this.lblProgresion.Size = new System.Drawing.Size(180, 15);
            this.lblProgresion.TabIndex = 17;
            this.lblProgresion.Text = "Progresión del récord personal:";
            //
            // dgvProgresion
            //
            this.dgvProgresion.AllowUserToAddRows = false;
            this.dgvProgresion.AllowUserToDeleteRows = false;
            this.dgvProgresion.ReadOnly = true;
            this.dgvProgresion.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProgresion.MultiSelect = false;
            this.dgvProgresion.AutoGenerateColumns = true;
            this.dgvProgresion.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProgresion.Location = new System.Drawing.Point(20, 440);
            this.dgvProgresion.Name = "dgvProgresion";
            this.dgvProgresion.RowHeadersVisible = false;
            this.dgvProgresion.Size = new System.Drawing.Size(600, 140);
            this.dgvProgresion.TabIndex = 18;
            //
            // btnExportarPdf
            //
            this.btnExportarPdf.Location = new System.Drawing.Point(20, 595);
            this.btnExportarPdf.Name = "btnExportarPdf";
            this.btnExportarPdf.Size = new System.Drawing.Size(120, 35);
            this.btnExportarPdf.TabIndex = 19;
            this.btnExportarPdf.Text = "Exportar PDF";
            this.btnExportarPdf.UseVisualStyleBackColor = true;
            this.btnExportarPdf.Click += new System.EventHandler(this.btnExportarPdf_Click);
            //
            // btnVolver
            //
            this.btnVolver.Location = new System.Drawing.Point(500, 595);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(120, 35);
            this.btnVolver.TabIndex = 20;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            //
            // frmReporteViabilidad
            //
            this.AcceptButton = this.btnGenerar;
            this.CancelButton = this.btnVolver;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 645);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.btnExportarPdf);
            this.Controls.Add(this.dgvProgresion);
            this.Controls.Add(this.lblProgresion);
            this.Controls.Add(this.lblConclusion);
            this.Controls.Add(this.lblVeredicto);
            this.Controls.Add(this.lblProyeccion);
            this.Controls.Add(this.lblTendencia);
            this.Controls.Add(this.lblRecordPersonal);
            this.Controls.Add(this.btnGenerar);
            this.Controls.Add(this.dtpFechaObjetivo);
            this.Controls.Add(this.lblFechaObjetivo);
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
            this.Name = "frmReporteViabilidad";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "NAT-SYS - Reporte de viabilidad de marca";
            ((System.ComponentModel.ISupportInitialize)(this.dgvProgresion)).EndInit();
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
        private System.Windows.Forms.Label lblFechaObjetivo;
        private System.Windows.Forms.DateTimePicker dtpFechaObjetivo;
        private System.Windows.Forms.Button btnGenerar;
        private System.Windows.Forms.Label lblRecordPersonal;
        private System.Windows.Forms.Label lblTendencia;
        private System.Windows.Forms.Label lblProyeccion;
        private System.Windows.Forms.Label lblVeredicto;
        private System.Windows.Forms.Label lblConclusion;
        private System.Windows.Forms.Label lblProgresion;
        private System.Windows.Forms.DataGridView dgvProgresion;
        private System.Windows.Forms.Button btnExportarPdf;
        private System.Windows.Forms.Button btnVolver;
    }
}
