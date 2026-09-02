namespace NatSys.UI
{
    partial class frmMenuPrincipal
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
            this.lblBienvenida = new System.Windows.Forms.Label();
            this.lblElijaOpcion = new System.Windows.Forms.Label();
            this.btnGestorTorneos = new System.Windows.Forms.Button();
            this.btnGestorAtletas = new System.Windows.Forms.Button();
            this.btnGestorMarcas = new System.Windows.Forms.Button();
            this.btnCalcularPasajes = new System.Windows.Forms.Button();
            this.btnReportes = new System.Windows.Forms.Button();
            this.btnGestorUsuarios = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // lblTitulo
            //
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(30, 25);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(260, 45);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Bienvenido a NAT-SYS";
            //
            // lblBienvenida
            //
            this.lblBienvenida.AutoSize = true;
            this.lblBienvenida.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblBienvenida.Location = new System.Drawing.Point(34, 75);
            this.lblBienvenida.Name = "lblBienvenida";
            this.lblBienvenida.Size = new System.Drawing.Size(120, 20);
            this.lblBienvenida.TabIndex = 1;
            this.lblBienvenida.Text = "Bienvenido/a";
            //
            // lblElijaOpcion
            //
            this.lblElijaOpcion.AutoSize = true;
            this.lblElijaOpcion.Location = new System.Drawing.Point(34, 110);
            this.lblElijaOpcion.Name = "lblElijaOpcion";
            this.lblElijaOpcion.Size = new System.Drawing.Size(90, 15);
            this.lblElijaOpcion.TabIndex = 2;
            this.lblElijaOpcion.Text = "Elija una opción:";
            //
            // btnGestorTorneos
            //
            this.btnGestorTorneos.Location = new System.Drawing.Point(30, 150);
            this.btnGestorTorneos.Name = "btnGestorTorneos";
            this.btnGestorTorneos.Size = new System.Drawing.Size(130, 70);
            this.btnGestorTorneos.TabIndex = 3;
            this.btnGestorTorneos.Text = "Gestor de torneos";
            this.btnGestorTorneos.UseVisualStyleBackColor = true;
            this.btnGestorTorneos.Click += new System.EventHandler(this.btnGestorTorneos_Click);
            //
            // btnGestorAtletas
            //
            this.btnGestorAtletas.Location = new System.Drawing.Point(170, 150);
            this.btnGestorAtletas.Name = "btnGestorAtletas";
            this.btnGestorAtletas.Size = new System.Drawing.Size(130, 70);
            this.btnGestorAtletas.TabIndex = 4;
            this.btnGestorAtletas.Text = "Gestor de atletas";
            this.btnGestorAtletas.UseVisualStyleBackColor = true;
            this.btnGestorAtletas.Click += new System.EventHandler(this.btnGestorAtletas_Click);
            //
            // btnGestorMarcas
            //
            this.btnGestorMarcas.Location = new System.Drawing.Point(310, 150);
            this.btnGestorMarcas.Name = "btnGestorMarcas";
            this.btnGestorMarcas.Size = new System.Drawing.Size(130, 70);
            this.btnGestorMarcas.TabIndex = 5;
            this.btnGestorMarcas.Text = "Gestor de marcas";
            this.btnGestorMarcas.UseVisualStyleBackColor = true;
            this.btnGestorMarcas.Click += new System.EventHandler(this.btnGestorMarcas_Click);
            //
            // btnCalcularPasajes
            //
            this.btnCalcularPasajes.Location = new System.Drawing.Point(450, 150);
            this.btnCalcularPasajes.Name = "btnCalcularPasajes";
            this.btnCalcularPasajes.Size = new System.Drawing.Size(130, 70);
            this.btnCalcularPasajes.TabIndex = 6;
            this.btnCalcularPasajes.Text = "Cálculo de pasajes";
            this.btnCalcularPasajes.UseVisualStyleBackColor = true;
            this.btnCalcularPasajes.Click += new System.EventHandler(this.btnCalcularPasajes_Click);
            //
            // btnReportes
            //
            this.btnReportes.Location = new System.Drawing.Point(590, 150);
            this.btnReportes.Name = "btnReportes";
            this.btnReportes.Size = new System.Drawing.Size(130, 70);
            this.btnReportes.TabIndex = 7;
            this.btnReportes.Text = "Reportes";
            this.btnReportes.UseVisualStyleBackColor = true;
            this.btnReportes.Click += new System.EventHandler(this.btnReportes_Click);
            //
            // btnGestorUsuarios
            //
            this.btnGestorUsuarios.Location = new System.Drawing.Point(240, 250);
            this.btnGestorUsuarios.Name = "btnGestorUsuarios";
            this.btnGestorUsuarios.Size = new System.Drawing.Size(150, 35);
            this.btnGestorUsuarios.TabIndex = 8;
            this.btnGestorUsuarios.Text = "Gestión de usuarios";
            this.btnGestorUsuarios.UseVisualStyleBackColor = true;
            this.btnGestorUsuarios.Click += new System.EventHandler(this.btnGestorUsuarios_Click);
            //
            // btnSalir
            //
            this.btnSalir.Location = new System.Drawing.Point(410, 250);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(100, 35);
            this.btnSalir.TabIndex = 9;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            //
            // FrmMenuPrincipal
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 320);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.btnGestorUsuarios);
            this.Controls.Add(this.btnReportes);
            this.Controls.Add(this.btnCalcularPasajes);
            this.Controls.Add(this.btnGestorMarcas);
            this.Controls.Add(this.btnGestorAtletas);
            this.Controls.Add(this.btnGestorTorneos);
            this.Controls.Add(this.lblElijaOpcion);
            this.Controls.Add(this.lblBienvenida);
            this.Controls.Add(this.lblTitulo);
            this.MaximizeBox = false;
            this.Name = "frmMenuPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NAT-SYS - Menú principal";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblBienvenida;
        private System.Windows.Forms.Label lblElijaOpcion;
        private System.Windows.Forms.Button btnGestorTorneos;
        private System.Windows.Forms.Button btnGestorAtletas;
        private System.Windows.Forms.Button btnGestorMarcas;
        private System.Windows.Forms.Button btnCalcularPasajes;
        private System.Windows.Forms.Button btnReportes;
        private System.Windows.Forms.Button btnGestorUsuarios;
        private System.Windows.Forms.Button btnSalir;
    }
}