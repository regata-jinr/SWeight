using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Collections;


//TODO: add try catch;
//ToDo: add unit tests - https://docs.microsoft.com/en-us/visualstudio/test/getting-started-with-unit-testing?view=vs-2017;
//ToDo: analyze performance of code - https://docs.microsoft.com/en-us/visualstudio/profiling/beginners-guide-to-performance-profiling?view=vs-2017;

namespace SWeight
{
    public partial class FaceForm : Form
    {
        private Dictionary<string, string> tabSelects = new Dictionary<string, string>();
        private Dictionary<string, string> tabButtonName = new Dictionary<string, string>();
        private Dictionary<string, string> tabTables = new Dictionary<string, string>();
        private Dictionary<string, DataGridView[]> tabDgvs = new Dictionary<string, DataGridView[]>();
        private SqlConnection con = new SqlConnection();

        private void InitialsSettings()
        {
            con = Connect2DB();
            tabSelects.Add("tabSamples", "select Country_Code as F_Country_Code , Client_ID as F_Client_ID, Year as F_Year, Sample_Set_ID as F_Sample_Set_ID, Sample_Set_Index as F_Sample_Set_Index from table_Sample_Set order by year,Sample_Set_ID, Country_Code, Client_ID,  Sample_Set_Index");
            tabSelects.Add("tabStandarts", "select SRM_Set_Name, SRM_Set_Number from table_SRM_Set");
            tabSelects.Add("tabMonitors", "select Monitor_Set_Name, Monitor_Set_Number from table_Monitor_Set");
            DataGridView[] dgvArray1 = new DataGridView[2];
            dgvArray1[0] = dataGridView_SamplesSet;
            dgvArray1[1] = dataGridView_Samples;
            DataGridView[] dgvArray2 = new DataGridView[2];
            dgvArray2[0] = dataGridView_StandartsSet;
            dgvArray2[1] = dataGridView_Standarts;
            DataGridView[] dgvArray3 = new DataGridView[2];
            dgvArray3[0] = dataGridView_MonitorsSet;
            dgvArray3[1] = dataGridView_Monitors;
            tabDgvs.Add("tabSamples", dgvArray1);
            tabDgvs.Add("tabStandarts", dgvArray2);
            tabDgvs.Add("tabMonitors", dgvArray3);
            tabButtonName.Add("tabSamples", "образеца");
            tabButtonName.Add("tabStandarts", "стандарта");
            tabButtonName.Add("tabMonitors", "монитора");
            tabTables.Add("tabSamples", "table_Sample");
            tabTables.Add("tabStandarts", "table_SRM");
            tabTables.Add("tabMonitors", "table_Monitor");
            checkedListBoxTypes.SetItemChecked(0, true);
            checkedListBoxTypes.SetSelected(1, true);


        }

        public FaceForm()
        {
            InitializeComponent();
            tabs.Selecting += new TabControlCancelEventHandler(tabs_Selecting);
            this.KeyPreview = true;
            InitialsSettings();
            DataGridViewSQLWorker.DataGridSqlFilling(tabDgvs["tabSamples"][0], tabSelects["tabSamples"], con);
        }

        private SqlConnection Connect2DB()
        {
            string connetionString = ConfigurationManager.ConnectionStrings[0].ConnectionString;
            try
            {
                SqlConnection cnn = new SqlConnection(connetionString);
                cnn.Open();
                return cnn;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Can not open connection!\n\n\n\n {ex.ToString()}");
                return null;
            }
        }

        void tabs_Selecting(object sender, TabControlCancelEventArgs e)
        {
            TabPage current = (sender as TabControl).SelectedTab;
            DataGridViewSQLWorker.DataGridSqlFilling(tabDgvs[current.Name][0], tabSelects[current.Name], con);
            if (current.Name == "tabSamples")
                buttonAddRow.Enabled = false;
            else
                buttonAddRow.Enabled = true;
        }

