namespace NatSys.UI
{
    partial class frmGrupos
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
            this.dgvGrupos = new System.Windows.Forms.DataGridView();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.btnAsignarPermisos = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.btnVolver = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGrupos)).BeginInit();
            this.SuspendLayout();
            //
            // dgvGrupos
            //
            this.dgvGrupos.AllowUserToAddRows = false;
            this.dgvGrupos.AllowUserToDeleteRows = false;
            this.dgvGrupos.ReadOnly = true;
            this.dgvGrupos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGrupos.MultiSelect = false;
            this.dgvGrupos.AutoGenerateColumns = true;
            this.dgvGrupos.Location = new System.Drawing.Point(20, 20);
            this.dgvGrupos.Name = "dgvGrupos";
            this.dgvGrupos.RowHeadersWidth = 25;
            this.dgvGrupos.Size = new System.Drawing.Size(650, 300);
            this.dgvGrupos.TabIndex = 0;
            //
            // btnNuevo
            //
            this.btnNuevo.Location = new System.Drawing.Point(20, 335);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(110, 35);
            this.btnNuevo.TabIndex = 1;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = true;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            //
            // btnAsignarPermisos
            //
            this.btnAsignarPermisos.Location = new System.Drawing.Point(140, 335);
            this.btnAsignarPermisos.Name = "btnAsignarPermisos";
            this.btnAsignarPermisos.Size = new System.Drawing.Size(130, 35);
            this.btnAsignarPermisos.TabIndex = 2;
            this.btnAsignarPermisos.Text = "Asignar permisos";
            this.btnAsignarPermisos.UseVisualStyleBackColor = true;
            this.btnAsignarPermisos.Click += new System.EventHandler(this.btnAsignarPermisos_Click);
            //
            // btnEliminar
            //
            this.btnEliminar.Location = new System.Drawing.Point(280, 335);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(110, 35);
            this.btnEliminar.TabIndex = 3;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            //
            // btnActualizar
            //
            this.btnActualizar.Location = new System.Drawing.Point(400, 335);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(110, 35);
            this.btnActualizar.TabIndex = 4;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = true;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            //
            // btnVolver
            //
            this.btnVolver.Location = new System.Drawing.Point(560, 335);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(110, 35);
            this.btnVolver.TabIndex = 5;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            //
            // frmGrupos
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 390);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.btnActualizar);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnAsignarPermisos);
            this.Controls.Add(this.btnNuevo);
            this.Controls.Add(this.dgvGrupos);
            this.MaximizeBox = false;
            this.Name = "frmGrupos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NAT-SYS - Gestión de grupos";
            ((System.ComponentModel.ISupportInitialize)(this.dgvGrupos)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.DataGridView dgvGrupos;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button btnAsignarPermisos;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnVolver;
    }
}