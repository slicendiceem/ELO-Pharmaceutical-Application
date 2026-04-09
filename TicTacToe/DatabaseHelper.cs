using System;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace TicTacToe
{
    /// <summary>
    /// All database access via SQLite. The DB file sits next to the executable.
    /// </summary>
    internal static class DatabaseHelper
    {
        private static string DbPath
        {
            get
            {
                // Walk up from bin\Release\ (or bin\Debug\) to the project root
                string exeDir = AppDomain.CurrentDomain.BaseDirectory;
                DirectoryInfo dir = new DirectoryInfo(exeDir);
                // Traverse up until we find the folder containing the .csproj, or stop at 4 levels
                for (int i = 0; i < 4; i++)
                {
                    if (dir == null) break;
                    if (Directory.GetFiles(dir.FullName, "*.csproj").Length > 0)
                        return Path.Combine(dir.Parent.FullName, "ELODB.sqlite");
                    dir = dir.Parent;
                }
                // Fallback: place it next to the exe
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ELODB.sqlite");
            }
        }

        private static string ConnectionString
        {
            get { return "Data Source=" + DbPath + ";Version=3;"; }
        }

        private static SQLiteConnection OpenConnection()
        {
            var conn = new SQLiteConnection(ConnectionString);
            conn.Open();
            return conn;
        }

        // Creates tables on first run (idempotent)
        public static void EnsureDatabase()
        {
            using (var conn = OpenConnection())
            {
                string[] ddl = new string[]
                {
                    "CREATE TABLE IF NOT EXISTS users (" +
                    "  ID          INTEGER PRIMARY KEY AUTOINCREMENT," +
                    "  First_Name  TEXT NOT NULL," +
                    "  Second_Name TEXT NOT NULL," +
                    "  Email       TEXT NOT NULL UNIQUE," +
                    "  Mobile      TEXT," +
                    "  Photo       BLOB," +
                    "  Password    TEXT NOT NULL," +
                    "  Role        TEXT NOT NULL DEFAULT 'cashier')",

                    "CREATE TABLE IF NOT EXISTS Drug (" +
                    "  ID           INTEGER PRIMARY KEY AUTOINCREMENT," +
                    "  Name         TEXT NOT NULL," +
                    "  Manufacturer TEXT," +
                    "  Purpose      TEXT," +
                    "  Restricted   TEXT," +
                    "  Price        REAL," +
                    "  Sale_Price   REAL," +
                    "  Stock_Amount INTEGER," +
                    "  Photo        BLOB," +
                    "  Prod         TEXT," +
                    "  Exp          TEXT)",

                    "CREATE TABLE IF NOT EXISTS Exp_Drug (" +
                    "  ID           INTEGER PRIMARY KEY," +
                    "  Name         TEXT NOT NULL," +
                    "  Manufacturer TEXT," +
                    "  Purpose      TEXT," +
                    "  Stock_Amount INTEGER," +
                    "  Prod         TEXT," +
                    "  Exp          TEXT)"
                };

                foreach (string sql in ddl)
                {
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                    }
                }

                // Migration: add Role column to existing databases that predate this field
                bool hasRole = false;
                using (var pragmaCmd = conn.CreateCommand())
                {
                    pragmaCmd.CommandText = "PRAGMA table_info(users)";
                    using (var r = pragmaCmd.ExecuteReader())
                        while (r.Read())
                            if (r["name"].ToString() == "Role") { hasRole = true; break; }
                }
                if (!hasRole)
                {
                    using (var altCmd = conn.CreateCommand())
                    {
                        altCmd.CommandText = "ALTER TABLE users ADD COLUMN Role TEXT NOT NULL DEFAULT 'cashier'";
                        altCmd.ExecuteNonQuery();
                    }
                }

                // Seed: ensure admin@elo.com exists with admin role.
                // INSERT OR IGNORE handles the UNIQUE constraint gracefully if the email already exists.
                // UPDATE promotes the account to admin (and resets the default password) only if it
                // is not already an admin — so a real admin who changed their password is never affected.
                string adminHash = PasswordHelper.HashPassword("Admin@1234");
                using (var insCmd = conn.CreateCommand())
                {
                    insCmd.CommandText =
                        "INSERT OR IGNORE INTO users " +
                        "(First_Name, Second_Name, Email, Mobile, Photo, Password, Role) " +
                        "VALUES ('Admin', 'User', 'admin@elo.com', NULL, NULL, @pass, 'admin')";
                    insCmd.Parameters.AddWithValue("@pass", adminHash);
                    insCmd.ExecuteNonQuery();
                }
                // If admin@elo.com was already registered as a non-admin, promote and reset password
                using (var promoteCmd = conn.CreateCommand())
                {
                    promoteCmd.CommandText =
                        "UPDATE users SET Role = 'admin', Password = @pass " +
                        "WHERE Email = 'admin@elo.com' AND Role != 'admin'";
                    promoteCmd.Parameters.AddWithValue("@pass", adminHash);
                    promoteCmd.ExecuteNonQuery();
                }
            }
        }

        // ── Users ─────────────────────────────────────────────────────────────

        public static user GetUserByEmail(string email)
        {
            using (var conn = OpenConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText =
                    "SELECT ID, First_Name, Second_Name, Email, Mobile, Photo, Password, Role " +
                    "FROM users WHERE Email = @email LIMIT 1";
                cmd.Parameters.AddWithValue("@email", email);

                using (var reader = cmd.ExecuteReader())
                {
                    if (!reader.Read()) return null;

                    return new user
                    {
                        ID = reader.GetInt32(0),
                        First_Name = reader.GetString(1),
                        Second_Name = reader.GetString(2),
                        Email = reader.GetString(3),
                        Mobile = reader.IsDBNull(4) ? (long?)null : long.Parse(reader.GetString(4)),
                        Photo = reader.IsDBNull(5) ? null : (byte[])reader["Photo"],
                        Password = reader.GetString(6),
                        Role = reader.IsDBNull(7) ? "cashier" : reader.GetString(7)
                    };
                }
            }
        }

        public static void AddUser(user u)
        {
            using (var conn = OpenConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText =
                    "INSERT INTO users (First_Name, Second_Name, Email, Mobile, Photo, Password, Role) " +
                    "VALUES (@fn, @sn, @email, @mobile, @photo, @pass, @role)";
                cmd.Parameters.AddWithValue("@fn", u.First_Name);
                cmd.Parameters.AddWithValue("@sn", u.Second_Name);
                cmd.Parameters.AddWithValue("@email", u.Email);
                cmd.Parameters.AddWithValue("@mobile",
                    u.Mobile.HasValue ? (object)u.Mobile.Value.ToString() : DBNull.Value);
                cmd.Parameters.AddWithValue("@photo", (object)u.Photo ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@pass", u.Password);
                cmd.Parameters.AddWithValue("@role", string.IsNullOrWhiteSpace(u.Role) ? "cashier" : u.Role);
                cmd.ExecuteNonQuery();
            }
        }

        // ── Drugs ─────────────────────────────────────────────────────────────

        // Column names match DataPropertyName values in Form1.Designer.cs
        public static DataTable GetDrugs()
        {
            return SearchDrugs("");
        }

        public static DataTable SearchDrugs(string term)
        {
            string sql = "SELECT Name, Manufacturer, Purpose, Restricted, Price, " +
                         "Sale_Price, Stock_Amount, Prod, Exp FROM Drug";
            if (!string.IsNullOrWhiteSpace(term))
                sql += " WHERE Name LIKE @t OR Manufacturer LIKE @t OR Purpose LIKE @t";
            sql += " ORDER BY Name";
            using (var conn = OpenConnection())
            using (var adapter = new SQLiteDataAdapter(sql, conn))
            {
                if (!string.IsNullOrWhiteSpace(term))
                    adapter.SelectCommand.Parameters.AddWithValue("@t", "%" + term + "%");
                var dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        public static DataTable GetExpDrugs()
        {
            return SearchExpDrugs("");
        }

        public static DataTable SearchExpDrugs(string term)
        {
            string sql = "SELECT ID, Name, Manufacturer, Purpose, Stock_Amount, Prod, Exp " +
                         "FROM Exp_Drug";
            if (!string.IsNullOrWhiteSpace(term))
                sql += " WHERE Name LIKE @t OR Manufacturer LIKE @t";
            sql += " ORDER BY Exp";
            using (var conn = OpenConnection())
            using (var adapter = new SQLiteDataAdapter(sql, conn))
            {
                if (!string.IsNullOrWhiteSpace(term))
                    adapter.SelectCommand.Parameters.AddWithValue("@t", "%" + term + "%");
                var dt = new DataTable();
                adapter.Fill(dt);
                // Add computed TimeStatus column (time left or elapsed since expiry)
                dt.Columns.Add("TimeStatus", typeof(string));
                foreach (DataRow row in dt.Rows)
                {
                    string expStr = row["Exp"] == DBNull.Value ? "" : row["Exp"].ToString();
                    row["TimeStatus"] = FormatTimeLeft(expStr);
                }
                return dt;
            }
        }

        public static int GetTotalExpiredStock()
        {
            using (var conn = OpenConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT COALESCE(SUM(Stock_Amount), 0) FROM Exp_Drug";
                object result = cmd.ExecuteScalar();
                return result == null || result == DBNull.Value ? 0 : Convert.ToInt32(result);
            }
        }

        public static void RemoveExpiredDrug(int id)
        {
            using (var conn = OpenConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM Exp_Drug WHERE ID = @id";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public static void ClearAllExpiredDrugs()
        {
            using (var conn = OpenConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM Exp_Drug";
                cmd.ExecuteNonQuery();
            }
        }

        public static void ExportDrugsToCSV(string filePath)
        {
            var dt = GetDrugs();
            var sb = new System.Text.StringBuilder();
            var headers = new System.Collections.Generic.List<string>();
            foreach (System.Data.DataColumn col in dt.Columns)
                headers.Add("\"" + col.ColumnName + "\"");
            sb.AppendLine(string.Join(",", headers));
            foreach (System.Data.DataRow row in dt.Rows)
            {
                var fields = new System.Collections.Generic.List<string>();
                foreach (object val in row.ItemArray)
                    fields.Add("\"" + (val == null ? "" : val.ToString().Replace("\"", "\"\"")) + "\"");
                sb.AppendLine(string.Join(",", fields));
            }
            System.IO.File.WriteAllText(filePath, sb.ToString(), System.Text.Encoding.UTF8);
        }

        public static DataTable GetDrugsByName(string name)
        {
            using (var conn = OpenConnection())
            using (var adapter = new SQLiteDataAdapter(
                "SELECT ID, Name, Prod, Exp, Stock_Amount FROM Drug " +
                "WHERE Name = @name COLLATE NOCASE ORDER BY Prod", conn))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@name", name);
                var dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        public static System.Collections.Generic.List<string> GetDrugNames()
        {
            var list = new System.Collections.Generic.List<string>();
            using (var conn = OpenConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT DISTINCT Name FROM Drug ORDER BY Name COLLATE NOCASE";
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                        list.Add(reader.GetString(0));
            }
            return list;
        }

        public static DataTable GetDrugFullByName(string name)
        {
            using (var conn = OpenConnection())
            using (var adapter = new SQLiteDataAdapter(
                "SELECT ID, Name, Manufacturer, Purpose, Restricted, Price, Sale_Price, " +
                "Stock_Amount, Prod, Exp FROM Drug WHERE Name = @name COLLATE NOCASE " +
                "ORDER BY ID LIMIT 1", conn))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@name", name);
                var dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        public static void UpdateDrugStock(int id, int additionalStock)
        {
            using (var conn = OpenConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "UPDATE Drug SET Stock_Amount = COALESCE(Stock_Amount, 0) + @add WHERE ID = @id";
                cmd.Parameters.AddWithValue("@add", additionalStock);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public static void DeductDrugStock(int id, int qty)
        {
            using (var conn = OpenConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "UPDATE Drug SET Stock_Amount = Stock_Amount - @qty WHERE ID = @id AND Stock_Amount >= @qty";
                cmd.Parameters.AddWithValue("@qty", qty);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public static DataTable GetDrugsForCashier(string term)
        {
            string sql = "SELECT ID, Name, Sale_Price, Stock_Amount FROM Drug WHERE Stock_Amount > 0";
            if (!string.IsNullOrWhiteSpace(term))
                sql += " AND (Name LIKE @t OR Manufacturer LIKE @t)";
            sql += " ORDER BY Name";
            using (var conn = OpenConnection())
            using (var adapter = new SQLiteDataAdapter(sql, conn))
            {
                if (!string.IsNullOrWhiteSpace(term))
                    adapter.SelectCommand.Parameters.AddWithValue("@t", "%" + term + "%");
                var dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        public static void AddDrug(Drug d)
        {
            using (var conn = OpenConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText =
                    "INSERT INTO Drug " +
                    "(Name, Manufacturer, Purpose, Restricted, Price, Sale_Price, " +
                    " Stock_Amount, Photo, Prod, Exp) " +
                    "VALUES (@name, @mfg, @purpose, @restricted, @price, @salePrice, " +
                    "        @stock, @photo, @prod, @exp)";
                cmd.Parameters.AddWithValue("@name", d.Name);
                cmd.Parameters.AddWithValue("@mfg", (object)d.Manufacturer ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@purpose", (object)d.Purpose ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@restricted", (object)d.Restricted ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@price",
                    d.Price.HasValue ? (object)d.Price.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@salePrice",
                    d.Sale_Price.HasValue ? (object)d.Sale_Price.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@stock",
                    d.Stock_Amount.HasValue ? (object)d.Stock_Amount.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@photo", (object)d.Photo ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@prod",
                    d.Prod.HasValue ? (object)d.Prod.Value.ToString("yyyy-MM-dd") : DBNull.Value);
                cmd.Parameters.AddWithValue("@exp",
                    d.Exp.HasValue ? (object)d.Exp.Value.ToString("yyyy-MM-dd") : DBNull.Value);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Moves drugs expiring within the next <paramref name="daysAhead"/> days
        /// into Exp_Drug in a single atomic transaction.
        /// Returns the number of drugs transferred.
        /// </summary>
        public static int TransferExpiredDrugs(int daysAhead = 91)
        {
            string threshold = DateTime.Today.AddDays(daysAhead).ToString("yyyy-MM-dd");
            using (var conn = OpenConnection())
            {
                var tx = conn.BeginTransaction();
                try
                {
                    int count;
                    using (var insert = conn.CreateCommand())
                    {
                        insert.Transaction = tx;
                        insert.CommandText =
                            "INSERT OR IGNORE INTO Exp_Drug " +
                            "  (ID, Name, Manufacturer, Purpose, Stock_Amount, Prod, Exp) " +
                            "SELECT ID, Name, Manufacturer, Purpose, Stock_Amount, Prod, Exp " +
                            "FROM Drug WHERE Exp IS NOT NULL AND Exp <= @threshold";
                        insert.Parameters.AddWithValue("@threshold", threshold);
                        count = insert.ExecuteNonQuery();
                    }

                    if (count > 0)
                    {
                        using (var delete = conn.CreateCommand())
                        {
                            delete.Transaction = tx;
                            delete.CommandText =
                                "DELETE FROM Drug WHERE Exp IS NOT NULL AND Exp <= @threshold";
                            delete.Parameters.AddWithValue("@threshold", threshold);
                            delete.ExecuteNonQuery();
                        }
                    }

                    tx.Commit();
                    return count;
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
        }

        public static DataTable GetAllUsers()
        {
            string sql = "SELECT ID, First_Name, Second_Name, Email, Mobile, Role FROM users ORDER BY ID";
            using (var conn = OpenConnection())
            using (var adapter = new SQLiteDataAdapter(sql, conn))
            {
                var dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        public static void SetUserRole(int id, string role)
        {
            using (var conn = OpenConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "UPDATE users SET Role = @role WHERE ID = @id";
                cmd.Parameters.AddWithValue("@role", role);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Returns a human-readable label for time remaining until, or elapsed since, expDateStr.
        /// Examples: "3mo 15d left", "45d left", "2mo ago", "Today"
        /// </summary>
        public static string FormatTimeLeft(string expDateStr)
        {
            if (string.IsNullOrWhiteSpace(expDateStr)) return "";
            if (expDateStr.Length > 10) expDateStr = expDateStr.Substring(0, 10);
            DateTime expDate;
            if (!DateTime.TryParse(expDateStr, out expDate)) return "";
            int totalDays = (int)(expDate.Date - DateTime.Today).TotalDays;
            if (totalDays == 0) return "Today";
            if (totalDays > 0)
            {
                int mo = totalDays / 30;
                int dy = totalDays % 30;
                if (mo == 0) return string.Format("{0}d left", dy);
                if (dy == 0) return string.Format("{0}mo left", mo);
                return string.Format("{0}mo {1}d left", mo, dy);
            }
            else
            {
                int pastDays = -totalDays;
                int mo = pastDays / 30;
                int dy = pastDays % 30;
                if (mo == 0) return string.Format("{0}d ago", dy);
                if (dy == 0) return string.Format("{0}mo ago", mo);
                return string.Format("{0}mo {1}d ago", mo, dy);
            }
        }
    }
}
