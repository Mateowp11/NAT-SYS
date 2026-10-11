namespace NatSys.UI
{
    partial class frmReporteEvolucion
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
            this.lblLargoPileta = new System.Windows.Forms.Label();
            this.cmbLargoPileta = new System.Windows.Forms.ComboBox();
            this.lblEstilo = new System.Windows.Forms.Label();
            this.cmbEstilo = new System.Windows.Forms.ComboBox();
            this.lblDesde = new System.Windows.Forms.Label();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.lblHasta = new System.Windows.Forms.Label();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.btnGenerar = new System.Windows.Forms.Button();
            this.pnlGrafico = new System.Windows.Forms.Panel();
            this.lblResumen = new System.Windows.Forms.Label();
            this.dgvResumen = new System.Windows.Forms.DataGridView();
            this.lblCriterio = new System.Windows.Forms.Label();
            this.btnExportarPdf = new System.Windows.Forms.Button();
            this.btnVolver = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResumen)).BeginInit();
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
            this.cmbAtleta.Location = new System.Drawing.Point(75, 20);
            this.cmbAtleta.Name = "cmbAtleta";
            this.cmbAtleta.Size = new System.Drawing.Size(210, 23);
            this.cmbAtleta.TabIndex = 1;
            this.cmbAtleta.SelectedIndexChanged += new System.EventHandler(this.Filtro_Cambiado);
            //
            // lblLargoPileta
            //
            this.lblLargoPileta.AutoSize = true;
            this.lblLargoPileta.Location = new System.Drawing.Point(305, 23);
            this.lblLargoPileta.Name = "lblLargoPileta";
            this.lblLargoPileta.Size = new System.Drawing.Size(40, 15);
            this.lblLargoPileta.TabIndex = 2;
            this.lblLargoPileta.Text = "Pileta:";
            //
            // cmbLargoPileta
            //
            this.cmbLargoPileta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLargoPileta.Items.AddRange(new object[] { "25", "50" });
            this.cmbLargoPileta.Location = new System.Drawing.Point(355, 20);
            this.cmbLargoPileta.Name = "cmbLargoPileta";
            this.cmbLargoPileta.Size = new System.Drawing.Size(60, 23);
            this.cmbLargoPileta.TabIndex = 3;
            this.cmbLargoPileta.SelectedIndexChanged += new System.EventHandler(this.Filtro_Cambiado);
            //
            // lblEstilo
            //
            this.lblEstilo.AutoSize = true;
            this.lblEstilo.Location = new System.Drawing.Point(435, 23);
            this.lblEstilo.Name = "lblEstilo";
            this.lblEstilo.Size = new System.Drawing.Size(40, 15);
            this.lblEstilo.TabIndex = 4;
            this.lblEstilo.Text = "Estilo:";
            //
            // cmbEstilo
            //
            this.cmbEstilo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            // Los nombres tienen que coincidir con los Estilos cargados en la base
            this.cmbEstilo.Items.AddRange(new object[] { "Todos", "Crol", "Espalda", "Pecho", "Mariposa", "Combinado" });
            this.cmbEstilo.Location = new System.Drawing.Point(485, 20);
            this.cmbEstilo.Name = "cmbEstilo";
            this.cmbEstilo.Size = new System.Drawing.Size(120, 23);
            this.cmbEstilo.TabIndex = 5;
            this.cmbEstilo.SelectedIndexChanged += new System.EventHandler(this.Filtro_Cambiado);
            //
            // lblDesde
            //
            this.lblDesde.AutoSize = true;
            this.lblDesde.Location = new System.Drawing.Point(20, 58);
            this.lblDesde.Name = "lblDesde";
            this.lblDesde.Size = new System.Drawing.Size(42, 15);
            this.lblDesde.TabIndex = 6;
            this.lblDesde.Text = "Desde:";
            //
            // dtpDesde
            //
            this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDesde.Location = new System.Drawing.Point(75, 55);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.Size = new System.Drawing.Size(110, 23);
            this.dtpDesde.TabIndex = 7;
            this.dtpDesde.ValueChanged += new System.EventHandler(this.Filtro_Cambiado);
            //
            // lblHasta
            //
            this.lblHasta.AutoSize = true;
            this.lblHasta.Location = new System.Drawing.Point(205, 58);
            this.lblHasta.Name = "lblHasta";
            this.lblHasta.Size = new System.Drawing.Size(40, 15);
            this.lblHasta.TabIndex = 8;
            this.lblHasta.Text = "Hasta:";
            //
            // dtpHasta
            //
            this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHasta.Location = new System.Drawing.Point(255, 55);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.Size = new System.Drawing.Size(110, 23);
            this.dtpHasta.TabIndex = 9;
            this.dtpHasta.ValueChanged += new System.EventHandler(this.Filtro_Cambiado);
            //
            // btnGenerar
            //
            this.btnGenerar.Location = new System.Drawing.Point(385, 52);
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.Size = new System.Drawing.Size(120, 30);
            this.btnGenerar.TabIndex = 10;
            this.btnGenerar.Text = "Generar";
            this.btnGenerar.UseVisualStyleBackColor = true;
            this.btnGenerar.Click += new System.EventHandler(this.btnGenerar_Click);
            //
            // pnlGrafico (aca adentro se agrega el grafico de ScottPlot por codigo)
            //
            this.pnlGrafico.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGrafico.Location = new System.Drawing.Point(20, 95);
            this.pnlGrafico.Name = "pnlGrafico";
            this.pnlGrafico.Size = new System.Drawing.Size(860, 330);
            this.pnlGrafico.TabIndex = 11;
            //
            // lblResumen
            //
            this.lblResumen.AutoSize = true;
            this.lblResumen.Location = new System.Drawing.Point(20, 435);
            this.lblResumen.Name = "lblResumen";
            this.lblResumen.Size = new System.Drawing.Size(120, 15);
            this.lblResumen.TabIndex = 12;
            this.lblResumen.Text = "Resumen por prueba:";
            //
            // dgvResumen
            //
            this.dgvResumen.AllowUserToAddRows = false;
            this.dgvResumen.AllowUserToDeleteRows = false;
            this.dgvResumen.ReadOnly = true;
            this.dgvResumen.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvResumen.MultiSelect = false;
            this.dgvResumen.AutoGenerateColumns = true;
            this.dgvResumen.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvResumen.Location = new System.Drawing.Point(20, 455);
            this.dgvResumen.Name = "dgvResumen";
            this.dgvResumen.RowHeadersVisible = false;
            this.dgvResumen.Size = new System.Drawing.Size(860, 140);
            this.dgvResumen.TabIndex = 13;
            //
            // lblCriterio
            //
            this.lblCriterio.AutoSize = true;
            this.lblCriterio.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblCriterio.Location = new System.Drawing.Point(20, 600);
            this.lblCriterio.Name = "lblCriterio";
            this.lblCriterio.Size = new System.Drawing.Size(700, 15);
            this.lblCriterio.TabIndex = 14;
            this.lblCriterio.Text = "Mejora notable: bajó 2 % o más · Mejora: entre 0.5 y 2 % · Estancado: menos de 0.5 % · " +
                "En retroceso: la última marca es más de 0.5 % más lenta que la primera";
            //
            // btnExportarPdf
            //
            this.btnExportarPdf.Location = new System.Drawing.Point(20, 625);
            this.btnExportarPdf.Name = "btnExportarPdf";
            this.btnExportarPdf.Size = new System.Drawing.Size(120, 35);
            this.btnExportarPdf.TabIndex = 15;
            this.btnExportarPdf.Text = "Exportar PDF";
            this.btnExportarPdf.UseVisualStyleBackColor = true;
            this.btnExportarPdf.Click += new System.EventHandler(this.btnExportarPdf_Click);
            //
            // btnVolver
            //
            this.btnVolver.Location = new System.Drawing.Point(760, 625);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(120, 35);
            this.btnVolver.TabIndex = 16;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            //
            // frmReporteEvolucion
            //
            this.AcceptButton = this.btnGenerar;
            this.CancelButton = this.btnVolver;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 675);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.btnExportarPdf);
            this.Controls.Add(this.lblCriterio);
            this.Controls.Add(this.dgvResumen);
            this.Controls.Add(this.lblResumen);
            this.Controls.Add(this.pnlGrafico);
            this.Controls.Add(this.btnGenerar);
            this.Controls.Add(this.dtpHasta);
            this.Controls.Add(this.lblHasta);
            this.Controls.Add(this.dtpDesde);
            this.Controls.Add(this.lblDesde);
            this.Controls.Add(this.cmbEstilo);
            this.Controls.Add(this.lblEstilo);
            this.Controls.Add(this.cmbLargoPileta);
            this.Controls.Add(this.lblLargoPileta);
            this.Controls.Add(this.cmbAtleta);
            this.Controls.Add(this.lblAtleta);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmReporteEvolucion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "NAT-SYS - Análisis de evolución por estilo";
            ((System.ComponentModel.ISupportInitialize)(this.dgvResumen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblAtleta;
        private System.Windows.Forms.ComboBox cmbAtleta;
        private System.Windows.Forms.Label lblLargoPileta;
        private System.Windows.Forms.ComboBox cmbLargoPileta;
        private System.Windows.Forms.Label lblEstilo;
        private System.Windows.Forms.ComboBox cmbEstilo;
        private System.Windows.Forms.Label lblDesde;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.Label lblHasta;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.Button btnGenerar;
        private System.Windows.Forms.Panel pnlGrafico;
        private System.Windows.Forms.Label lblResumen;
        private System.Windows.Forms.DataGridView dgvResumen;
        private System.Windows.Forms.Label lblCriterio;
        private System.Windows.Forms.Button btnExportarPdf;
        private System.Windows.Forms.Button btnVolver;
    }
}
