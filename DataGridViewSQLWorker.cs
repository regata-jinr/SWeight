using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace SWeight
{
    class DataGridViewSQLWorker
    {
        private static Dictionary<string, string> colHeaders = new Dictionary<string, string>();


        private static double RunQuery(string query, bool scalar=false)
        {
            string connetionString = Properties.Resources.conn;
            try
            {
                using (SqlConnection con = new SqlConnection(connetionString))
                {
                    using (SqlCommand sCmd = new SqlCommand(query, con))
                    {
                        con.Open();
                        if (scalar)
                        {
                            if (sCmd.ExecuteScalar() is int)
                                return (int) sCmd.ExecuteScalar();
                            if (sCmd.ExecuteScalar() is float)
                                return (float) sCmd.ExecuteScalar();
                            throw new InvalidCastException();
                        }
                        else return sCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Can not open connection!\n\n\n\n {ex.ToString()}");
                return 0;
            }
        }

        private static DataSet GetDataSet(string query)
        {
            string connetionString = Properties.Resources.conn;
            try
            {
                using (SqlConnection con = new SqlConnection(connetionString))
                {
                    using (var dataAdapter = new SqlDataAdapter(query, con))
                    {
                        con.Open();
                        using (var ds = new DataSet())
                        {
                            dataAdapter.Fill(ds);
                            return ds;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Can not open connection!\n\n\n\n {ex.ToString()}");
                return null;
            }
        }

        static DataGridViewSQLWorker()
        {
            colHeaders.Add("F_Country_Code", "Код страны");
            colHeaders.Add("F_Client_ID", "Код клиента");
            colHeaders.Add("F_Year", "Год");
            colHeaders.Add("F_Sample_Set_ID", "Номер партии");
            colHeaders.Add("F_Sample_Set_Index", "Индекс партии");
            colHeaders.Add("SRM_Set_Name", "Имя партии стандартов");
            colHeaders.Add("SRM_Set_Number", "Номер партии стандартов");
            colHeaders.Add("Monitor_Set_Name", "Имя партии мониторов");
            colHeaders.Add("Monitor_Set_Number", "Номер партии мониторов");
            colHeaders.Add("A_Sample_ID", "Номер образца");
            colHeaders.Add("P_Weighting_SLI", "вес, г (КЖИ)");
            colHeaders.Add("P_Weighting_LLI", "вес, г (ДЖИ)");
            colHeaders.Add("A_Client_Sample_ID", "Клиентский номер образца");
            colHeaders.Add("SRM_Number", "Номер стандарта");
            colHeaders.Add("SRM_SLI_Weight", "вес, г (КЖИ)");
            colHeaders.Add("skip", "empty");
            colHeaders.Add("SRM_LLI_Weight", "вес, г (ДЖИ)");
            colHeaders.Add("Monitor_Number", "Номер монитора");
            colHeaders.Add("Monitor_SLI_Weight", "вес, г (КЖИ)");
            colHeaders.Add("Monitor_LLI_Weight", "вес, г (ДЖИ)");
        }

        public static void DataGridSqlFilling(DataGridView dgv, string select)
        {
            Debug.WriteLine($"Start filling of {dgv.Name}:");
            Debug.WriteLine($"Query text: \n {select}");
            var ds = GetDataSet(select);
            dgv.DataSource = ds.Tables[0];
            if (dgv.RowCount != 0) dgv.CurrentCell = dgv[0, dgv.RowCount - 1];
            if (!dgv.Name.Contains("Set"))
            {
                dgv.Columns[0].ReadOnly = true;
                if (dgv.Name != "dataGridView_Samples") dgv.Columns[1].Visible = false;
                else dgv.Columns[3].ValueType = typeof(double);
            }
            foreach (DataGridViewColumn col in dgv.Columns)
                col.HeaderText = colHeaders[col.Name];
        }

        public static void DataGridViewSave2DB(DataGridView[] dgvs, string table_name)
        {
            try
            {
                Debug.WriteLine($"Starting to save content of {dgvs[1].Name} to DB({table_name}):");
                int cnt;
                double setWeight;
                var temStr = "";
                string query = "";
                Dictionary<string,string> conditionalDict = new Dictionary<string, string>();
                foreach (DataGridViewColumn col in dgvs[0].Columns)
                    conditionalDict.Add(col.Name, dgvs[0].SelectedCells[col.Index].Value.ToString());
                Dictionary<string, string> valuesDict = new Dictionary<string, string>();
                foreach (DataGridViewRow row in dgvs[1].Rows)
                {
                    temStr = dgvs[1].Rows[row.Index].Cells[0].Value.ToString();
                    // patch for fucking A_Client_Sample_ID should be on the second place in the table
                    if (dgvs[1].Name.Contains("Samples")) temStr = temStr.Substring(1, temStr.Length-1);
                    conditionalDict.Add(dgvs[1].Columns[0].Name, temStr);
                    for (int i = 1; i < dgvs[1].ColumnCount; ++i)
                    {
                        if (dgvs[1].Columns[i].Name == "skip" || dgvs[1].Columns[i].Name == "A_Client_Sample_ID") continue;
                        valuesDict.Add(dgvs[1].Columns[i].Name, dgvs[1].Rows[row.Index].Cells[i].Value.ToString());

                    }
                    query = GenerateCountQuery(conditionalDict, table_name);
                    cnt = (int)RunQuery(query, true); 
                    Debug.WriteLine($"Number of elements(samples,srms,monitors) in {table_name} from selected set is {cnt}");
                    if (!table_name.ToLower().Contains("sample"))
                        query = GenerateSetWeightQuery(conditionalDict, table_name);
                    setWeight = Convert.ToDouble(RunQuery(query, true));
                    Debug.WriteLine($"Weight of set of [samples,srms,monitors] is {setWeight}");
                   
                    if (cnt == 1) { query = GenerateUpdateQuery(conditionalDict, valuesDict, table_name); }
                    else if (cnt == 0) { query = GenerateInsertQuery(conditionalDict, valuesDict, table_name, setWeight); }
                    else
                    {
                        MessageBox.Show($"The query might be ambiguous. Check the sql-statements.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    //for debug comment this
                    RunQuery(query);
                    conditionalDict.Remove(dgvs[1].Columns[0].Name);
                    valuesDict.Clear();
                }
            }
            catch (SqlException sqlEx) { MessageBox.Show($"SQL exception:\n {sqlEx.ToString()}", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (Exception ex) { MessageBox.Show($"Exception message:\n {ex.ToString()}", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private static string GenerateUpdateQuery(Dictionary<string, string> conDict, Dictionary<string, string> valDict, string table_name)
        {
            Debug.WriteLine($"Update query:");
            string tempString1 = "";
            string tempString2 = "";
            string upQuery = $"update {table_name} set ";
            foreach (string colName in valDict.Keys)
                tempString1 += $"{colName}='{valDict[colName]}',";
            tempString1 = tempString1.Substring(0, tempString1.Length - 1);
            upQuery += $"{tempString1} where ";
            foreach (string colName in conDict.Keys)
                tempString2 += $"{colName}='{conDict[colName]}' and ";
            tempString2 = tempString2.Substring(0, tempString2.Length - 4);
            upQuery += $"{tempString2}";
            Debug.WriteLine($"{upQuery}");
            return upQuery;
        }
        private static string GenerateInsertQuery(Dictionary<string,string> conDict, Dictionary<string, string> valDict, string table_name, double addValue=0)
        {
            Debug.WriteLine($"Insert query:");
            string inQuery = $"insert into {table_name} (";
            string tempString1 = "";
            string tempString2 = "";

            foreach (string colName in conDict.Keys)
            {
                tempString1 += $"{colName},";
                tempString2 += $"'{conDict[colName]}',";
            }
            foreach (string colName in valDict.Keys)
            {
                tempString1 += $"{colName},";
                tempString2 += $"'{valDict[colName]}',";
            }
            
            //todo: patch for avoid errors during insert new rows to table_SRM. DB doesn't allow to write null to field table_SRM.SRM_Set_Weight.The value known from initial form, so before insert we should read it in table_SRM_Set. (the same behaviour for table_Monitor)
            if (!table_name.ToLower().Contains("sample"))
                tempString1 += $"{table_name}_Set_Weight,".Replace("table_", "");

            tempString1 = tempString1.Substring(0, tempString1.Length - 1);
            inQuery += $"{tempString1}) values(";
  
            if (!table_name.ToLower().Contains("sample")) tempString2 += $"{addValue}";
            else tempString2 = tempString2.Substring(0, tempString2.Length - 1);

            inQuery += $"{tempString2})";
            Debug.WriteLine(inQuery);
            return inQuery;
        }

        private static string GenerateCountQuery(Dictionary<string,string> conDict, string table_name, int index=0)
        {
            Debug.WriteLine($"Count query is:");
            string tempString2 = "";
            string cntQuery = $"select count(*) from {table_name} where ";
            foreach (string colName in conDict.Keys)
                tempString2 += $"{colName}='{conDict[colName]}' and ";
            tempString2 = tempString2.Substring(0, tempString2.Length - 4);
            cntQuery += $"{tempString2}";
            Debug.WriteLine(cntQuery);
            return cntQuery;
        }

        private static string GenerateSetWeightQuery(Dictionary<string, string> conDict, string table_name, int index = 0)
        {
            Debug.WriteLine($"Set Weight query is:");
            string tempString = "";
            string wQuery = $"select {table_name.Replace("table_", "")}_Set_Weight from {table_name}_Set where ";
            foreach (string colName in conDict.Keys)
            {
                if (colName.Equals(conDict.Keys.Last())) break;
                tempString += $"{colName}='{conDict[colName]}' and ";
            }
            tempString = tempString.Substring(0, tempString.Length - 4);
            wQuery += $"{tempString}";
            Debug.WriteLine(wQuery);
            return wQuery;
        }
    }
}
