namespace TicTacToe
{
    partial class NewUser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewUser));
            this.kryptonPalette1 = new ComponentFactory.Krypton.Toolkit.KryptonPalette(this.components);
            this.back_btn = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.label4 = new System.Windows.Forms.Label();
            this.imageenter = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.personalphoto = new System.Windows.Forms.PictureBox();
            this.createfirstname = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.createlastname = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.emailinput = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.numberinput = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.createpassword = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.confirmpassword = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonButton1 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.personalphoto)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPalette1
            // 
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Back.Color1 = System.Drawing.Color.Gainsboro;
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Back.Color2 = System.Drawing.Color.Gainsboro;
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Border.Color1 = System.Drawing.Color.Gainsboro;
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Border.Color2 = System.Drawing.Color.Gainsboro;
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Border.Rounding = 10;
            this.kryptonPalette1.HeaderStyles.HeaderForm.StateCommon.Back.Color1 = System.Drawing.Color.Gainsboro;
            this.kryptonPalette1.HeaderStyles.HeaderForm.StateCommon.Back.Color2 = System.Drawing.Color.Gainsboro;
            this.kryptonPalette1.HeaderStyles.HeaderForm.StateCommon.ButtonPadding = new System.Windows.Forms.Padding(10, -1, -1, -1);
            // 
            // back_btn
            // 
            this.back_btn.Location = new System.Drawing.Point(60, 423);
            this.back_btn.Name = "back_btn";
            this.back_btn.Size = new System.Drawing.Size(70, 64);
            this.back_btn.StateCommon.Back.Color1 = System.Drawing.Color.Red;
            this.back_btn.StateCommon.Back.Color2 = System.Drawing.Color.Transparent;
            this.back_btn.StateCommon.Back.ColorAngle = 135F;
            this.back_btn.StateCommon.Border.Color1 = System.Drawing.Color.DarkRed;
            this.back_btn.StateCommon.Border.Color2 = System.Drawing.Color.DeepSkyBlue;
            this.back_btn.StateCommon.Border.ColorAngle = 45F;
            this.back_btn.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.back_btn.StateCommon.Border.Rounding = 20;
            this.back_btn.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.back_btn.StateCommon.Content.ShortText.Color2 = System.Drawing.Color.White;
            this.back_btn.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.back_btn.StateTracking.Back.Color1 = System.Drawing.Color.Red;
            this.back_btn.StateTracking.Back.Color2 = System.Drawing.Color.Red;
            this.back_btn.StateTracking.Border.Color1 = System.Drawing.Color.DarkRed;
            this.back_btn.StateTracking.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.back_btn.TabIndex = 13;
            this.back_btn.Values.Image = ((System.Drawing.Image)(resources.GetObject("back_btn.Values.Image")));
            this.back_btn.Values.Text = "";
            this.back_btn.Click += new System.EventHandler(this.back_btn_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Copperplate Gothic Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(506, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(152, 17);
            this.label4.TabIndex = 30;
            this.label4.Text = "Personal Photo";
            // 
            // imageenter
            // 
            this.imageenter.Location = new System.Drawing.Point(509, 309);
            this.imageenter.Name = "imageenter";
            this.imageenter.Size = new System.Drawing.Size(146, 37);
            this.imageenter.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.imageenter.StateCommon.Border.Rounding = 5;
            this.imageenter.TabIndex = 36;
            this.imageenter.Values.Image = ((System.Drawing.Image)(resources.GetObject("imageenter.Values.Image")));
            this.imageenter.Values.Text = "Upload Photo";
            this.imageenter.Click += new System.EventHandler(this.imageenter_Click);
            // 
            // personalphoto
            // 
            this.personalphoto.BackColor = System.Drawing.Color.White;
            this.personalphoto.Location = new System.Drawing.Point(504, 130);
            this.personalphoto.Name = "personalphoto";
            this.personalphoto.Size = new System.Drawing.Size(156, 163);
            this.personalphoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.personalphoto.TabIndex = 31;
            this.personalphoto.TabStop = false;
            // 
            // createfirstname
            // 
            this.createfirstname.Location = new System.Drawing.Point(61, 130);
            this.createfirstname.Margin = new System.Windows.Forms.Padding(2);
            this.createfirstname.Name = "createfirstname";
            this.createfirstname.Size = new System.Drawing.Size(165, 26);
            this.createfirstname.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.createfirstname.StateCommon.Border.Rounding = 5;
            this.createfirstname.StateCommon.Content.Color1 = System.Drawing.Color.Gray;
            this.createfirstname.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.createfirstname.TabIndex = 42;
            this.createfirstname.Text = "First Name";
            this.createfirstname.TextChanged += new System.EventHandler(this.kryptonTextBox1_TextChanged);
            // 
            // createlastname
            // 
            this.createlastname.Location = new System.Drawing.Point(61, 169);
            this.createlastname.Margin = new System.Windows.Forms.Padding(2);
            this.createlastname.Name = "createlastname";
            this.createlastname.Size = new System.Drawing.Size(165, 27);
            this.createlastname.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.createlastname.StateCommon.Border.Rounding = 5;
            this.createlastname.StateCommon.Content.Color1 = System.Drawing.Color.Gray;
            this.createlastname.TabIndex = 43;
            this.createlastname.Text = "Last Name";
            // 
            // emailinput
            // 
            this.emailinput.Location = new System.Drawing.Point(61, 210);
            this.emailinput.Margin = new System.Windows.Forms.Padding(2);
            this.emailinput.Name = "emailinput";
            this.emailinput.Size = new System.Drawing.Size(165, 27);
            this.emailinput.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.emailinput.StateCommon.Border.Rounding = 5;
            this.emailinput.StateCommon.Content.Color1 = System.Drawing.Color.Gray;
            this.emailinput.TabIndex = 44;
            this.emailinput.Text = "E-Mail";
            // 
            // numberinput
            // 
            this.numberinput.Location = new System.Drawing.Point(61, 249);
            this.numberinput.Margin = new System.Windows.Forms.Padding(2);
            this.numberinput.Name = "numberinput";
            this.numberinput.Size = new System.Drawing.Size(165, 27);
            this.numberinput.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.numberinput.StateCommon.Border.Rounding = 5;
            this.numberinput.StateCommon.Content.Color1 = System.Drawing.Color.Gray;
            this.numberinput.TabIndex = 45;
            this.numberinput.Text = "Mobile Number";
            // 
            // createpassword
            // 
            this.createpassword.Location = new System.Drawing.Point(61, 288);
            this.createpassword.Margin = new System.Windows.Forms.Padding(2);
            this.createpassword.Name = "createpassword";
            this.createpassword.Size = new System.Drawing.Size(165, 27);
            this.createpassword.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.createpassword.StateCommon.Border.Rounding = 5;
            this.createpassword.StateCommon.Content.Color1 = System.Drawing.Color.Gray;
            this.createpassword.TabIndex = 46;
            this.createpassword.Text = "Password";
            // 
            // confirmpassword
            // 
            this.confirmpassword.Location = new System.Drawing.Point(61, 329);
            this.confirmpassword.Margin = new System.Windows.Forms.Padding(2);
            this.confirmpassword.Name = "confirmpassword";
            this.confirmpassword.Size = new System.Drawing.Size(165, 27);
            this.confirmpassword.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.confirmpassword.StateCommon.Border.Rounding = 5;
            this.confirmpassword.StateCommon.Content.Color1 = System.Drawing.Color.Gray;
            this.confirmpassword.TabIndex = 47;
            this.confirmpassword.Text = "Confirm Password";
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Location = new System.Drawing.Point(155, 423);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.Size = new System.Drawing.Size(70, 64);
            this.kryptonButton1.StateCommon.Back.Color1 = System.Drawing.Color.Transparent;
            this.kryptonButton1.StateCommon.Back.Color2 = System.Drawing.Color.DeepSkyBlue;
            this.kryptonButton1.StateCommon.Back.ColorAngle = 135F;
            this.kryptonButton1.StateCommon.Border.Color1 = System.Drawing.Color.DeepSkyBlue;
            this.kryptonButton1.StateCommon.Border.Color2 = System.Drawing.Color.DeepSkyBlue;
            this.kryptonButton1.StateCommon.Border.ColorAngle = 45F;
            this.kryptonButton1.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonButton1.StateCommon.Border.Rounding = 20;
            this.kryptonButton1.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.kryptonButton1.StateCommon.Content.ShortText.Color2 = System.Drawing.Color.White;
            this.kryptonButton1.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonButton1.StateTracking.Back.Color1 = System.Drawing.Color.DeepSkyBlue;
            this.kryptonButton1.StateTracking.Back.Color2 = System.Drawing.Color.SkyBlue;
            this.kryptonButton1.StateTracking.Back.ColorAngle = 135F;
            this.kryptonButton1.StateTracking.Border.Color1 = System.Drawing.Color.DarkTurquoise;
            this.kryptonButton1.StateTracking.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonButton1.TabIndex = 48;
            this.kryptonButton1.Values.Image = ((System.Drawing.Image)(resources.GetObject("kryptonButton1.Values.Image")));
            this.kryptonButton1.Values.Text = "";
            this.kryptonButton1.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(12, 12);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(307, 50);
            this.kryptonLabel1.StateCommon.ShortText.Font = new System.Drawing.Font("Palatino Linotype", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonLabel1.TabIndex = 49;
            this.kryptonLabel1.Values.Image = global::TicTacToe.Properties.Resources.Add_User_Male1;
            this.kryptonLabel1.Values.Text = "Account Registeration";
            // 
            // NewUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.BackgroundImage = global::TicTacToe.Properties.Resources.main;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(754, 526);
            this.Controls.Add(this.kryptonLabel1);
            this.Controls.Add(this.kryptonButton1);
            this.Controls.Add(this.confirmpassword);
            this.Controls.Add(this.createpassword);
            this.Controls.Add(this.numberinput);
            this.Controls.Add(this.emailinput);
            this.Controls.Add(this.createlastname);
            this.Controls.Add(this.createfirstname);
            this.Controls.Add(this.imageenter);
            this.Controls.Add(this.personalphoto);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.back_btn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NewUser";
            this.Palette = this.kryptonPalette1;
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.StateInactive.Header.Back.Color1 = System.Drawing.Color.White;
            this.StateInactive.Header.Back.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.Solid;
            this.StateInactive.Header.Border.Color1 = System.Drawing.Color.White;
            this.StateInactive.Header.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.Text = "ELO";
            this.Load += new System.EventHandler(this.NewUser_Load);
            ((System.ComponentModel.ISupportInitialize)(this.personalphoto)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private ComponentFactory.Krypton.Toolkit.KryptonPalette kryptonPalette1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton back_btn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox personalphoto;
        private ComponentFactory.Krypton.Toolkit.KryptonButton imageenter;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox createfirstname;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox createlastname;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox emailinput;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox numberinput;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox createpassword;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox confirmpassword;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
    }
}