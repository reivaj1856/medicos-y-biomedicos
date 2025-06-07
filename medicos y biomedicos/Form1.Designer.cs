namespace medicos_y_biomedicos
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panelContenedor = new System.Windows.Forms.Panel();
            this.panelformularios = new System.Windows.Forms.Panel();
            this.menu = new System.Windows.Forms.Panel();
            this.panelregistros = new System.Windows.Forms.Panel();
            this.btnmantenimiento = new System.Windows.Forms.Button();
            this.btnreventa = new System.Windows.Forms.Button();
            this.btnregistros = new System.Windows.Forms.Button();
            this.panelVenta = new System.Windows.Forms.Panel();
            this.btnequipos = new System.Windows.Forms.Button();
            this.btnventa = new System.Windows.Forms.Button();
            this.logo = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panelsuperior = new System.Windows.Forms.Panel();
            this.btnRestaurar = new System.Windows.Forms.PictureBox();
            this.minimizar = new System.Windows.Forms.PictureBox();
            this.btnMaximizar = new System.Windows.Forms.PictureBox();
            this.cerrar = new System.Windows.Forms.PictureBox();
            this.panelRegistro = new System.Windows.Forms.Panel();
            this.btnReMantenimiento = new System.Windows.Forms.Button();
            this.btnReEquipo = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.btnsalir = new System.Windows.Forms.Button();
            this.panelContenedor.SuspendLayout();
            this.menu.SuspendLayout();
            this.panelregistros.SuspendLayout();
            this.panelVenta.SuspendLayout();
            this.logo.SuspendLayout();
            this.panelsuperior.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnRestaurar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minimizar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMaximizar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cerrar)).BeginInit();
            this.panelRegistro.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelContenedor
            // 
            this.panelContenedor.Controls.Add(this.panelformularios);
            this.panelContenedor.Controls.Add(this.menu);
            this.panelContenedor.Controls.Add(this.panelsuperior);
            this.panelContenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenedor.Location = new System.Drawing.Point(0, 0);
            this.panelContenedor.Name = "panelContenedor";
            this.panelContenedor.Size = new System.Drawing.Size(1280, 720);
            this.panelContenedor.TabIndex = 0;
            // 
            // panelformularios
            // 
            this.panelformularios.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(139)))), ((int)(((byte)(150)))));
            this.panelformularios.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelformularios.Location = new System.Drawing.Point(250, 40);
            this.panelformularios.Name = "panelformularios";
            this.panelformularios.Size = new System.Drawing.Size(1030, 680);
            this.panelformularios.TabIndex = 4;
            // 
            // menu
            // 
            this.menu.AutoScroll = true;
            this.menu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.menu.Controls.Add(this.btnsalir);
            this.menu.Controls.Add(this.panelRegistro);
            this.menu.Controls.Add(this.button3);
            this.menu.Controls.Add(this.panelregistros);
            this.menu.Controls.Add(this.btnregistros);
            this.menu.Controls.Add(this.panelVenta);
            this.menu.Controls.Add(this.btnventa);
            this.menu.Controls.Add(this.logo);
            this.menu.Dock = System.Windows.Forms.DockStyle.Left;
            this.menu.Location = new System.Drawing.Point(0, 40);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(250, 680);
            this.menu.TabIndex = 3;
            // 
            // panelregistros
            // 
            this.panelregistros.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(64)))), ((int)(((byte)(83)))));
            this.panelregistros.Controls.Add(this.btnmantenimiento);
            this.panelregistros.Controls.Add(this.btnreventa);
            this.panelregistros.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelregistros.Location = new System.Drawing.Point(0, 245);
            this.panelregistros.Name = "panelregistros";
            this.panelregistros.Size = new System.Drawing.Size(250, 101);
            this.panelregistros.TabIndex = 4;
            // 
            // btnmantenimiento
            // 
            this.btnmantenimiento.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnmantenimiento.FlatAppearance.BorderSize = 0;
            this.btnmantenimiento.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnmantenimiento.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnmantenimiento.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnmantenimiento.ForeColor = System.Drawing.Color.Transparent;
            this.btnmantenimiento.Location = new System.Drawing.Point(0, 45);
            this.btnmantenimiento.Name = "btnmantenimiento";
            this.btnmantenimiento.Padding = new System.Windows.Forms.Padding(35, 0, 0, 0);
            this.btnmantenimiento.Size = new System.Drawing.Size(250, 45);
            this.btnmantenimiento.TabIndex = 1;
            this.btnmantenimiento.Text = "Registros de mantenimiento";
            this.btnmantenimiento.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnmantenimiento.UseVisualStyleBackColor = true;
            this.btnmantenimiento.Click += new System.EventHandler(this.btnmantenimiento_Click);
            // 
            // btnreventa
            // 
            this.btnreventa.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnreventa.FlatAppearance.BorderSize = 0;
            this.btnreventa.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnreventa.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnreventa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnreventa.ForeColor = System.Drawing.Color.Transparent;
            this.btnreventa.Location = new System.Drawing.Point(0, 0);
            this.btnreventa.Name = "btnreventa";
            this.btnreventa.Padding = new System.Windows.Forms.Padding(35, 0, 0, 0);
            this.btnreventa.Size = new System.Drawing.Size(250, 45);
            this.btnreventa.TabIndex = 0;
            this.btnreventa.Text = "Regitros de venta";
            this.btnreventa.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnreventa.UseVisualStyleBackColor = true;
            this.btnreventa.Click += new System.EventHandler(this.btnreventa_Click);
            // 
            // btnregistros
            // 
            this.btnregistros.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnregistros.FlatAppearance.BorderSize = 0;
            this.btnregistros.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnregistros.ForeColor = System.Drawing.Color.Transparent;
            this.btnregistros.Location = new System.Drawing.Point(0, 200);
            this.btnregistros.Name = "btnregistros";
            this.btnregistros.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnregistros.Size = new System.Drawing.Size(250, 45);
            this.btnregistros.TabIndex = 3;
            this.btnregistros.Text = "Registros";
            this.btnregistros.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnregistros.UseVisualStyleBackColor = true;
            this.btnregistros.Click += new System.EventHandler(this.btnregistros_Click);
            // 
            // panelVenta
            // 
            this.panelVenta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(64)))), ((int)(((byte)(83)))));
            this.panelVenta.Controls.Add(this.btnequipos);
            this.panelVenta.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelVenta.Location = new System.Drawing.Point(0, 145);
            this.panelVenta.Name = "panelVenta";
            this.panelVenta.Size = new System.Drawing.Size(250, 55);
            this.panelVenta.TabIndex = 2;
            // 
            // btnequipos
            // 
            this.btnequipos.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnequipos.FlatAppearance.BorderSize = 0;
            this.btnequipos.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnequipos.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnequipos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnequipos.ForeColor = System.Drawing.Color.Transparent;
            this.btnequipos.Location = new System.Drawing.Point(0, 0);
            this.btnequipos.Name = "btnequipos";
            this.btnequipos.Padding = new System.Windows.Forms.Padding(35, 0, 0, 0);
            this.btnequipos.Size = new System.Drawing.Size(250, 45);
            this.btnequipos.TabIndex = 0;
            this.btnequipos.Text = "Equipos";
            this.btnequipos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnequipos.UseVisualStyleBackColor = true;
            this.btnequipos.Click += new System.EventHandler(this.btnequipos_Click);
            // 
            // btnventa
            // 
            this.btnventa.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnventa.FlatAppearance.BorderSize = 0;
            this.btnventa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnventa.ForeColor = System.Drawing.Color.Transparent;
            this.btnventa.Location = new System.Drawing.Point(0, 100);
            this.btnventa.Name = "btnventa";
            this.btnventa.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnventa.Size = new System.Drawing.Size(250, 45);
            this.btnventa.TabIndex = 1;
            this.btnventa.Text = "Venta";
            this.btnventa.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnventa.UseVisualStyleBackColor = true;
            this.btnventa.Click += new System.EventHandler(this.btnventa_Click);
            // 
            // logo
            // 
            this.logo.Controls.Add(this.label1);
            this.logo.Dock = System.Windows.Forms.DockStyle.Top;
            this.logo.Location = new System.Drawing.Point(0, 0);
            this.logo.Name = "logo";
            this.logo.Size = new System.Drawing.Size(250, 100);
            this.logo.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(51, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "NOMBRE";
            // 
            // panelsuperior
            // 
            this.panelsuperior.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panelsuperior.Controls.Add(this.btnRestaurar);
            this.panelsuperior.Controls.Add(this.minimizar);
            this.panelsuperior.Controls.Add(this.btnMaximizar);
            this.panelsuperior.Controls.Add(this.cerrar);
            this.panelsuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelsuperior.Location = new System.Drawing.Point(0, 0);
            this.panelsuperior.Name = "panelsuperior";
            this.panelsuperior.Size = new System.Drawing.Size(1280, 40);
            this.panelsuperior.TabIndex = 2;
            this.panelsuperior.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelsuperior_MouseMove);
            // 
            // btnRestaurar
            // 
            this.btnRestaurar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRestaurar.Image = ((System.Drawing.Image)(resources.GetObject("btnRestaurar.Image")));
            this.btnRestaurar.Location = new System.Drawing.Point(1205, 7);
            this.btnRestaurar.Name = "btnRestaurar";
            this.btnRestaurar.Size = new System.Drawing.Size(30, 30);
            this.btnRestaurar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnRestaurar.TabIndex = 3;
            this.btnRestaurar.TabStop = false;
            this.btnRestaurar.Visible = false;
            this.btnRestaurar.Click += new System.EventHandler(this.btnRestaurar_Click);
            // 
            // minimizar
            // 
            this.minimizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.minimizar.Image = ((System.Drawing.Image)(resources.GetObject("minimizar.Image")));
            this.minimizar.Location = new System.Drawing.Point(1169, 7);
            this.minimizar.Name = "minimizar";
            this.minimizar.Size = new System.Drawing.Size(30, 30);
            this.minimizar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.minimizar.TabIndex = 2;
            this.minimizar.TabStop = false;
            this.minimizar.Click += new System.EventHandler(this.minimizar_Click);
            // 
            // btnMaximizar
            // 
            this.btnMaximizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMaximizar.Image = ((System.Drawing.Image)(resources.GetObject("btnMaximizar.Image")));
            this.btnMaximizar.Location = new System.Drawing.Point(1205, 7);
            this.btnMaximizar.Name = "btnMaximizar";
            this.btnMaximizar.Size = new System.Drawing.Size(30, 30);
            this.btnMaximizar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnMaximizar.TabIndex = 1;
            this.btnMaximizar.TabStop = false;
            this.btnMaximizar.Click += new System.EventHandler(this.maximizar_Click);
            // 
            // cerrar
            // 
            this.cerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cerrar.Image = ((System.Drawing.Image)(resources.GetObject("cerrar.Image")));
            this.cerrar.Location = new System.Drawing.Point(1241, 7);
            this.cerrar.Name = "cerrar";
            this.cerrar.Size = new System.Drawing.Size(30, 30);
            this.cerrar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.cerrar.TabIndex = 0;
            this.cerrar.TabStop = false;
            this.cerrar.Click += new System.EventHandler(this.cerrar_Click);
            // 
            // panelRegistro
            // 
            this.panelRegistro.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(64)))), ((int)(((byte)(83)))));
            this.panelRegistro.Controls.Add(this.btnReMantenimiento);
            this.panelRegistro.Controls.Add(this.btnReEquipo);
            this.panelRegistro.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelRegistro.Location = new System.Drawing.Point(0, 391);
            this.panelRegistro.Name = "panelRegistro";
            this.panelRegistro.Size = new System.Drawing.Size(250, 101);
            this.panelRegistro.TabIndex = 7;
            this.panelRegistro.Visible = false;
            // 
            // btnReMantenimiento
            // 
            this.btnReMantenimiento.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnReMantenimiento.FlatAppearance.BorderSize = 0;
            this.btnReMantenimiento.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnReMantenimiento.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnReMantenimiento.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReMantenimiento.ForeColor = System.Drawing.Color.Transparent;
            this.btnReMantenimiento.Location = new System.Drawing.Point(0, 45);
            this.btnReMantenimiento.Name = "btnReMantenimiento";
            this.btnReMantenimiento.Padding = new System.Windows.Forms.Padding(35, 0, 0, 0);
            this.btnReMantenimiento.Size = new System.Drawing.Size(250, 45);
            this.btnReMantenimiento.TabIndex = 1;
            this.btnReMantenimiento.Text = "Registros de mantenimiento";
            this.btnReMantenimiento.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReMantenimiento.UseVisualStyleBackColor = true;
            this.btnReMantenimiento.Click += new System.EventHandler(this.btnReMantenimiento_Click);
            // 
            // btnReEquipo
            // 
            this.btnReEquipo.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnReEquipo.FlatAppearance.BorderSize = 0;
            this.btnReEquipo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnReEquipo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnReEquipo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReEquipo.ForeColor = System.Drawing.Color.Transparent;
            this.btnReEquipo.Location = new System.Drawing.Point(0, 0);
            this.btnReEquipo.Name = "btnReEquipo";
            this.btnReEquipo.Padding = new System.Windows.Forms.Padding(35, 0, 0, 0);
            this.btnReEquipo.Size = new System.Drawing.Size(250, 45);
            this.btnReEquipo.TabIndex = 0;
            this.btnReEquipo.Text = "Regitrar Equipo";
            this.btnReEquipo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReEquipo.UseVisualStyleBackColor = true;
            this.btnReEquipo.Click += new System.EventHandler(this.btnReEquipo_Click);
            // 
            // button3
            // 
            this.button3.Dock = System.Windows.Forms.DockStyle.Top;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.ForeColor = System.Drawing.Color.Transparent;
            this.button3.Location = new System.Drawing.Point(0, 346);
            this.button3.Name = "button3";
            this.button3.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.button3.Size = new System.Drawing.Size(250, 45);
            this.button3.TabIndex = 6;
            this.button3.Text = "Registro";
            this.button3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // btnsalir
            // 
            this.btnsalir.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnsalir.FlatAppearance.BorderSize = 0;
            this.btnsalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnsalir.ForeColor = System.Drawing.Color.Transparent;
            this.btnsalir.Location = new System.Drawing.Point(0, 492);
            this.btnsalir.Name = "btnsalir";
            this.btnsalir.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnsalir.Size = new System.Drawing.Size(250, 45);
            this.btnsalir.TabIndex = 8;
            this.btnsalir.Text = "Salir";
            this.btnsalir.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnsalir.UseVisualStyleBackColor = true;
            this.btnsalir.Click += new System.EventHandler(this.cerrar_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.panelContenedor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panelContenedor.ResumeLayout(false);
            this.menu.ResumeLayout(false);
            this.panelregistros.ResumeLayout(false);
            this.panelVenta.ResumeLayout(false);
            this.logo.ResumeLayout(false);
            this.logo.PerformLayout();
            this.panelsuperior.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnRestaurar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minimizar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMaximizar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cerrar)).EndInit();
            this.panelRegistro.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelContenedor;
        private System.Windows.Forms.Panel menu;
        private System.Windows.Forms.Panel panelregistros;
        private System.Windows.Forms.Button btnmantenimiento;
        private System.Windows.Forms.Button btnreventa;
        private System.Windows.Forms.Button btnregistros;
        private System.Windows.Forms.Panel panelVenta;
        private System.Windows.Forms.Button btnequipos;
        private System.Windows.Forms.Button btnventa;
        private System.Windows.Forms.Panel logo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelsuperior;
        private System.Windows.Forms.Panel panelformularios;
        private System.Windows.Forms.PictureBox minimizar;
        private System.Windows.Forms.PictureBox btnMaximizar;
        private System.Windows.Forms.PictureBox cerrar;
        private System.Windows.Forms.PictureBox btnRestaurar;
        private System.Windows.Forms.Button btnsalir;
        private System.Windows.Forms.Panel panelRegistro;
        private System.Windows.Forms.Button btnReMantenimiento;
        private System.Windows.Forms.Button btnReEquipo;
        private System.Windows.Forms.Button button3;
    }
}

