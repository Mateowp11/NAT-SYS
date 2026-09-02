namespace NatSys.UI
{
    partial class frmRecuperarClave
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
            this.lblUsuario = new System.Windows.Forms.Label();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.btnBuscarPregunta = new System.Windows.Forms.Button();
            this.lblPregunta = new System.Windows.Forms.Label();
            this.lblRespuesta = new System.Windows.Forms.Label();
            this.txtRespuesta = new System.Windows.Forms.TextBox();
            this.lblClaveNueva = new System.Windows.Forms.Label();
            this.txtClaveNueva = new System.Windows.Forms.TextBox();
            this.lblConfirmarClave = new System.Windows.Forms.Label();
            this.txtConfirmarClave = new System.Windows.Forms.TextBox();
            this.btnRestablecer = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // lblTitulo
            //
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(30, 20);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(220, 32);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Recuperar contraseña";
            //
            // lblUsuario
            //
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(30, 70);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(58, 15);
            this.lblUsuario.TabIndex = 1;
            this.lblUsuario.Text = "Usuario:";
            //
            // txtUsuario
            //
            this.txtUsuario.Location = new System.Drawing.Point(140, 67);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(180, 23);
            this.txtUsuario.TabIndex = 2;
            //
            // btnBuscarPregunta
            //
            this.btnBuscarPregunta.Location = new System.Drawing.Point(140, 100);
            this.btnBuscarPregunta.Name = "btnBuscarPregunta";
            this.btnBuscarPregunta.Size = new System.Drawing.Size(180, 30);
            this.btnBuscarPregunta.TabIndex = 3;
            this.btnBuscarPregunta.Text = "Buscar pregunta";
            this.btnBuscarPregunta.UseVisualStyleBackColor = true;
            this.btnBuscarPregunta.Click += new System.EventHandler(this.btnBuscarPregunta_Click);
            //
            // lblPregunta
            //
            this.lblPregunta.Location = new System.Drawing.Point(30, 145);
            this.lblPregunta.Name = "lblPregunta";
            this.lblPregunta.Size = new System.Drawing.Size(290, 40);
            this.lblPregunta.TabIndex = 4;
            this.lblPregunta.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblPregunta.Visible = false;
            //
            // lblRespuesta
            //
            this.lblRespuesta.AutoSize = true;
            this.lblRespuesta.Location = new System.Drawing.Point(30, 195);
            this.lblRespuesta.Name = "lblRespuesta";
            this.lblRespuesta.Size = new System.Drawing.Size(72, 15);
            this.lblRespuesta.TabIndex = 5;
            this.lblRespuesta.Text = "Respuesta:";
            this.lblRespuesta.Visible = false;
            //
            // txtRespuesta
            //
            this.txtRespuesta.Location = new System.Drawing.Point(140, 192);
            this.txtRespuesta.Name = "txtRespuesta";
            this.txtRespuesta.Size = new System.Drawing.Size(180, 23);
            this.txtRespuesta.TabIndex = 6;
            this.txtRespuesta.Visible = false;
            //
            // lblClaveNueva
            //
            this.lblClaveNueva.AutoSize = true;
            this.lblClaveNueva.Location = new System.Drawing.Point(30, 230);
            this.lblClaveNueva.Name = "lblClaveNueva";
            this.lblClaveNueva.Size = new System.Drawing.Size(85, 15);
            this.lblClaveNueva.TabIndex = 7;
            this.lblClaveNueva.Text = "Nueva clave:";
            this.lblClaveNueva.Visible = false;
            //
            // txtClaveNueva
            //
            this.txtClaveNueva.Location = new System.Drawing.Point(140, 227);
            this.txtClaveNueva.Name = "txtClaveNueva";
            this.txtClaveNueva.Size = new System.Drawing.Size(180, 23);
            this.txtClaveNueva.TabIndex = 8;
            this.txtClaveNueva.UseSystemPasswordChar = true;
            this.txtClaveNueva.Visible = false;
            //
            // lblConfirmarClave
            //
            this.lblConfirmarClave.AutoSize = true;
            this.lblConfirmarClave.Location = new System.Drawing.Point(30, 265);
            this.lblConfirmarClave.Name = "lblConfirmarClave";
            this.lblConfirmarClave.Size = new System.Drawing.Size(97, 15);
            this.lblConfirmarClave.TabIndex = 9;
            this.lblConfirmarClave.Text = "Confirmar clave:";
            this.lblConfirmarClave.Visible = false;
            //
            // txtConfirmarClave
            //
            this.txtConfirmarClave.Location = new System.Drawing.Point(140, 262);
            this.txtConfirmarClave.Name = "txtConfirmarClave";
            this.txtConfirmarClave.Size = new System.Drawing.Size(180, 23);
            this.txtConfirmarClave.TabIndex = 10;
            this.txtConfirmarClave.UseSystemPasswordChar = true;
            this.txtConfirmarClave.Visible = false;
            //
            // btnRestablecer
            //
            this.btnRestablecer.Location = new System.Drawing.Point(60, 305);
            this.btnRestablecer.Name = "btnRestablecer";
            this.btnRestablecer.Size = new System.Drawing.Size(110, 32);
            this.btnRestablecer.TabIndex = 11;
            this.btnRestablecer.Text = "Restablecer";
            this.btnRestablecer.UseVisualStyleBackColor = true;
            this.btnRestablecer.Visible = false;
            this.btnRestablecer.Click += new System.EventHandler(this.btnRestablecer_Click);
            //
            // btnCancelar
            //
            this.btnCancelar.Location = new System.Drawing.Point(180, 305);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(110, 32);
            this.btnCancelar.TabIndex = 12;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            //
            // frmRecuperarClave
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 360);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnRestablecer);
            this.Controls.Add(this.txtConfirmarClave);
            this.Controls.Add(this.lblConfirmarClave);
            this.Controls.Add(this.txtClaveNueva);
            this.Controls.Add(this.lblClaveNueva);
            this.Controls.Add(this.txtRespuesta);
            this.Controls.Add(this.lblRespuesta);
            this.Controls.Add(this.lblPregunta);
            this.Controls.Add(this.btnBuscarPregunta);
            this.Controls.Add(this.txtUsuario);
            this.Controls.Add(this.lblUsuario);
            this.Controls.Add(this.lblTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRecuperarClave";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "NAT-SYS - Recuperar contraseña";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Button btnBuscarPregunta;
        private System.Windows.Forms.Label lblPregunta;
        private System.Windows.Forms.Label lblRespuesta;
        private System.Windows.Forms.TextBox txtRespuesta;
        private System.Windows.Forms.Label lblClaveNueva;
        private System.Windows.Forms.TextBox txtClaveNueva;
        private System.Windows.Forms.Label lblConfirmarClave;
        private System.Windows.Forms.TextBox txtConfirmarClave;
        private System.Windows.Forms.Button btnRestablecer;
        private System.Windows.Forms.Button btnCancelar;
    }
}