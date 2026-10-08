namespace NatSys.UI
{
    partial class frmMarcas
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
            this.lblFiltroAtleta = new System.Windows.Forms.Label();
            this.cmbFiltroAtleta = new System.Windows.Forms.ComboBox();
            this.lblFiltroPrueba = new System.Windows.Forms.Label();
            this.cmbFiltroPrueba = new System.Windows.Forms.ComboBox();
            this.lblFiltroTipo = new System.Windows.Forms.Label();
            this.cmbFiltroTipo = new System.Windows.Forms.ComboBox();
            this.dgvMarcas = new System.Windows.Forms.DataGridView();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnVerDetalle = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.btnVolver = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMarcas)).BeginInit();
            this.SuspendLayout();
            //
            // lblFiltroAtleta
            //
            this.lblFiltroAtleta.AutoSize = true;
            this.lblFiltroAtleta.Location = new System.Drawing.Point(20, 20);
            this.lblFiltroAtleta.Name = "lblFiltroAtleta";
            this.lblFiltroAtleta.Size = new System.Drawing.Size(44, 15);
            this.lblFiltroAtleta.TabIndex = 0;
            this.lblFiltroAtleta.Text = "Atleta:";
            //
            // cmbFiltroAtleta
            //
            this.cmbFiltroAtleta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFiltroAtleta.Location = new System.Drawing.Point(20, 40);
            this.cmbFiltroAtleta.Name = "cmbFiltroAtleta";
            this.cmbFiltroAtleta.Size = new System.Drawing.Size(200, 23);
            this.cmbFiltroAtleta.TabIndex = 1;
            this.cmbFiltroAtleta.SelectedIndexChanged += new System.EventHandler(this.Filtro_SelectedIndexChanged);
            //
            // lblFiltroPrueba
            //
            this.lblFiltroPrueba.AutoSize = true;
            this.lblFiltroPrueba.Location = new System.Drawing.Point(235, 20);
            this.lblFiltroPrueba.Name = "lblFiltroPrueba";
            this.lblFiltroPrueba.Size = new System.Drawing.Size(49, 15);
            this.lblFiltroPrueba.TabIndex = 2;
            this.lblFiltroPrueba.Text = "Prueba:";
            //
            // cmbFiltroPrueba
            //
            this.cmbFiltroPrueba.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFiltroPrueba.Location = new System.Drawing.Point(235, 40);
            this.cmbFiltroPrueba.Name = "cmbFiltroPrueba";
            this.cmbFiltroPrueba.Size = new System.Drawing.Size(160, 23);
            this.cmbFiltroPrueba.TabIndex = 3;
            this.cmbFiltroPrueba.SelectedIndexChanged += new System.EventHandler(this.Filtro_SelectedIndexChanged);
            //
            // lblFiltroTipo
            //
            this.lblFiltroTipo.AutoSize = true;
            this.lblFiltroTipo.Location = new System.Drawing.Point(410, 20);
            this.lblFiltroTipo.Name = "lblFiltroTipo";
            this.lblFiltroTipo.Size = new System.Drawing.Size(34, 15);
            this.lblFiltroTipo.TabIndex = 4;
            this.lblFiltroTipo.Text = "Tipo:";
            //
            // cmbFiltroTipo
            //
            this.cmbFiltroTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            // El texto tiene que coincidir con lo que se guarda en Marca.Tipo
            this.cmbFiltroTipo.Items.AddRange(new object[] { "Todos", "oficial", "test" });
            this.cmbFiltroTipo.Location = new System.Drawing.Point(410, 40);
            this.cmbFiltroTipo.Name = "cmbFiltroTipo";
            this.cmbFiltroTipo.Size = new System.Drawing.Size(110, 23);
            this.cmbFiltroTipo.TabIndex = 5;
            this.cmbFiltroTipo.SelectedIndexChanged += new System.EventHandler(this.Filtro_SelectedIndexChanged);
            //
            // dgvMarcas
            //
            this.dgvMarcas.AllowUserToAddRows = false;
            this.dgvMarcas.AllowUserToDeleteRows = false;
            this.dgvMarcas.ReadOnly = true;
            this.dgvMarcas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMarcas.MultiSelect = false;
            this.dgvMarcas.AutoGenerateColumns = true;
            // Fill: las columnas se reparten todo el ancho de la grilla
            this.dgvMarcas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMarcas.Location = new System.Drawing.Point(20, 75);
            this.dgvMarcas.Name = "dgvMarcas";
            this.dgvMarcas.RowHeadersWidth = 25;
            this.dgvMarcas.Size = new System.Drawing.Size(760, 300);
            this.dgvMarcas.TabIndex = 6;
            //
            // btnNuevo
            //
            this.btnNuevo.Location = new System.Drawing.Point(20, 390);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(110, 35);
            this.btnNuevo.TabIndex = 7;
            this.btnNuevo.Text = "Nueva marca";
            this.btnNuevo.UseVisualStyleBackColor = true;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            //
            // btnModificar
            //
            this.btnModificar.Location = new System.Drawing.Point(140, 390);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(110, 35);
            this.btnModificar.TabIndex = 8;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.UseVisualStyleBackColor = true;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            //
            // btnVerDetalle
            //
            this.btnVerDetalle.Location = new System.Drawing.Point(260, 390);
            this.btnVerDetalle.Name = "btnVerDetalle";
            this.btnVerDetalle.Size = new System.Drawing.Size(110, 35);
            this.btnVerDetalle.TabIndex = 9;
            this.btnVerDetalle.Text = "Ver detalle";
            this.btnVerDetalle.UseVisualStyleBackColor = true;
            this.btnVerDetalle.Click += new System.EventHandler(this.btnVerDetalle_Click);
            //
            // btnEliminar
            //
            this.btnEliminar.Location = new System.Drawing.Point(380, 390);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(110, 35);
            this.btnEliminar.TabIndex = 10;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            //
            // btnActualizar
            //
            this.btnActualizar.Location = new System.Drawing.Point(500, 390);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(110, 35);
            this.btnActualizar.TabIndex = 11;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = true;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            //
            // btnVolver
            //
            this.btnVolver.Location = new System.Drawing.Point(670, 390);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(110, 35);
            this.btnVolver.TabIndex = 12;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            //
            // frmMarcas
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 445);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.btnActualizar);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnVerDetalle);
            this.Controls.Add(this.btnModificar);
            this.Controls.Add(this.btnNuevo);
            this.Controls.Add(this.dgvMarcas);
            this.Controls.Add(this.cmbFiltroTipo);
            this.Controls.Add(this.lblFiltroTipo);
            this.Controls.Add(this.cmbFiltroPrueba);
            this.Controls.Add(this.lblFiltroPrueba);
            this.Controls.Add(this.cmbFiltroAtleta);
            this.Controls.Add(this.lblFiltroAtleta);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMarcas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "NAT-SYS - Gestión de marcas";
            ((System.ComponentModel.ISupportInitialize)(this.dgvMarcas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblFiltroAtleta;
        private System.Windows.Forms.ComboBox cmbFiltroAtleta;
        private System.Windows.Forms.Label lblFiltroPrueba;
        private System.Windows.Forms.ComboBox cmbFiltroPrueba;
        private System.Windows.Forms.Label lblFiltroTipo;
        private System.Windows.Forms.ComboBox cmbFiltroTipo;
        private System.Windows.Forms.DataGridView dgvMarcas;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnVerDetalle;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnVolver;
    }
}