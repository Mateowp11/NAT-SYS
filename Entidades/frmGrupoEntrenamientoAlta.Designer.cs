namespace NatSys.UI
{
    partial class frmGrupoEntrenamientoAlta
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
            this.lblEntrenador = new System.Windows.Forms.Label();
            this.cmbEntrenador = new System.Windows.Forms.ComboBox();
            this.lblAtletas = new System.Windows.Forms.Label();
            this.clbAtletas = new System.Windows.Forms.CheckedListBox();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // lblNombre
            //
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(20, 23);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(107, 15);
            this.lblNombre.TabIndex = 0;
            this.lblNombre.Text = "Nombre del grupo:";
            //
            // txtNombre
            //
            this.txtNombre.Location = new System.Drawing.Point(150, 20);
            this.txtNombre.MaxLength = 60;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(250, 23);
            this.txtNombre.TabIndex = 1;
            //
            // lblEntrenador
            //
            this.lblEntrenador.AutoSize = true;
            this.lblEntrenador.Location = new System.Drawing.Point(20, 58);
            this.lblEntrenador.Name = "lblEntrenador";
            this.lblEntrenador.Size = new System.Drawing.Size(69, 15);
            this.lblEntrenador.TabIndex = 2;
            this.lblEntrenador.Text = "Entrenador:";
            //
            // cmbEntrenador
            //
            this.cmbEntrenador.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEntrenador.FormattingEnabled = true;
            this.cmbEntrenador.Location = new System.Drawing.Point(150, 55);
            this.cmbEntrenador.Name = "cmbEntrenador";
            this.cmbEntrenador.Size = new System.Drawing.Size(250, 23);
            this.cmbEntrenador.TabIndex = 3;
            //
            // lblAtletas
            //
            this.lblAtletas.AutoSize = true;
            this.lblAtletas.Location = new System.Drawing.Point(20, 95);
            this.lblAtletas.Name = "lblAtletas";
            this.lblAtletas.Size = new System.Drawing.Size(244, 15);
            this.lblAtletas.TabIndex = 4;
            this.lblAtletas.Text = "Atletas del grupo (tildá los que lo integran):";
            //
            // clbAtletas
            //
            this.clbAtletas.CheckOnClick = true;
            this.clbAtletas.FormattingEnabled = true;
            this.clbAtletas.Location = new System.Drawing.Point(20, 118);
            this.clbAtletas.Name = "clbAtletas";
            this.clbAtletas.Size = new System.Drawing.Size(380, 256);
            this.clbAtletas.TabIndex = 5;
            this.clbAtletas.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbAtletas_ItemCheck);
            //
            // lblCantidad
            //
            this.lblCantidad.AutoSize = true;
            this.lblCantidad.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblCantidad.Location = new System.Drawing.Point(20, 382);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Size = new System.Drawing.Size(140, 15);
            this.lblCantidad.TabIndex = 6;
            this.lblCantidad.Text = "0 atletas seleccionados";
            //
            // btnGuardar
            //
            this.btnGuardar.Location = new System.Drawing.Point(180, 410);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(120, 35);
            this.btnGuardar.TabIndex = 7;
            this.btnGuardar.Text = "Crear grupo";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            //
            // btnCancelar
            //
            this.btnCancelar.Location = new System.Drawing.Point(310, 410);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(90, 35);
            this.btnCancelar.TabIndex = 8;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            //
            // frmGrupoEntrenamientoAlta
            //
            this.AcceptButton = this.btnGuardar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(420, 465);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.lblCantidad);
            this.Controls.Add(this.clbAtletas);
            this.Controls.Add(this.lblAtletas);
            this.Controls.Add(this.cmbEntrenador);
            this.Controls.Add(this.lblEntrenador);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.lblNombre);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmGrupoEntrenamientoAlta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Agregar grupo";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblEntrenador;
        private System.Windows.Forms.ComboBox cmbEntrenador;
        private System.Windows.Forms.Label lblAtletas;
        private System.Windows.Forms.CheckedListBox clbAtletas;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
    }
}
