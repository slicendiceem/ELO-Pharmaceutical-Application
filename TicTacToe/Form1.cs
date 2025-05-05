using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace TicTacToe
{
    public partial class Form1 : KryptonForm
    {
        public Form1()
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

        public static string loggedaccountnum = "";

        MemoryStream ms;
        private void kryptonPage1_Click(object sender, EventArgs e)
        {

        }

        public string loggedaccount
        {
            set { loggedaccountnum = value; }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'eLODataSetExp.Exp_Drug' table. You can move, or remove it, as needed.
            this.exp_DrugTableAdapter.Fill(this.eLODataSetExp.Exp_Drug);

            // TODO: This line of code loads data into the 'eLODataSet.Drug' table. You can move, or remove it, as needed.
            this.drugTableAdapter.Fill(this.eLODataSet.Drug);
            DateTime localDate = DateTime.Now;
            kryptonLabel6.Text = localDate.ToString();
            numreg.Text = kryptonDataGridView1.Rows.Count.ToString();
            label5.Text = kryptonDataGridView2.Rows.Count.ToString();

            //Load user data for profile display
            LoadUserData();
            





        }

        private void createfirstname_TextChanged(object sender, EventArgs e)
        {

        }

      
        private void AddStockAmount_Click(object sender, EventArgs e)
        {

        }

        private void kryptonTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void kryptonLabel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void kryptonGroupBox1_Panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void med_photoadd_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Image img = Image.FromFile(ofd.FileName);
                pictureBox1.Image = img;
                ms = new MemoryStream();
                img.Save(ms, img.RawFormat);
            }
        }

        private void add_medicine_Click(object sender, EventArgs e)
        {
            //-------Database Declaration-------
            ELOEntities dbe = new ELOEntities();
            //----------------------------------
            Drug dru = new Drug();
            // Validate Name
            if (string.IsNullOrWhiteSpace(kryptonTextBox2.Text) && kryptonTextBox2.Text == "Name")
            {
                KryptonMessageBox.Show("Drug name cannot be empty", "Validation Error",
                                     MessageBoxButtons.OK, MessageBoxIcon.Warning);
                kryptonTextBox2.Focus();
            }
            else
            {
                // Validate Manufacturer
                if (string.IsNullOrWhiteSpace(AddManufacturer.Text) && AddManufacturer.Text == "Manufacturer")
                {
                    KryptonMessageBox.Show("Manufacturer cannot be empty", "Validation Error",
                                         MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    AddManufacturer.Focus();
                }
                else
                {
                    // Validate Purpose
                    if (string.IsNullOrWhiteSpace(AddPurpose.Text) && AddPurpose.Text == "Purpose")
                    {
                        KryptonMessageBox.Show("Purpose cannot be empty", "Validation Error",
                                             MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        AddPurpose.Focus();
                    }
                    else
                    {
                        // Validate Price
                        if (!int.TryParse(AddPrice.Text, out int price) || price <= 0)
                        {
                            KryptonMessageBox.Show("Enter valid positive price", "Validation Error",
                                                 MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            AddPrice.Focus();
                        }
                        else
                        {
                            // Validate Sale Price
                            if (!int.TryParse(AddSalePrice.Text, out int salePrice) || salePrice <= 0)
                            {
                                KryptonMessageBox.Show("Enter valid positive sale price", "Validation Error",
                                                     MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                AddSalePrice.Focus();
                            }
                            else
                            {
                                // Validate Stock
                                if (!int.TryParse(kryptonTextBox1.Text, out int stock) || stock < 0)
                                {
                                    KryptonMessageBox.Show("Enter valid stock amount", "Validation Error",
                                                         MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    kryptonTextBox1.Focus();
                                }
                                else
                                {
                                    // Validate Dates
                                    if (prod_date.Value >= exp_date.Value)
                                    {
                                        KryptonMessageBox.Show("Expiry date must be after production", "Validation Error",
                                                             MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        exp_date.Focus();
                                    }
                                    else
                                    {
                                        // ALL VALIDATIONS PASSED - Create drug object
                                        Drug drug = new Drug();
                                        dru.Name = kryptonTextBox2.Text.Trim();
                                        dru.Manufacturer = AddManufacturer.Text.Trim();
                                        dru.Purpose = AddPurpose.Text.Trim();
                                        dru.Restricted = restricted_y.Checked ? "Yes" : "No";
                                        dru.Price = price;
                                        dru.Sale_Price = salePrice;
                                        dru.Stock_Amount = stock;
                                        dru.Prod = prod_date.Value;
                                        dru.Exp = exp_date.Value;
                                        int no;
                                        var item = dbe.Drug.ToArray();
                                        no = item.LastOrDefault().ID + 1;
                                        dru.ID = no;
                                        dbe.Drug.Add(dru);
                                        dbe.SaveChanges();

                                        // Proceed with saving the drug
                                        KryptonMessageBox.Show("Drug validated successfully!", "Success",
                                                             MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        AddNameMedicine.Text = AddNameMedicine.Tag.ToString();
                                        AddManufacturer.Text = AddManufacturer.Tag.ToString();
                                        AddPurpose.Text = AddPurpose.Tag.ToString();
                                        AddPrice.Text = AddPrice.Tag.ToString();
                                        AddSalePrice.Text = AddSalePrice.Tag.ToString();
                                        kryptonTextBox1.Text = kryptonTextBox1.Tag.ToString();
                                        pictureBox1.Image = Properties.Resources.Caduceus;
                                    }
                                }
                            }
                        }
                    }
                }
            }


            
            
        }

        private ELOEntities db = new ELOEntities();
        private ELODataSet ds = new ELODataSet();
        private ELODataSetTableAdapters.DrugTableAdapter drugTA = new ELODataSetTableAdapters.DrugTableAdapter();
        private ELODataSetTableAdapters.Exp_DrugTableAdapter expDrugTA = new ELODataSetTableAdapters.Exp_DrugTableAdapter();

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                // 1. Initialize fresh DataSet
                var ds = new ELODataSet();

                // 2. Load data
                drugTA.Fill(ds.Drug);
                expDrugTA.Fill(ds.Exp_Drug);

                // 3. Find drugs to transfer (expiring within 91 days)
                var expiryThreshold = DateTime.Today.AddDays(91);
                var drugsToTransfer = ds.Drug
                    .Where(d => !d.IsExpNull() && d.Exp <= expiryThreshold && !ds.Exp_Drug.Any(x => x.ID == d.ID))
                    .ToList();

                if (drugsToTransfer.Count > 0)
                {
                    // 4. Process each drug
                    foreach (var drug in drugsToTransfer)
                    {
                        // Add to expired table
                        var newExpDrug = ds.Exp_Drug.NewExp_DrugRow();
                        newExpDrug.ID = drug.ID;
                        newExpDrug.Name = drug.Name;
                        newExpDrug.Manufacturer = drug.Manufacturer;
                        newExpDrug.Purpose = drug.Purpose;
                        newExpDrug.Stock_Amount = drug.Stock_Amount;
                        newExpDrug.Prod = drug.Prod;
                        newExpDrug.Exp = drug.Exp;
                        ds.Exp_Drug.AddExp_DrugRow(newExpDrug);

                        // Remove from active table
                        drug.Delete();
                    }

                    // 5. Update database - FIRST delete from Drug, THEN add to Exp_Drug
                    drugTA.Update(ds.Drug);       // Deletes removed rows
                    expDrugTA.Update(ds.Exp_Drug); // Adds new rows

                    // 6. Refresh data
                    ds.Clear();
                    drugTA.Fill(ds.Drug);
                    expDrugTA.Fill(ds.Exp_Drug);

                    // 7. Update UI
                    kryptonDataGridView1.DataSource = ds.Drug;
                    kryptonDataGridView2.DataSource = ds.Exp_Drug;

                    KryptonMessageBox.Show($"Transferred {drugsToTransfer.Count} drugs to expired table.",
                                         "Success",
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Information);
                }
                else
                {
                    KryptonMessageBox.Show("No expiring drugs found to transfer.",
                                         "Info",
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Information);
                }

                UpdateCounters();
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show($"Transfer failed: {ex.Message}\n\n{ex.InnerException?.Message}",
                                     "Error",
                                     MessageBoxButtons.OK,
                                     MessageBoxIcon.Error);
                Debug.WriteLine($"ERROR: {ex}");
            }
        }

        private void UpdateCounters()
        {
            numreg.Text = ds.Drug.Count.ToString();
            label5.Text = ds.Exp_Drug.Count.ToString();
            kryptonLabel6.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            exp_DrugTableAdapter.Fill(eLODataSetExp.Exp_Drug);
            drugTableAdapter.Fill(eLODataSet.Drug);
            numreg.Text = kryptonDataGridView1.Rows.Count.ToString();
            label5.Text = kryptonDataGridView2.Rows.Count.ToString();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            
        }

        private void kryptonLabel7_Paint(object sender, PaintEventArgs e)
        {


        }

        private void kryptonPalette1_PalettePaint(object sender, PaletteLayoutEventArgs e)
        {

        }

        private void drugBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void kryptonGroupBox1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void restricted_n_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void restricted_y_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void AddPurpose_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void kryptonLabel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void AddNameMedicine_TextChanged(object sender, EventArgs e)
        {

        }

        private void AddManufacturer_TextChanged(object sender, EventArgs e)
        {

        }

        private void AddPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void AddSalePrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void exp_date_ValueChanged(object sender, EventArgs e)
        {

        }

        private void prod_date_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void kryptonDataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void numreg_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void kryptonPage2_Click(object sender, EventArgs e)
        {

        }

        private void kryptonLabel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void kryptonPage1_Click_1(object sender, EventArgs e)
        {

        }

        private void kryptonLabel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void kryptonLabel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void kryptonDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void kryptonLabel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            loggedaccountnum = "";
            LogIn frm = new LogIn();
            frm.Show();
            this.Hide();
        }

        private void LoadUserData()
        {
            try
            {
                using (ELOEntities bse = new ELOEntities())
                {
                    // Retrieve the user with the specified ID
                    user userData = bse.users.FirstOrDefault(u => u.Email == loggedaccountnum);

                    if (userData != null)
                    {
                        // Display data in labels
                        LabelFullName.Text = $"{userData.First_Name} {userData.Second_Name}!";
                        lblEmail.Text = userData.Email;
                        lblMobile.Text = userData.Mobile.ToString();
                        lblID.Text = userData.ID.ToString();
                        // Display photo if available
                        if (userData.Photo != null && userData.Photo.Length > 0)
                        {
                            using (MemoryStream ms = new MemoryStream(userData.Photo))
                            {
                                pictureBoxProfile.Image = Image.FromStream(ms);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("User not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading user data: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Example of how to call this form from another form (like after login):
        // UserProfileForm profileForm = new UserProfileForm(loggedInUserId);
        // profileForm.Show();
    }
}