        // we allow to users save weight (double value) from dgv. Without checking it allows to use sql injections. Let's add ValueType to columns with weight in oder to avoid it.
        private void dataGridView_SamplesSet_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;// get the Row Index
            DataGridViewRow selectedRow = dataGridView_SamplesSet.Rows[index];
            string select = $"select A_Sample_ID, P_Weighting_SLI, P_Weighting_LLI, A_Client_Sample_ID from table_Sample where F_Country_Code = '{selectedRow.Cells[0].Value}' and F_Client_ID = '{selectedRow.Cells[1].Value}' and F_Year = '{selectedRow.Cells[2].Value}' and F_Sample_Set_ID = '{selectedRow.Cells[3].Value}' and F_Sample_Set_Index = '{selectedRow.Cells[4].Value}'";
            DataGridViewSQLWorker.DataGridSqlFilling(dataGridView_Samples, select, con);
            if (dataGridView_Samples.RowCount == 0) return;
            dataGridView_Samples.CurrentCell = dataGridView_Samples[0, 0];
            dataGridView_Samples.Columns[0].HeaderText = "номер образца";
            dataGridView_Samples.Columns[1].ValueType = typeof(double);
            dataGridView_Samples.Columns[1].HeaderText = "вес, г (КЖИ)";
            dataGridView_Samples.Columns[2].HeaderText = "вес, г(ДЖИ)";
            dataGridView_Samples.Columns[2].ValueType = typeof(double);
            dataGridView_Samples.Columns[3].HeaderText = "клиентский номер образца";
            dataGridView_Samples.Columns[0].ReadOnly = true;
            dataGridView_Samples.Columns[3].ReadOnly = true;
        }

