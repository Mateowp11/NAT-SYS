namespace NatSys.UI
{
    partial class frmLogin
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
            this.lblClave = new System.Windows.Forms.Label();
            this.txtClave = new System.Windows.Forms.TextBox();
            this.btnIngresar = new System.Windows.Forms.Button();
            this.btnOlvideClave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // lblTitulo
            //
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(90, 30);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(180, 41);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Iniciar sesión";
            //
            // lblUsuario
            //
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(50, 110);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(58, 15);
            this.lblUsuario.TabIndex = 1;
            this.lblUsuario.Text = "Usuario:";
            //
            // txtUsuario
            //
            this.txtUsuario.Location = new System.Drawing.Point(140, 107);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(180, 23);
            this.txtUsuario.TabIndex = 2;
            //
            // lblClave
            //
            this.lblClave.AutoSize = true;
            this.lblClave.Location = new System.Drawing.Point(50, 150);
            this.lblClave.Name = "lblClave";
            this.lblClave.Size = new System.Drawing.Size(70, 15);
            this.lblClave.TabIndex = 3;
            this.lblClave.Text = "Contraseña:";
            //
            // txtClave
            //
            this.txtClave.Location = new System.Drawing.Point(140, 147);
            this.txtClave.Name = "txtClave";
            this.txtClave.Size = new System.Drawing.Size(180, 23);
            this.txtClave.TabIndex = 4;
            this.txtClave.UseSystemPasswordChar = true;
            //
            // btnIngresar
            //
            this.btnIngresar.Location = new System.Drawing.Point(140, 190);
            this.btnIngresar.Name = "btnIngresar";
            this.btnIngresar.Size = new System.Drawing.Size(90, 30);
            this.btnIngresar.TabIndex = 5;
            this.btnIngresar.Text = "Ingresar";
            this.btnIngresar.UseVisualStyleBackColor = true;
            this.btnIngresar.Click += new System.EventHandler(this.btnIngresar_Click);
            //
            // btnOlvideClave
            //
            this.btnOlvideClave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOlvideClave.FlatAppearance.BorderSize = 0;
            this.btnOlvideClave.ForeColor = System.Drawing.Color.SteelBlue;
            this.btnOlvideClave.Location = new System.Drawing.Point(50, 230);
            this.btnOlvideClave.Name = "btnOlvideClave";
            this.btnOlvideClave.Size = new System.Drawing.Size(180, 25);
            this.btnOlvideClave.TabIndex = 6;
            this.btnOlvideClave.Text = "Olvidé mi contraseña";
            this.btnOlvideClave.UseVisualStyleBackColor = true;
            this.btnOlvideClave.Click += new System.EventHandler(this.btnOlvideClave_Click);
            //
            // FrmLogin
            //
            this.AcceptButton = this.btnIngresar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 290);
            this.Controls.Add(this.btnOlvideClave);
            this.Controls.Add(this.btnIngresar);
            this.Controls.Add(this.txtClave);
            this.Controls.Add(this.lblClave);
            this.Controls.Add(this.txtUsuario);
            this.Controls.Add(this.lblUsuario);
            this.Controls.Add(this.lblTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NAT-SYS - Iniciar sesión";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Label lblClave;
        private System.Windows.Forms.TextBox txtClave;
        private System.Windows.Forms.Button btnIngresar;
        private System.Windows.Forms.Button btnOlvideClave;
    }
}