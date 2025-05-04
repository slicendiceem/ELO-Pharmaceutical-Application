using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
            
            // TODO: This line of code loads data into the 'eLODataSet.Drug' table. You can move, or remove it, as needed.
            this.drugTableAdapter.Fill(this.eLODataSet.Drug);
            DateTime localDate = DateTime.Now;
            kryptonLabel6.Text = localDate.ToString();
            numreg.Text = kryptonDataGridView1.Rows.Count.ToString();
            label5.Text = kryptonDataGridView2.Rows.Count.ToString();
            

            

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
            dru.Name = AddNameMedicine.Text;
            dru.Manufacturer = AddManufacturer.Text;
            dru.Purpose = AddPurpose.Text;
            if (restricted_y.Checked)
                dru.Restricted = "Yes";
            else
                dru.Restricted = "No";
            dru.Price = int.Parse(AddPrice.Text);
            dru.Sale_Price = int.Parse(AddSalePrice.Text);
            dru.Stock_Amount = int.Parse(kryptonTextBox1.Text);
            dru.Prod = prod_date.Value;
            dru.Exp = exp_date.Value;
            //dru.Photo = ms.ToArray();
            int no;
            var item = dbe.Drug.ToArray();
            no = item.LastOrDefault().ID + 1;
            dru.ID = no;
            dbe.Drug.Add(dru);
            dbe.SaveChanges();
            MessageBox.Show("Drug has been added successfully.", "Drug Added", MessageBoxButtons.OK, MessageBoxIcon.Information);

            AddNameMedicine.Text = AddNameMedicine.Tag.ToString();
            AddManufacturer.Text = AddManufacturer.Tag.ToString();
            AddPurpose.Text = AddPurpose.Tag.ToString();
            AddPrice.Text = AddPrice.Tag.ToString();
            AddSalePrice.Text = AddSalePrice.Tag.ToString();
            kryptonTextBox1.Text = kryptonTextBox1.Tag.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //-------Database Declaration-------
            ELOEntities dbe = new ELOEntities();
            //----------------------------------
            this.drugTableAdapter.Fill(this.eLODataSet.Drug);
            DateTime localDate = DateTime.Now;
            kryptonLabel6.Text = localDate.ToString();
            numreg.Text = kryptonDataGridView1.Rows.Count.ToString();
            label5.Text = kryptonDataGridView2.Rows.Count.ToString();
            
            if (kryptonDataGridView2.Columns.Count == 0)
            {
                foreach (DataGridViewColumn column in kryptonDataGridView1.Columns)
                {
                    kryptonDataGridView2.Columns.Add((DataGridViewColumn)column.Clone());
                }
            }
            for (int i = kryptonDataGridView1.Rows.Count - 1; i >= 0; i--)
            {
                try
                {
                    // Get the expiry date from the correct column (index 8 in this case)
                    var cellValue = kryptonDataGridView1.Rows[i].Cells[8].Value;
                    if (cellValue == null || cellValue == DBNull.Value)
                    {
                        continue; // Skip rows with no expiry date
                    }

                    // Parse the expiry date
                    DateTime expiryDate = Convert.ToDateTime(cellValue.ToString());
                    TimeSpan diffResult = expiryDate - DateTime.Now; // Days until expiry
                    Console.WriteLine($"Row {i}: Days until expiry = {diffResult.TotalDays}");

                    // Check if the expiry date is within the next 3 months (91 days)
                    if (diffResult.TotalDays >= 0 && diffResult.TotalDays <= 91)
                    {
                        // Check for duplicate row in kryptonDataGridView2
                        bool isDuplicate = false;
                        foreach (DataGridViewRow row in kryptonDataGridView2.Rows)
                        {
                            bool rowsMatch = true;
                            for (int j = 0; j < kryptonDataGridView1.Columns.Count; j++)
                            {
                                if (!row.Cells[j].Value?.ToString().Equals(kryptonDataGridView1.Rows[i].Cells[j].Value?.ToString()) ?? true)
                                {
                                    rowsMatch = false;
                                    break;
                                }
                            }

                            if (rowsMatch)
                            {
                                isDuplicate = true;
                                break;
                            }
                        }

                        // Skip adding if duplicate is found
                        if (isDuplicate)
                        {
                            Console.WriteLine($"Duplicate found for row {i}, skipping addition.");
                            continue;
                        }

                        // Add the row to the second DataGridView
                        int newRowIndex = kryptonDataGridView2.Rows.Add();
                        for (int j = 0; j < kryptonDataGridView1.Columns.Count; j++)
                        {
                            kryptonDataGridView2.Rows[newRowIndex].Cells[j].Value = kryptonDataGridView1.Rows[i].Cells[j].Value;
                        }

                        // Remove the row from the first DataGridView
                        kryptonDataGridView1.Rows.RemoveAt(i);

                        // Optional: Notify success for this row
                        Console.WriteLine($"Row {i} successfully moved to kryptonDataGridView2.");
                    }
                    if (kryptonDataGridView2.Rows.Count > 0)
                    {
                        MessageBox.Show("There are Expired Drugs!", "Expired Drugs", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    // Log errors for debugging
                    Console.WriteLine($"Error processing row {i}: {ex.Message}");
                    MessageBox.Show($"Error processing row {i}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            
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
    }
    }
