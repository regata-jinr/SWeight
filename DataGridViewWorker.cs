using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
//using System.Configuration;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace SWeight
{
    class DataGridViewWorker
    {
        public static void DataGridSqlFilling(DataGridView dgv, string select, SqlConnection con)
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            var dataAdapter = new SqlDataAdapter(select, con);
            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dgv.DataSource = ds.Tables[0];
            con.Close();
            if (dgv.RowCount == 0) return;
            dgv.CurrentCell = dgv[0, dgv.RowCount - 1];
        }

        public static void DataGridViewSave2DB(DataGridView[] dgvs, string table_name, SqlConnection con)
        {
            try
            {
                int cnt;
                double setWeight;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlCommand sCmd = new SqlCommand();
                sCmd.CommandType = CommandType.Text;
                sCmd.Connection = con;
                sCmd.CommandText = GenerateCountQuery(dgvs, table_name);
                cnt = (int)sCmd.ExecuteScalar();
                Debug.WriteLine(cnt.ToString());
                if (!table_name.ToLower().Contains("sample"))
                    sCmd.CommandText = GenerateSetWeightQuery(dgvs, table_name);
                setWeight = Convert.ToDouble(sCmd.ExecuteScalar());
                Debug.WriteLine(sCmd.CommandText);
                Debug.WriteLine(setWeight.ToString());
                return;
                if (cnt == 1) { sCmd.CommandText = GenerateUpdateQuery(dgvs, table_name); }
                else if (cnt == 0) { sCmd.CommandText = GenerateInsertQuery(dgvs, table_name,0,setWeight); }
                else
                {
                    MessageBox.Show($"The query could be ambiguous. Check your sql-statements.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                Debug.WriteLine(sCmd.CommandText);
               // sCmd.ExecuteNonQuery();
                con.Close();
            }
            catch (SqlException sqlEx) { MessageBox.Show($"SQL exception:\n {sqlEx.ToString()}", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (Exception ex) { MessageBox.Show($"Exception message:\n {ex.ToString()}", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private static string GenerateUpdateQuery(DataGridView[] dgvs, string table_name, int index = 0)
        {
            string tempString1 = "";
            string tempString2 = "";
            string upQuery = $"update {table_name} set ";
            tempString1 = $"{dgvs[1].Columns[1].Name}='{dgvs[1].Rows[index].Cells[1].Value.ToString()}',{dgvs[1].Columns[2].Name}='{dgvs[1].Rows[index].Cells[2].Value.ToString()}'";
            upQuery += $"{tempString1} where ";
            foreach (DataGridViewColumn col in dgvs[0].Columns)
                tempString2 += $"{col.Name}='{dgvs[0].SelectedCells[col.Index].Value.ToString()}' and ";
            tempString2 += $"{dgvs[1].Columns[0].Name}='{dgvs[1].Rows[index].Cells[0].Value.ToString()}' ";
            upQuery += $"{tempString2}";
            if (index < dgvs[1].RowCount - 1)
                GenerateUpdateQuery(dgvs, table_name, ++index);
            return upQuery;
        }
        private static string GenerateInsertQuery(DataGridView[] dgvs, string table_name, int index=0, double setWeight=0)
        {
            string inQuery = $"insert into {table_name} (";
            string tempString1 = "";
            string tempString2 = "";

            foreach (DataGridViewColumn col in dgvs[0].Columns)
                tempString1 += $"{col.Name},";
            foreach (DataGridViewColumn col in dgvs[1].Columns)
                tempString1 += $"{col.Name},";

            //patch for avoid errors during insert new rows to table_SRM. DB doesn't allow to write null to field table_SRM.SRM_Set_Weight.The value known from initial form, so before insert we should read it in table_SRM_Set. (the same behaviour for table_Monitor)
            if (!table_name.ToLower().Contains("sample"))
                tempString1 += $"{table_name}_Weight,".Replace("table_", "");

            tempString1 = tempString1.Substring(0, tempString1.Length - 1);
            inQuery += $"{tempString1}) values(";

            foreach (DataGridViewColumn col in dgvs[0].Columns)
                tempString2 += $"'{dgvs[0].SelectedCells[col.Index].Value.ToString()}',";
            foreach (DataGridViewColumn col in dgvs[1].Columns)
                tempString2 += $"'{dgvs[1].Rows[index].Cells[col.Index].Value.ToString()}',";

            if (!table_name.ToLower().Contains("sample"))
                tempString2 += $"{setWeight}";
            tempString2 = tempString2.Substring(0, tempString2.Length - 1);
            inQuery += $"{tempString2})";
            if (index < dgvs[1].RowCount-1)
                GenerateInsertQuery(dgvs, table_name, ++index, setWeight);
            return inQuery;
        }

        private static string GenerateCountQuery(DataGridView[] dgvs, string table_name, int index=0)
        {
            string tempString2 = "";
            string cntQuery = $"select count(*) from {table_name} where ";
            foreach (DataGridViewColumn col in dgvs[0].Columns)
                tempString2 += $"{col.Name}='{dgvs[0].SelectedCells[col.Index].Value.ToString()}' and ";
            tempString2 += $"{dgvs[1].Columns[0].Name}='{dgvs[1].Rows[index].Cells[0].Value.ToString()}' ";
            cntQuery += $"{tempString2}";
            Debug.WriteLine(cntQuery);
            return cntQuery;
        }

        private static string GenerateSetWeightQuery(DataGridView[] dgvs, string table_name, int index = 0)
        {
            string tempString = "";
            string wQuery = $"select {table_name.Replace("NAA_DB_new.dbo.table_", "")}_Set_Weight from {table_name}_Set where ";
            foreach (DataGridViewColumn col in dgvs[0].Columns)
                tempString += $"{col.Name}='{dgvs[0].SelectedCells[col.Index].Value.ToString()}' and ";
            tempString = tempString.Substring(0, tempString.Length - 4);

            wQuery += $"{tempString}";
            Debug.WriteLine(wQuery);
            return wQuery;
        }
    }
}
