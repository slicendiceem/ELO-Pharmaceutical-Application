using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using ComponentFactory.Krypton.Toolkit;
namespace TicTacToe
{
    public partial class NewUser : KryptonForm
    {
        
        public NewUser()
        {
            InitializeComponent();
        }
        //SqlConnection conn = new SqlConnection(@"Data Source=AHMED\SQLEXPRESS01;Initial Catalog=ELO;Integrated Security=True;Trust Server Certificate=True;");
        private void NewUser_Load(object sender, EventArgs e)
        {
            load_Date();
            load_Account();
        }
        MemoryStream ms;
        private void createbtn_Click(object sender, EventArgs e)
        {

        }

        private int load_Account()
        {
            int no;
            ELOEntities bse = new ELOEntities();
            var item = bse.users.ToArray();
            no = item.LastOrDefault().ID + 1;
            return no;


        }

        private void cancelcreation_Click(object sender, EventArgs e)
        {
            LogIn logIn = new LogIn();
            logIn.Show();
            this.Close();
        }

        private void exit1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        public void load_Date()
        {
            //currentdate.Text = DateTime.Now.ToString("MM/dd/yyyy");
        }
        

        private void back_btn_Click(object sender, EventArgs e)
        {
            LogIn logIn = new LogIn();
            logIn.Show();
            this.Close();
        }

        private void kryptonComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void createacc_Click(object sender, EventArgs e)
        {
            
        }
        
        private void currentdate_Click(object sender, EventArgs e)
        {
            
        }

        private void imageenter_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Image img =Image.FromFile(ofd.FileName);
                personalphoto.Image = img;
                ms = new MemoryStream();
                img.Save(ms, img.RawFormat);
            }
        }

        private void numberinput_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void numberinput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar)) e.Handled = true;         //Just Digits
            if (e.KeyChar == (char)8) e.Handled = false;            //Allow Backspace
            if (e.KeyChar == (char)13) createacc_Click(sender, e);  //Allow Enter 
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void kryptonTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            string gender = string.Empty;
            string firstname = string.Empty;
            string middlename = string.Empty;
            string lastname = string.Empty;
            var confirmCreation = MessageBox.Show("Submit details and create account?", "Confirm Creation", MessageBoxButtons.YesNo);
            if (confirmCreation == DialogResult.Yes)
            {
                if (confirmpassword.Text == createpassword.Text)
                {
                    if (createfirstname.Text != "" && createlastname.Text != "")
                    {
                        if (int.TryParse(numberinput.Text, out int result))
                        {
                            if (personalphoto.Image != null)
                            {

                                ELOEntities bse = new ELOEntities();
                                user acc = new user();
                                acc.ID = load_Account();
                                acc.First_Name = Convert.ToString(createfirstname.Text);
                                acc.Second_Name = Convert.ToString(createlastname.Text);
                                acc.Email = emailinput.Text;
                                acc.Mobile = Convert.ToInt32(numberinput.Text);
                                acc.Password = Convert.ToString(createpassword.Text);
                                acc.Photo = ms.ToArray();
                                bse.users.Add(acc);
                                bse.SaveChanges();
                                MessageBox.Show("Account has been created successfully.", "Account Created", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                LogIn login = new LogIn();
                                login.Show();
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Empty Personal Photo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                numberinput.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Invalid Mobile Number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            numberinput.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Enter your full name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        createfirstname.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Passwords don't match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                return;
            }



        }
    }
}