        private void dataGridView_StandartsSet_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;// get the Row Index
            DataGridViewRow selectedRow = dataGridView_StandartsSet.Rows[index];
            string select = $"select SRM_Number, SRM_SLI_Weight, SRM_LLI_Weight from table_SRM  where SRM_Set_Name = '{selectedRow.Cells[0].Value}' and SRM_Set_Number = '{selectedRow.Cells[1].Value}'";
            DataGridViewSQLWorker.DataGridSqlFilling(dataGridView_Standarts, select, con);
            if (dataGridView_Standarts.RowCount == 0) return;
            dataGridView_Standarts.CurrentCell = dataGridView_Standarts[0, 0];
            dataGridView_Standarts.Columns[0].HeaderText = "номер стандарта";
            dataGridView_Standarts.Columns[1].HeaderText = "вес, г (КЖИ)";
            dataGridView_Standarts.Columns[1].ValueType = typeof(double);
            dataGridView_Standarts.Columns[2].HeaderText = "вес, г(ДЖИ)";
            dataGridView_Standarts.Columns[2].ValueType = typeof(double);
            dataGridView_Standarts.Columns[0].ReadOnly = true;
        }

        private void dataGridView_MonitorsSet_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;// get the Row Index
            DataGridViewRow selectedRow = dataGridView_MonitorsSet.Rows[index];
            string select = $"select Monitor_Number, Monitor_SLI_Weight, Monitor_LLI_Weight from table_Monitor where Monitor_Set_Name = '{selectedRow.Cells[0].Value}' and Monitor_Set_Number = '{selectedRow.Cells[1].Value}'";
            DataGridViewSQLWorker.DataGridSqlFilling(dataGridView_Monitors, select, con);
            if (dataGridView_Monitors.RowCount == 0) return;
            dataGridView_Monitors.CurrentCell = dataGridView_Monitors[0, 0];
            dataGridView_Monitors.Columns[0].HeaderText = "номер монитора";
            dataGridView_Monitors.Columns[1].HeaderText = "вес, г (КЖИ)";
            dataGridView_Monitors.Columns[1].ValueType = typeof(double);
            dataGridView_Monitors.Columns[2].HeaderText = "вес, г(ДЖИ)";
            dataGridView_Monitors.Columns[2].ValueType = typeof(double);
            dataGridView_Monitors.Columns[0].ReadOnly = true;
        }

        //todo: complete this action not clear what behaviour should be. consult with users.
        private void buttonReadFromFile_Click(object sender, EventArgs e)
        {
            TabPage current = tabs.SelectedTab;
            string[] FileNameArray = new string[5];
            string fileName = "";

            if (openFileDialog_ReadFromFile.ShowDialog() == DialogResult.OK)
            {
                fileName = Path.GetFileNameWithoutExtension(openFileDialog_ReadFromFile.FileName);

                if (fileName.Split('-').Length != 5)
                {
                    MessageBox.Show($"Имя файла должно содержать 'countryCode-clientId-year-SampleSetId-SampleSetIndex.ves'", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                FileNameArray = fileName.Split('-');
                for (var i = 0; i < 5; ++i)
                {
                    if (FileNameArray[i] != tabDgvs[current.Name][0].SelectedRows[0].Cells[i].Value.ToString())
                    {
                        MessageBox.Show($"Выбранная партия не совпадает с партией записанной в файле. Выберите одинаковые партии", "Match", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                        //TODO: add opp. to search and select row in dgv by given file name
                        //DialogResult dialogResult = MessageBox.Show($"Выбранная партия не совпадает с партией записанной в файле. Хотите выбрать одинаковые партии?", "Match", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        //if (dialogResult == DialogResult.OK)
                        //{
                        //    SelectStringByValues(tabDgvs[current.Name][0], FileNameArray)
                        //}
                        //else if (dialogResult == DialogResult.Cancel)
                        //{
                        //    break;
                        //}
                    }
                }

                var dt = CSVParser.CSV2DataTable(openFileDialog_ReadFromFile.FileName);
                // tabDgvs[current.Name][1].Rows.Clear();
                tabDgvs[current.Name][1].DataSource = dt;
                return;

            }
            else
            {
                return;
            }

        }

        //TODO: add opp. to search and select row in dgv by given file name
        private void SelectStringByValues(DataGridView dgv, string[] strArr)
        {
            int rowIndex;
            foreach (DataGridViewRow row in dgv.Rows)
            {
                for (var j = 0; j < 5; ++j)
                {
                    if (row.Cells[0].Value.ToString().Equals(strArr[j]))
                    {
                    }
                    else
                    {
                    }
                }
            }
        }

        private void buttonSave2File_Click(object sender, EventArgs e)
        {
            TabPage current = tabs.SelectedTab;
            string[] FileNameArray = new string[5];
            string fileName = "";
            ArrayList header = new ArrayList();
            header.Add($"Код страны: ");
            header.Add($"Клиент: ");
            header.Add($"Год: ");
            header.Add($"Номер партии образцов: ");
            header.Add($"Индекс партии образцов: ");
            header.Add($"Тип образцов: -");
            header.Add($"Количество образцов: {tabDgvs[current.Name][1].RowCount}");
            header.Add($"--------------------------------------------------");
            header.Add($"Номер 	Инд. + 	Вес 	Вес");
            header.Add($"измер. 	№ обр. 	КЖИ, г 	ДЖИ, г");

            int num = 0;
            string add2Num = "";

            if (current.Name.Equals("tabStandarts"))
            {
                fileName += "s-s-s-";
                header[0] += "s";
                header[1] += "s";
                header[2] += "s";
                num = 3;
                add2Num = tabDgvs[current.Name][0].SelectedRows[0].Cells[0].Value.ToString();
            }
            else if ((current.Name.Equals("tabMonitors")))
            {
                fileName += "m-m-m-";
                header[0] += "m";
                header[1] += "m";
                header[2] += "m";
                num = 3;
                add2Num = tabDgvs[current.Name][0].SelectedRows[0].Cells[0].Value.ToString();
            }
            else add2Num = tabDgvs[current.Name][0].SelectedRows[0].Cells[4].Value.ToString();

            for (int i = 0; i < tabDgvs[current.Name][0].ColumnCount; ++i)
            {
                fileName += $"{tabDgvs[current.Name][0].SelectedRows[0].Cells[i].Value.ToString()}-";
                header[i + num] += tabDgvs[current.Name][0].SelectedRows[0].Cells[i].Value.ToString();
            }
            saveFileDialog_Save2File.FileName = fileName.Substring(0, fileName.Length - 1);
            if (saveFileDialog_Save2File.ShowDialog() == DialogResult.OK)
            {
                CSVParser.DataGridView2CSV(tabDgvs[current.Name][1], header, saveFileDialog_Save2File.FileName, add2Num);
                return;
            }
            else { return; }
        }

        private void buttonAddRow_Click(object sender, EventArgs e)
        {
            TabPage current = tabs.SelectedTab;
            int cnt = tabDgvs[current.Name][1].RowCount;
            DataTable dt = new DataTable(); // tabDataSets[current.Name].Tables[0];
            dt = (DataTable)tabDgvs[current.Name][1].DataSource;
            dt.Rows.Add();
            dt.Rows[cnt][0] = (cnt + 1).ToString("D2");
            tabDgvs[current.Name][1].DataSource = dt;
        }

        private void FaceForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Exit();
        }

        private void buttonSave2DB_Click(object sender, EventArgs e)
        {
            TabPage current = tabs.SelectedTab;
            DataGridViewSQLWorker.DataGridViewSave2DB(tabDgvs[current.Name], tabTables[current.Name], con);
            // return;
        }

        private void dataGridView_Samples_DataError(object sender, DataGridViewDataErrorEventArgs anError)
        {
            MessageBox.Show("The most probably you are trying to use no-number format in weight columns. We can't allow to do it, because in the other case it will allow to use sql-injection. Please use only number formats (01,10,1,10.23,...)", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void dataGridView_Standarts_DataError(object sender, DataGridViewDataErrorEventArgs anError)
        {
            MessageBox.Show("The most probably you are trying to use no-number format in weight columns. We can't allow to do it, because in the other case it will allow to use sql-injection. Please use only number formats (01,10,1,10.23,...)", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void dataGridView_Monitors_DataError(object sender, DataGridViewDataErrorEventArgs anError)
        {
            MessageBox.Show("The most probably you are trying to use no-number format in weight columns. We can't allow to do it, because in the other case it will allow to use sql-injection. Please use only number formats (01,10,1,10.23,...)", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private int clickCount = 0;

        private void buttonReadWeight_Click(object sender, EventArgs e)
        {
            TabPage current = tabs.SelectedTab;
            if (tabDgvs[current.Name][1].DataSource == null)
            {
                MessageBox.Show("Please choose one of the lines from the top table.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SerialPortsWorker worker = new SerialPortsWorker();
           
            int curRowNum = tabDgvs[current.Name][1].SelectedCells[0].RowIndex;
            checkedListBoxTypes.ClearSelected();
            Debug.WriteLine(checkedListBoxTypes.GetItemChecked(0).ToString());
            if (checkedListBoxTypes.GetItemChecked(0))
            {
                if (clickCount % 2 == 0)
                {
                    tabDgvs[current.Name][1].Rows[curRowNum].Cells[1].Value = worker.GetWeight();
                    curRowNum--;
                    checkedListBoxTypes.SetSelected(2, true);
                }
                else
                {
                    tabDgvs[current.Name][1].Rows[curRowNum].Cells[2].Value = worker.GetWeight();
                    checkedListBoxTypes.SetSelected(1, true);
                }
            }
            else if (checkedListBoxTypes.GetItemChecked(1))
                tabDgvs[current.Name][1].Rows[curRowNum].Cells[1].Value = worker.GetWeight();
            else if (checkedListBoxTypes.GetItemChecked(2))
                tabDgvs[current.Name][1].Rows[curRowNum].Cells[2].Value = worker.GetWeight();
            if ((curRowNum + 1) == tabDgvs[current.Name][1].RowCount) return;
            tabDgvs[current.Name][1].ClearSelection();
            tabDgvs[current.Name][1].Rows[++curRowNum].Cells[0].Selected = true;
            clickCount++;
        }

        private void checkedListBoxTypes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (checkedListBoxTypes.CheckedItems.Count >= 1 && e.CurrentValue != CheckState.Checked)
            {
                for (int ix = 0; ix < checkedListBoxTypes.Items.Count; ++ix)
                    if (ix != e.Index) checkedListBoxTypes.SetItemChecked(ix, false);
            }
            clickCount = 0;
            if (!checkedListBoxTypes.GetItemChecked(0) && checkedListBoxTypes.GetSelected(0))
                checkedListBoxTypes.SetSelected(1, true);

        }

        private void dataGridView_Samples_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            clickCount = 0;
        }
        private void dataGridView_Standarts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            clickCount = 0;
        }
        private void dataGridView_Monitors_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            clickCount = 0;
        }

        private void FaceForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
                buttonReadWeight.PerformClick();
        }

    }
}
