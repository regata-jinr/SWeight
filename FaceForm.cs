using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Collections;
using System.Deployment.Application;
using System.Drawing;

//TODO: add try catch;
//TODO: add mechnism of weighting for selected cell
//TODO: hit the space should be event for weighting independent from button focus
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
        private int currRowIndex = 0, currColIndex = 0;
        //private SerialPortsWorker worker;
       

        private void InitialsSettings()
        {
            //update message
            string UpdMsg = $"Уменьшена пауза между взвешиваниями. Теперь, после нажатия на кнопку 'Взвесить', кнопка станет неактивной до тех пор пока взвешивание не произойдет. В случае ошибки, программа автоматически попробует считать вес еще раз, проделав до трех попыток. Также в процессе взвешивания на рабочем столе сохраняется файл с именем 'код партии.tmp.ves'. В него сохраняются данные после каждого взвешивания. Таким образом, если вдруг, на последнем образце программа вылетела, Вы можете просто загрузить этот файл в базу. Затем зайти в программу и увидеть все веса, кроме последнего. Если все прошло нормально, после сохранения в БД программа автоматически удалит этот файл.";
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                ApplicationDeployment current = ApplicationDeployment.CurrentDeployment;
                if (current.IsFirstRun)
                    MessageBox.Show($"В новой версии программы {Application.ProductVersion}{System.Environment.NewLine}{System.Environment.NewLine}{UpdMsg}{System.Environment.NewLine}{System.Environment.NewLine}Свои комментарии, замечания, сообщения об ошибках Вы можете сообщить мне {System.Environment.NewLine}по почте - bdrum@jinr.ru{System.Environment.NewLine}по телефону - 6 24 36{System.Environment.NewLine}лично{System.Environment.NewLine}С уважением,{System.Environment.NewLine}Борис Румянцев", $"Обновление весовой программы", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
           

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
            string version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            Text += version.Substring(0, version.Length - 2);
            DataGridViewSQLWorker.DataGridSqlFilling(dataGridView_SamplesSet, tabSelects["tabSamples"]);
            DataGridViewSQLWorker.DataGridSqlFilling(dataGridView_StandartsSet, tabSelects["tabStandarts"]);
            DataGridViewSQLWorker.DataGridSqlFilling(dataGridView_MonitorsSet, tabSelects["tabMonitors"]);
            //worker = new SerialPortsWorker();
    }

        public FaceForm()
        {
            InitializeComponent();
            tabs.Selecting += new TabControlCancelEventHandler(tabs_Selecting);
            KeyPreview = true;
            InitialsSettings();
            DataGridViewSQLWorker.DataGridSqlFilling(tabDgvs["tabSamples"][0], tabSelects["tabSamples"]);
        }

     
        void tabs_Selecting(object sender, TabControlCancelEventArgs e)
        {
            TabPage current = (sender as TabControl).SelectedTab;
          //  DataGridViewSQLWorker.DataGridSqlFilling(tabDgvs[current.Name][0], tabSelects[current.Name], con);
            if (current.Name == "tabSamples")
                buttonAddRow.Enabled = false;
            else
                buttonAddRow.Enabled = true;
        }

        // we allow to users save weight (double value) from dgv. Without checking it allows to use sql injections. Let's add ValueType to columns with weight in oder to avoid it.
        private void dataGridView_SamplesSet_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;// get the Row Index
            // for sorting
            if (index < 0) return;
            
            DataGridViewRow selectedRow = dataGridView_SamplesSet.Rows[index];
            string select = $"select F_Sample_Set_Index+A_Sample_ID as A_Sample_ID,A_Client_Sample_ID as A_Client_Sample_ID, P_Weighting_SLI, P_Weighting_LLI from table_Sample where F_Country_Code = '{selectedRow.Cells[0].Value}' and F_Client_ID = '{selectedRow.Cells[1].Value}' and F_Year = '{selectedRow.Cells[2].Value}' and F_Sample_Set_ID = '{selectedRow.Cells[3].Value}' and F_Sample_Set_Index = '{selectedRow.Cells[4].Value}'";
            DataGridViewSQLWorker.DataGridSqlFilling(dataGridView_Samples, select);
            ColorizeAndSelect(dataGridView_Samples);
            if (dataGridView_Samples.RowCount == 0) return;
            dataGridView_Samples.Columns[1].ReadOnly = true;
            dataGridView_Samples.Focus();
        }

        private void dataGridView_StandartsSet_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            int index = e.RowIndex;// get the Row Index
            // for sorting
            if (index < 0) return;
            DataGridViewRow selectedRow = dataGridView_StandartsSet.Rows[index];
            string select = $"select SRM_Number,1 as skip, SRM_SLI_Weight, SRM_LLI_Weight from table_SRM  where SRM_Set_Name = '{selectedRow.Cells[0].Value}' and SRM_Set_Number = '{selectedRow.Cells[1].Value}'";
            DataGridViewSQLWorker.DataGridSqlFilling(dataGridView_Standarts, select);
            ColorizeAndSelect(dataGridView_Standarts);
            if (dataGridView_Standarts.RowCount == 0) return;
            dataGridView_Standarts.Focus();
        }

        private void dataGridView_MonitorsSet_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;// get the Row Index
            // for sorting
            if (index < 0) return;
            DataGridViewRow selectedRow = dataGridView_MonitorsSet.Rows[index];
            string select = $"select Monitor_Number,1 as skip,Monitor_SLI_Weight, Monitor_LLI_Weight from table_Monitor where Monitor_Set_Name = '{selectedRow.Cells[0].Value}' and Monitor_Set_Number = '{selectedRow.Cells[1].Value}'";
            DataGridViewSQLWorker.DataGridSqlFilling(dataGridView_Monitors, select);
            ColorizeAndSelect(dataGridView_Monitors);
            if (dataGridView_Monitors.RowCount == 0) return;
            dataGridView_Monitors.Focus();
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

        private void PrepareForSavingFile(bool _showDialog)
        {
            TabPage current = tabs.SelectedTab;
            ArrayList header = new ArrayList();
            string fileName = "";
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
            //else add2Num = tabDgvs[current.Name][0].SelectedRows[0].Cells[4].Value.ToString();

            for (int i = 0; i < tabDgvs[current.Name][0].ColumnCount; ++i)
            {
                fileName += $"{tabDgvs[current.Name][0].SelectedRows[0].Cells[i].Value.ToString()}-";
                header[i + num] += tabDgvs[current.Name][0].SelectedRows[0].Cells[i].Value.ToString();
            }
            saveFileDialog_Save2File.FileName = fileName.Substring(0, fileName.Length - 1);
            Debug.WriteLine(saveFileDialog_Save2File.FileName);
            if (_showDialog) { 
            if (saveFileDialog_Save2File.ShowDialog() == DialogResult.OK)
                    Debug.WriteLine(saveFileDialog_Save2File.FileName);
                CSVParser.DataGridView2CSV(tabDgvs[current.Name][1], header, saveFileDialog_Save2File.FileName, add2Num);
            }
            else CSVParser.DataGridView2CSV(tabDgvs[current.Name][1], header, $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\{saveFileDialog_Save2File.FileName}.tmp.ves", add2Num);
            Debug.WriteLine(saveFileDialog_Save2File.FileName);

        }

        private void buttonSave2File_Click(object sender, EventArgs e)
        {
            PrepareForSavingFile(true);
            if (checkBoxDB.Checked) buttonSave2DB_Click(sender, e);

            var files = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "*.tmp.ves");
            foreach (var file in files) File.Delete(file);
            MessageBox.Show("Сохранение завершено!");
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

        private bool m_isExiting = false;
        private void FaceForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!m_isExiting)
            {
                DialogResult d = MessageBox.Show("Вы уверены, что сохранили все данные и хотите выйти из приложения?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (d == DialogResult.Yes)
                {
                    m_isExiting = true;
                    Application.Exit();
                }
                else e.Cancel = true;
            }
        }

        private void buttonSave2DB_Click(object sender, EventArgs e)
        {
            TabPage current = tabs.SelectedTab;
            DataGridViewSQLWorker.DataGridViewSave2DB(tabDgvs[current.Name], tabTables[current.Name]);
            var files = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "*.tmp.ves");
            foreach (var file in files) File.Delete(file);
            MessageBox.Show("Сохранение завершено!");
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

        private void buttonReadWeight_Click(object sender, EventArgs e)
        {
            try
            {
                buttonReadWeight.Enabled = false;
                TabPage current = tabs.SelectedTab;
                currRowIndex = tabDgvs[current.Name][1].CurrentCellAddress.Y;
                currColIndex = tabDgvs[current.Name][1].CurrentCellAddress.X;
                if (tabDgvs[current.Name][1].DataSource == null)
                {
                    MessageBox.Show("Please choose one of the lines from the top table.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                tabDgvs[current.Name][1].Rows[currRowIndex].Cells[currColIndex].Value = Weighting();

                if (radioButtonTypeBoth.Checked)
                {
                    if (tabDgvs[current.Name][1].Columns[currColIndex].HeaderText.Contains("ДЖИ"))
                    {
                        if ((currRowIndex + 1) == tabDgvs[current.Name][1].RowCount) return;
                        tabDgvs[current.Name][1].CurrentCell = tabDgvs[current.Name][1].Rows[currRowIndex + 1].Cells[currColIndex - 1];
                        tabDgvs[current.Name][1].Rows[currRowIndex + 1].Cells[currColIndex - 1].Selected = true;
                    }
                    else if (tabDgvs[current.Name][1].Columns[currColIndex].HeaderText.Contains("КЖИ"))
                    {
                        tabDgvs[current.Name][1].CurrentCell = tabDgvs[current.Name][1].Rows[currRowIndex].Cells[currColIndex + 1];
                        tabDgvs[current.Name][1].Rows[currRowIndex].Cells[currColIndex + 1].Selected = true;
                    }
                }
                else
                {
                    if ((currRowIndex + 1) == tabDgvs[current.Name][1].RowCount) return;
                    tabDgvs[current.Name][1].CurrentCell = tabDgvs[current.Name][1].Rows[currRowIndex + 1].Cells[currColIndex];
                    tabDgvs[current.Name][1].Rows[currRowIndex + 1].Cells[currColIndex].Selected = true;
                }

            }
            finally
            {
                PrepareForSavingFile(false);
                buttonReadWeight.Enabled = true;
            }
        }


        private void RadioButtonsCheckedChanges(object sender, EventArgs e) {
            TabPage current = tabs.SelectedTab;
            var dgv = tabDgvs[current.Name][1];
            if (dgv.RowCount == 0) return;
            int sliColIndex = 0, lliColIndex = 0;
            currRowIndex = dgv.CurrentCell.RowIndex;
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                if (col.HeaderText.Contains("КЖИ")) sliColIndex = col.Index;
                if (col.HeaderText.Contains("ДЖИ")) lliColIndex = col.Index;
            }
            if (radioButtonTypeBoth.Checked || radioButtonTypeSLI.Checked)
            {
                dgv.CurrentCell = dgv.Rows[currRowIndex].Cells[sliColIndex];
                dgv.Rows[currRowIndex].Cells[sliColIndex].Selected = true;
            }
            else
            {
                dgv.CurrentCell = dgv.Rows[currRowIndex].Cells[sliColIndex];
                dgv.Rows[currRowIndex].Cells[lliColIndex].Selected = true;
            }
        }

        private void datagridview_SamplesSelectionChanged(object sender, EventArgs e)
        {
            TabPage current = tabs.SelectedTab;
            CommonSelectionMechanics(tabDgvs[current.Name][1]);
        }


        private void CommonSelectionMechanics(DataGridView dgv)
        {
            if (dgv.CurrentCell == null) return;
            Debug.WriteLine($"Start selection mech:");
            currRowIndex = dgv.CurrentCellAddress.Y;
            currColIndex = dgv.CurrentCellAddress.X;
            Debug.WriteLine($"Initial position: row-{currRowIndex}, col-{currColIndex}");
            int sliColIndex = 0, lliColIndex = 0;
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                if (col.HeaderText.Contains("КЖИ")) sliColIndex = col.Index;
                if (col.HeaderText.Contains("ДЖИ")) lliColIndex = col.Index;
            }

            if (radioButtonTypeBoth.Checked && !dgv.Columns[currColIndex].HeaderText.Contains("ДЖИ") && !dgv.Columns[currColIndex].HeaderText.Contains("КЖИ"))
            {
                Debug.WriteLine($"See that both types checked and non weight cell chosen:");
                Debug.WriteLine($"Current position: row-{currRowIndex}, col-{sliColIndex}");
                dgv.CurrentCell = dgv.Rows[currRowIndex].Cells[sliColIndex];
                dgv.Rows[currRowIndex].Cells[sliColIndex].Selected = true;
            }
            else if (radioButtonTypeSLI.Checked && !dgv.Columns[currColIndex].HeaderText.Contains("КЖИ"))
            {
                Debug.WriteLine($"See that SLI types checked and non sli-weight cell chosen:");
                Debug.WriteLine($"Current position: row-{currRowIndex}, col-{sliColIndex}");
                dgv.CurrentCell = dgv.Rows[currRowIndex].Cells[sliColIndex];
                dgv.Rows[currRowIndex].Cells[sliColIndex].Selected = true;
            }
            else if (radioButtonTypeLLI.Checked && !dgv.Columns[currColIndex].HeaderText.Contains("ДЖИ"))
            {
                Debug.WriteLine($"See that LLI types checked and non lli-weight cell chosen:");
                Debug.WriteLine($"Current position: row-{currRowIndex}, col-{lliColIndex}");
                dgv.CurrentCell = dgv.Rows[currRowIndex].Cells[lliColIndex];
                dgv.Rows[currRowIndex].Cells[lliColIndex].Selected = true;
            }

            DataGridViewCellStyle styleWhite = new DataGridViewCellStyle();
            styleWhite.BackColor = Color.White;
            DataGridViewCellStyle styleGray = new DataGridViewCellStyle();
            styleGray.BackColor = Color.LightGray;

            if (dgv.SelectedCells.Count != 1) return;

            for (int r = 0; r < dgv.RowCount; ++r)
            {
                if (r == dgv.SelectedCells[0].RowIndex)
                {
                    dgv.Rows[dgv.SelectedCells[0].RowIndex].Cells[0].Style = styleGray;
                    continue;
                }
                    dgv.Rows[r].Cells[0].Style = styleWhite;
            }
        }

        private void FaceForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space)
                buttonReadWeight.PerformClick();
        }

        private void ColorizeAndSelect(DataGridView dgv)
        {
            var isFirst = true;
           

            foreach (DataGridViewColumn col in dgv.Columns)
            {
                if (col.HeaderText.Contains("вес"))
                {
                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        dgv.Rows[row.Index].Cells[col.Index].Style.BackColor = Color.PaleTurquoise;
                        if ((string.IsNullOrEmpty(dgv.Rows[row.Index].Cells[col.Index].Value.ToString()) || dgv.Rows[row.Index].Cells[col.Index].Value.ToString() == "0") && isFirst)
                        {
                           
                            if (radioButtonTypeBoth.Checked) dgv.CurrentCell = dgv.Rows[row.Index].Cells[col.Index];
                            if (radioButtonTypeSLI.Checked) dgv.CurrentCell = dgv.Rows[row.Index].Cells[dgv.ColumnCount - 2];
                            if (radioButtonTypeLLI.Checked) dgv.CurrentCell = dgv.Rows[row.Index].Cells[dgv.ColumnCount - 1];
                            isFirst = false;
                        }
                    }
                }
                if (isFirst && !dgv.Name.Contains("Set") && dgv.RowCount != 0) dgv.ClearSelection();
            }
        }

        private double Weighting(int n = 0)
        {
            n++;
            double w = -1;
            using (var worker = new SerialPortsWorker())
            {
                w = worker.GetWeight();
            }
            if (w == 0 || w == -1)
            {
                System.Threading.Thread.Sleep(1000);
                if (n < 3) Weighting(n);
                else MessageBox.Show("Probably some problems in scales connection. Try to restart program if no result, restart computer.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return w;
        }
    }
       
}
