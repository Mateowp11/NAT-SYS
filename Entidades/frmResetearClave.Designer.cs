namespace NatSys.UI
{
    partial class frmResetearClave
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
            this.lblClaveNueva = new System.Windows.Forms.Label();
            this.txtClaveNueva = new System.Windows.Forms.TextBox();
            this.lblConfirmarClave = new System.Windows.Forms.Label();
            this.txtConfirmarClave = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // lblClaveNueva
            //
            this.lblClaveNueva.AutoSize = true;
            this.lblClaveNueva.Location = new System.Drawing.Point(20, 25);
            this.lblClaveNueva.Name = "lblClaveNueva";
            this.lblClaveNueva.Size = new System.Drawing.Size(85, 15);
            this.lblClaveNueva.TabIndex = 0;
            this.lblClaveNueva.Text = "Nueva clave:";
            //
            // txtClaveNueva
            //
            this.txtClaveNueva.Location = new System.Drawing.Point(130, 22);
            this.txtClaveNueva.Name = "txtClaveNueva";
            this.txtClaveNueva.Size = new System.Drawing.Size(170, 23);
            this.txtClaveNueva.TabIndex = 1;
            this.txtClaveNueva.UseSystemPasswordChar = true;
            //
            // lblConfirmarClave
            //
            this.lblConfirmarClave.AutoSize = true;
            this.lblConfirmarClave.Location = new System.Drawing.Point(20, 60);
            this.lblConfirmarClave.Name = "lblConfirmarClave";
            this.lblConfirmarClave.Size = new System.Drawing.Size(97, 15);
            this.lblConfirmarClave.TabIndex = 2;
            this.lblConfirmarClave.Text = "Confirmar clave:";
            //
            // txtConfirmarClave
            //
            this.txtConfirmarClave.Location = new System.Drawing.Point(130, 57);
            this.txtConfirmarClave.Name = "txtConfirmarClave";
            this.txtConfirmarClave.Size = new System.Drawing.Size(170, 23);
            this.txtConfirmarClave.TabIndex = 3;
            this.txtConfirmarClave.UseSystemPasswordChar = true;
            //
            // btnGuardar
            //
            this.btnGuardar.Location = new System.Drawing.Point(40, 100);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(110, 32);
            this.btnGuardar.TabIndex = 4;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            //
            // btnCancelar
            //
            this.btnCancelar.Location = new System.Drawing.Point(160, 100);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(110, 32);
            this.btnCancelar.TabIndex = 5;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            //
            // frmResetearClave
            //
            this.AcceptButton = this.btnGuardar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 150);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.txtConfirmarClave);
            this.Controls.Add(this.lblConfirmarClave);
            this.Controls.Add(this.txtClaveNueva);
            this.Controls.Add(this.lblClaveNueva);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmResetearClave";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Resetear contraseña";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblClaveNueva;
        private System.Windows.Forms.TextBox txtClaveNueva;
        private System.Windows.Forms.Label lblConfirmarClave;
        private System.Windows.Forms.TextBox txtConfirmarClave;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
    }
}