namespace TicTacToe
{
    partial class LogIn
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogIn));
            this.kryptonPalette1 = new ComponentFactory.Krypton.Toolkit.KryptonPalette(this.components);
            this.userinput = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.passinput = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.log_in = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.signup = new ComponentFactory.Krypton.Toolkit.KryptonLinkLabel();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // kryptonPalette1
            // 
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Back.Color1 = System.Drawing.Color.Gainsboro;
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Back.Color2 = System.Drawing.Color.Gainsboro;
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Border.Rounding = 12;
            this.kryptonPalette1.HeaderStyles.HeaderForm.StateCommon.Back.Color1 = System.Drawing.Color.Gainsboro;
            this.kryptonPalette1.HeaderStyles.HeaderForm.StateCommon.Back.Color2 = System.Drawing.Color.Gainsboro;
            this.kryptonPalette1.HeaderStyles.HeaderForm.StateCommon.ButtonEdgeInset = 10;
            this.kryptonPalette1.HeaderStyles.HeaderForm.StateCommon.Content.Padding = new System.Windows.Forms.Padding(10, -1, -1, -1);
            this.kryptonPalette1.PalettePaint += new System.EventHandler<ComponentFactory.Krypton.Toolkit.PaletteLayoutEventArgs>(this.kryptonPalette1_PalettePaint);
            // 
            // userinput
            // 
            this.userinput.Location = new System.Drawing.Point(413, 210);
            this.userinput.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.userinput.Name = "userinput";
            this.userinput.Size = new System.Drawing.Size(361, 31);
            this.userinput.StateCommon.Border.Color1 = System.Drawing.Color.Silver;
            this.userinput.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.userinput.StateCommon.Border.Rounding = 1;
            this.userinput.StateCommon.Border.Width = 3;
            this.userinput.StateCommon.Content.Color1 = System.Drawing.Color.Gray;
            this.userinput.TabIndex = 4;
            // 
            // passinput
            // 
            this.passinput.Location = new System.Drawing.Point(413, 299);
            this.passinput.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.passinput.Name = "passinput";
            this.passinput.PasswordChar = '*';
            this.passinput.Size = new System.Drawing.Size(361, 31);
            this.passinput.StateCommon.Border.Color1 = System.Drawing.Color.Silver;
            this.passinput.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.passinput.StateCommon.Border.Rounding = 1;
            this.passinput.StateCommon.Border.Width = 3;
            this.passinput.StateCommon.Content.Color1 = System.Drawing.Color.Gray;
            this.passinput.TabIndex = 5;
            this.passinput.TextChanged += new System.EventHandler(this.passinput_TextChanged_1);
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(417, 178);
            this.kryptonLabel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(55, 24);
            this.kryptonLabel1.TabIndex = 14;
            this.kryptonLabel1.Values.Text = "E-Mail";
            this.kryptonLabel1.Paint += new System.Windows.Forms.PaintEventHandler(this.kryptonLabel1_Paint_1);
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(413, 270);
            this.kryptonLabel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(76, 24);
            this.kryptonLabel2.TabIndex = 15;
            this.kryptonLabel2.Values.Text = "Password";
            this.kryptonLabel2.Paint += new System.Windows.Forms.PaintEventHandler(this.kryptonLabel2_Paint);
            // 
            // log_in
            // 
            this.log_in.Location = new System.Drawing.Point(417, 368);
            this.log_in.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.log_in.Name = "log_in";
            this.log_in.Size = new System.Drawing.Size(133, 46);
            this.log_in.StateCommon.Back.Color1 = System.Drawing.Color.DeepSkyBlue;
            this.log_in.StateCommon.Back.Color2 = System.Drawing.Color.DeepSkyBlue;
            this.log_in.StateCommon.Border.Color1 = System.Drawing.Color.DeepSkyBlue;
            this.log_in.StateCommon.Border.Color2 = System.Drawing.Color.DeepSkyBlue;
            this.log_in.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.log_in.StateCommon.Border.Rounding = 10;
            this.log_in.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.log_in.StateCommon.Content.ShortText.Color2 = System.Drawing.Color.White;
            this.log_in.StateCommon.Content.ShortText.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.Sigma;
            this.log_in.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.log_in.TabIndex = 16;
            this.log_in.Values.Text = "Login";
            this.log_in.Click += new System.EventHandler(this.log_in_Click_2);
            // 
            // signup
            // 
            this.signup.Location = new System.Drawing.Point(565, 658);
            this.signup.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.signup.Name = "signup";
            this.signup.OverrideFocus.ShortText.Color1 = System.Drawing.Color.DeepSkyBlue;
            this.signup.OverrideNotVisited.ShortText.Color1 = System.Drawing.Color.DeepSkyBlue;
            this.signup.Size = new System.Drawing.Size(65, 23);
            this.signup.StateCommon.ShortText.Font = new System.Drawing.Font("Microsoft Tai Le", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.signup.StateNormal.LongText.Color1 = System.Drawing.Color.DeepSkyBlue;
            this.signup.StateNormal.LongText.Color2 = System.Drawing.Color.DeepSkyBlue;
            this.signup.TabIndex = 17;
            this.signup.Values.Text = "Sign Up";
            this.signup.LinkClicked += new System.EventHandler(this.signup_LinkClicked);
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(389, 657);
            this.kryptonLabel3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(171, 24);
            this.kryptonLabel3.TabIndex = 18;
            this.kryptonLabel3.Values.Text = "Don\'t have an account?";
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(417, 50);
            this.kryptonLabel4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(226, 41);
            this.kryptonLabel4.StateCommon.ShortText.Font = new System.Drawing.Font("Elephant", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonLabel4.TabIndex = 20;
            this.kryptonLabel4.Values.Text = "Welcome back!";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = global::TicTacToe.Properties.Resources.IMG_1345;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Location = new System.Drawing.Point(32, 115);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(309, 277);
            this.panel1.TabIndex = 21;
            // 
            // LogIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.BackgroundImage = global::TicTacToe.Properties.Resources.login3;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(837, 686);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.kryptonLabel4);
            this.Controls.Add(this.kryptonLabel3);
            this.Controls.Add(this.signup);
            this.Controls.Add(this.log_in);
            this.Controls.Add(this.kryptonLabel2);
            this.Controls.Add(this.kryptonLabel1);
            this.Controls.Add(this.userinput);
            this.Controls.Add(this.passinput);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LogIn";
            this.Palette = this.kryptonPalette1;
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.StateCommon.Back.Color1 = System.Drawing.Color.Gainsboro;
            this.StateCommon.Back.Color2 = System.Drawing.Color.Gainsboro;
            this.StateInactive.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.StateInactive.Header.Back.Color1 = System.Drawing.Color.White;
            this.StateInactive.Header.Back.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.Solid;
            this.StateInactive.Header.Back.Draw = ComponentFactory.Krypton.Toolkit.InheritBool.True;
            this.StateInactive.Header.Border.Color1 = System.Drawing.Color.White;
            this.StateInactive.Header.Border.Color2 = System.Drawing.Color.White;
            this.StateInactive.Header.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.Text = "ELO";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private ComponentFactory.Krypton.Toolkit.KryptonPalette kryptonPalette1;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox userinput;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox passinput;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonButton log_in;
        private ComponentFactory.Krypton.Toolkit.KryptonLinkLabel signup;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private System.Windows.Forms.Panel panel1;
    }
}

