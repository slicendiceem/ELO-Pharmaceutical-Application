using System;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace TicTacToe
{
    
    public partial class LogIn : KryptonForm
    {
        public static string loggedaccountnum = "";

        public LogIn()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            userinput.Focus();
            userinput.Focus();
        }
        
        public void loadaccountnum()
        {
            
        }


        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void log_in_Click(object sender, EventArgs e)
        {

        }

        private void exit_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        private void newuser_Click(object sender, EventArgs e)
        {
            
        }

        public void log_in_Click_1(object sender, EventArgs e)
        {

        }


        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            
        }

        private void kryptonPalette1_PalettePaint(object sender, PaletteLayoutEventArgs e)
        {

        }

        private void kryptonLabel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void passinput_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void kryptonPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void passinput_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void kryptonLabel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void kryptonLabel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void kryptonLabel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void signup_LinkClicked(object sender, EventArgs e)
        {
            NewUser newUser = new NewUser();
            newUser.Show();
            this.Hide();
        }

        private void log_in_Click_2(object sender, EventArgs e)
        {
            string username, password;
            username = userinput.Text;
            password = passinput.Text;
            ELOEntities dbe = new ELOEntities();
            if (userinput.Text != string.Empty || passinput.Text != string.Empty)
            {

                var user2 = dbe.users.FirstOrDefault(a => a.Email.Equals(userinput.Text));
                if (user2 != null)
                {
                    if (user2.Password.Equals(passinput.Text))
                    {
                        loggedaccountnum = user2.Email;
                        Form1 Form1 = new Form1();
                        Form1.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Invalid Details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        userinput.Clear();
                        passinput.Clear();
                    }

                }
                else
                {
                    MessageBox.Show("Invalid Details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    userinput.Clear();
                    passinput.Clear();
                }
            }
            else
            {
                MessageBox.Show("Empty Input.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
