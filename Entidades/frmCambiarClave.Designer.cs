namespace NatSys.UI
{
    partial class frmCambiarClave
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
            this.lblClaveActual = new System.Windows.Forms.Label();
            this.txtClaveActual = new System.Windows.Forms.TextBox();
            this.lblClaveNueva = new System.Windows.Forms.Label();
            this.txtClaveNueva = new System.Windows.Forms.TextBox();
            this.lblConfirmarClave = new System.Windows.Forms.Label();
            this.txtConfirmarClave = new System.Windows.Forms.TextBox();
            this.chkMostrar = new System.Windows.Forms.CheckBox();
            this.lblRequisitos = new System.Windows.Forms.Label();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // lblClaveActual
            //
            this.lblClaveActual.AutoSize = true;
            this.lblClaveActual.Location = new System.Drawing.Point(20, 23);
            this.lblClaveActual.Name = "lblClaveActual";
            this.lblClaveActual.Size = new System.Drawing.Size(105, 15);
            this.lblClaveActual.TabIndex = 0;
            this.lblClaveActual.Text = "Contraseña actual:";
            //
            // txtClaveActual
            //
            this.txtClaveActual.Location = new System.Drawing.Point(160, 20);
            this.txtClaveActual.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.txtClaveActual.Name = "txtClaveActual";
            this.txtClaveActual.Size = new System.Drawing.Size(190, 23);
            this.txtClaveActual.TabIndex = 1;
            this.txtClaveActual.UseSystemPasswordChar = true;
            //
            // lblClaveNueva
            //
            this.lblClaveNueva.AutoSize = true;
            this.lblClaveNueva.Location = new System.Drawing.Point(20, 58);
            this.lblClaveNueva.Name = "lblClaveNueva";
            this.lblClaveNueva.Size = new System.Drawing.Size(104, 15);
            this.lblClaveNueva.TabIndex = 2;
            this.lblClaveNueva.Text = "Contraseña nueva:";
            //
            // txtClaveNueva
            //
            this.txtClaveNueva.Location = new System.Drawing.Point(160, 55);
            this.txtClaveNueva.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.txtClaveNueva.Name = "txtClaveNueva";
            this.txtClaveNueva.Size = new System.Drawing.Size(190, 23);
            this.txtClaveNueva.TabIndex = 3;
            this.txtClaveNueva.UseSystemPasswordChar = true;
            //
            // lblConfirmarClave
            //
            this.lblConfirmarClave.AutoSize = true;
            this.lblConfirmarClave.Location = new System.Drawing.Point(20, 93);
            this.lblConfirmarClave.Name = "lblConfirmarClave";
            this.lblConfirmarClave.Size = new System.Drawing.Size(114, 15);
            this.lblConfirmarClave.TabIndex = 4;
            this.lblConfirmarClave.Text = "Repetir la nueva:";
            //
            // txtConfirmarClave
            //
            this.txtConfirmarClave.Location = new System.Drawing.Point(160, 90);
            this.txtConfirmarClave.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.txtConfirmarClave.Name = "txtConfirmarClave";
            this.txtConfirmarClave.Size = new System.Drawing.Size(190, 23);
            this.txtConfirmarClave.TabIndex = 5;
            this.txtConfirmarClave.UseSystemPasswordChar = true;
            //
            // chkMostrar
            //
            this.chkMostrar.AutoSize = true;
            this.chkMostrar.Location = new System.Drawing.Point(160, 122);
            this.chkMostrar.Name = "chkMostrar";
            this.chkMostrar.Size = new System.Drawing.Size(140, 19);
            this.chkMostrar.TabIndex = 6;
            this.chkMostrar.Text = "Mostrar contraseñas";
            this.chkMostrar.UseVisualStyleBackColor = true;
            this.chkMostrar.CheckedChanged += new System.EventHandler(this.chkMostrar_CheckedChanged);
            //
            // lblRequisitos
            //
            this.lblRequisitos.AutoSize = true;
            this.lblRequisitos.ForeColor = System.Drawing.Color.DimGray;
            this.lblRequisitos.Location = new System.Drawing.Point(20, 152);
            this.lblRequisitos.Name = "lblRequisitos";
            this.lblRequisitos.Size = new System.Drawing.Size(270, 15);
            this.lblRequisitos.TabIndex = 7;
            this.lblRequisitos.Text = "Mínimo 8 caracteres, combinando letras y números.";
            //
            // btnGuardar
            //
            this.btnGuardar.Location = new System.Drawing.Point(70, 185);
            this.btnGuardar.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(110, 32);
            this.btnGuardar.TabIndex = 8;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            //
            // btnCancelar
            //
            this.btnCancelar.Location = new System.Drawing.Point(190, 185);
            this.btnCancelar.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(110, 32);
            this.btnCancelar.TabIndex = 9;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            //
            // frmCambiarClave
            //
            this.AcceptButton = this.btnGuardar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(370, 235);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.lblRequisitos);
            this.Controls.Add(this.chkMostrar);
            this.Controls.Add(this.txtConfirmarClave);
            this.Controls.Add(this.lblConfirmarClave);
            this.Controls.Add(this.txtClaveNueva);
            this.Controls.Add(this.lblClaveNueva);
            this.Controls.Add(this.txtClaveActual);
            this.Controls.Add(this.lblClaveActual);
            this.MinimizeBox = false;
            this.Name = "frmCambiarClave";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "NAT-SYS - Cambiar contraseña";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblClaveActual;
        private System.Windows.Forms.TextBox txtClaveActual;
        private System.Windows.Forms.Label lblClaveNueva;
        private System.Windows.Forms.TextBox txtClaveNueva;
        private System.Windows.Forms.Label lblConfirmarClave;
        private System.Windows.Forms.TextBox txtConfirmarClave;
        private System.Windows.Forms.CheckBox chkMostrar;
        private System.Windows.Forms.Label lblRequisitos;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
    }
}
