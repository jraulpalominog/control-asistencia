namespace WindowsFormsApp1
{
    partial class FormHome
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormHome));
            this.btnAdminPanel = new Guna.UI2.WinForms.Guna2ImageButton();
            this.btnIniciarSistema = new Guna.UI2.WinForms.Guna2ImageButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnSalirlogin = new Guna.UI2.WinForms.Guna2ImageButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAdminPanel
            // 
            this.btnAdminPanel.BackColor = System.Drawing.Color.Transparent;
            this.btnAdminPanel.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnAdminPanel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdminPanel.HoverState.ImageSize = new System.Drawing.Size(140, 140);
            this.btnAdminPanel.Image = global::WindowsFormsApp1.Properties.Resources._263_2635979_admin_abuse;
            this.btnAdminPanel.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnAdminPanel.ImageRotate = 0F;
            this.btnAdminPanel.ImageSize = new System.Drawing.Size(150, 150);
            this.btnAdminPanel.Location = new System.Drawing.Point(435, 160);
            this.btnAdminPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnAdminPanel.Name = "btnAdminPanel";
            this.btnAdminPanel.PressedState.ImageSize = new System.Drawing.Size(140, 140);
            this.btnAdminPanel.Size = new System.Drawing.Size(219, 171);
            this.btnAdminPanel.TabIndex = 14;
            this.btnAdminPanel.UseTransparentBackground = true;
            this.btnAdminPanel.Click += new System.EventHandler(this.btnAdminPanel_Click);
            // 
            // btnIniciarSistema
            // 
            this.btnIniciarSistema.BackColor = System.Drawing.Color.Transparent;
            this.btnIniciarSistema.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnIniciarSistema.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIniciarSistema.HoverState.ImageSize = new System.Drawing.Size(140, 140);
            this.btnIniciarSistema.Image = global::WindowsFormsApp1.Properties.Resources.boton_de_reproduccion;
            this.btnIniciarSistema.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnIniciarSistema.ImageRotate = 0F;
            this.btnIniciarSistema.ImageSize = new System.Drawing.Size(150, 150);
            this.btnIniciarSistema.Location = new System.Drawing.Point(181, 160);
            this.btnIniciarSistema.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnIniciarSistema.Name = "btnIniciarSistema";
            this.btnIniciarSistema.PressedState.ImageSize = new System.Drawing.Size(140, 140);
            this.btnIniciarSistema.Size = new System.Drawing.Size(219, 171);
            this.btnIniciarSistema.TabIndex = 15;
            this.btnIniciarSistema.UseTransparentBackground = true;
            this.btnIniciarSistema.Click += new System.EventHandler(this.btnIniciarSistema_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(230, 321);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 23);
            this.label1.TabIndex = 17;
            this.label1.Text = "Iniciar sistema";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(444, 320);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(187, 23);
            this.label2.TabIndex = 18;
            this.label2.Text = "Panel de Administrador";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Arial Narrow", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label5.Location = new System.Drawing.Point(56, 20);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 31);
            this.label5.TabIndex = 20;
            this.label5.Text = "INICIO";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::WindowsFormsApp1.Properties.Resources.casa__1_;
            this.pictureBox1.Location = new System.Drawing.Point(9, 10);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(43, 47);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 23;
            this.pictureBox1.TabStop = false;
            // 
            // btnSalirlogin
            // 
            this.btnSalirlogin.BackColor = System.Drawing.Color.Transparent;
            this.btnSalirlogin.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnSalirlogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalirlogin.HoverState.ImageSize = new System.Drawing.Size(60, 60);
            this.btnSalirlogin.Image = global::WindowsFormsApp1.Properties.Resources.cerrar_sesion;
            this.btnSalirlogin.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnSalirlogin.ImageRotate = 0F;
            this.btnSalirlogin.Location = new System.Drawing.Point(9, 423);
            this.btnSalirlogin.Margin = new System.Windows.Forms.Padding(2);
            this.btnSalirlogin.Name = "btnSalirlogin";
            this.btnSalirlogin.PressedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnSalirlogin.Size = new System.Drawing.Size(77, 71);
            this.btnSalirlogin.TabIndex = 24;
            // 
            // FormHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::WindowsFormsApp1.Properties.Resources.image__1_;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(828, 505);
            this.Controls.Add(this.btnSalirlogin);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnIniciarSistema);
            this.Controls.Add(this.btnAdminPanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FormHome";
            this.Text = "Inicio";
            this.Load += new System.EventHandler(this.FormHome_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Guna.UI2.WinForms.Guna2ImageButton btnAdminPanel;
        private Guna.UI2.WinForms.Guna2ImageButton btnIniciarSistema;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Guna.UI2.WinForms.Guna2ImageButton btnSalirlogin;
    }
}