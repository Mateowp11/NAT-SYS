namespace NatSys.UI
{
    partial class frmTrabajoDiarioAlta
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
            this.lblGrupo = new System.Windows.Forms.Label();
            this.lblFecha = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.lblTipo = new System.Windows.Forms.Label();
            this.cmbTipo = new System.Windows.Forms.ComboBox();
            this.lblArchivo = new System.Windows.Forms.Label();
            this.txtArchivo = new System.Windows.Forms.TextBox();
            this.btnAdjuntar = new System.Windows.Forms.Button();
            this.btnQuitarArchivo = new System.Windows.Forms.Button();
            this.lblBloques = new System.Windows.Forms.Label();
            this.dgvBloques = new System.Windows.Forms.DataGridView();
            this.colDescripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDistancia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRepeticiones = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblAyuda = new System.Windows.Forms.Label();
            this.btnQuitarBloque = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBloques)).BeginInit();
            this.SuspendLayout();
            //
            // lblGrupo
            //
            this.lblGrupo.AutoSize = true;
            this.lblGrupo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblGrupo.Location = new System.Drawing.Point(20, 20);
            this.lblGrupo.Name = "lblGrupo";
            this.lblGrupo.Size = new System.Drawing.Size(50, 17);
            this.lblGrupo.TabIndex = 0;
            this.lblGrupo.Text = "Grupo:";
            //
            // lblFecha
            //
            this.lblFecha.AutoSize = true;
            this.lblFecha.Location = new System.Drawing.Point(20, 58);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(41, 15);
            this.lblFecha.TabIndex = 1;
            this.lblFecha.Text = "Fecha:";
            //
            // dtpFecha
            //
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecha.Location = new System.Drawing.Point(75, 55);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(130, 23);
            this.dtpFecha.TabIndex = 2;
            //
            // lblTipo
            //
            this.lblTipo.AutoSize = true;
            this.lblTipo.Location = new System.Drawing.Point(240, 58);
            this.lblTipo.Name = "lblTipo";
            this.lblTipo.Size = new System.Drawing.Size(34, 15);
            this.lblTipo.TabIndex = 3;
            this.lblTipo.Text = "Tipo:";
            //
            // cmbTipo
            //
            this.cmbTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipo.FormattingEnabled = true;
            this.cmbTipo.Location = new System.Drawing.Point(285, 55);
            this.cmbTipo.Name = "cmbTipo";
            this.cmbTipo.Size = new System.Drawing.Size(140, 23);
            this.cmbTipo.TabIndex = 4;
            this.cmbTipo.SelectedIndexChanged += new System.EventHandler(this.cmbTipo_SelectedIndexChanged);
            //
            // lblArchivo
            //
            this.lblArchivo.AutoSize = true;
            this.lblArchivo.Location = new System.Drawing.Point(20, 98);
            this.lblArchivo.Name = "lblArchivo";
            this.lblArchivo.Size = new System.Drawing.Size(145, 15);
            this.lblArchivo.TabIndex = 5;
            this.lblArchivo.Text = "Archivo Excel (opcional):";
            //
            // txtArchivo
            //
            this.txtArchivo.Location = new System.Drawing.Point(20, 118);
            this.txtArchivo.Name = "txtArchivo";
            this.txtArchivo.ReadOnly = true;
            this.txtArchivo.Size = new System.Drawing.Size(330, 23);
            this.txtArchivo.TabIndex = 6;
            this.txtArchivo.TabStop = false;
            //
            // btnAdjuntar
            //
            this.btnAdjuntar.Location = new System.Drawing.Point(360, 117);
            this.btnAdjuntar.Name = "btnAdjuntar";
            this.btnAdjuntar.Size = new System.Drawing.Size(90, 25);
            this.btnAdjuntar.TabIndex = 7;
            this.btnAdjuntar.Text = "Adjuntar...";
            this.btnAdjuntar.UseVisualStyleBackColor = true;
            this.btnAdjuntar.Click += new System.EventHandler(this.btnAdjuntar_Click);
            //
            // btnQuitarArchivo
            //
            this.btnQuitarArchivo.Location = new System.Drawing.Point(455, 117);
            this.btnQuitarArchivo.Name = "btnQuitarArchivo";
            this.btnQuitarArchivo.Size = new System.Drawing.Size(85, 25);
            this.btnQuitarArchivo.TabIndex = 8;
            this.btnQuitarArchivo.Text = "Quitar";
            this.btnQuitarArchivo.UseVisualStyleBackColor = true;
            this.btnQuitarArchivo.Click += new System.EventHandler(this.btnQuitarArchivo_Click);
            //
            // lblBloques
            //
            this.lblBloques.AutoSize = true;
            this.lblBloques.Location = new System.Drawing.Point(20, 160);
            this.lblBloques.Name = "lblBloques";
            this.lblBloques.Size = new System.Drawing.Size(260, 15);
            this.lblBloques.TabIndex = 9;
            this.lblBloques.Text = "Bloques de la serie principal (uno por fila):";
            //
            // dgvBloques
            //
            this.dgvBloques.AllowUserToAddRows = true;
            this.dgvBloques.AllowUserToDeleteRows = true;
            this.dgvBloques.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDescripcion,
            this.colDistancia,
            this.colRepeticiones});
            this.dgvBloques.Location = new System.Drawing.Point(20, 182);
            this.dgvBloques.MultiSelect = false;
            this.dgvBloques.Name = "dgvBloques";
            this.dgvBloques.RowHeadersWidth = 25;
            this.dgvBloques.Size = new System.Drawing.Size(520, 200);
            this.dgvBloques.TabIndex = 10;
            //
            // colDescripcion
            //
            this.colDescripcion.HeaderText = "Descripción";
            this.colDescripcion.MaxInputLength = 80;
            this.colDescripcion.Name = "colDescripcion";
            this.colDescripcion.Width = 270;
            //
            // colDistancia
            //
            this.colDistancia.HeaderText = "Distancia (m)";
            this.colDistancia.MaxInputLength = 5;
            this.colDistancia.Name = "colDistancia";
            this.colDistancia.Width = 105;
            //
            // colRepeticiones
            //
            this.colRepeticiones.HeaderText = "Repeticiones";
            this.colRepeticiones.MaxInputLength = 3;
            this.colRepeticiones.Name = "colRepeticiones";
            this.colRepeticiones.Width = 105;
            //
            // lblAyuda
            //
            this.lblAyuda.AutoSize = true;
            this.lblAyuda.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblAyuda.Location = new System.Drawing.Point(20, 390);
            this.lblAyuda.Name = "lblAyuda";
            this.lblAyuda.Size = new System.Drawing.Size(250, 15);
            this.lblAyuda.TabIndex = 11;
            this.lblAyuda.Text = "Ejemplo: Crol a ritmo 200 | 100 | 4";
            //
            // btnQuitarBloque
            //
            this.btnQuitarBloque.Location = new System.Drawing.Point(420, 386);
            this.btnQuitarBloque.Name = "btnQuitarBloque";
            this.btnQuitarBloque.Size = new System.Drawing.Size(120, 25);
            this.btnQuitarBloque.TabIndex = 12;
            this.btnQuitarBloque.Text = "Quitar bloque";
            this.btnQuitarBloque.UseVisualStyleBackColor = true;
            this.btnQuitarBloque.Click += new System.EventHandler(this.btnQuitarBloque_Click);
            //
            // btnGuardar
            //
            this.btnGuardar.Location = new System.Drawing.Point(300, 430);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(130, 35);
            this.btnGuardar.TabIndex = 13;
            this.btnGuardar.Text = "Guardar trabajo";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            //
            // btnCancelar
            //
            this.btnCancelar.Location = new System.Drawing.Point(440, 430);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(100, 35);
            this.btnCancelar.TabIndex = 14;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            //
            // frmTrabajoDiarioAlta
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(560, 485);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnQuitarBloque);
            this.Controls.Add(this.lblAyuda);
            this.Controls.Add(this.dgvBloques);
            this.Controls.Add(this.lblBloques);
            this.Controls.Add(this.btnQuitarArchivo);
            this.Controls.Add(this.btnAdjuntar);
            this.Controls.Add(this.txtArchivo);
            this.Controls.Add(this.lblArchivo);
            this.Controls.Add(this.cmbTipo);
            this.Controls.Add(this.lblTipo);
            this.Controls.Add(this.dtpFecha);
            this.Controls.Add(this.lblFecha);
            this.Controls.Add(this.lblGrupo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTrabajoDiarioAlta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Agregar trabajo";
            ((System.ComponentModel.ISupportInitialize)(this.dgvBloques)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblGrupo;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Label lblTipo;
        private System.Windows.Forms.ComboBox cmbTipo;
        private System.Windows.Forms.Label lblArchivo;
        private System.Windows.Forms.TextBox txtArchivo;
        private System.Windows.Forms.Button btnAdjuntar;
        private System.Windows.Forms.Button btnQuitarArchivo;
        private System.Windows.Forms.Label lblBloques;
        private System.Windows.Forms.DataGridView dgvBloques;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDistancia;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRepeticiones;
        private System.Windows.Forms.Label lblAyuda;
        private System.Windows.Forms.Button btnQuitarBloque;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
    }
}
