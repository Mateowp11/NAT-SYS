namespace NatSys.UI
{
    partial class frmTrabajosDiarios
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
            this.cmbGrupo = new System.Windows.Forms.ComboBox();
            this.lblFecha = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.lblFiltro = new System.Windows.Forms.Label();
            this.dgvTrabajos = new System.Windows.Forms.DataGridView();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnAbrir = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnVolver = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrabajos)).BeginInit();
            this.SuspendLayout();
            //
            // lblGrupo
            //
            this.lblGrupo.AutoSize = true;
            this.lblGrupo.Location = new System.Drawing.Point(20, 23);
            this.lblGrupo.Name = "lblGrupo";
            this.lblGrupo.Size = new System.Drawing.Size(43, 15);
            this.lblGrupo.TabIndex = 0;
            this.lblGrupo.Text = "Grupo:";
            //
            // cmbGrupo
            //
            this.cmbGrupo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGrupo.FormattingEnabled = true;
            this.cmbGrupo.Location = new System.Drawing.Point(75, 20);
            this.cmbGrupo.Name = "cmbGrupo";
            this.cmbGrupo.Size = new System.Drawing.Size(250, 23);
            this.cmbGrupo.TabIndex = 1;
            this.cmbGrupo.SelectedIndexChanged += new System.EventHandler(this.cmbGrupo_SelectedIndexChanged);
            //
            // lblFecha
            //
            this.lblFecha.AutoSize = true;
            this.lblFecha.Location = new System.Drawing.Point(350, 23);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(41, 15);
            this.lblFecha.TabIndex = 2;
            this.lblFecha.Text = "Fecha:";
            //
            // dtpFecha
            //
            this.dtpFecha.Checked = false;
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecha.Location = new System.Drawing.Point(400, 20);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.ShowCheckBox = true;
            this.dtpFecha.Size = new System.Drawing.Size(140, 23);
            this.dtpFecha.TabIndex = 3;
            this.dtpFecha.ValueChanged += new System.EventHandler(this.dtpFecha_ValueChanged);
            //
            // lblFiltro
            //
            this.lblFiltro.AutoSize = true;
            this.lblFiltro.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblFiltro.Location = new System.Drawing.Point(550, 23);
            this.lblFiltro.Name = "lblFiltro";
            this.lblFiltro.Size = new System.Drawing.Size(160, 15);
            this.lblFiltro.TabIndex = 4;
            this.lblFiltro.Text = "Tildá la fecha para ver un día";
            //
            // dgvTrabajos
            //
            this.dgvTrabajos.AllowUserToAddRows = false;
            this.dgvTrabajos.AllowUserToDeleteRows = false;
            this.dgvTrabajos.ReadOnly = true;
            this.dgvTrabajos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTrabajos.MultiSelect = false;
            this.dgvTrabajos.AutoGenerateColumns = true;
            this.dgvTrabajos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTrabajos.Location = new System.Drawing.Point(20, 60);
            this.dgvTrabajos.Name = "dgvTrabajos";
            this.dgvTrabajos.RowHeadersWidth = 25;
            this.dgvTrabajos.Size = new System.Drawing.Size(720, 300);
            this.dgvTrabajos.TabIndex = 5;
            //
            // btnAgregar
            //
            this.btnAgregar.Location = new System.Drawing.Point(20, 375);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(130, 35);
            this.btnAgregar.TabIndex = 6;
            this.btnAgregar.Text = "Agregar trabajo";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            //
            // btnAbrir
            //
            this.btnAbrir.Location = new System.Drawing.Point(160, 375);
            this.btnAbrir.Name = "btnAbrir";
            this.btnAbrir.Size = new System.Drawing.Size(170, 35);
            this.btnAbrir.TabIndex = 7;
            this.btnAbrir.Text = "Abrir archivo de trabajo";
            this.btnAbrir.UseVisualStyleBackColor = true;
            this.btnAbrir.Click += new System.EventHandler(this.btnAbrir_Click);
            //
            // btnEliminar
            //
            this.btnEliminar.Location = new System.Drawing.Point(340, 375);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(130, 35);
            this.btnEliminar.TabIndex = 8;
            this.btnEliminar.Text = "Eliminar trabajo";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            //
            // btnVolver
            //
            this.btnVolver.Location = new System.Drawing.Point(630, 375);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(110, 35);
            this.btnVolver.TabIndex = 9;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            //
            // frmTrabajosDiarios
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 430);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnAbrir);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.dgvTrabajos);
            this.Controls.Add(this.lblFiltro);
            this.Controls.Add(this.dtpFecha);
            this.Controls.Add(this.lblFecha);
            this.Controls.Add(this.cmbGrupo);
            this.Controls.Add(this.lblGrupo);
            this.MaximizeBox = false;
            this.Name = "frmTrabajosDiarios";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NAT-SYS - Cargar trabajo";
            this.Load += new System.EventHandler(this.frmTrabajosDiarios_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrabajos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblGrupo;
        private System.Windows.Forms.ComboBox cmbGrupo;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Label lblFiltro;
        private System.Windows.Forms.DataGridView dgvTrabajos;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnAbrir;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnVolver;
    }
}
