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
        public static string loggedrole = "";
        public static int loggeduserid = 0;

        MemoryStream ms;
        private void kryptonPage1_Click(object sender, EventArgs e)
        {

        }

        public string loggedaccount
        {
            set { loggedaccountnum = value; }
        }

        public string loggedroleProperty
        {
            set { loggedrole = value; }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Resize admin tab icon to match the 24x24 used by other tabs
            adminPage.ImageSmall = new System.Drawing.Bitmap(Properties.Resources.Add_User_Male, new System.Drawing.Size(24, 24));

            // Ensure process exits cleanly when Form1 closes (X button or logout)
            this.FormClosing += (s, fe) => { if (fe.CloseReason != CloseReason.ApplicationExitCall) Application.Exit(); };

            // Load user data for profile display
            LoadUserData();

            // Attach row painters for low-stock / colour-coded expiry
            kryptonDataGridView1.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(Grid1_RowPrePaint);
            kryptonDataGridView2.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(Grid2_RowPrePaint);

            // Wire expand-collapse click for Medicine List grid
            kryptonDataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(Grid1_CellClick);

            // Reset Add Medicine tab to mode-select whenever it is navigated to
            kryptonNavigator1.SelectedPageChanged += new System.EventHandler(navigator1_SelectedPageChanged);

            // Initialise Add Medicine tab in mode-select state
            SetAddMode(0);

            // Initialise cashier cart and grid
            InitCartTable();
            RefreshCashierMeds("");

            // Populate grids from SQLite
            RefreshGrids();

            // Apply role-based tab visibility after the form is fully rendered
            this.Shown += new System.EventHandler(Form1_Shown);
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            // Unhook so it only fires once
            this.Shown -= new System.EventHandler(Form1_Shown);
            ApplyRolePermissions();
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
            Drug dru = new Drug();
            int price = 0, salePrice = 0, stock = 0;
            // Validate Name
            if (string.IsNullOrWhiteSpace(kryptonTextBox2.Text) || kryptonTextBox2.Text.Trim() == "Name")
            {
                KryptonMessageBox.Show("Drug name cannot be empty", "Validation Error",
                                     MessageBoxButtons.OK, MessageBoxIcon.Warning);
                kryptonTextBox2.Focus();
            }
            else
            {
                // Validate Manufacturer
                if (string.IsNullOrWhiteSpace(AddManufacturer.Text) || AddManufacturer.Text.Trim() == "Manufacturer")
                {
                    KryptonMessageBox.Show("Manufacturer cannot be empty", "Validation Error",
                                         MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    AddManufacturer.Focus();
                }
                else
                {
                    // Validate Purpose
                    if (string.IsNullOrWhiteSpace(AddPurpose.Text) || AddPurpose.Text.Trim() == "Purpose")
                    {
                        KryptonMessageBox.Show("Purpose cannot be empty", "Validation Error",
                                             MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        AddPurpose.Focus();
                    }
                    else
                    {
                        // Validate Price
                        if (!int.TryParse(AddPrice.Text, out price) || price <= 0)
                        {
                            KryptonMessageBox.Show("Enter valid positive price", "Validation Error",
                                                 MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            AddPrice.Focus();
                        }
                        else
                        {
                            // Validate Sale Price
                            if (!int.TryParse(AddSalePrice.Text, out salePrice) || salePrice <= 0)
                            {
                                KryptonMessageBox.Show("Enter valid positive sale price", "Validation Error",
                                                     MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                AddSalePrice.Focus();
                            }
                            else
                            {
                                // Validate Stock
                                if (!int.TryParse(kryptonTextBox1.Text, out stock) || stock < 0)
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
                                        // ALL VALIDATIONS PASSED - build drug object
                                        dru.Name = kryptonTextBox2.Text.Trim();
                                        dru.Manufacturer = AddManufacturer.Text.Trim();
                                        dru.Purpose = AddPurpose.Text.Trim();
                                        dru.Restricted = restricted_y.Checked ? "Yes" : "No";
                                        dru.Price = price;
                                        dru.Sale_Price = salePrice;
                                        dru.Stock_Amount = stock;
                                        dru.Prod = prod_date.Value;
                                        dru.Exp = exp_date.Value;
                                        dru.Photo = ms != null ? ms.ToArray() : null;

                                        // Check for existing drugs with the same name
                                        System.Data.DataTable existing = DatabaseHelper.GetDrugsByName(dru.Name);
                                        if (existing.Rows.Count > 0)
                                        {
                                            string inputProd = dru.Prod.Value.ToString("yyyy-MM-dd");
                                            string inputExp = dru.Exp.Value.ToString("yyyy-MM-dd");
                                            int exactId = -1;
                                            int exactStock = 0;
                                            foreach (System.Data.DataRow row in existing.Rows)
                                            {
                                                string dbProd = row["Prod"] == System.DBNull.Value ? "" : row["Prod"].ToString();
                                                string dbExp = row["Exp"] == System.DBNull.Value ? "" : row["Exp"].ToString();
                                                if (dbProd.Length >= 10) dbProd = dbProd.Substring(0, 10);
                                                if (dbExp.Length >= 10) dbExp = dbExp.Substring(0, 10);
                                                if (dbProd == inputProd && dbExp == inputExp)
                                                {
                                                    exactId = Convert.ToInt32(row["ID"]);
                                                    exactStock = row["Stock_Amount"] == System.DBNull.Value ? 0 : Convert.ToInt32(row["Stock_Amount"]);
                                                    break;
                                                }
                                            }
                                            if (exactId != -1)
                                            {
                                                // Same name AND same dates → just increment stock
                                                DatabaseHelper.UpdateDrugStock(exactId, stock);
                                                RefreshGrids();
                                                KryptonMessageBox.Show(
                                                    string.Format("Stock updated! '{0}' (Prod: {1} | Exp: {2}) now has {3} units.",
                                                        dru.Name, inputProd, inputExp, exactStock + stock),
                                                    "Stock Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                ResetAddForm();
                                            }
                                            else
                                            {
                                                // Same name, different dates → show batch picker
                                                _pendingDrug = dru;
                                                ShowDupPanel(existing);
                                            }
                                        }
                                        else
                                        {
                                            // No existing drug with this name → insert as new
                                            DatabaseHelper.AddDrug(dru);
                                            RefreshGrids();
                                            KryptonMessageBox.Show("Drug added successfully!", "Success",
                                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            ResetAddForm();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }




        }

        private ELODataSet ds = new ELODataSet();

        private Drug _pendingDrug;
        private int[] _dupBatchIds;
        private System.Collections.Generic.Dictionary<string, bool> _expandedGroups = new System.Collections.Generic.Dictionary<string, bool>(System.StringComparer.OrdinalIgnoreCase);
        private System.Data.DataTable _lastFlatDrugs;
        private System.Data.DataTable _lastGroupedTable;

        private void ResetAddForm()
        {
            kryptonTextBox2.Text = kryptonTextBox2.Tag != null ? kryptonTextBox2.Tag.ToString() : "Name";
            AddNameMedicine.Text = AddNameMedicine.Tag != null ? AddNameMedicine.Tag.ToString() : "";
            AddManufacturer.Text = AddManufacturer.Tag != null ? AddManufacturer.Tag.ToString() : "";
            AddPurpose.Text = AddPurpose.Tag != null ? AddPurpose.Tag.ToString() : "";
            AddPrice.Text = AddPrice.Tag != null ? AddPrice.Tag.ToString() : "";
            AddSalePrice.Text = AddSalePrice.Tag != null ? AddSalePrice.Tag.ToString() : "";
            kryptonTextBox1.Text = kryptonTextBox1.Tag != null ? kryptonTextBox1.Tag.ToString() : "";
            pictureBox1.Image = Properties.Resources.Caduceus;
            ms = null;
        }

        private void ShowDupPanel(System.Data.DataTable batches)
        {
            lstDupBatches.Items.Clear();
            _dupBatchIds = new int[batches.Rows.Count];
            for (int i = 0; i < batches.Rows.Count; i++)
            {
                System.Data.DataRow row = batches.Rows[i];
                _dupBatchIds[i] = Convert.ToInt32(row["ID"]);
                string prod = row["Prod"] == System.DBNull.Value ? "N/A" : row["Prod"].ToString();
                string exp = row["Exp"] == System.DBNull.Value ? "N/A" : row["Exp"].ToString();
                int stock = row["Stock_Amount"] == System.DBNull.Value ? 0 : Convert.ToInt32(row["Stock_Amount"]);
                if (prod.Length > 10) prod = prod.Substring(0, 10);
                if (exp.Length > 10) exp = exp.Substring(0, 10);
                lstDupBatches.Items.Add(string.Format("Prod: {0}   Exp: {1}   Stock: {2}", prod, exp, stock));
            }
            if (lstDupBatches.Items.Count > 0)
                lstDupBatches.SelectedIndex = 0;
            lblDupTitle.Text = string.Format(
                "'{0}' already exists in {1} batch(es) with different dates.\nSelect a batch to add {2} unit(s) to, or add this as a new separate batch:",
                _pendingDrug.Name, batches.Rows.Count, _pendingDrug.Stock_Amount.GetValueOrDefault());
            pnlDupWarning.Visible = true;
            pnlDupWarning.BringToFront();
        }

        private void btnAddToSelected_Click(object sender, EventArgs e)
        {
            int idx = lstDupBatches.SelectedIndex;
            if (idx < 0 || _dupBatchIds == null || idx >= _dupBatchIds.Length)
            {
                KryptonMessageBox.Show("Please select a batch from the list.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int addAmount = _pendingDrug.Stock_Amount.GetValueOrDefault();
            DatabaseHelper.UpdateDrugStock(_dupBatchIds[idx], addAmount);
            RefreshGrids();
            pnlDupWarning.Visible = false;
            KryptonMessageBox.Show(
                string.Format("Added {0} unit(s) to the selected batch of '{1}'.", addAmount, _pendingDrug.Name),
                "Stock Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ResetAddForm();
            _pendingDrug = null;
        }

        private void btnAddAsNew_Click(object sender, EventArgs e)
        {
            if (_pendingDrug == null) return;
            DatabaseHelper.AddDrug(_pendingDrug);
            RefreshGrids();
            pnlDupWarning.Visible = false;
            KryptonMessageBox.Show("Drug added as a new batch successfully!", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            ResetAddForm();
            _pendingDrug = null;
        }

        private void btnDupCancel_Click(object sender, EventArgs e)
        {
            pnlDupWarning.Visible = false;
            _pendingDrug = null;
        }

        private void navigator1_SelectedPageChanged(object sender, System.EventArgs e)
        {
            if (kryptonNavigator1.SelectedIndex == 0)
                SetAddMode(0);
            if (kryptonNavigator1.SelectedPage == cashierPage)
                RefreshCashierMeds(txtCashierSearch.Text);
            if (kryptonNavigator1.SelectedPage == adminPage)
                LoadAdminUserGrid();
        }

        // ── Add Medicine tab: two-mode UI ──────────────────────────────────────

        private void SetAddMode(int mode)
        {
            // mode 0 = selection screen, 1 = add new, 2 = add batch to existing
            pnlModeSelect.Visible = (mode == 0);

            bool showNew = (mode == 1);
            kryptonTextBox2.Visible = showNew;
            AddManufacturer.Visible = showNew;
            AddPurpose.Visible = showNew;
            kryptonGroupBox1.Visible = showNew;
            med_photoadd.Visible = showNew;
            pictureBox1.Visible = showNew;
            AddPrice.Visible = showNew;
            AddSalePrice.Visible = showNew;
            kryptonTextBox1.Visible = showNew;
            label1.Visible = showNew;
            label2.Visible = showNew;
            prod_date.Visible = showNew;
            exp_date.Visible = showNew;
            add_medicine.Visible = showNew;
            AddNameMedicine.Visible = showNew;
            btnAddNewBack.Visible = showNew;
            if (mode == 1) pnlDupWarning.Visible = false;

            pnlAddBatch.Visible = (mode == 2);
            if (mode == 2)
                LoadBatchDrugCombo();
        }

        private void LoadBatchDrugCombo()
        {
            cboBatchDrug.Items.Clear();
            var names = DatabaseHelper.GetDrugNames();
            foreach (string n in names)
                cboBatchDrug.Items.Add(n);
            if (cboBatchDrug.Items.Count > 0)
                cboBatchDrug.SelectedIndex = 0;
        }

        private void btnModeNew_Click(object sender, System.EventArgs e)
        {
            SetAddMode(1);
        }

        private void btnModeBatch_Click(object sender, System.EventArgs e)
        {
            SetAddMode(2);
        }

        private void btnAddNewBack_Click(object sender, System.EventArgs e)
        {
            SetAddMode(0);
        }

        private void btnBatchBack_Click(object sender, System.EventArgs e)
        {
            SetAddMode(0);
        }

        // ── Cashier tab ────────────────────────────────────────────────────
        private System.Data.DataTable _cartTable;

        private void InitCartTable()
        {
            _cartTable = new System.Data.DataTable();
            _cartTable.Columns.Add("_ID", typeof(int));
            _cartTable.Columns.Add("Name", typeof(string));
            _cartTable.Columns.Add("Qty", typeof(int));
            _cartTable.Columns.Add("UnitPrice", typeof(decimal));
            _cartTable.Columns.Add("Subtotal", typeof(decimal));
            dgvCart.DataSource = _cartTable;
        }

        private void RefreshCashierMeds(string term)
        {
            string search = (term == (string)txtCashierSearch.Tag) ? "" : term;
            dgvCashierMeds.DataSource = DatabaseHelper.GetDrugsForCashier(search);
        }

        private void UpdateCartTotal()
        {
            decimal total = 0m;
            if (_cartTable != null)
                foreach (System.Data.DataRow r in _cartTable.Rows)
                    total += r["Subtotal"] == System.DBNull.Value ? 0m : System.Convert.ToDecimal(r["Subtotal"]);
            lblCartTotal.Text = string.Format("EGP {0:0.00}", total);
        }

        private void txtCashierSearch_TextChanged(object sender, System.EventArgs e)
        {
            RefreshCashierMeds(txtCashierSearch.Text);
        }

        private void dgvCashierMeds_CellDoubleClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            AddSelectedMedToCart();
        }

        private void btnAddToCart_Click(object sender, System.EventArgs e)
        {
            AddSelectedMedToCart();
        }

        private void AddSelectedMedToCart()
        {
            if (dgvCashierMeds.SelectedRows.Count == 0) return;
            System.Windows.Forms.DataGridViewRow row = dgvCashierMeds.SelectedRows[0];
            int drugId = System.Convert.ToInt32(row.Cells["colCashierID"].Value);
            string name = row.Cells["colCashierName"].Value == null ? "" : row.Cells["colCashierName"].Value.ToString();
            int inStock = row.Cells["colCashierStock"].Value == null ? 0 : System.Convert.ToInt32(row.Cells["colCashierStock"].Value);
            decimal unitPrice = row.Cells["colCashierPrice"].Value == null || row.Cells["colCashierPrice"].Value == System.DBNull.Value
                ? 0m : System.Convert.ToDecimal(row.Cells["colCashierPrice"].Value);
            int qty = (int)nudCashierQty.Value;

            // Find if same drug already in cart
            int cartIdx = -1;
            for (int i = 0; i < _cartTable.Rows.Count; i++)
                if (System.Convert.ToInt32(_cartTable.Rows[i]["_ID"]) == drugId) { cartIdx = i; break; }

            int existingQty = cartIdx >= 0 ? System.Convert.ToInt32(_cartTable.Rows[cartIdx]["Qty"]) : 0;
            if (existingQty + qty > inStock)
            {
                KryptonMessageBox.Show(
                    string.Format("Not enough stock. In stock: {0}, already in cart: {1}.", inStock, existingQty),
                    "Insufficient Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cartIdx >= 0)
            {
                int newQty = existingQty + qty;
                _cartTable.Rows[cartIdx]["Qty"] = newQty;
                _cartTable.Rows[cartIdx]["Subtotal"] = unitPrice * newQty;
            }
            else
            {
                _cartTable.Rows.Add(drugId, name, qty, unitPrice, unitPrice * qty);
            }

            nudCashierQty.Value = 1;
            UpdateCartTotal();
        }

        private void btnRemoveCartItem_Click(object sender, System.EventArgs e)
        {
            if (dgvCart.SelectedRows.Count == 0) return;
            int rowIdx = dgvCart.SelectedRows[0].Index;
            if (rowIdx >= 0 && rowIdx < _cartTable.Rows.Count)
                _cartTable.Rows.RemoveAt(rowIdx);
            UpdateCartTotal();
        }

        private void btnClearCart_Click(object sender, System.EventArgs e)
        {
            if (_cartTable.Rows.Count == 0) return;
            if (KryptonMessageBox.Show("Clear all items from the cart?", "Clear Cart",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _cartTable.Clear();
                UpdateCartTotal();
            }
        }

        private void btnCheckout_Click(object sender, System.EventArgs e)
        {
            if (_cartTable == null || _cartTable.Rows.Count == 0)
            {
                KryptonMessageBox.Show("The cart is empty.", "Checkout", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            decimal total = 0m;
            foreach (System.Data.DataRow r in _cartTable.Rows)
                total += System.Convert.ToDecimal(r["Subtotal"]);

            var sb = new System.Text.StringBuilder();
            sb.AppendLine("Receipt:");
            sb.AppendLine(new string('-', 38));
            foreach (System.Data.DataRow r in _cartTable.Rows)
                sb.AppendLine(string.Format("{0,-22} x{1,3}   EGP {2,7:0.00}",
                    r["Name"].ToString().Length > 22 ? r["Name"].ToString().Substring(0, 22) : r["Name"].ToString(),
                    r["Qty"], r["Subtotal"]));
            sb.AppendLine(new string('-', 38));
            sb.AppendLine(string.Format("TOTAL:                    EGP {0,7:0.00}", total));

            if (KryptonMessageBox.Show(sb.ToString() + "\n\nConfirm sale and deduct stock?",
                    "Confirm Checkout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            foreach (System.Data.DataRow r in _cartTable.Rows)
                DatabaseHelper.DeductDrugStock(System.Convert.ToInt32(r["_ID"]), System.Convert.ToInt32(r["Qty"]));

            _cartTable.Clear();
            UpdateCartTotal();
            RefreshGrids();
            RefreshCashierMeds(txtCashierSearch.Text);
            KryptonMessageBox.Show("Sale completed successfully!", "Checkout", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnBatchAdd_Click(object sender, System.EventArgs e)
        {
            string medName = cboBatchDrug.SelectedItem != null ? cboBatchDrug.SelectedItem.ToString() : "";
            if (string.IsNullOrWhiteSpace(medName))
            {
                KryptonMessageBox.Show("Please select a medicine.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dtpBatchProd.Value.Date >= dtpBatchExp.Value.Date)
            {
                KryptonMessageBox.Show("Expiry date must be after production date.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int stock;
            if (!int.TryParse(txtBatchStock.Text.Trim(), out stock) || stock <= 0)
            {
                KryptonMessageBox.Show("Enter a valid positive stock amount.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string inputProd = dtpBatchProd.Value.ToString("yyyy-MM-dd");
            string inputExp = dtpBatchExp.Value.ToString("yyyy-MM-dd");

            // Check for an existing batch with the same name + same dates
            System.Data.DataTable existing = DatabaseHelper.GetDrugsByName(medName);
            int exactId = -1;
            int exactStock = 0;
            foreach (System.Data.DataRow r in existing.Rows)
            {
                string dbProd = r["Prod"] == System.DBNull.Value ? "" : r["Prod"].ToString();
                string dbExp = r["Exp"] == System.DBNull.Value ? "" : r["Exp"].ToString();
                if (dbProd.Length >= 10) dbProd = dbProd.Substring(0, 10);
                if (dbExp.Length >= 10) dbExp = dbExp.Substring(0, 10);
                if (string.Equals(dbProd, inputProd) && string.Equals(dbExp, inputExp))
                {
                    exactId = System.Convert.ToInt32(r["ID"]);
                    exactStock = r["Stock_Amount"] == System.DBNull.Value ? 0 : System.Convert.ToInt32(r["Stock_Amount"]);
                    break;
                }
            }

            if (exactId != -1)
            {
                // Same batch — merge stock
                DatabaseHelper.UpdateDrugStock(exactId, stock);
                RefreshGrids();
                KryptonMessageBox.Show(
                    string.Format("Stock updated. '{0}' (Prod: {1} | Exp: {2}) now has {3} units.",
                        medName, inputProd, inputExp, exactStock + stock),
                    "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtBatchStock.Text = "";
            }
            else
            {
                // New batch with different dates — copy master details from first existing row
                string msg = string.Format(
                    "A new batch of '{0}' will be added.\nProd: {1}  Exp: {2}  Stock: {3}" +
                    "\n\nManufacturer and other details will be copied from the existing record.\nProceed?",
                    medName, inputProd, inputExp, stock);
                if (KryptonMessageBox.Show(msg, "Add New Batch",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    System.Data.DataTable full = DatabaseHelper.GetDrugFullByName(medName);
                    var d = new Drug();
                    d.Name = medName;
                    if (full.Rows.Count > 0)
                    {
                        System.Data.DataRow src = full.Rows[0];
                        d.Manufacturer = src["Manufacturer"] == System.DBNull.Value ? null : src["Manufacturer"].ToString();
                        d.Purpose = src["Purpose"] == System.DBNull.Value ? null : src["Purpose"].ToString();
                        d.Restricted = src["Restricted"] == System.DBNull.Value ? "No" : src["Restricted"].ToString();
                        d.Price = src["Price"] == System.DBNull.Value ? (decimal?)null : System.Convert.ToDecimal(src["Price"]);
                        d.Sale_Price = src["Sale_Price"] == System.DBNull.Value ? (decimal?)null : System.Convert.ToDecimal(src["Sale_Price"]);
                    }
                    else
                    {
                        d.Restricted = "No";
                    }
                    d.Stock_Amount = stock;
                    d.Prod = dtpBatchProd.Value.Date;
                    d.Exp = dtpBatchExp.Value.Date;
                    DatabaseHelper.AddDrug(d);
                    RefreshGrids();
                    KryptonMessageBox.Show("New batch added successfully!", "Done",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtBatchStock.Text = "";
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                DatabaseHelper.TransferExpiredDrugs();
                RefreshGrids();
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show("Transfer failed: " + ex.Message,
                                     "Error",
                                     MessageBoxButtons.OK,
                                     MessageBoxIcon.Error);
                Debug.WriteLine("ERROR: " + ex);
            }
        }

        private void RefreshGrids()
        {
            ApplyGroupedMedList("");
            expDrugBindingSource.DataMember = null;
            expDrugBindingSource.DataSource = DatabaseHelper.GetExpDrugs();
            UpdateCounters();
        }

        private void RefreshFilteredGrids()
        {
            string medTerm = txtSearchMedicine.Text == (string)txtSearchMedicine.Tag ? "" : txtSearchMedicine.Text;
            string expTerm = txtSearchExpired.Text == (string)txtSearchExpired.Tag ? "" : txtSearchExpired.Text;
            ApplyGroupedMedList(medTerm);
            expDrugBindingSource.DataMember = null;
            expDrugBindingSource.DataSource = DatabaseHelper.SearchExpDrugs(expTerm);
            UpdateCounters();
        }

        private void ApplyGroupedMedList(string searchTerm)
        {
            _lastFlatDrugs = DatabaseHelper.SearchDrugs(searchTerm);
            _lastGroupedTable = BuildGroupedMedTable(_lastFlatDrugs);
            kryptonDataGridView1.DataSource = null;
            kryptonDataGridView1.DataSource = _lastGroupedTable;
        }

        private System.Data.DataTable BuildGroupedMedTable(System.Data.DataTable flat)
        {
            var display = new System.Data.DataTable();
            display.Columns.Add("_RowType", typeof(string));
            display.Columns.Add("_GroupName", typeof(string));
            display.Columns.Add("_Expand", typeof(string));
            display.Columns.Add("Name", typeof(string));
            display.Columns.Add("Manufacturer", typeof(string));
            display.Columns.Add("Purpose", typeof(string));
            display.Columns.Add("Restricted", typeof(string));
            display.Columns.Add("Price", typeof(object));
            display.Columns.Add("Sale_Price", typeof(object));
            display.Columns.Add("Stock_Amount", typeof(object));
            display.Columns.Add("Prod", typeof(string));
            display.Columns.Add("Exp", typeof(string));
            display.Columns.Add("TimeLeft", typeof(string));

            var groups = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<System.Data.DataRow>>(System.StringComparer.OrdinalIgnoreCase);
            var order = new System.Collections.Generic.List<string>();
            foreach (System.Data.DataRow r in flat.Rows)
            {
                string n = r["Name"] == System.DBNull.Value ? "" : r["Name"].ToString();
                if (!groups.ContainsKey(n)) { groups[n] = new System.Collections.Generic.List<System.Data.DataRow>(); order.Add(n); }
                groups[n].Add(r);
            }

            foreach (string name in order)
            {
                var batches = groups[name];
                if (batches.Count > 1)
                {
                    bool expanded = _expandedGroups.ContainsKey(name) && _expandedGroups[name];
                    int totalStock = 0;
                    foreach (System.Data.DataRow r in batches)
                        if (r["Stock_Amount"] != System.DBNull.Value)
                            totalStock += System.Convert.ToInt32(r["Stock_Amount"]);
                    // Find nearest expiry across all batches for the header label
                    string nearestExpStr = "";
                    DateTime nearestExpDate = DateTime.MaxValue;
                    foreach (System.Data.DataRow rb in batches)
                    {
                        if (rb["Exp"] != System.DBNull.Value)
                        {
                            string es = rb["Exp"].ToString();
                            if (es.Length > 10) es = es.Substring(0, 10);
                            DateTime ed;
                            if (DateTime.TryParse(es, out ed) && ed < nearestExpDate)
                            {
                                nearestExpDate = ed;
                                nearestExpStr = es;
                            }
                        }
                    }
                    display.Rows.Add("header", name, expanded ? "\u25BC" : "\u25BA", name,
                        batches[0]["Manufacturer"], batches[0]["Purpose"], batches[0]["Restricted"],
                        batches[0]["Price"], batches[0]["Sale_Price"],
                        totalStock, "(" + batches.Count.ToString() + " batches)", "",
                        DatabaseHelper.FormatTimeLeft(nearestExpStr));
                    if (expanded)
                    {
                        foreach (System.Data.DataRow r in batches)
                        {
                            string prod = r["Prod"] == System.DBNull.Value ? "" : r["Prod"].ToString();
                            string exp = r["Exp"] == System.DBNull.Value ? "" : r["Exp"].ToString();
                            if (prod.Length > 10) prod = prod.Substring(0, 10);
                            if (exp.Length > 10) exp = exp.Substring(0, 10);
                            int s = r["Stock_Amount"] == System.DBNull.Value ? 0 : System.Convert.ToInt32(r["Stock_Amount"]);
                            display.Rows.Add("detail", name, "", "    \u21B3  " + name,
                                r["Manufacturer"], r["Purpose"], r["Restricted"],
                                r["Price"], r["Sale_Price"], s, prod, exp,
                                DatabaseHelper.FormatTimeLeft(exp));
                        }
                    }
                }
                else
                {
                    System.Data.DataRow r = batches[0];
                    string prod = r["Prod"] == System.DBNull.Value ? "" : r["Prod"].ToString();
                    string exp = r["Exp"] == System.DBNull.Value ? "" : r["Exp"].ToString();
                    if (prod.Length > 10) prod = prod.Substring(0, 10);
                    if (exp.Length > 10) exp = exp.Substring(0, 10);
                    display.Rows.Add("single", name, "", r["Name"],
                        r["Manufacturer"], r["Purpose"], r["Restricted"],
                        r["Price"], r["Sale_Price"], r["Stock_Amount"], prod, exp,
                        DatabaseHelper.FormatTimeLeft(exp));
                }
            }
            return display;
        }

        private void Grid1_CellClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (_lastGroupedTable == null || !_lastGroupedTable.Columns.Contains("_RowType") || e.RowIndex >= _lastGroupedTable.Rows.Count) return;
            System.Data.DataRow dataRow = _lastGroupedTable.Rows[e.RowIndex];
            if (dataRow["_RowType"].ToString() != "header") return;
            string groupName = dataRow["_GroupName"].ToString();
            bool current = _expandedGroups.ContainsKey(groupName) && _expandedGroups[groupName];
            _expandedGroups[groupName] = !current;
            _lastGroupedTable = BuildGroupedMedTable(_lastFlatDrugs);
            kryptonDataGridView1.DataSource = null;
            kryptonDataGridView1.DataSource = _lastGroupedTable;
        }

        private void UpdateCounters()
        {
            int drugCount = _lastFlatDrugs != null ? _lastFlatDrugs.Rows.Count : 0;
            numreg.Text = drugCount.ToString();
            label5.Text = kryptonDataGridView2.Rows.Count.ToString();
            lblMedListCountVal.Text = drugCount.ToString() + " entries";
            lblExpTotalVal.Text = kryptonDataGridView2.Rows.Count.ToString();
            lblExpStockVal.Text = DatabaseHelper.GetTotalExpiredStock().ToString();
            kryptonLabel6.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            kryptonLabel6.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
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

        private void SearchBox_Enter(object sender, EventArgs e)
        {
            KryptonTextBox box = sender as KryptonTextBox;
            if (box != null && box.Text == (string)box.Tag)
            {
                box.Text = "";
                box.StateCommon.Content.Color1 = System.Drawing.Color.FromArgb(26, 26, 46);
            }
        }

        private void SearchBox_Leave(object sender, EventArgs e)
        {
            KryptonTextBox box = sender as KryptonTextBox;
            if (box != null && string.IsNullOrWhiteSpace(box.Text))
            {
                box.Text = (string)box.Tag;
                box.StateCommon.Content.Color1 = System.Drawing.Color.FromArgb(160, 160, 175);
            }
        }

        private void txtSearchMedicine_TextChanged(object sender, EventArgs e)
        {
            if (txtSearchMedicine.Text == (string)txtSearchMedicine.Tag) return;
            ApplyGroupedMedList(txtSearchMedicine.Text);
            UpdateCounters();
        }

        private void txtSearchExpired_TextChanged(object sender, EventArgs e)
        {
            if (txtSearchExpired.Text == (string)txtSearchExpired.Tag) return;
            string term = txtSearchExpired.Text;
            expDrugBindingSource.DataMember = null;
            expDrugBindingSource.DataSource = DatabaseHelper.SearchExpDrugs(term);
            lblExpTotalVal.Text = kryptonDataGridView2.Rows.Count.ToString();
        }

        private void btnRemoveSelected_Click(object sender, EventArgs e)
        {
            if (kryptonDataGridView2.SelectedRows.Count == 0)
            {
                KryptonMessageBox.Show("Please select one or more rows to remove.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            System.Collections.Generic.List<int> ids = new System.Collections.Generic.List<int>();
            foreach (System.Windows.Forms.DataGridViewRow row in kryptonDataGridView2.SelectedRows)
            {
                object idVal = row.Cells["iDDataGridViewTextBoxColumn"].Value;
                int id;
                if (idVal != null && int.TryParse(idVal.ToString(), out id))
                    ids.Add(id);
            }
            if (ids.Count == 0) return;
            string msg = ids.Count == 1
                ? "Remove the selected expired medicine record?"
                : "Remove " + ids.Count.ToString() + " selected expired medicine records?";
            if (KryptonMessageBox.Show(msg, "Confirm Remove",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (int id in ids)
                    DatabaseHelper.RemoveExpiredDrug(id);
                RefreshGrids();
                string searchTerm = txtSearchExpired.Text;
                if (searchTerm != (string)txtSearchExpired.Tag && !string.IsNullOrEmpty(searchTerm))
                    expDrugBindingSource.DataSource = DatabaseHelper.SearchExpDrugs(searchTerm);
            }
        }

        private void btnClearExpired_Click(object sender, EventArgs e)
        {
            if (kryptonDataGridView2.Rows.Count == 0)
            {
                KryptonMessageBox.Show("There are no expired records to remove.", "Nothing to Clear",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DialogResult confirm = KryptonMessageBox.Show(
                "This will permanently delete all " + kryptonDataGridView2.Rows.Count.ToString() + " expired drug records. Continue?",
                "Confirm Clear", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm == DialogResult.Yes)
            {
                DatabaseHelper.ClearAllExpiredDrugs();
                RefreshGrids();
                txtSearchExpired.Text = (string)txtSearchExpired.Tag;
                KryptonMessageBox.Show("All expired records have been removed.", "Done",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnExportMedicine_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            sfd.FileName = "MedicineList_" + DateTime.Now.ToString("yyyyMMdd") + ".csv";
            sfd.Title = "Export Medicine List";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    DatabaseHelper.ExportDrugsToCSV(sfd.FileName);
                    KryptonMessageBox.Show("Medicine list exported successfully to:\n" + sfd.FileName,
                        "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    KryptonMessageBox.Show("Export failed: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Grid1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= kryptonDataGridView1.Rows.Count) return;
            DataGridViewRow row = kryptonDataGridView1.Rows[e.RowIndex];
            System.Data.DataTable dt = _lastGroupedTable;
            if (dt == null || !dt.Columns.Contains("_RowType") || e.RowIndex >= dt.Rows.Count)
            {
                // Fallback: existing low-stock logic
                object sv = row.Cells["stockAmountDataGridViewTextBoxColumn"].Value;
                int st;
                if (sv != null && int.TryParse(sv.ToString(), out st) && st <= 10)
                {
                    row.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 243, 224);
                    row.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(230, 81, 0);
                }
                else
                {
                    row.DefaultCellStyle.BackColor = System.Drawing.Color.Empty;
                    row.DefaultCellStyle.ForeColor = System.Drawing.Color.Empty;
                }
                return;
            }
            string rowType = dt.Rows[e.RowIndex]["_RowType"].ToString();
            if (rowType == "header")
            {
                row.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(227, 242, 253);
                row.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(13, 71, 161);
                row.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold);
                return;
            }
            if (rowType == "detail")
            {
                object stockVal2 = row.Cells["stockAmountDataGridViewTextBoxColumn"].Value;
                int stock2;
                if (stockVal2 != null && int.TryParse(stockVal2.ToString(), out stock2) && stock2 <= 10)
                {
                    row.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 245, 220);
                    row.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(200, 100, 0);
                }
                else
                {
                    row.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 248, 255);
                    row.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(50, 80, 120);
                }
                row.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Regular);
                return;
            }
            // "single" rows — existing low-stock amber logic
            object stockVal = row.Cells["stockAmountDataGridViewTextBoxColumn"].Value;
            int stock;
            if (stockVal != null && int.TryParse(stockVal.ToString(), out stock) && stock <= 10)
            {
                row.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 243, 224);
                row.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(230, 81, 0);
            }
            else
            {
                row.DefaultCellStyle.BackColor = System.Drawing.Color.Empty;
                row.DefaultCellStyle.ForeColor = System.Drawing.Color.Empty;
            }
        }

        private void Grid2_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= kryptonDataGridView2.Rows.Count) return;
            DataGridViewRow row = kryptonDataGridView2.Rows[e.RowIndex];
            object expVal = row.Cells["expDataGridViewTextBoxColumn1"].Value;
            if (expVal == null || expVal == DBNull.Value) return;
            DateTime expDate;
            if (DateTime.TryParse(expVal.ToString(), out expDate))
            {
                if (expDate < DateTime.Today)
                {
                    row.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 235, 238);
                    row.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(183, 28, 28);
                }
                else
                {
                    row.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 243, 224);
                    row.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(230, 81, 0);
                }
            }
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
            var result = KryptonMessageBox.Show(
                "Are you sure you want to log out?",
                "Log Out", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            loggedaccountnum = "";
            LogIn frm = new LogIn();
            frm.Show();
            this.Close();
        }

        private void LoadUserData()
        {
            try
            {
                user userData = DatabaseHelper.GetUserByEmail(loggedaccountnum);
                if (userData != null)
                {
                    loggeduserid = userData.ID;
                    loggedrole = userData.Role ?? "cashier";
                    string roleDisplay = !string.IsNullOrWhiteSpace(userData.Role)
                        ? "  [" + char.ToUpper(userData.Role[0]) + userData.Role.Substring(1) + "]"
                        : "";
                    LabelFullName.Text = userData.First_Name + " " + userData.Second_Name + "!" + roleDisplay;
                    lblEmail.Text = userData.Email;
                    lblMobile.Text = userData.Mobile.ToString();
                    lblID.Text = userData.ID.ToString();
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
            catch (Exception ex)
            {
                MessageBox.Show("Error loading user data: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── Role-based access ──────────────────────────────────────────────────

        private void ApplyRolePermissions()
        {
            bool isAdmin = string.Equals(loggedrole, "admin", StringComparison.OrdinalIgnoreCase);
            bool isPharmacist = string.Equals(loggedrole, "pharmacist", StringComparison.OrdinalIgnoreCase);

            // Directly control the Pages collection — the only reliable way
            // to show/hide tabs in Krypton Navigator. Clear and re-add in order.
            kryptonNavigator1.Pages.Clear();

            if (isAdmin || isPharmacist)
                kryptonNavigator1.Pages.Add(addmed);
            if (isAdmin || isPharmacist)
                kryptonNavigator1.Pages.Add(menu);

            kryptonNavigator1.Pages.Add(cashierPage);
            kryptonNavigator1.Pages.Add(kryptonPage2);   // Profile

            if (isAdmin || isPharmacist)
                kryptonNavigator1.Pages.Add(kryptonPage1); // Medicine List

            if (isAdmin)
                kryptonNavigator1.Pages.Add(adminPage);

            kryptonNavigator1.Pages.Add(logoutpage);

            // Default landing page
            if (isAdmin || isPharmacist)
                kryptonNavigator1.SelectedPage = menu;
            else
                kryptonNavigator1.SelectedPage = cashierPage;

            kryptonNavigator1.PerformLayout();

            // Pre-load admin data
            if (isAdmin)
            {
                LoadAdminUserGrid();
                if (cboAdminRole.Items.Count > 0)
                    cboAdminRole.SelectedIndex = 0;
            }
        }

        private void LoadAdminUserGrid()
        {
            dgvAdminUsers.DataSource = null;
            dgvAdminUsers.DataSource = DatabaseHelper.GetAllUsers();
        }

        private void btnAdminApply_Click(object sender, System.EventArgs e)
        {
            if (dgvAdminUsers.SelectedRows.Count == 0)
            {
                ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show(
                    "Please select a user first.", "No Selection",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Warning);
                return;
            }
            if (cboAdminRole.SelectedItem == null)
            {
                ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show(
                    "Please choose a role.", "No Role",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Warning);
                return;
            }
            int selectedId = System.Convert.ToInt32(
                dgvAdminUsers.SelectedRows[0].Cells["colAdminID"].Value);
            string newRole = cboAdminRole.SelectedItem.ToString().ToLower();

            if (selectedId == loggeduserid && newRole != "admin")
            {
                ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show(
                    "You cannot remove your own admin role.", "Warning",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Warning);
                return;
            }
            DatabaseHelper.SetUserRole(selectedId, newRole);
            LoadAdminUserGrid();
            ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show(
                "Role updated successfully.", "Success",
                System.Windows.Forms.MessageBoxButtons.OK,
                System.Windows.Forms.MessageBoxIcon.Information);
        }
    }
}
