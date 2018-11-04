namespace MainForm
{
    partial class Authorization
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Authorization));
            this.labelCompanyName = new System.Windows.Forms.Label();
            this.textBoxLogin = new System.Windows.Forms.TextBox();
            this.textBoxPass = new System.Windows.Forms.TextBox();
            this.labelInfo = new System.Windows.Forms.Label();
            this.buttonEnter = new System.Windows.Forms.Button();
            this.buttonEnterAs = new System.Windows.Forms.Button();
            this.labelLogin = new System.Windows.Forms.Label();
            this.labelPass = new System.Windows.Forms.Label();
            this.buttonShowPass = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelCompanyName
            // 
            this.labelCompanyName.AutoSize = true;
            this.labelCompanyName.Font = new System.Drawing.Font("Segoe UI", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelCompanyName.Location = new System.Drawing.Point(48, 28);
            this.labelCompanyName.Name = "labelCompanyName";
            this.labelCompanyName.Size = new System.Drawing.Size(405, 45);
            this.labelCompanyName.TabIndex = 0;
            this.labelCompanyName.Text = "Приют животных \"Ласка\"";
            // 
            // textBoxLogin
            // 
            this.textBoxLogin.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxLogin.ForeColor = System.Drawing.Color.Gray;
            this.textBoxLogin.Location = new System.Drawing.Point(207, 98);
            this.textBoxLogin.MaxLength = 20;
            this.textBoxLogin.Name = "textBoxLogin";
            this.textBoxLogin.Size = new System.Drawing.Size(258, 30);
            this.textBoxLogin.TabIndex = 2;
            this.textBoxLogin.Text = "Введите логин";
            this.textBoxLogin.Enter += new System.EventHandler(this.textBoxLogin_Enter);
            this.textBoxLogin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            this.textBoxLogin.Leave += new System.EventHandler(this.textBoxLogin_Leave);
            // 
            // textBoxPass
            // 
            this.textBoxPass.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxPass.ForeColor = System.Drawing.Color.Gray;
            this.textBoxPass.Location = new System.Drawing.Point(207, 152);
            this.textBoxPass.Margin = new System.Windows.Forms.Padding(0);
            this.textBoxPass.MaxLength = 20;
            this.textBoxPass.Name = "textBoxPass";
            this.textBoxPass.Size = new System.Drawing.Size(225, 30);
            this.textBoxPass.TabIndex = 4;
            this.textBoxPass.Text = "Введите пароль";
            this.textBoxPass.Enter += new System.EventHandler(this.textBoxPass_Enter);
            this.textBoxPass.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            this.textBoxPass.Leave += new System.EventHandler(this.textBoxPass_Leave);
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.labelInfo.Location = new System.Drawing.Point(53, 207);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(417, 23);
            this.labelInfo.TabIndex = 5;
            this.labelInfo.Text = "* Если вы волонтёр,  вам не нужно вводить пароль";
            // 
            // buttonEnter
            // 
            this.buttonEnter.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.buttonEnter.Location = new System.Drawing.Point(56, 261);
            this.buttonEnter.Name = "buttonEnter";
            this.buttonEnter.Size = new System.Drawing.Size(80, 35);
            this.buttonEnter.TabIndex = 6;
            this.buttonEnter.Text = "Войти";
            this.buttonEnter.UseVisualStyleBackColor = true;
            this.buttonEnter.Click += new System.EventHandler(this.buttonEnter_Click);
            // 
            // buttonEnterAs
            // 
            this.buttonEnterAs.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.buttonEnterAs.Location = new System.Drawing.Point(285, 261);
            this.buttonEnterAs.Name = "buttonEnterAs";
            this.buttonEnterAs.Size = new System.Drawing.Size(180, 35);
            this.buttonEnterAs.TabIndex = 7;
            this.buttonEnterAs.Text = "Войти как волонтёр";
            this.buttonEnterAs.UseVisualStyleBackColor = true;
            this.buttonEnterAs.Click += new System.EventHandler(this.buttonEnterAs_Click);
            // 
            // labelLogin
            // 
            this.labelLogin.AutoSize = true;
            this.labelLogin.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelLogin.Location = new System.Drawing.Point(29, 101);
            this.labelLogin.Name = "labelLogin";
            this.labelLogin.Size = new System.Drawing.Size(157, 23);
            this.labelLogin.TabIndex = 1;
            this.labelLogin.Text = "Имя пользователя";
            this.labelLogin.Click += new System.EventHandler(this.labelLogin_Click);
            // 
            // labelPass
            // 
            this.labelPass.AutoSize = true;
            this.labelPass.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.labelPass.Location = new System.Drawing.Point(29, 155);
            this.labelPass.Name = "labelPass";
            this.labelPass.Size = new System.Drawing.Size(69, 23);
            this.labelPass.TabIndex = 3;
            this.labelPass.Text = "Пароль";
            this.labelPass.Click += new System.EventHandler(this.labelPass_Click);
            // 
            // buttonShowPass
            // 
            this.buttonShowPass.BackColor = System.Drawing.Color.Transparent;
            this.buttonShowPass.BackgroundImage = global::MainForm.Properties.Resources.showPass;
            this.buttonShowPass.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonShowPass.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.buttonShowPass.FlatAppearance.BorderSize = 0;
            this.buttonShowPass.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonShowPass.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonShowPass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonShowPass.ForeColor = System.Drawing.Color.Transparent;
            this.buttonShowPass.Location = new System.Drawing.Point(435, 152);
            this.buttonShowPass.Margin = new System.Windows.Forms.Padding(0);
            this.buttonShowPass.Name = "buttonShowPass";
            this.buttonShowPass.Size = new System.Drawing.Size(30, 30);
            this.buttonShowPass.TabIndex = 8;
            this.buttonShowPass.TabStop = false;
            this.buttonShowPass.UseVisualStyleBackColor = false;
            this.buttonShowPass.MouseEnter += new System.EventHandler(this.buttonShowPass_MouseEnter);
            this.buttonShowPass.MouseLeave += new System.EventHandler(this.buttonShowPass_MouseLeave);
            // 
            // Authorization
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 329);
            this.Controls.Add(this.buttonShowPass);
            this.Controls.Add(this.labelPass);
            this.Controls.Add(this.labelLogin);
            this.Controls.Add(this.buttonEnterAs);
            this.Controls.Add(this.buttonEnter);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.textBoxPass);
            this.Controls.Add(this.textBoxLogin);
            this.Controls.Add(this.labelCompanyName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Authorization";
            this.Opacity = 0.98D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Приют животных \"Ласка\"  | Вход";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelCompanyName;
        private System.Windows.Forms.TextBox textBoxLogin;
        private System.Windows.Forms.TextBox textBoxPass;
        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.Button buttonEnter;
        private System.Windows.Forms.Button buttonEnterAs;
        private System.Windows.Forms.Label labelLogin;
        private System.Windows.Forms.Label labelPass;
        private System.Windows.Forms.Button buttonShowPass;
    }
}