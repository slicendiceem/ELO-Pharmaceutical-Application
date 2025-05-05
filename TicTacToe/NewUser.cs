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
using System.Data.SQLite;
namespace TicTacToe
{
    public partial class NewUser : KryptonForm
    {
        private SQLiteConnection GetConnection()
        {
            //=========================New Database Path=========================
            string dbPath = Path.Combine(Application.StartupPath, "ELODB.sqlite");
            return new SQLiteConnection($"Data Source={dbPath};Version=3;");
        }

        public NewUser()
        {
            InitializeComponent();

            foreach (Control control in this.Controls)
            {
                if (control is TextBox)
                {
                    control.Enter += (sender, e) => ((TextBox)sender).SelectAll();
                }
                else if (control is KryptonTextBox) // For KryptonTextBox
                {
                    control.Enter += (sender, e) => ((KryptonTextBox)sender).SelectAll();
                }
            }
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
            //-------Database Declaration-------
            ELOEntities bse = new ELOEntities();
            //----------------------------------
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
                ms = new MemoryStream();
                Image img = Image.FromFile(ofd.FileName);
                personalphoto.Image = img;

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
            if(confirmCreation == DialogResult.Yes)
{
                if (confirmpassword.Text == createpassword.Text)
                {
                    if (!string.IsNullOrWhiteSpace(createfirstname.Text) &&
                        !string.IsNullOrWhiteSpace(createlastname.Text) && createlastname.Text != "Last Name" && createfirstname.Text != "First Name")
                    {
                        if (!string.IsNullOrWhiteSpace(emailinput.Text) &&
                            emailinput.Text.Contains("@") &&
                            emailinput.Text.Contains("."))
                        {
                            if (int.TryParse(numberinput.Text, out int mobileNumber) &&
                                numberinput.Text.Length >= 11) // Basic mobile number validation
                            {
                                if (personalphoto.Image != null)
                                {
                                    try
                                    {
                                        //-------Database Declaration-------
                                        using (ELOEntities bse = new ELOEntities())
                                        {
                                            //----------------------------------
                                            user acc = new user();
                                            acc.ID = load_Account();
                                            acc.First_Name = createfirstname.Text.Trim();
                                            acc.Second_Name = createlastname.Text.Trim();
                                            acc.Email = emailinput.Text.Trim();
                                            acc.Mobile = mobileNumber;
                                            acc.Password = createpassword.Text; // Consider hashing this

                                            // Convert image to byte array
                                            using (MemoryStream ms = new MemoryStream())
                                            {
                                                personalphoto.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                                                acc.Photo = ms.ToArray();
                                            }

                                            bse.users.Add(acc);
                                            bse.SaveChanges();

                                            // Reset form
                                            personalphoto.Image = null;

                                            MessageBox.Show("Account created successfully!", "Success",
                                                          MessageBoxButtons.OK, MessageBoxIcon.Information);

                                            LogIn login = new LogIn();
                                            login.Show();
                                            this.Close();
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show($"Error creating account: {ex.Message}", "Error",
                                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Personal photo is required. Please upload a photo.",
                                                  "Photo Required",
                                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    // Optional: Add code to trigger photo upload button click
                                    // btnUploadPhoto.PerformClick();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Please enter a valid 10-digit mobile number.",
                                              "Invalid Mobile Number",
                                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                numberinput.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please enter a valid email address.",
                                          "Invalid Email",
                                          MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            emailinput.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please enter both first and last name.",
                                      "Name Required",
                                      MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        createfirstname.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Passwords do not match. Please re-enter your password.",
                                  "Password Mismatch",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    createpassword.Focus();
                }
            }
        }
    }
}

