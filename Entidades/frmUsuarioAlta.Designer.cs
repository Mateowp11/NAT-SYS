namespace NatSys.UI
{
    partial class frmUsuarioAlta
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
            this.lblApellido = new System.Windows.Forms.Label();
            this.txtApellido = new System.Windows.Forms.TextBox();
            this.lblEspecialidad = new System.Windows.Forms.Label();
            this.txtEspecialidad = new System.Windows.Forms.TextBox();
            this.lblNombreUsuario = new System.Windows.Forms.Label();
            this.txtNombreUsuario = new System.Windows.Forms.TextBox();
            this.lblClaveInicial = new System.Windows.Forms.Label();
            this.txtClaveInicial = new System.Windows.Forms.TextBox();
            this.lblPregunta = new System.Windows.Forms.Label();
            this.cmbPregunta = new System.Windows.Forms.ComboBox();
            this.lblRespuesta = new System.Windows.Forms.Label();
            this.txtRespuesta = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // lblNombre
            //
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(20, 20);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(55, 15);
            this.lblNombre.TabIndex = 0;
            this.lblNombre.Text = "Nombre:";
            //
            // txtNombre
            //
            this.txtNombre.Location = new System.Drawing.Point(150, 17);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(190, 23);
            this.txtNombre.TabIndex = 1;
            //
            // lblApellido
            //
            this.lblApellido.AutoSize = true;
            this.lblApellido.Location = new System.Drawing.Point(20, 53);
            this.lblApellido.Name = "lblApellido";
            this.lblApellido.Size = new System.Drawing.Size(58, 15);
            this.lblApellido.TabIndex = 2;
            this.lblApellido.Text = "Apellido:";
            //
            // txtApellido
            //
            this.txtApellido.Location = new System.Drawing.Point(150, 50);
            this.txtApellido.Name = "txtApellido";
            this.txtApellido.Size = new System.Drawing.Size(190, 23);
            this.txtApellido.TabIndex = 3;
            //
            // lblEspecialidad
            //
            this.lblEspecialidad.AutoSize = true;
            this.lblEspecialidad.Location = new System.Drawing.Point(20, 86);
            this.lblEspecialidad.Name = "lblEspecialidad";
            this.lblEspecialidad.Size = new System.Drawing.Size(80, 15);
            this.lblEspecialidad.TabIndex = 4;
            this.lblEspecialidad.Text = "Especialidad:";
            //
            // txtEspecialidad
            //
            this.txtEspecialidad.Location = new System.Drawing.Point(150, 83);
            this.txtEspecialidad.Name = "txtEspecialidad";
            this.txtEspecialidad.Size = new System.Drawing.Size(190, 23);
            this.txtEspecialidad.TabIndex = 5;
            //
            // lblNombreUsuario
            //
            this.lblNombreUsuario.AutoSize = true;
            this.lblNombreUsuario.Location = new System.Drawing.Point(20, 125);
            this.lblNombreUsuario.Name = "lblNombreUsuario";
            this.lblNombreUsuario.Size = new System.Drawing.Size(105, 15);
            this.lblNombreUsuario.TabIndex = 6;
            this.lblNombreUsuario.Text = "Nombre de usuario:";
            //
            // txtNombreUsuario
            //
            this.txtNombreUsuario.Location = new System.Drawing.Point(150, 122);
            this.txtNombreUsuario.Name = "txtNombreUsuario";
            this.txtNombreUsuario.Size = new System.Drawing.Size(190, 23);
            this.txtNombreUsuario.TabIndex = 7;
            //
            // lblClaveInicial
            //
            this.lblClaveInicial.AutoSize = true;
            this.lblClaveInicial.Location = new System.Drawing.Point(20, 158);
            this.lblClaveInicial.Name = "lblClaveInicial";
            this.lblClaveInicial.Size = new System.Drawing.Size(97, 15);
            this.lblClaveInicial.TabIndex = 8;
            this.lblClaveInicial.Text = "Clave inicial:";
            //
            // txtClaveInicial
            //
            this.txtClaveInicial.Location = new System.Drawing.Point(150, 155);
            this.txtClaveInicial.Name = "txtClaveInicial";
            this.txtClaveInicial.Size = new System.Drawing.Size(190, 23);
            this.txtClaveInicial.TabIndex = 9;
            this.txtClaveInicial.UseSystemPasswordChar = true;
            //
            // lblPregunta
            //
            this.lblPregunta.AutoSize = true;
            this.lblPregunta.Location = new System.Drawing.Point(20, 191);
            this.lblPregunta.Name = "lblPregunta";
            this.lblPregunta.Size = new System.Drawing.Size(115, 15);
            this.lblPregunta.TabIndex = 10;
            this.lblPregunta.Text = "Pregunta seguridad:";
            //
            // cmbPregunta
            //
            this.cmbPregunta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPregunta.Location = new System.Drawing.Point(150, 188);
            this.cmbPregunta.Name = "cmbPregunta";
            this.cmbPregunta.Size = new System.Drawing.Size(190, 23);
            this.cmbPregunta.TabIndex = 11;
            //
            // lblRespuesta
            //
            this.lblRespuesta.AutoSize = true;
            this.lblRespuesta.Location = new System.Drawing.Point(20, 224);
            this.lblRespuesta.Name = "lblRespuesta";
            this.lblRespuesta.Size = new System.Drawing.Size(72, 15);
            this.lblRespuesta.TabIndex = 12;
            this.lblRespuesta.Text = "Respuesta:";
            //
            // txtRespuesta
            //
            this.txtRespuesta.Location = new System.Drawing.Point(150, 221);
            this.txtRespuesta.Name = "txtRespuesta";
            this.txtRespuesta.Size = new System.Drawing.Size(190, 23);
            this.txtRespuesta.TabIndex = 13;
            //
            // btnGuardar
            //
            this.btnGuardar.Location = new System.Drawing.Point(80, 265);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(110, 32);
            this.btnGuardar.TabIndex = 14;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            //
            // btnCancelar
            //
            this.btnCancelar.Location = new System.Drawing.Point(200, 265);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(110, 32);
            this.btnCancelar.TabIndex = 15;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            //
            // frmUsuarioAlta
            //
            this.AcceptButton = this.btnGuardar;
            this.CancelButton = this.btnCancelar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 315);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.txtRespuesta);
            this.Controls.Add(this.lblRespuesta);
            this.Controls.Add(this.cmbPregunta);
            this.Controls.Add(this.lblPregunta);
            this.Controls.Add(this.txtClaveInicial);
            this.Controls.Add(this.lblClaveInicial);
            this.Controls.Add(this.txtNombreUsuario);
            this.Controls.Add(this.lblNombreUsuario);
            this.Controls.Add(this.txtEspecialidad);
            this.Controls.Add(this.lblEspecialidad);
            this.Controls.Add(this.txtApellido);
            this.Controls.Add(this.lblApellido);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.lblNombre);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmUsuarioAlta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Nuevo usuario (entrenador)";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblApellido;
        private System.Windows.Forms.TextBox txtApellido;
        private System.Windows.Forms.Label lblEspecialidad;
        private System.Windows.Forms.TextBox txtEspecialidad;
        private System.Windows.Forms.Label lblNombreUsuario;
        private System.Windows.Forms.TextBox txtNombreUsuario;
        private System.Windows.Forms.Label lblClaveInicial;
        private System.Windows.Forms.TextBox txtClaveInicial;
        private System.Windows.Forms.Label lblPregunta;
        private System.Windows.Forms.ComboBox cmbPregunta;
        private System.Windows.Forms.Label lblRespuesta;
        private System.Windows.Forms.TextBox txtRespuesta;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
    }
}