namespace NatSys.UI
{
    partial class frmPlanesAtleta
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
            this.dgvPlanes = new System.Windows.Forms.DataGridView();
            this.lblSinPlanes = new System.Windows.Forms.Label();
            this.lblTramos = new System.Windows.Forms.Label();
            this.dgvTramos = new System.Windows.Forms.DataGridView();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnVolver = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlanes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTramos)).BeginInit();
            this.SuspendLayout();
            //
            // lblTitulo
            //
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(20, 15);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(180, 21);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Planes guardados";
            //
            // dgvPlanes
            //
            this.dgvPlanes.AllowUserToAddRows = false;
            this.dgvPlanes.AllowUserToDeleteRows = false;
            this.dgvPlanes.ReadOnly = true;
            this.dgvPlanes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPlanes.MultiSelect = false;
            this.dgvPlanes.AutoGenerateColumns = true;
            this.dgvPlanes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPlanes.Location = new System.Drawing.Point(20, 50);
            this.dgvPlanes.Name = "dgvPlanes";
            this.dgvPlanes.RowHeadersWidth = 25;
            this.dgvPlanes.Size = new System.Drawing.Size(700, 200);
            this.dgvPlanes.TabIndex = 1;
            this.dgvPlanes.SelectionChanged += new System.EventHandler(this.dgvPlanes_SelectionChanged);
            //
            // lblSinPlanes
            //
            this.lblSinPlanes.AutoSize = true;
            this.lblSinPlanes.ForeColor = System.Drawing.Color.DimGray;
            this.lblSinPlanes.Location = new System.Drawing.Point(30, 90);
            this.lblSinPlanes.Name = "lblSinPlanes";
            this.lblSinPlanes.Size = new System.Drawing.Size(260, 15);
            this.lblSinPlanes.TabIndex = 2;
            this.lblSinPlanes.Text = "Este atleta no tiene planes de pasajes guardados.";
            this.lblSinPlanes.Visible = false;
            //
            // lblTramos
            //
            this.lblTramos.AutoSize = true;
            this.lblTramos.Location = new System.Drawing.Point(20, 265);
            this.lblTramos.Name = "lblTramos";
            this.lblTramos.Size = new System.Drawing.Size(170, 15);
            this.lblTramos.TabIndex = 3;
            this.lblTramos.Text = "Tramos del plan seleccionado:";
            //
            // dgvTramos
            //
            this.dgvTramos.AllowUserToAddRows = false;
            this.dgvTramos.AllowUserToDeleteRows = false;
            this.dgvTramos.ReadOnly = true;
            this.dgvTramos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTramos.MultiSelect = false;
            this.dgvTramos.AutoGenerateColumns = true;
            this.dgvTramos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTramos.Location = new System.Drawing.Point(20, 290);
            this.dgvTramos.Name = "dgvTramos";
            this.dgvTramos.RowHeadersVisible = false;
            this.dgvTramos.Size = new System.Drawing.Size(700, 180);
            this.dgvTramos.TabIndex = 4;
            //
            // btnEliminar
            //
            this.btnEliminar.Location = new System.Drawing.Point(20, 485);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(130, 35);
            this.btnEliminar.TabIndex = 5;
            this.btnEliminar.Text = "Eliminar plan";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            //
            // btnVolver
            //
            this.btnVolver.Location = new System.Drawing.Point(620, 485);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(100, 35);
            this.btnVolver.TabIndex = 6;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            //
            // frmPlanesAtleta
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 540);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.dgvTramos);
            this.Controls.Add(this.lblTramos);
            this.Controls.Add(this.lblSinPlanes);
            this.Controls.Add(this.dgvPlanes);
            this.Controls.Add(this.lblTitulo);
            this.MaximizeBox = false;
            this.Name = "frmPlanesAtleta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NAT-SYS - Planes de pasajes guardados";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlanes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTramos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.DataGridView dgvPlanes;
        private System.Windows.Forms.Label lblSinPlanes;
        private System.Windows.Forms.Label lblTramos;
        private System.Windows.Forms.DataGridView dgvTramos;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnVolver;
    }
}